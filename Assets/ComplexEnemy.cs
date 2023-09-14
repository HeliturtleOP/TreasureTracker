using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
    public float speed;

    public float targetingRange = 5f;
    public float retreatRange = 2;

    public float rotSpeed = 0.1f;

    public float attackFrequency = 3f;
    public float cannonForce = 6;

    public GameObject cannonBall;

    public GameObject[] cannonPositions;

    public ParticleSystem[] particlePoints;

    private float rotation;

    private float desiredRotation;
    private Vector2 movementDir;

    private float attackTimer;

    private GameObject player;
    private SpriteStack spriteStack;
    private Rigidbody2D rb;
    private ScreenShake screenShake;

    // Start is called before the first frame update
    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteStack = GetComponentInChildren<SpriteStack>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetPlayer();
        rotateAroundPlayer();
    }

    private void FixedUpdate()
    {
        rb.velocity = spriteStack.getRotation() * speed;
    }

    void TargetPlayer()
    {

        movementDir = player.transform.position;
        movementDir = new Vector2(movementDir.x - transform.position.x, movementDir.y - transform.position.y);
        movementDir = movementDir.normalized;

        if (desiredRotation < 0) 
        {

            rotation -= rotSpeed * Time.deltaTime;
        
        }else if (desiredRotation > 0)
        {
            rotation += rotSpeed * Time.deltaTime;
        }

        spriteStack.rotation = rotation;
    }

    void rotateAroundPlayer() {

        if (Vector2.Distance(transform.position, player.transform.position) < retreatRange)
        {
            desiredRotation = Vector2.SignedAngle(-spriteStack.sprites[0].transform.up, movementDir);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < targetingRange)
        {
             desiredRotation = Vector2.SignedAngle(-spriteStack.sprites[0].transform.right, movementDir);

            if (desiredRotation >= -0.5f && desiredRotation <= 0.5f) {

                attackTimer += Time.deltaTime;
                if (attackTimer >= attackFrequency) {
                    Shoot();
                    attackTimer = 0;
                }

            
            }
        }
        else
        {
            desiredRotation = Vector2.SignedAngle(spriteStack.sprites[0].transform.up, movementDir);
        }


    }

    void Shoot() {

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

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, retreatRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }

}
