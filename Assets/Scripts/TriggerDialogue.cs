using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : EventClass
{
    GameObject textBox;
    public List<string> text = new List<string>();
    TextBox textScript;
    public void Awake()
    {
        textScript = GameObject.Find("Text").GetComponent<TextBox>();
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
    void OnTriggerExit2D()
    {
        this.gameObject.SetActive(false);
    }
}
