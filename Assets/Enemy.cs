using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PersonScript
{
    public bool canShoot = false;
    public GameObject projectile;
    public Transform shootPoint;
    public float range = 10;
    public Transform player;
    public bool moves = false;
    public Rigidbody2D rb;
    public float speed = 5;
    int shootTimer = 0;
    int shootTime = 400;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        if (canShoot)
        {
            Shoot();
        }
    }
    public Vector3 Aim()
    {
        Vector3 posB = player.position;
        Vector3 posA = this.transform.position;
        Vector3 dir = (posB - posA).normalized;
        return dir;
    }
    public void Shoot()
    {
        GameObject firedProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        firedProjectile.GetComponent<Rigidbody2D>().velocity = Aim()*10;
    }
    public void MoveToPlayer()
    {
        rb.velocity = speed * Aim();
    }
    // Update is called once per frame
    void Update()
    {
        Random.InitState((int)Time.time);
        if (Vector3.Distance(player.position, this.gameObject.transform.position) >= range)
        {
            if (moves)
            {
                MoveToPlayer();
            }
        }
        else if (shootTimer >= shootTime)
        {
            print(shootTimer);
            shootTimer = 0;
            Shoot();
        }
        else
        {
            shootTimer++;
        }
    }
}
