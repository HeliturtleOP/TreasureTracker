using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannons : MonoBehaviour
{
    public float cannonForce = 1;

    public GameObject cannonBall;

    public GameObject[] cannonPositions;

    public ParticleSystem[] particlePoints;

    public AudioSource cannonAudio;

    private ScreenShake screenShake;

    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            cannonAudio.Play();
            screenShake.ShakeScreen(0.03f, 0.05f, 3);
            for (int i = 0; i < cannonPositions.Length; i++)
            {
                particlePoints[i].Play();
                GameObject fresh = Instantiate(cannonBall, cannonPositions[i].transform.position, cannonPositions[i].transform.rotation);
                fresh.GetComponent<Rigidbody2D>().AddForce(cannonPositions[i].transform.up * cannonForce, ForceMode2D.Impulse);
            }

        
        }
    }
}
