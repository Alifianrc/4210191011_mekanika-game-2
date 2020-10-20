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
    private bool isDead;
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
    private bool isEnoughStarTrap;
    private bool isEnoughStarFlash;
    private bool isEnoghStarSuperJump;
    private float stunTime;
    public UiStarCount theUiStarCount;
    private int starUsed;
    public Text trapLvlText;
    public Text flashLvlText;
    public Text superJumpLvlText;
    public static int flashValue = 10;//star cost
    private int flashSpeed = 10;
    private int flashLevel = 0;
    private int superJumpValue = 10;
    private Rigidbody2D theRigid;
    

    void Start()
    {
        isDead = false;
        speed = runSpeed;
        starValue = 0;
        theUiStarCount.changeStarText(starValue);
        isEnoughStarTrap = false;
        isEnoughStarFlash = false;
        isEnoghStarSuperJump = false;
        trapLvlText.text = "";
        flashLvlText.text = "Level " + flashLevel;
        superJumpLvlText.text = "MAX";
        theRigid = GetComponent<Rigidbody2D>();
    }
        
    void Update()
    {
        if (thecooldown.SuperJumpIsChannelling == false && isDead == false)
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

            if (Input.GetButtonDown("Fire1") && thecooldown.trapIsCooldown == false && isEnoughStarTrap == true)
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

            if(Input.GetButtonDown("Fire2") && thecooldown.flashIsChannelling == false && isEnoughStarFlash == true)
            {
                thecooldown.flashIsChannelling = true;
                starValue -= flashValue;
                theUiStarCount.changeStarText(starValue);
            }

            if(Input.GetButtonDown("Fire3") && isEnoghStarSuperJump == true)
            {
                //stop the character
                horizontalMove = 0; 
                animator.SetFloat("Speed", 0);
                thecooldown.SuperJumpIsChannelling = true; Debug.Log("Fire 3");
                starValue -= superJumpValue;
                theUiStarCount.changeStarText(starValue);
            }
        }
                
    }

    public void OnLanding()
    {       
        animator.SetBool("IsJump", false);
        controller.changeAirControl(true);
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
        if (collision.gameObject.tag == "Main_Ground" || collision.gameObject.tag == "Mud") 
        {
            //controller.changeAirControl(true); //Debug.Log("Air Active");
        }
        if (collision.gameObject.tag == "DeadGround")
        {
            isDead = true; //Debug.Log("Player Dead");
            animator.SetBool("IsDead", true);
            animator.SetFloat("Speed", 0);
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
        if (starValue >= 10)
        {
            isEnoughStarFlash = true;
            isEnoghStarSuperJump = true;
        }
        else if (starValue >= 1)
        {
            isEnoughStarTrap = true;
            setTrapLvl(starValue);
        }
        else
        {
            isEnoughStarTrap = false;
            isEnoughStarFlash = false;
            isEnoghStarSuperJump = false;
            trapLvlText.text = "";
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

    public void addFlashSpeed()
    {
        runSpeed += flashSpeed; Debug.Log("Flash");
        flashLevel ++;
        flashLvlText.text = "Level " + flashLevel;
    }

    public void theSuperJump()
    {
        controller.changeAirControl(false);
        theRigid.AddForce(new Vector2(2000, 2000));
        animator.SetBool("IsJump", true);
    }
}
