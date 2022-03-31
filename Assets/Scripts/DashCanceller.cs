using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCanceller : MonoBehaviour
{
    public GameObject player;
    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<CharacterScript2D>().dashing = false;
    }
}
