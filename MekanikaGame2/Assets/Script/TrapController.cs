using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private EnemyController theEnemy;
    private float TrapValue;
    public Animator boom;
    private float zeroTime = 1;
    private float animationLong = 0.8f;
    private bool isBoom;
    private Vector3 scaleChange;
    private bool scaleChanged;
   
    // Start is called before the first frame update
    void Start()
    {
        isBoom = false;
        theEnemy = FindObjectOfType<EnemyController>();
        scaleChange = new Vector3(1.3f, 1.3f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBoom == true)
        {
            zeroTime -= 1 / animationLong * Time.deltaTime;
            if (zeroTime <= 0)
            {
                Destroy(gameObject);
            }
        }          
    }

    public void setTrapValue(float a)
    {
        //how long trap will stun enemy(in second)
        TrapValue = a;
        if (TrapValue <= 0.3f)
        {
            animationLong = 0.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isBoom = true;
            boom.SetBool("IsBoom", true);
            changeScale();
            theEnemy.trapped(TrapValue);
        }
    }

    private void changeScale()
    {
        transform.localScale += scaleChange;
    }
}
