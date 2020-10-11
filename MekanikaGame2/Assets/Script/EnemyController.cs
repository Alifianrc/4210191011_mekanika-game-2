using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    Rigidbody2D myRigidbody;
    public float changeSpeedTime = 5f;
    public float trappedTime;
    private float zeroTime = 1f;
    private bool isTrapped = false;
    public Animator myAnimator;
    

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        speed = Random.Range(6, 7.5f);
        myAnimator.SetFloat("Speed", Mathf.Abs(speed));
        myRigidbody.velocity = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTrapped == true)
        {
            speed = 0;
            myRigidbody.velocity = new Vector3(speed, 0, 0);
            myAnimator.SetFloat("Speed", Mathf.Abs(speed));
            myAnimator.SetBool("IsKnock", isTrapped);
            zeroTime -= 1 / trappedTime * Time.deltaTime; //Debug.Log(zeroTime);
            if (zeroTime <= 0)
            {
                isTrapped = false;
                myAnimator.SetBool("IsKnock", isTrapped);
                //Debug.Log("Count Down Trap Working");
            }
        }
        else if (isTrapped == false)
        {
            zeroTime -= 1 / changeSpeedTime * Time.deltaTime;
            if (zeroTime <= 0)
            {
                speed = Random.Range(6, 7.5f); //Debug.Log(speed);
                myRigidbody.velocity = new Vector3(speed, 0, 0);
                myAnimator.SetFloat("Speed", Mathf.Abs(speed));
                zeroTime = 1;
            }
        }
        //Debug.Log(speed);
    }

    public void trapped(float a)
    {
        isTrapped = true;
        zeroTime = 1;
        trappedTime = a;
    }

}
