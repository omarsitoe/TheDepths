using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    [SerializeField] private GameObject player, enemy;
    [SerializeField] private GameObject heart1, heart2, heart3, heart4, heart5;
    //[SerializeField] private Image scrnDark;
    private playerManager pm;
    private enemyManager em;


    public Image scrnDark;
    public Color scrnColor;
    public Slider airSlide;
    public Slider enemyHealth;

    void Start()
    {
        //make health show at start
        pm = player.GetComponent<playerManager>();
        em = enemy.GetComponent<enemyManager>();

        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        heart4.SetActive(true);
        heart5.SetActive(true);

        //
        scrnColor = scrnDark.color;
    }

    void LateUpdate()
    {
        //update visual representation of health
        if(pm.health == 5);
        else if(pm.health == 4)
            heart5.SetActive(false);
        else if(pm.health == 3)
            heart4.SetActive(false);
        else if(pm.health == 2)
            heart3.SetActive(false);
        else if(pm.health == 1)
            heart2.SetActive(false);
        else if(pm.health == 0)
            heart1.SetActive(false);

        //darken screen based on air-levels
        scrnColor.a = 1.0f - (pm.currAir * 0.01f);
        scrnDark.color = scrnColor;


        airSlide.value = pm.currAir * 0.01f;
        enemyHealth.value = em.enHealth * 0.01f;

    }
}
