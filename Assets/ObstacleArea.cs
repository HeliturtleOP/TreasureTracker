using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleArea : MonoBehaviour
{
    public GameObject[] obstacles;

    public float SpawnRadius = 2;
    public float SpawnAmount = 5;

    public AnimationCurve weight = AnimationCurve.Linear(0,0,1,1);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= SpawnAmount; i++)
        {
            Vector2 spawnPoint = Random.insideUnitCircle * SpawnRadius;
            spawnPoint += (Vector2)transform.position;
            Instantiate(obstacles[randomIndex()], spawnPoint, Quaternion.identity);
        }
    }

    int randomIndex()
    {
        float num = Random.Range(0f, 1f);

        num = weight.Evaluate(num) * (obstacles.Length - 1);

        return Mathf.RoundToInt(num);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SpawnRadius);
    }

}
