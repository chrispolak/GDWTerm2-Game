using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.gameObject.GetComponent<CharacterScript2D>().stopping)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), Time.deltaTime * 5f);
        }
        
        if (player.position.y >= transform.position.y + 2 || player.position.y <= transform.position.y - 2)
        {
            transform.position = new Vector3(transform.position.x, player.position.y+1.9f, transform.position.z);
        }
    }
}
