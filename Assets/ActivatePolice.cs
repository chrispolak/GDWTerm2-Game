using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePolice : EventClass
{
    public ProgressScript progress;
    public void Start()
    {
        progress = FindObjectOfType<ProgressScript>();
    }
    public override void RunEvent()
    {
        progress.policeActive = true;
    }
}
