using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Exit();
            }
            else
            {
                SceneManager.LoadScene(0);
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void LoadScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public int currentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int lastScene()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
