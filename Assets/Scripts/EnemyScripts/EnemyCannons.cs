using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCannons : MonoBehaviour
{

    public float cannonForce = 6;

    public float shotFrequency = 3;

    public GameObject cannonBall;

    public GameObject[] cannonPositions;

    public ParticleSystem[] particlePoints;

    public LayerMask mask;

    private AudioSource sound;

    

    private ScreenShake screenShake;
    private SpriteStack spriteStack;
    private float shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponentInChildren<AudioSource>();
        spriteStack = GetComponentInChildren<SpriteStack>();
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {

        for (int i = 0; i < cannonPositions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(cannonPositions[i].transform.position, cannonPositions[i].transform.up, Mathf.Infinity, mask);

            if (hit && hit.collider.gameObject.layer == 8)
            {
                if (shotTimer >= shotFrequency)
                {
                    Shoot();
                }
            }

        }

    }

    void Shoot()
    {

        screenShake.ShakeScreen(0.03f, 0.05f, 3);
        sound.Play();

        for (int i = 0; i < cannonPositions.Length; i++)
        {
            var main = particlePoints[i].main;
            float rot = (Random.Range(0, 4) * 90 * Mathf.Deg2Rad) + cannonPositions[i].transform.parent.rotation.z;
            main.startRotation = rot;

            particlePoints[i].Play();
            GameObject fresh = Instantiate(cannonBall, cannonPositions[i].transform.position, cannonPositions[i].transform.rotation);
            fresh.GetComponent<Rigidbody2D>().AddForce(cannonPositions[i].transform.up * cannonForce, ForceMode2D.Impulse);
        }

        shotTimer = 0;

    }

    private void OnDrawGizmosSelected()
    {

        for (int i = 0; i < cannonPositions.Length; i++)
        {
            Gizmos.DrawRay(cannonPositions[i].transform.position, cannonPositions[i].transform.up * 10);
        }
    }

}
