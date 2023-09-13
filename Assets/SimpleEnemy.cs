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

    private GameObject player;
    private SpriteStack spriteStack;
    private Rigidbody2D rb;

    private float timer = 0;
    private Vector2 movementDir;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");        
        rb = GetComponent<Rigidbody2D>();
        spriteStack = GetComponentInChildren<SpriteStack>();

        speed = moveSpeed;
    }

    void Update()
    {

        FindRotation();
        TargetingLogic();

        

        if (absoluteLock)
        {
            lockMove = true;
            lockRot = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {

            rb.constraints = RigidbodyConstraints2D.None;
        }

        SetRotation();

    }

    private void FixedUpdate()
    {
        if (!lockMove)
            rb.velocity = spriteStack.getRotation() * speed;
    }

    void FindRotation() {

        movementDir = player.transform.position;
        movementDir = new Vector2(movementDir.x - transform.position.x, movementDir.y - transform.position.y);
        movementDir = movementDir.normalized;

    }

    void TargetingLogic() {

        if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance && timer < attackLength)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
            {

                timer += Time.deltaTime;
                lockRot = true;
                speed = attackSpeed;

            }
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance && timer > attackLength)
        {
            lockRot = false;
            speed = moveSpeed;
            movementDir = -movementDir;
        }
        else
        {
            lockRot = false;
            speed = moveSpeed;
            timer = 0;
        }

    }

    void SetRotation()
    {

        if (!lockRot)
        {

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDir);
            rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, 0, rotation)), toRotation, rotSpeed).eulerAngles.z;
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
