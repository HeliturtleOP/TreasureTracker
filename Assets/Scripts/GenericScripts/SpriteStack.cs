using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpriteStack : MonoBehaviour
{
    public GameObject wake;
    public GameObject bubbles;
    public bool sink;
    public bool riseAtStart;
    public float sinkDuration = 1;

    public bool wiggle;

    public float rotation = 0;
    float currentRotation = 0;

    [Tooltip("Set to 1 for regualer rotation and -1 for reversed\nDo not set to 0")]
    [Range(-1, 1)]
    public int flipRotation = -1;

    public int baseSortingOrder;

    public float pixelSpacing = 1;
    private float spacing;

    public Transform[] sprites;

    public bool autoRotate = false;

    void Start()
    {
        currentRotation = rotation;

        sprites = new Transform[transform.childCount];

        spacing = 1 / transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.pixelsPerUnit * pixelSpacing;

        for (int i = 0; i < sprites.Length; i++) 
        {
            sprites[i] = transform.GetChild(i);
            sprites[i].transform.localPosition = new Vector2(0, i*spacing);
            sprites[i].GetComponent<SpriteRenderer>().sortingOrder = i + baseSortingOrder;
            sprites[i].rotation = Quaternion.Euler(new Vector3(0, 0, rotation * flipRotation));
        }

        if (riseAtStart) {

            for (int i = 0;i < sprites.Length;i++)
            {
                sprites[i].GetComponent<SpriteRenderer>().sortingOrder -= sprites.Length;
                sprites[i].transform.position = new Vector2(sprites[i].transform.position.x, sprites[i].transform.position.y - (spacing * sprites.Length));
                
            }
            StartCoroutine(sinkAnim(1));
        }

    }

    void Update()
    {

        if(autoRotate)
        {
            rotation += Time.deltaTime * 360 / 5;
        }

        manageRotation();

        if (sink) {

            StartCoroutine(sinkAnim(-1));
            sink = false;
        
        }

        if (wiggle) {

            sinWiggle();
        }

    }

    private void manageRotation() { 
    
        if (currentRotation != rotation) 
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].rotation = Quaternion.Euler(new Vector3(0,0,rotation * flipRotation));
            }
        
        }

        currentRotation = rotation;
    
    }

    public IEnumerator sinkAnim(int dir)
    {
        if (wake)
        {
            wake.SetActive(false);
        }

        
        ParticleSystem bubbleParticles = Instantiate(bubbles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();

        bubbleParticles.Stop();
        var main = bubbleParticles.main;
        main.duration = sinkDuration + (0.2f);
        bubbleParticles.Play();

        for (int i = 0; i <= sprites.Length; ++i)
        {
            yield return new WaitForSeconds(sinkDuration/sprites.Length);
            for (int j = 0; j < sprites.Length; ++j)
            {
                sprites[j].transform.position = new Vector2(sprites[j].transform.position.x, sprites[j].transform.position.y + spacing * dir);
                sprites[j].GetComponent<SpriteRenderer>().sortingOrder += dir;
            }

        }

        wake.SetActive(true);

    }

    public Vector3 getRotation() { 
    
        return sprites[0].transform.up;
    
    }

    float timer = 0;

    public void sinWiggle() { 
    
        for (int i = 0;i < sprites.Length; ++i)
        {

            sprites[i].transform.localPosition = new Vector2(Mathf.Sin(((float)i) + timer) * spacing, sprites[i].transform.localPosition.y);

        }

        timer += Time.deltaTime;

    }

}
