using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTargeting))]
public class ComplexEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;
    public float rotSpeed = 50f;
     
    [Header("Behaviour Settings")]
    public float targetingRange = 5f;
    public float retreatRange = 2;
    public float navPointRadius = 1;
    public Transform leftNavPoint;
    public Transform rightNavPoint;
    public LayerMask mask;

    private float speed;
    private float rotation;

    private float desiredRotation;
    private Vector2 movementDir;

    private Vector2 target;
    private SpriteStack spriteStack;
    private Rigidbody2D rb;
    private ScreenShake screenShake;
    private EnemyTargeting targeting;

    int randomDir;

    // Start is called before the first frame update
    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteStack = GetComponentInChildren<SpriteStack>();
        rb = GetComponent<Rigidbody2D>();
        targeting = GetComponent<EnemyTargeting>();

        speed = moveSpeed;

        randomDir = Random.Range(0, 2);
        randomDir = (int)Mathf.Lerp(-1,1,randomDir);

    }

    // Update is called once per frame
    void Update()
    {
        TargetPlayer();
        rotateAroundPlayer();
    }

    private void FixedUpdate()
    {
        rb.AddForce(spriteStack.getRotation() * speed);
    }

    void TargetPlayer()
    {

        target = targeting.target;

        movementDir = target;
        movementDir = new Vector2(movementDir.x - transform.position.x, movementDir.y - transform.position.y);
        movementDir = movementDir.normalized;

        Collider2D left = Physics2D.OverlapCircle(leftNavPoint.position, navPointRadius, mask);
        Collider2D right = Physics2D.OverlapCircle(rightNavPoint.position, navPointRadius, mask);

        if (left != null && right != null)
        {
            speed = moveSpeed * -1;
        }else if (left!= null)
        {
            speed = moveSpeed;
            rotation -= rotSpeed * Time.deltaTime * 2;
        }
        else if (right != null)
        {
            speed = moveSpeed;
            rotation += rotSpeed * Time.deltaTime * 2;
        }
        else
        {
            speed = moveSpeed;
            if (desiredRotation < 0)
            {

                rotation -= rotSpeed * Time.deltaTime;

            }
            else if (desiredRotation > 0)
            {
                rotation += rotSpeed * Time.deltaTime;
            }
        }



        spriteStack.rotation = rotation;
    }

    void rotateAroundPlayer() {

        if (Vector2.Distance(transform.position, target) < retreatRange)
        {
            desiredRotation = Vector2.SignedAngle(-spriteStack.sprites[0].transform.up, movementDir);
        }
        else if (Vector2.Distance(transform.position, target) < targetingRange)
        {
             desiredRotation = Vector2.SignedAngle(randomDir* spriteStack.sprites[0].transform.right, movementDir);
        }
        else
        {
            desiredRotation = Vector2.SignedAngle(spriteStack.sprites[0].transform.up, movementDir);
            randomDir = Random.Range(0, 2);
            randomDir = (int)Mathf.Lerp(-1, 1, randomDir);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, retreatRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetingRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(leftNavPoint.position, navPointRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rightNavPoint.position, navPointRadius);

    }

}
