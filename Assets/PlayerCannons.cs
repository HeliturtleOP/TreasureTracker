using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannons : MonoBehaviour
{
    public float cannonForce = 1;

    public GameObject cannonBall;

    public GameObject[] cannonPositions;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        { 
        
            foreach (GameObject obj in cannonPositions)
            {
                GameObject fresh = Instantiate(cannonBall, obj.transform.position, obj.transform.rotation);
                fresh.GetComponent<Rigidbody2D>().AddForce(obj.transform.up * cannonForce, ForceMode2D.Impulse);
            }
        
        }
    }
}
