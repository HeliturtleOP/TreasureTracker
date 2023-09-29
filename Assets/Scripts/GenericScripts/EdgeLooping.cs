using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeLooping : MonoBehaviour
{
    public float triggerOffset;
    public float moveOffset;

    public BoxCollider2D bc;


    private Camera cam;
    private float xLimit;
    private float yLimit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
        xLimit = Screen.width * cam.orthographicSize / Screen.height;
        yLimit = cam.orthographicSize;

        bc = GetComponent<BoxCollider2D>();
        bc.size = new Vector2((xLimit*2) + 1, (yLimit*2) + 1);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.layer == 9)
            {
                Destroy(collision.transform.root.gameObject);
                return;
            }

            Vector2 offsetSize = collision.gameObject.GetComponent<CapsuleCollider2D>().size;
            if (offsetSize == null)
            {
                offsetSize = collision.gameObject.GetComponent<BoxCollider2D>().size;
            }

            if (collision.transform.position.x < -xLimit)
            {
                Debug.Log("tp right");

                collision.transform.root.position = new Vector2((xLimit + offsetSize.y), collision.transform.root.position.y);
            }
            else if (collision.transform.position.x > xLimit)
            {
                Debug.Log("tp left");

                collision.transform.root.position = new Vector2(-(xLimit + offsetSize.y), collision.transform.root.position.y);
            }

            if (collision.transform.position.y < -yLimit)
            {
                Debug.Log("tp up");

                collision.transform.root.position = new Vector2(collision.transform.root.position.x, (yLimit + offsetSize.y));
            }
            else if (collision.transform.position.y > yLimit)
            {
                Debug.Log("tp down");

                collision.transform.root.position = new Vector2(collision.transform.root.position.x, -(yLimit + offsetSize.y));
            }

        }
    }

}
