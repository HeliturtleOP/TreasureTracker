using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float harshness = 5f;


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

    public void ShakeScreen(float magnitude, float frequency, float shakeAmount) {

        StartCoroutine(Shake(magnitude, frequency, shakeAmount));
    
    }

    public IEnumerator Shake(float magnitude, float frequency, float shakeAmount)
    {

        for (int i = 0; i < shakeAmount; i++)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);

            desiredPosition = new Vector3(x, y, transform.position.z);

            timer = 0;

            yield return new WaitForSeconds(frequency);
        }

        desiredPosition = cache;

    }

}


