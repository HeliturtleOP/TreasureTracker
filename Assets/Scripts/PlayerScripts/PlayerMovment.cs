using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovment : MonoBehaviour
{

    public float moveSpeed = 1;
    public float rotSpeed = 100;
    public float rotAccelerationSpeed = 2;
    public float rotDecelerationSpeed = 1;

    private float smoothing;

    private SpriteStack spriteStack;
    private Rigidbody2D rb;


    void Start()
    {
        spriteStack = GetComponentInChildren<SpriteStack>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rotateShip();

    }

    private void FixedUpdate()
    {

        if (Input.GetAxisRaw("Vertical") == 1)
        {

            rb.AddForce(spriteStack.getRotation() * moveSpeed);

        }

    }

    void rotateShip() {

        float xInput = Input.GetAxisRaw("Horizontal");

        spriteStack.rotation += inputSmoothing(xInput) * rotSpeed * Time.deltaTime;

    }

    float lastSmooth = 0;
    float timer;
    float inputSmoothing(float xInput) {

        if(xInput != 0) {
            timer = 0;
            smoothing += Time.deltaTime * rotAccelerationSpeed * xInput;
            lastSmooth = smoothing;
        }
        else
        {

            timer += Time.deltaTime * rotDecelerationSpeed;
            smoothing = Mathf.Lerp(lastSmooth, 0, timer);

        }

        smoothing = Mathf.Clamp(smoothing, -1, 1);

        return smoothing;
    
    }
}
