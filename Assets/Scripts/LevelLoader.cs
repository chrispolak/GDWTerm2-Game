using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public string[] scenePaths;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLevel(int levelNum)
    {
        if (levelNum == 1)
        {
            SceneManager.LoadScene(scenePaths[0], LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(scenePaths[levelNum], LoadSceneMode.Single);
        }
    }    
}
