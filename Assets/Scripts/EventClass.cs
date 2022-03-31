using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClass : MonoBehaviour
{
    public virtual void RunEvent()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("A");
            RunEvent();
        }
    }
}
