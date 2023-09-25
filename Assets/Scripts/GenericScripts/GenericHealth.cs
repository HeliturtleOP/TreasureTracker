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

    public AudioSource hitSound;

    private float lastHealth;
    private float health;
    private bool dead = false;

    private float lerpTimer;

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
        lastHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTimer += Time.deltaTime * 4;

        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Lerp(lastHealth/maxHealth, health / maxHealth, AnimationCurve.EaseInOut(0,0,1,1).Evaluate( lerpTimer));
        }

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

    public void Damage(float damage)
    {
        lerpTimer = 0;
        lastHealth = health;

        hitSound.pitch = 1 + Random.Range(-0.25f, 0.26f);

        hitSound.Play();
        health -= damage;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((hitMask & (1 << collision.gameObject.layer)) != 0)
        {
            Damage(1);
        }

    }

    private void OnDestroy()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().triggerUpdate();
    }

}
