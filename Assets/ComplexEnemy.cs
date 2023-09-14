using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
    public float speed;

    public float targetingRange = 5f;
    public float retreatRange = 2;

    public float rotSpeed = 0.1f;

    private float rotation;

    private float desiredRotation;
    private Vector2 movementDir;

    private GameObject player;
    private SpriteStack spriteStack;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
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

    private float x;
    void TargetPlayer()
    {

        movementDir = player.transform.position;
        movementDir = new Vector2(movementDir.x - transform.position.x, movementDir.y - transform.position.y);
        movementDir = movementDir.normalized;

        //desiredRotation = Vector2.SignedAngle(spriteStack.sprites[0].transform.right, movementDir);

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
        }
        else
        {

            desiredRotation = Vector2.SignedAngle(spriteStack.sprites[0].transform.up, movementDir);
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
