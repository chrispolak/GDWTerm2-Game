using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour
{
    public float policeSpeed = 5.0f;
    public Transform startMark;
    public Transform endMark;
    public Transform playerTrans;
    public Slider playerSlider;
    public Slider policeSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider.value = (playerTrans.position.x - startMark.position.x) / (endMark.position.x - startMark.position.x);
        policeSlider.value += 0.0001f * policeSpeed;
    }
}
