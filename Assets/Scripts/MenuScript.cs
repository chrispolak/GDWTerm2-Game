using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CheatCode
{
    public List<KeyCode> cheatCode;
}

public class MenuScript : MonoBehaviour
{
    public List<CheatCode> cheatCodes = new List<CheatCode>();
    public int currentSelection = 0;
    public List<ButtonClass> buttons;
    private List<KeyCode> keysPressed = new List<KeyCode>();
    public int maxCodeLen = 10;
    public void changeSelection(int dir)
    {
        if(currentSelection >= 0 && currentSelection <= 1)
        {
            buttons[currentSelection].switchOffButton();
            currentSelection += dir;
            buttons[currentSelection].switchToButton();
        }
        print(currentSelection);
    }
    public int GetMaxCode()
    {
        int temp = 0;
        foreach (CheatCode code in cheatCodes)
        {
            if(code.cheatCode.Count > temp)
            {
                temp = code.cheatCode.Count;
            }
        }
        return temp;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxCodeLen = GetMaxCode();
    }
    public int CheckCodes()
    {
        int i = 0;
        foreach (CheatCode code in cheatCodes)
        {
            if (CheckForCode(code.cheatCode))
            {
                return i;
            }
            i++;
        }
        return -1;
    }
    public bool CheckForCode(List<KeyCode> code)
    {
        if (keysPressed.Count != code.Count)
            return false;
        for (int i = 0; i < keysPressed.Count; i++)
        {
            if (keysPressed[i] != code[i])
                return false;
        }
        return true;
    }
    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                keysPressed.Add(kcode);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            changeSelection(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            changeSelection(-1);
        }
        if (Input.anyKey)
        {
            detectPressedKeyOrButton();
        }
        if (CheckCodes() == 0)
        {
            KonamiCode();
        }
        if(keysPressed.Count >= maxCodeLen)
        {
            keysPressed.Clear();
        }
    }
    private void KonamiCode()
    {
        //print("a");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
