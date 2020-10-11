using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject theStar, thePureStar;
    private float starSize, pureStarSize;
    private float xRandom, yRandom, starRandom;
    private int starCount;
    
    
    void Start()
    {
        starSize = theStar.GetComponent<CircleCollider2D>().radius;
        pureStarSize = thePureStar.GetComponent<CircleCollider2D>().radius;
    }

    
    void Update()
    {
        
    }

    public void generateStar(float x)
    {
        int tempRand = Random.Range(1, 100);
        if (tempRand <= 70)
        {
            starCount = 1;
        }
        else if (tempRand <= 85)
        {
            starCount = 2;
        }
        else if (tempRand <= 95)
        {
            starCount = 3;
        }
        else if (tempRand <= 100)
        {
            starCount = 4;
        }

        for(int i = 0; i < starCount; i++)
        {
            xRandom = Random.Range(-5, 5);
            yRandom = Random.Range(-2, 2);
            starRandom = Random.Range(1, 100);
            if (starRandom <= 90)
            {
                Instantiate(theStar, new Vector3(transform.position.x + xRandom, transform.position.y + yRandom, transform.position.z), transform.rotation);
            }
            else if (starRandom > 90)
            {
                Instantiate(thePureStar, new Vector3(transform.position.x + xRandom, transform.position.y + yRandom, transform.position.z), transform.rotation);
            }
        }

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
