using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float lifeTime = 10;

    private ScreenShake screenShake;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.1f)
        {

            GetComponent<BoxCollider2D>().enabled = true;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Camera.main.GetComponent<ScreenShake>().ShakeScreen(0.07f, 0.04f, 6);
        Destroy(gameObject);

    }

}
