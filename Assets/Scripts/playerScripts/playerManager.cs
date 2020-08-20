using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    public bool isDead;

    public int startingHealth = 5;
    public float fullAir = 100.0f;
    public float currAir;
    public float drownSpeed = 1.0f;
    public float drownSpeedE = 0.05f;
    public int health;
    public float maxRec = 2.0f;
    public char pose;
    
    public bool invinsible;
    private float recCounter;
    [SerializeField] private GameObject nLight;
    [SerializeField] private GameObject redT;
    [SerializeField] private GameObject greenR;
    [SerializeField] private GameObject blueE;
    [SerializeField] private GameObject attCom;
    [SerializeField] private GameObject evCom;
    public commandManagement cm;

    public float evasionTimeMax = 10.0f;
    public bool evaded;
    private float evadeTimer;
    public float timerSpeed;

    void Start()
    {
        cm = GetComponent<commandManagement>();

        isDead = false;
        health = startingHealth;
        currAir = fullAir;
        invinsible = false;

        //starts at neutral pose
        pose = 'N';
        
        /*  poses will include
         *  neutral = 'N'
         *  triangle = 'T'
         *  rectangle = 'R'
         *  elipse = 'E'
         */

        nLight.SetActive(true);
        redT.SetActive(false);
        greenR.SetActive(false);
        blueE.SetActive(false);

        evadeTimer = 0.0f;
        evaded = true;

    }

    void Update()
    {
        //Constantly losing air
        if(currAir > 0.0f)
        {
            currAir -= Time.deltaTime * drownSpeed;

            //screen edges grow darker
            //droning sound intensifies
        }
        else
        //no air left
            TakeDamage();
        

        //make recovery period
        if(recCounter > 0.0f)
            recCounter -= Time.deltaTime;


        //GreenR light makes player invinsible
        if(pose == 'R')
            invinsible = true;
        else if(recCounter <= 0.0f)
            invinsible = false;


        //Lights
        if(pose == 'T')
        {
            nLight.SetActive(false);
            redT.SetActive(true);
            greenR.SetActive(false);
            blueE.SetActive(false);
        }
        else if(pose == 'R')
        {
            nLight.SetActive(false);
            redT.SetActive(false);
            greenR.SetActive(true);
            blueE.SetActive(false);
        }
        else if(pose == 'E')
        {
            nLight.SetActive(false);
            redT.SetActive(false);
            greenR.SetActive(false);
            blueE.SetActive(true);
        }
        else if(pose == 'N')
        {
            nLight.SetActive(true);
            redT.SetActive(false);
            greenR.SetActive(false);
            blueE.SetActive(false);
        }

        //Commands
        if(cm.command == "attack")
        {
            attCom.SetActive(true);
            evCom.SetActive(false);
        } else if(cm.command == "evade")
        {
            attCom.SetActive(false);
            evCom.SetActive(true);
        } else
        {
            attCom.SetActive(false);
            evCom.SetActive(false);
        }


        //evasion timer
        if(evadeTimer > 0.0f)
        {
            evadeTimer -= Time.deltaTime * timerSpeed;
        }
        else
        {
            if(evaded && cm.command == "evade")
                RefillAir();
            else
                evaded = true;

            evadeTimer = evasionTimeMax;
        }

        /////////////////DEBUGGING//////////////
        /* if(Input.GetKeyDown(KeyCode.Z))
        {
            //Debug.Log("Current air: " + currAir);
            RefillAir();
        }  */
    }

    public void SetLight(char lCol)
    {
        pose = lCol;
    }

    public void TakeDamage()
    {
        if(!invinsible)
        {
            if(health > 1)
            {
                Debug.Log("Ouch");
                //reduce health
                health -= 1;
            }

            //Queue damage sound
            //Damage animation

            //die if no health left
            else
                Death();

            evaded = false;

            //give recovery period
            recCounter = maxRec;
            invinsible = true;
        }
    }

    public void RefillAir()
    {
        Debug.Log("Air Refilled");

        currAir = fullAir;

        //reset state of fullAir
    }

    public void Death()
    {
        Debug.Log("Dead");
        //Death Screen
        isDead = true;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "enemProjectile")
        {
            TakeDamage();
        }
    }
}
