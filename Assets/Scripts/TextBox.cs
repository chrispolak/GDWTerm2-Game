using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public Text textObj;
    public List<string> textList = new List<string>();
    public int i = 0;
    public void UpdateText()
    {
        if (i > textList.Count - 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            textObj.text = textList[i];
            i++;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UpdateText();
        }
    }
}
