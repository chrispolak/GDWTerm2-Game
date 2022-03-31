using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxScript : MonoBehaviour
{
    public CharacterScript2D characterScript2D;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PersonScript>() != null && characterScript2D.dashing)
        {
            collider.gameObject.GetComponent<PersonScript>().Die();
            print("Hit");
        }
    }
}
