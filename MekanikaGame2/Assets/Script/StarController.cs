using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    PlayerMovement thePlayer;
    private int StarValue;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        if (this.tag == "PureStar")
        {
            StarValue = Random.Range(2, 3);
        }
        else
        {
            StarValue = 1;
        }
        //Debug.Log(this.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            thePlayer.addStarValue(StarValue);
            Destroy(gameObject);
        }
    }
}
