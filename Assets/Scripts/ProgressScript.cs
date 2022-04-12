using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public enum PoliceState
{
    Safe,
    Close,
    Caught,
    Escaped
}
public class ProgressScript : MonoBehaviour
{
    public Timer timer;
    public bool policeActive = true;
    float duration = 1.0f;
    Color color0 = Color.red;
    Color color1 = Color.blue;
    public float progressDifference = 0;
    public float policeSpeed = 5.0f;
    public GameObject siren;
    public UnityEngine.Rendering.Universal.Light2D sirenLight;
    public Transform startMark;
    public Transform endMark;
    public Transform playerTrans;
    public Slider playerSlider;
    public Slider policeSlider;
    public PoliceState policeState = PoliceState.Safe;
    public GameObject deathScreen, winScreen;
    // Start is called before the first frame update
    void Start()
    {
        policeSlider.value = -0.1f;
    }
    void UpdateSliders()
    {
        playerSlider.value = (playerTrans.position.x - startMark.position.x) / (endMark.position.x - startMark.position.x);
        policeSlider.value += 0.0001f * policeSpeed;
        siren.transform.position = new Vector3(startMark.position.x + ((endMark.position.x - startMark.position.x) * policeSlider.value) - 12, 0, 0);
    }
    void UpdatePoliceState()
    {
        if (progressDifference <= 0)
        {
            policeState = PoliceState.Caught;
        }
        else if (progressDifference < 0.2)
        {
            policeState = PoliceState.Close;
        }
        if (playerSlider.value >= 1)
        {
            policeState = PoliceState.Escaped;
        }
        else
        {
            policeState = PoliceState.Safe;
        }
    }
    public void ResetLevel()
    {
        playerTrans.position = startMark.position;
        policeSlider.value = 0;
        timer.Reset();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
        progressDifference = playerSlider.value - policeSlider.value;
        if (policeActive)
        {
            UpdatePoliceState();
        }
        
        if (progressDifference <= 0)
        {
            
            policeSlider.gameObject.SetActive(false);
            playerSlider.gameObject.SetActive(false);
            siren.SetActive(false);
        }
        if (policeState == PoliceState.Escaped)
        {
            winScreen.SetActive(true);
            policeSlider.gameObject.SetActive(false);
            playerSlider.gameObject.SetActive(false);
            siren.SetActive(false);
        }
        else if (policeState == PoliceState.Caught)
        {
            ResetLevel();
        }
        if (playerTrans.position.x > endMark.position.x)
        {
            print("a");
            GameObject.Find("LevelStuff").GetComponent<GameManagement>().EndLevel();
        }
    }
}
