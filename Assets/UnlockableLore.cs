using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class UnlockableLore : Interactable
{
    public string addLoreText = "Lore Here";
    public override void Interact()
    {
        GameObject.Find("LevelStuff").GetComponent<GameManagement>().UnlockUnlockable(addLoreText);
    }
}
