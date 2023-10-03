using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] enemies;

    private GameObject player;

    SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        //triggerUpdate();
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
            sceneChanger.LoadScene(1);
            Debug.Log("you lose");
        }

        if (won)
        {
            //win screen

            if (sceneChanger.currentScene() != sceneChanger.lastScene()-1)
            {
                sceneChanger.LoadScene(sceneChanger.currentScene() + 1);
            }
            else
            {
                sceneChanger.LoadScene(2);
            }

            Debug.Log("you win");
        }

    }

}

