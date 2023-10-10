using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private SpriteStack spriteStack;

    void Start()
    {
        spriteStack = GetComponentInChildren<SpriteStack>();
        if (spriteStack.rotation == 0)
        spriteStack.rotation = Random.Range(0f, 360f);
    }
}
