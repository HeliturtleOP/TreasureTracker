using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{


    public Vector2 target;

    public float edgeOffset = 2;

    private GameObject player;
    private float PlayspaceWidth, PlayspaceHeight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        PlayspaceHeight = Camera.main.orthographicSize * 2;

        PlayspaceWidth= (Screen.width * Camera.main.orthographicSize / Screen.height) * 2;

    }

    // Update is called once per frame
    void Update()
    {
        target = FindNearestPath();
    }

    Vector2 FindNearestPath()
    {

        float nearestDist = Vector2.Distance(transform.position, player.transform.position);
        Vector2 nearestPoint = player.transform.position;

        Vector2 right = (Vector2)player.transform.position + new Vector2(PlayspaceWidth + edgeOffset, 0);
        Vector2 left = (Vector2)player.transform.position + new Vector2(-PlayspaceWidth - edgeOffset, 0);
        Vector2 top = (Vector2)player.transform.position + new Vector2(0, PlayspaceHeight + edgeOffset);
        Vector2 bottom = (Vector2)player.transform.position + new Vector2(0, -PlayspaceHeight - edgeOffset);

        if (Vector2.Distance(transform.position, right) < nearestDist)
        {
            nearestDist = Vector2.Distance(transform.position, right);
            nearestPoint = right;
        }

        if (Vector2.Distance(transform.position, left) < nearestDist)
        {
            nearestDist = Vector2.Distance(transform.position, left);
            nearestPoint = left;
        }

        if (Vector2.Distance(transform.position, top) < nearestDist)
        {
            nearestDist = Vector2.Distance(transform.position, top);
            nearestPoint = top;
        }

        if (Vector2.Distance(transform.position, bottom) < nearestDist)
        {
            nearestDist = Vector2.Distance(transform.position, bottom);
            nearestPoint = bottom;
        }

        return nearestPoint;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        //right
        Gizmos.DrawSphere((Vector2)player.transform.position + new Vector2(PlayspaceWidth + edgeOffset, 0), 0.1f);
        //left
        Gizmos.DrawSphere((Vector2)player.transform.position + new Vector2(-PlayspaceWidth - edgeOffset, 0), 0.1f);
        //top
        Gizmos.DrawSphere((Vector2)player.transform.position + new Vector2(0, PlayspaceHeight + edgeOffset), 0.1f);
        //bottom
        Gizmos.DrawSphere((Vector2)player.transform.position + new Vector2(0, -PlayspaceHeight - edgeOffset), 0.1f);

        Gizmos.DrawLine(transform.position, target);

    }


}
