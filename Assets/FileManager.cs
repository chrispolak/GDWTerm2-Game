using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public struct levelStruct
{
    public string name;
    public bool unlocked;
}
public class FileManager : MonoBehaviour
{
    public levelStruct[] levels;
    int selectedLevel = 0;
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levels[levelNum].name, LoadSceneMode.Single);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
