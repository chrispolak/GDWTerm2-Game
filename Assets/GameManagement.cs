using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManagement : MonoBehaviour
{
    string saveDirectory;
    // Start is called before the first frame update
    void Start()
    {
        saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\CyberKiller";
    }
    public int levelNumber;
    public void EndLevel()
    {
        string[] levelText = File.ReadAllLines(saveDirectory + "\\LevelCompletion");
        levelText[levelNumber - 1] = "1";
        System.IO.File.WriteAllLines(saveDirectory + "\\LevelCompletion", levelText);
    }
    public void UnlockUnlockable(string unlockable)
    {
        System.IO.File.WriteAllText(saveDirectory + "\\Unlockables.txt", unlockable);
    }
}
