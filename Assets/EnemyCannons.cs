using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannons : MonoBehaviour
{

    public float cannonForce = 6;

    public float shotFrequency = 3;

    public GameObject cannonBall;

    public LayerMask mask;

    public GameObject[] cannonPositions;

    public ParticleSystem[] particlePoints;

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

        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, -spriteStack.sprites[0].transform.right, Mathf.Infinity);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, spriteStack.sprites[0].transform.right, Mathf.Infinity);

        if (leftHit.collider != null)
        {
            if (leftHit.collider.gameObject.layer == 8)
            {
                if (shotTimer >= shotFrequency)
                {
                    Shoot();
                }
            }
        }

        if (rightHit.collider != null)
        {
            if (rightHit.collider.gameObject.layer == 8)
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
        Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position, spriteStack.sprites[0].transform.right);
        Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position, -spriteStack.sprites[0].transform.right);
    }

}
