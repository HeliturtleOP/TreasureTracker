using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float magnitude = 0.05f;
    public float frequency = 30f;
    public float harshness = 5f;
    public int shakes = 5;

    private Vector3 cache;
    private Vector3 desiredPosition;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        cache = transform.position;
        desiredPosition = cache;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * harshness;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, timer);

    }

    public IEnumerator Shake()
    {

        for (int i = 0; i < shakes; i++)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);

            desiredPosition = new Vector3(x, y, transform.position.z);

            timer = 0;

            yield return new WaitForSeconds(1 / frequency);
        }

        desiredPosition = cache;

    }

}


