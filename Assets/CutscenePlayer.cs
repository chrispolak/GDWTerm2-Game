using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePlayer : MonoBehaviour
{
    public string loadScene;
    public double time;
    public double currentTime;
    // Use this for initialization
    void Start()
    {

        time = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().clip.length;
    }


    // Update is called once per frame
    void Update()
    {
        currentTime = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().time;
        if (currentTime >= time)
        {

            SceneManager.LoadScene(loadScene, LoadSceneMode.Single);
        }
    }
    
}
