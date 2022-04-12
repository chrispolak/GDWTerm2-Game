using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public string nextLevel;
    string saveDirectory;
    // Start is called before the first frame update
    void Start()
    {
        saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\CyberKiller";
    }
    public int levelNumber;
    public void EndLevel()
    {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        string[] levelText = File.ReadAllLines(saveDirectory + "\\LevelCompletion");
        levelText[levelNumber - 1] = "1";
        System.IO.File.WriteAllLines(saveDirectory + "\\LevelCompletion", levelText); 
    }
    public void UnlockUnlockable(string unlockable)
    {
        System.IO.File.WriteAllText(saveDirectory + "\\Unlockables.txt", unlockable);
    }
}
