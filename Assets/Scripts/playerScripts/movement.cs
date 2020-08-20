using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float swimSpeed = 8.0f;
    public float airSpeed = 20.0f;
    public float friction = -0.75f;
    public GameObject bubbls;

    float bTim;

    Rigidbody2D rb;
    Rigidbody2D rbB;
    Vector2 currPos;
    Vector2 currVel;
    private float horVec;
    private float verVec;
    private playerManager pm;
    private Animator anim;
    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pm = gameObject.GetComponent<playerManager>();
        anim = gameObject.GetComponent<Animator>();

        rb.gravityScale = 0.0f;
    }

    void FixedUpdate()
    {
        //
        currPos = transform.position; 
        currVel = rb.GetPointVelocity(currPos);
        horVec = Input.GetAxis("Horizontal");
        verVec = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(horVec, verVec);

        //Pushes object in desired direction if not holding shift
        if(!Input.GetKey(KeyCode.Space))
            rb.AddForce(dir * swimSpeed);
        else
        {
            rb.AddForce(dir * -1 * airSpeed);

            //use more air
            if(pm.pose != 'E')
                pm.currAir -= Time.deltaTime * pm.drownSpeed;
            else
                pm.currAir -= Time.deltaTime * pm.drownSpeedE;

            //
            if(dir != Vector2.zero && bTim <= 0.0f)
            {   
                Instantiate(bubbls, gameObject.transform);
                bTim = Random.Range(0.05f, 0.15f);
            }
        }

        bTim -= Time.deltaTime;

        /*if(dir != Vector2.zero)
        { 
            //movement anim trigger
        }
        else
            //idle animation trigger
        */

        //stops object if moving -- adds drag
        if(currVel != Vector2.zero)
            rb.AddForce(currVel * friction);

        //invinsibility
        if(pm.invinsible)
            anim.SetBool("isInvinsible", true);
        else
            anim.SetBool("isInvinsible", false);

        //death
        if(pm.isDead)
            DeathState();
    }

    public void DeathState()
    {
        rb.gravityScale = -0.75f;
        Destroy(this);
    }
}
