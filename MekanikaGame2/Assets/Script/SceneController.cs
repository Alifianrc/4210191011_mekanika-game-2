using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Transform finishPoint;
    public Transform thePlayer, theEnemy;
    public PlapformGenerator thePlatform;
    private int ifPlayerFrontPlatform = 4;
    private int ifPlayerBehindPlatform = 6;
    public GameObject GameOverPanel;
    private bool raceFinish = false;
    public static bool gameIsStarted = false;
    public GameObject StartPanel;
    private float coolDownStartTime = 3f;
    private float zeroTimeStart = 3;
    private bool startingGame = false;
    public Text theCountDownText;

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false);
        StartPanel.SetActive(true);
        theCountDownText.text = ("");
    }

    // Update is called once per frame
    void Update()
    {
        finishCheck();

        if (startingGame)
        {
            zeroTimeStart -= 3 / coolDownStartTime * Time.deltaTime;
            
            if(zeroTimeStart <= 0.5)
            {
                theCountDownText.text = ("RACE");
            }
            else
            {
                theCountDownText.text = zeroTimeStart.ToString("0");
            }
            if (zeroTimeStart <= 0)
            {
                gameIsStarted = true;
                startingGame = false;
                theCountDownText.text = ("");
            }
        }
    }

    private void FixedUpdate()
    {
        fairPlatform();
    }

    void finishCheck()
    {
        if (!raceFinish)
        {
            if (thePlayer.position.x > finishPoint.position.x)
            {
                //Debug.Log("Player Wins");
            }
            else if (thePlayer.position.x > finishPoint.position.x)
            {
                //Debug.Log("Enemy Wins!");
            }
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

    public void playerDead()
    {
        GameOverPanel.SetActive(true);
    }

    public void GameStarted()
    {
        StartPanel.SetActive(false);
        startingGame = true;
    }
}
