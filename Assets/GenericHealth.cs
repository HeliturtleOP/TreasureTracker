using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GenericHealth : MonoBehaviour
{

    public float maxHealth = 10;

    public Image healthBar;

    public LayerMask hitMask;

    private float health;
    private bool dead = false;
    public AudioSource hitSound;

    private SpriteStack spriteStack;
    private Rigidbody2D rb;

    private PlayerMovment playerMovment;
    private SimpleEnemy simpleEnemy;
    private ComplexEnemy complexEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovment = GetComponent<PlayerMovment>();
        spriteStack = GetComponentInChildren<SpriteStack>();
        simpleEnemy = GetComponent<SimpleEnemy>();
        complexEnemy = GetComponent<ComplexEnemy>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (dead == false)
            {
                if (playerMovment)
                {
                    playerMovment.moveSpeed = 0;
                    playerMovment.rotSpeed = 0;
                }

                if (complexEnemy)
                {
                    complexEnemy.moveSpeed = 0;
                    complexEnemy.rotSpeed = 0;
                }

                if (simpleEnemy)
                {
                    simpleEnemy.moveSpeed = 0;
                    simpleEnemy.rotSpeed = 0;
                }

                rb.velocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                spriteStack.sink = true;

                Destroy(gameObject, spriteStack.sinkDuration);
            }
            dead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((hitMask & (1 << collision.gameObject.layer)) != 0)
        {


            hitSound.Play();
            health -= 1f;

            if (healthBar != null)
            {
                healthBar.fillAmount = health / maxHealth;
            }

        }

    }

}
