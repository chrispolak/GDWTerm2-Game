using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : EventClass
{
    public GameObject textBox;
    public List<string> text = new List<string>();
    public TextBox textScript;
    public override void RunEvent()
    {
        if (!textBox.active)
        {
            textBox.SetActive(true);
            textScript.textList = text;
            textScript.UpdateText();
        }
    }
}
