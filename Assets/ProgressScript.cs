using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal; 
public enum PoliceState
{
    Safe,
    Close,
    Caught
}
public class ProgressScript : MonoBehaviour
{
    float duration = 1.0f;
    Color color0 = Color.red;
    Color color1 = Color.blue;
    public float progressDifference = 0;
    public float policeSpeed = 5.0f;
    public Light2D sirenLight;
    public Transform startMark;
    public Transform endMark;
    public Transform playerTrans;
    public Slider playerSlider;
    public Slider policeSlider;
    public PoliceState policeState = PoliceState.Safe;
    // Start is called before the first frame update
    void Start()
    {
        policeSlider.value = -0.1f;
    }
    void UpdateSliders()
    {
        playerSlider.value = (playerTrans.position.x - startMark.position.x) / (endMark.position.x - startMark.position.x);
        policeSlider.value += 0.0001f * policeSpeed;
    }
    void UpdatePoliceState()
    {
        if(progressDifference < 0.1)
        {
            if(progressDifference <= 0)
            {
                policeState = PoliceState.Caught;
            }
            else
            {
                policeState = PoliceState.Close;
            }
        }
        else
        {
            policeState = PoliceState.Safe;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
        progressDifference = playerSlider.value-policeSlider.value;
        UpdatePoliceState();
        print(policeState.ToString());
        if(policeState == PoliceState.Close)
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            sirenLight.color = Color.Lerp(color0, color1, t);
        }
        else
        {
            sirenLight.color = new Color(0, 0, 0, 0);
        }
    }
}
