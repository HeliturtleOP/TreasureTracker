using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCannons : MonoBehaviour
{
    public float cannonForce = 1;
    public float fireRate = 3;

    public Image reloadIndicator;

    public GameObject cannonBall;

    public GameObject[] cannonPositions;

    private float timer = 0;

    public ParticleSystem[] particlePoints;
    public AudioSource cannonAudio;
    private ScreenShake screenShake;

    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }
    
    void Update()
    {
        timer += Time.deltaTime / fireRate;

        reloadIndicator.fillAmount = timer;

        if (Input.GetButtonDown("Jump")) 
        {
            if (timer >= 1)
            {

                cannonAudio.Play();
                screenShake.ShakeScreen(0.03f, 0.05f, 3);
                for (int i = 0; i < cannonPositions.Length; i++)
                {
                    var main = particlePoints[i].main;
                    float rot = (Random.Range(0, 4) * 90 * Mathf.Deg2Rad) + cannonPositions[i].transform.parent.rotation.z;
                    main.startRotation = rot;

                    particlePoints[i].Play();
                    GameObject fresh = Instantiate(cannonBall, cannonPositions[i].transform.position, cannonPositions[i].transform.rotation);
                    fresh.GetComponent<Rigidbody2D>().AddForce(cannonPositions[i].transform.up * cannonForce, ForceMode2D.Impulse);
                }

                timer = 0;

            }



        
        }
    }
}
