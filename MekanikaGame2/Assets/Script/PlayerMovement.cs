using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float speed;
    public float runSpeed = 40f;
    public float runSpeedMud = 20f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;
    public GameObject trap;
    private GameObject trapTemp;
    private TrapController theTrap;
    public CooldownSkillUi thecooldown;
    private int starValue;
    public bool isEnoughStar;
    private float stunTime;
    public UiStarCount theUiStarCount;
    private int starUsed;
    public Text trapLvlText;

    void Start()
    {
        speed = runSpeed;
        starValue = 0;
        theUiStarCount.changeStarText(starValue);
        isEnoughStar = false;
        trapLvlText.text = "";
    }
        
    void Update()
    {
        //Get user input
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed; //Debug.Log(speed);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJump", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("IsCrouch", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("IsCrouch", false);
        }

        if (Input.GetButtonDown("Fire1") && thecooldown.trapIsCooldown == false && isEnoughStar == true)
        {
            trapTemp = Instantiate(trap, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), transform.rotation);
            theTrap = trapTemp.GetComponent<TrapController>();
            theTrap.setTrapValue(stunTime);
            starUsed = starUsedSet(stunTime);
            thecooldown.trapIsCooldown = true;
            starValue -= starUsed;
            ChekStarValue();
            theUiStarCount.changeStarText(starValue);
        }
    }

    public void OnLanding()
    {       
        animator.SetBool("IsJump", false);
        //Debug.Log("Un Jump");
    }

    //Function that called each second not frame
    private void FixedUpdate()
    {
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        if (jump == true)
        {
            getBackSpeed();
        }
        jump = false;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Mud")
        {
            speed = runSpeedMud; //Debug.Log("Mud")
        }
        else
        {
            speed = runSpeed;
        }
    }

    void getBackSpeed()
    {
        speed = runSpeed;
    }

    public void addStarValue(int a)
    {
        starValue += a;
        ChekStarValue();
        theUiStarCount.changeStarText(starValue);
        //Debug.Log(a); Debug.Log(starValue);
    }
    
    public void ChekStarValue()
    {
        if (starValue >= 1)
        {
            isEnoughStar = true;
            setTrapLvl(starValue);
        }
        else
        {
            isEnoughStar = false;
        }
    }

    private void setTrapLvl(int StarPoint)
    {
        //level 4
        if(StarPoint >= 8)
        {
            stunTime = 9f;
            trapLvlText.text = "Level 4"; 
        }
        //level 3
        else if(StarPoint >= 5)
        {
            stunTime = 5f;
            trapLvlText.text = "Level 3";
        }
        //level 2
        else if (StarPoint >= 3)
        {
            stunTime = 2f;
            trapLvlText.text = "Level 2";
        }
        //level 1
        else if (StarPoint >= 1)
        {
            stunTime = 0.2f;
            trapLvlText.text = "Level 1";
        }
    }

    public int GetStar()
    {
        return starValue;
    }

    private int starUsedSet(float stun)
    {
        int used = 0;
        if (stun == 0.2f)
        {
            used = 1;
        }
        else if (stun == 2)
        {
            used = 3;
        }
        else if (stun == 5)
        {
            used = 5;
        }
        else if (stun == 9)
        {
            used = 8;
        }

        return used;
    }
}
