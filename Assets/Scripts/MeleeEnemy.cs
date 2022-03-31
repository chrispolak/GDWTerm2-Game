using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : PersonScript
{
    Rigidbody2D rb;

    public float enemySpeed = 5.0f;

    Vector3 playerDir;
    bool calculatedThisFrame = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LateUpdate()
    {
        calculatedThisFrame = false;
    }

    public void TrackPlayer(Vector3 vector3)
    {
        rb.velocity = playerDir.normalized * enemySpeed;
    }

    public Vector3 GetPlayerDir()
    {
        Vector3 _playerDir;

        if (!calculatedThisFrame)
        {
            _playerDir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;

            playerDir = _playerDir;
            calculatedThisFrame = true;
        }
        else
        {
            _playerDir = playerDir;
        }

        return playerDir;
    }
}
