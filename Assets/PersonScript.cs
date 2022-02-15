using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonScript : MonoBehaviour
{
    public bool alive;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"&&other.gameObject.GetComponent<CharacterScript2D>().dashing == true)
        {
            Die();
        }
    }
    public void Die()
    {
        GameObject.Find("Player").GetComponent<CharacterScript2D>().RegenSpecDash();
        Destroy(gameObject);
    }
}
