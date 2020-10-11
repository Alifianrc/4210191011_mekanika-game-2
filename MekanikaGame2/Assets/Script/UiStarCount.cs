using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStarCount : MonoBehaviour
{
    // Start is called before the first frame update
    public Text theStarCountText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeStarText(int a)
    {
        theStarCountText.text = "" + a;
    }
}
