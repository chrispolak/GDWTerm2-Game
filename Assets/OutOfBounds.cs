using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public ProgressScript progressScript;
    void OnTriggerEnter(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            progressScript.ResetLevel();
        }
    }
}
