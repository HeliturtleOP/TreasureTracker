using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseCollider : MonoBehaviour
{
    public TMP_Text text;

    private Camera cam;
    private Rigidbody2D rb;

    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        int activeDisplay = -1;

        for (int i = 0; i < Display.displays.Length; i++)
        {
            if (Display.displays[i].active)
            {
                activeDisplay = i;
            }
        }

        float screenWidth;
        float screenHeight;

        if (activeDisplay == -1)
        {
            screenWidth = Display.main.systemWidth;
            screenHeight = Display.main.systemHeight;
        }
        else
        {
            screenWidth = Display.displays[activeDisplay].systemWidth;
            screenHeight = Display.displays[activeDisplay].systemHeight;
        }



        float aspect = screenWidth / screenHeight;
        //text.text = text.text + aspect + "\n";
        //Debug.Log(aspect);

        if (aspect >= 1.76f)
        {
            //Debug.Log(screenWidth);
            //Debug.Log(screenHeight);
            //Debug.Log((16f / 9f * screenHeight) / (screenHeight));
            //text.text = text.text + (16f / 9f * screenHeight) / (screenHeight) + "\n";
            //text.text = text.text + Screen.currentResolution.ToString() + "\n";
            Screen.SetResolution(Mathf.RoundToInt(16f/9f * screenHeight), (int)screenHeight, true);
            //text.text = text.text + Screen.width.ToString() + "\n";
            //text.text = text.text + Screen.height.ToString() + "\n";
            //text.text = text.text + screenWidth + "\n";
            //text.text = text.text + screenHeight + "\n";
        }
        else
        {
            //aspect = screenWidth / (float)Screen.currentResolution.width;
            Screen.SetResolution((int)screenWidth, Mathf.RoundToInt(9f/16f * screenWidth), true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
       Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 dif = mousePos - rb.position;

        //Debug.DrawRay(transform.position, dif);
        rb.position = mousePos;
        //rb.velocity = dif * 5;



    }
}
