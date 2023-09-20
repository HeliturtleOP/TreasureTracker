using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{

    public bool lockMove = false;
    public bool lockRot = false;
    public bool absoluteLock;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public float rotSpeed = 0.01f;
    [Header("Targeting Settings")]
    public float attackRange = 1;
    public float retreatDistance = 2;
    [Header("Attack Settings")]
    public float attackSpeed = 2;
    public float attackLength = 1;

    private float speed;
    private float rotation = 0;
    private float desiredRotation;

    private Vector2 target;
    private SpriteStack spriteStack;
    private Rigidbody2D rb;
    private EnemyTargeting targeting;

    private float attackTimer = 0;
    private Vector2 movementDir;

    void Start()
    {

        targeting = GetComponent<EnemyTargeting>();       
        rb = GetComponent<Rigidbody2D>();
        spriteStack = GetComponentInChildren<SpriteStack>();

        speed = moveSpeed;
    }

    void Update()
    {

        FindRotation();
        TargetingLogic();
        SetRotation();


        if (absoluteLock)
        {
            lockMove = true;
            lockRot = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        

    }

    private void FixedUpdate()
    {
        if (!lockMove)
            rb.velocity = spriteStack.getRotation() * speed;
    }

    void FindRotation() {

        target = targeting.target;

        movementDir = target;
        movementDir = new Vector2(movementDir.x - transform.position.x, movementDir.y - transform.position.y);
        movementDir = movementDir.normalized;       

    }

    void TargetingLogic() {

        float distance = Vector2.Distance(transform.position, target);

        if (distance < attackRange && attackTimer <= attackLength)
        {

            attackTimer += Time.deltaTime;
            lockRot = true;
            speed = attackSpeed;

        }
        else if(distance < retreatDistance && attackTimer != 0)
        {
            lockRot = false;
            speed = moveSpeed;
            desiredRotation = desiredRotation = Vector2.SignedAngle(-spriteStack.sprites[0].transform.up, movementDir);
        }
        else
        {
            lockRot = false;
            speed = moveSpeed;
            attackTimer = 0;
            desiredRotation = Vector2.SignedAngle(spriteStack.sprites[0].transform.up, movementDir);
        }

    }

    void SetRotation()
    {

        if (!lockRot)
        {

            if (desiredRotation < 0)
            {

                rotation -= rotSpeed * Time.deltaTime;

            }
            else if (desiredRotation > 0)
            {


                rotation += rotSpeed * Time.deltaTime;
            }

            spriteStack.rotation = rotation;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) {

            Camera.main.GetComponent<ScreenShake>().ShakeScreen(0.07f, 0.04f, 6);
            absoluteLock = true;
            GetComponentInChildren<Collider2D>().enabled = false;
            spriteStack.sink = true;
            Destroy(gameObject, spriteStack.sinkDuration);
        
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, retreatDistance);
    }

}
