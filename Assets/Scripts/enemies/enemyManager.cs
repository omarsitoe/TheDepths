using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyManager : MonoBehaviour
{
    public float enHealth;
    public float maxEnHealth;
    public float launchTimer;

    [SerializeField] public GameObject projectile;
    [SerializeField] public GameObject vicScreen;
    [SerializeField] public GameObject player;
    private bool dead;
    private playerManager pm;
    private commandManagement cm;


    void Start()
    {
        pm = player.GetComponent<playerManager>();
        cm = player.GetComponent<commandManagement>();

        vicScreen.SetActive(false);
        enHealth = maxEnHealth;
    }

    void Update()
    {

        if(launchTimer <= 0.0f && !dead)
        {
            Instantiate(projectile, gameObject.transform);

            //reset timer to random duration
            launchTimer = Random.Range(2.0f, 7.0f);
        }


        launchTimer -= Time.deltaTime;

        //victory
        if(enHealth <= 0.0f)
            Victory();
    }

    public void Victory()
    {
        //call victory state
        vicScreen.SetActive(true);
        
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Menu");
        }

        dead = true;
    }

    public void LowerHealth(float value)
    {
        if(enHealth > 0.0f)
            enHealth -= value;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "bubbleAttack")
        {
            if(pm.pose == 'T')
                LowerHealth(2.0f);
            else
                LowerHealth(0.5f);

            if(cm.command == "attack")
            {
                pm.RefillAir();
            }
        }
        
    }
}
