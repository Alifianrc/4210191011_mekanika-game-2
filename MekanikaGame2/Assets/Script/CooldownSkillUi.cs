using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownSkillUi : MonoBehaviour
{
    public Image trapImage;
    public Image flashImage;
    public Image SuperJumpImage;
    private float cooldownTrapTime = 5f;
    private float channellingFlashTime = 2f;
    private float channellingSuperJump = 7f;
    private float zeroTime = 1f;
    public bool trapIsCooldown = false;
    public bool flashIsChannelling = false;
    public bool SuperJumpIsChannelling = false;
    public PlayerMovement thePlayer;
    private int starCount;
    private float theZero = 1f;
    private float superZero = 1f;

    // Start is called before the first frame update
    void Start()
    {
        trapImage.fillAmount = 0;
        flashImage.fillAmount = 0;
        SuperJumpImage.fillAmount = 0;
        starCount = thePlayer.GetStar();
    }

    // Update is called once per frame
    void Update()
    {
        starCount = thePlayer.GetStar(); 
        if (trapIsCooldown == true)
        {
            trapImage.fillAmount = 0;
            trapAbility();
        }
        if (flashIsChannelling == true)
        {
            flashAbility();
        }
        if (SuperJumpIsChannelling == true)
        {
            superJumpAbility();
        }
        if (starCount < 10 && flashIsChannelling == false)
        {
            flashImage.fillAmount = 1; 
        }
        else if (starCount >= 10 && flashIsChannelling == false)
        {
            flashImage.fillAmount = 0;
        }
        if (starCount < 10 && SuperJumpIsChannelling == false)
        {
            SuperJumpImage.fillAmount = 1; 
        }
        else if (starCount >= 10 && SuperJumpIsChannelling == false)
        {
            SuperJumpImage.fillAmount = 0;
        }
        if (starCount <= 0 && trapIsCooldown == false)
        {
            trapImage.fillAmount = 1; 
        }
        else if (starCount > 0 && trapIsCooldown == false)
        {
            trapImage.fillAmount = 0;
        }
    }

    void trapAbility()
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
    void flashAbility()
    {
        flashImage.fillAmount = theZero;
        if (flashImage.fillAmount <= 0)
        {
            flashImage.fillAmount = 0;
        }
        theZero -= 1 / channellingFlashTime * Time.deltaTime;
        if (theZero <= 0)
        {
            flashIsChannelling = false;
            theZero = 1f;
            thePlayer.addFlashSpeed();
        }
    }

    void superJumpAbility()
    {
        SuperJumpImage.fillAmount = superZero;
        if (SuperJumpImage.fillAmount < 0)
        {
            SuperJumpImage.fillAmount = 0;
        }
        superZero -= 1 / channellingSuperJump * Time.deltaTime;
        if (superZero <= 0)
        {
            SuperJumpIsChannelling = false;
            superZero = 1f; Debug.Log("Super Jump!!!");
            thePlayer.theSuperJump();
        }
    }
}
