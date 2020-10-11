using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSkillUi : MonoBehaviour
{
    public Image trapImage;
    private float cooldownTrapTime = 5f;
    private float zeroTime = 1f;
    public bool trapIsCooldown = false;
    public PlayerMovement thePlayer;
    private int starCount;

    // Start is called before the first frame update
    void Start()
    {
        trapImage.fillAmount = 0;
        starCount = thePlayer.GetStar();
    }

    // Update is called once per frame
    void Update()
    {
        starCount = thePlayer.GetStar(); 
        if (starCount <= 0 && trapIsCooldown == false)
        {
            trapImage.fillAmount = 1; //Debug.Log(starCount);
        }
        else
        {
            trapImage.fillAmount = 0;
            trapAbility();
        }
    }

    void trapAbility()
    {
        if (trapIsCooldown == true)
        {
            trapImage.fillAmount = zeroTime;
            if(trapImage.fillAmount <= 0)
            {
                trapImage.fillAmount = 0;
            }
            zeroTime -= 1 / cooldownTrapTime * Time.deltaTime;
            if (zeroTime <= 0)
            {
                trapIsCooldown = false;
                zeroTime = 1f;
            }
        }
    }
}
