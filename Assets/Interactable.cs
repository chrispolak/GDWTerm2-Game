using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Interact();
    }
    public virtual void Interact()
    {

    }
}
