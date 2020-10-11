using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Transform finishPoint;
    public Transform thePlayer, theEnemy;
    public PlapformGenerator thePlatform;
    private int ifPlayerFrontPlatform = 4;
    private int ifPlayerBehindPlatform = 6;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        finishCheck();
    }

    private void FixedUpdate()
    {
        fairPlatform();
    }

    void finishCheck()
    {
        if (thePlayer.position.x > finishPoint.position.x)
        {
            Debug.Log("Player Wins");
        }
        else if (thePlayer.position.x > finishPoint.position.x)
        {
            Debug.Log("Enemy Wins!");
        }
    }

    void fairPlatform()
    {
        if(thePlayer.position.x > theEnemy.position.x)
        {
            thePlatform.changeRandomPlatform(ifPlayerFrontPlatform);
        }
        else
        {
            thePlatform.changeRandomPlatform(ifPlayerBehindPlatform);
        }
    }
}
