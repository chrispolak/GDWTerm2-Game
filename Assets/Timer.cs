using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public float time;
    void Update()
    {
        time += Time.deltaTime;
        GetComponent<Text>().text = Mathf.Round(time).ToString();
    }
    public void Reset()
    {
        time = 0f;
    }
}