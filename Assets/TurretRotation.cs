using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretRotation : MonoBehaviour
{

    public float rotSpeed = 50;
    Vector2 target;
    private EnemyTargeting targeting;
    private SpriteStack stack;

    // Start is called before the first frame update
    void Start()
    {
        targeting = GetComponent<EnemyTargeting>();
        stack = GetComponentInChildren<SpriteStack>();
    }

    // Update is called once per frame
    void Update()
    {
        target = targeting.target;

        Vector2 rotDiff = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized;

        float rotDir = Vector2.SignedAngle(stack.sprites[0].transform.up, rotDiff);

        if (rotDir != 0)
        {
            stack.rotation += (rotDir / Mathf.Abs(rotDir)) * rotSpeed * Time.deltaTime;
        }
    }


}
