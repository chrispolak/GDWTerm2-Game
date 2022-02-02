using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PersonScript>() != null)
        {
            collider.gameObject.GetComponent<PersonScript>().Die();
            print("Hit");
        }
    }
}
