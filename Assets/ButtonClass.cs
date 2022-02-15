using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClass : MonoBehaviour
{
    public bool selected = false;
    public Image buttonImage;
    public void switchToButton()
    {
        selected = true;
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 255);
    }
    public void switchOffButton()
    {
        selected = false;
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && selected)
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
