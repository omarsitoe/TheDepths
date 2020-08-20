using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRandomizer : MonoBehaviour
{
    [SerializeField] private float maxTime;

    private float timer;
    private float timerC;
    private float dice;
    private playerManager pm;
    private commandManagement cm;

    void Start()
    {
        pm = gameObject.GetComponent<playerManager>();
        cm = GetComponent<commandManagement>();
        timer = maxTime;
    }

    void FixedUpdate()
    {
        //choose random light
        if(timer <= 0.0f)
        {
            dice = Random.Range(0.0f, 4.0f);

            if(dice <= 1.0f)
                pm.SetLight('N');
            else if(dice <= 2.0f)
                pm.SetLight('T');
            else if(dice <= 3.0f)
                pm.SetLight('R');
            else
                pm.SetLight('E');

            //restart timer
            timer = maxTime;
        }

        //choose random command
        if(timerC <= 0.0f)
        {
            dice = Random.Range(0.0f, 2.0f);

            if(dice <= 1.0f)
                cm.SetCommand("attack");
            else if(dice <= 2.0f)
                cm.SetCommand("evade");

            //restart timer
            timerC = maxTime;
        }

        //count down
        timer -= Time.deltaTime;
        timerC -= Time.deltaTime;
    }
}
