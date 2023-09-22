using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] enemies;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void triggerUpdate()
    {
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    Debug.Log(enemies[i]);
        //}
        StartCoroutine(checkEnemies());
    }

    private IEnumerator checkEnemies()
    {
        bool won = true;
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                won = false;
            }
        }

        if (player == null)
        {
            //loose screen
            Debug.Log("you lose");
        }

        if (won)
        {
            //win screen
            Debug.Log("you win");
        }

    }

}

