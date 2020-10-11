using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlapformGenerator : MonoBehaviour
{
    public GameObject MainPlatform;
    public GameObject MudPlatform;
    public Transform generatePoint;
    private float MainPlatformLength, MudPlatformLength;
    public int randomPlatform;
    public float randomRavine;
    public float randomHigh;
    public StarGenerator theStar;
    private int randomPlatformChange;

    // Start is called before the first frame update
    void Start()
    {
        MainPlatformLength = MainPlatform.GetComponent<BoxCollider2D>().size.x;
        MudPlatformLength = MudPlatform.GetComponent<BoxCollider2D>().size.x;
        randomPlatformChange = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(generatePoint.position.x > transform.position.x)
        {
            randomPlatform = Random.Range(1, 10);
            randomRavine = Random.Range(1, 7);
            randomHigh = Random.Range(-2.5f, 1);
            if(randomPlatform <= randomPlatformChange)
            {
                Instantiate(MainPlatform, new Vector3(transform.position.x + randomRavine, transform.position.y + randomHigh, transform.position.z), transform.rotation);
                transform.position = new Vector3(transform.position.x + MainPlatformLength + randomRavine, transform.position.y, transform.position.z);
            }
            else if(randomPlatform > randomPlatformChange)
            {
                Instantiate(MudPlatform, new Vector3(transform.position.x + randomRavine, transform.position.y + randomHigh, transform.position.z), transform.rotation);
                transform.position = new Vector3(transform.position.x + MudPlatformLength + randomRavine, transform.position.y, transform.position.z);
            }
            theStar.generateStar(transform.position.x);
            //Debug.Log("New Platform Created");
        }
    }

    public void changeRandomPlatform(int a)
    {
        randomPlatformChange = a;
    }
}
