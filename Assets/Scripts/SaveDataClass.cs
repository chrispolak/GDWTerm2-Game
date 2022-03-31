using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
[System.Serializable]
public struct Unlockable
{
    public string name;
    public string description;
}
public class SaveDataClass : MonoBehaviour
{
    string saveDirectory;
    public bool[] levelCompletion;
    public Text loreText;
    public string[] LoreText;
    System.IO.DirectoryInfo saveLocation;
    void Awake()
    {
        loreText.text = String.Join(" ", LoreText);
    }
    public static string GetUserPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    public void InitializeGameFiles()
    {
        saveDirectory = GetUserPath() + "\\My Games\\CyberKiller";
        if (!Directory.Exists(saveDirectory))
        {
            saveLocation = Directory.CreateDirectory(saveDirectory);
        }
        if (!File.Exists(saveDirectory + "\\LevelCompletion.txt"))
        {
            File.Copy(Application.dataPath + "\\Default Save Data Templates\\LevelCompletion.txt", saveDirectory + "\\LevelCompletion.txt");
        }
        if (!File.Exists(saveDirectory + "\\Unlockables.txt"))
        {
            File.Copy(Application.dataPath + "\\Default Save Data Templates\\Unlockables.txt", saveDirectory + "\\Unlockables.txt");
        }
    }

    public void LoadLevelCompletion()
    {
        int i = 0;
        string[] levelText = File.ReadAllLines(saveDirectory + "\\LevelCompletion.txt");
        foreach (string levelDat in levelText)
        {
            if(levelDat[0] == '0')
            {
                levelCompletion[i] = false;
            }
            else
            {
                levelCompletion[i] = true;
            }
            i++;
        }
    }

    public void LoadUnlockables()
    {
        LoreText = File.ReadAllLines(saveDirectory + "\\Unlockables.txt");
    }

    public void LoadSaveData()
    {
        LoadUnlockables();
    }

    public void SaveData()
    {
        List<string> levelText = new List<string>();
        foreach (bool level in levelCompletion)
        {
            if (level)
            {
                levelText.Add("1");
            }
            else
            {
                levelText.Add("0");
            }
        }
        System.IO.File.WriteAllLines(saveDirectory + "\\LevelCompletion", levelText);
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeGameFiles();
        LoadUnlockables();
        SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
