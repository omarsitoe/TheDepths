using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleManager : MonoBehaviour
{
    public float bubbleFric = -0.75f;
    public float bubbleLifespan = 1.50f;
    private float desTimer;

    Rigidbody2D rbB;
    Vector2 currPos;
    Vector2 currVel;
    Vector3 bubOffset;
    
    void Start()
    {
        //offset spawn position randomly
        bubOffset = new Vector3(Random.Range(-0.35f, 0.35f), Random.Range(-0.35f, 0.35f), 0.0f);
        transform.position += bubOffset;
        transform.parent = null;


        //shoot bubbles out
        rbB = gameObject.GetComponent<Rigidbody2D>();
        float horVec = Input.GetAxis("Horizontal");
        float verVec = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(horVec, verVec);

        rbB.AddForce(dir * 200.0f);

        //Start lifespan timer
        desTimer = bubbleLifespan;
        
    }

    void FixedUpdate()
    {
        //Destroy bubble after some time
        desTimer -= Time.deltaTime;
        if(desTimer <= 0.0f)
            Destroy(gameObject);
    }
}
