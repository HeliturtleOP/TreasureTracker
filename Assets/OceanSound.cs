using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class OceanSound : MonoBehaviour
{

    private AudioSource source;

    public float minVolume = 0.1f;
    public float maxVolume = 0.3f;

    float smoothInput = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxisRaw("Vertical") == 1)
        {
            smoothInput += Time.deltaTime;
        }else
        {
            smoothInput -= Time.deltaTime;
        }

        smoothInput = Mathf.Clamp(smoothInput, 0, 1);

        float sinVolume = maxVolume + Mathf.Sin(Time.time) * 0.1f;

        float volume = Mathf.Lerp(minVolume + Mathf.Sin(Time.time) * 0.05f, maxVolume + Mathf.Sin(Time.time) * 0.05f, smoothInput);

        source.volume = volume;

    }
}
