using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Image mask;
    public float animationTime = 2;

    private Vector3 start = Vector3.zero, end = Vector3.one * 40;

    public GameObject[] enemies;

    private GameObject player;

    private float timer = 0;

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
        if (player)
            mask.transform.position = Camera.main.WorldToScreenPoint(player.transform.position);

        timer += Time.deltaTime/animationTime;

        mask.transform.localScale = Vector3.Lerp(start, end, timer);

    }

    public void triggerUpdate()
    {
        StartCoroutine(checkEnemies());
    }

    public int getEnemies()
    {
        int count = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                count++;
            }
        }
        return count;
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
            StartCoroutine(SceneChangeAnim(1));
            Debug.Log("you lose");
        }

        if (won)
        {
            //win screen

            if (sceneChanger.currentScene() != sceneChanger.lastScene()-1)
            {
                StartCoroutine(SceneChangeAnim(sceneChanger.currentScene() + 1));
            }
            else
            {
                StartCoroutine(SceneChangeAnim(2));
            }

            Debug.Log("you win");
        }

    }

    public IEnumerator SceneChangeAnim(int scene)
    {
        start = Vector3.one * 40;
        end = Vector3.zero;
        timer = 0;
        yield return new WaitForSeconds(animationTime);
        sceneChanger.LoadScene(scene);
    }

}

