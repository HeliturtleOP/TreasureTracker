using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EdgeLooping : MonoBehaviour
{
    public float triggerOffset;
    public float moveOffset;

    public BoxCollider2D left;
    public BoxCollider2D right;
    public BoxCollider2D top;
    public BoxCollider2D bottom;

    private Camera cam;
    private float xLimit;
    private float yLimit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
        xLimit = Screen.width * cam.orthographicSize / Screen.height;
        yLimit = cam.orthographicSize;

        top.transform.position = Vector2.up * (yLimit + triggerOffset);
        bottom.transform.position = Vector2.down * (yLimit + triggerOffset);
        left.transform.position = Vector2.left * (xLimit + triggerOffset);
        right.transform.position = Vector2.right * (xLimit + triggerOffset);

        top.size = new Vector2((xLimit + triggerOffset + 1) * 2, 1);
        bottom.size = new Vector2((xLimit + triggerOffset + 1) * 2, 1);
        left.size = new Vector2(1, (yLimit + triggerOffset + 1) * 2);
        right.size = new Vector2(1, (yLimit + triggerOffset + 1) * 2);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {

            if (collision.gameObject.layer == 9)
            {
                Destroy(collision.transform.root.gameObject);
                return;
            }

            if (collision.transform.position.x < -xLimit - moveOffset)
            {
                Debug.Log("tp right");

                collision.transform.root.position = new Vector2((xLimit + moveOffset), collision.transform.root.position.y);
            }
            else if (collision.transform.position.x > xLimit + moveOffset)
            {
                Debug.Log("tp left");

                collision.transform.root.position = new Vector2(-(xLimit + moveOffset), collision.transform.root.position.y);
            }

            if (collision.transform.position.y < -yLimit - moveOffset)
            {
                Debug.Log("tp right");

                collision.transform.root.position = new Vector2(collision.transform.root.position.x, (yLimit + moveOffset));
            }
            else if (collision.transform.position.y > yLimit+ moveOffset)
            {
                Debug.Log("tp left");

                collision.transform.root.position = new Vector2(collision.transform.root.position.x, -(yLimit + moveOffset));
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector2.up * (yLimit + triggerOffset), 0.1f);
        Gizmos.DrawSphere(Vector2.down * (yLimit + triggerOffset), 0.1f);
        Gizmos.DrawSphere(Vector2.left * (xLimit + triggerOffset), 0.1f);
        Gizmos.DrawSphere(Vector2.right * (xLimit + triggerOffset), 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Vector2.up * (yLimit + moveOffset), 0.1f);
        Gizmos.DrawSphere(Vector2.down * (yLimit + moveOffset), 0.1f);
        Gizmos.DrawSphere(Vector2.left * (xLimit + moveOffset), 0.1f);
        Gizmos.DrawSphere(Vector2.right * (xLimit + moveOffset), 0.1f);
    }

}
