using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : EventClass
{
    public GameObject textBox;
    public List<string> text = new List<string>();
    TextBox textScript;
    public void Awake()
    {
        textScript = textBox.GetComponent<TextBox>();
    }
    public override void RunEvent()
    {
        if (!textBox.activeSelf)
        {
            textBox.SetActive(true);
        }
        textScript.textList = text;
        textScript.UpdateText();
        textScript.i = 0;
    }

}
