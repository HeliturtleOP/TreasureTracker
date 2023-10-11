using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private SpriteStack spriteStack;
    public bool destructible = false;
    private int health = 3;

    void Start()
    {
        spriteStack = GetComponentInChildren<SpriteStack>();
        if (spriteStack.rotation == 0)
        spriteStack.rotation = Random.Range(0f, 360f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destructible)
        {
            if (collision.gameObject.layer != 13 && collision.gameObject.layer != 12)
            {
                health -= 1;

                if (health == 0)
                {
                    spriteStack.sink = true;
                    Destroy(gameObject, spriteStack.sinkDuration);
                }

            }
        }
    }

}
