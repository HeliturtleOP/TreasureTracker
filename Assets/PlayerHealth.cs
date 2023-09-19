using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 10;

    private float health;
    private bool dead = false;
    public AudioSource hitSound;

    private SpriteStack spriteStack;
    private PlayerMovment playerMovment;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovment = GetComponent<PlayerMovment>();
        spriteStack = GetComponentInChildren<SpriteStack>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (dead == false)
            {
                playerMovment.moveSpeed = 0;
                playerMovment.rotSpeed = 0;

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
        hitSound.Play();
        health -= 0.5f;
    }

}
