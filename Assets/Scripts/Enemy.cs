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
    public int shootTimer = 0;
    public int shootTime = 400;
    int direction = 1;
    public bool melee;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
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
        firedProjectile.GetComponent<Rigidbody2D>().velocity = Aim() * 10;
        
    }
    public void MoveToPlayer()
    {
        rb.velocity = new Vector2(speed * Aim().x, rb.velocity.y);
        if (Aim().x < 0)
        {

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        direction = (int)Aim().normalized.x;
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
        else if (shootTimer >= shootTime && canShoot)
        {
            print(shootTimer);
            shootTimer = 0;
            Shoot();
            StartCoroutine(PlayAnim());


        }
        else
        {
            shootTimer++;
        }



        if (shootTimer >= shootTime && melee) {
            
            RaycastHit2D hit;
            if (direction == 1)
            {
                hit = Physics2D.Raycast(shootPoint.position, Vector2.right);
            }
            else
            {
                hit = Physics2D.Raycast(shootPoint.position, Vector2.left);
                Debug.DrawRay(shootPoint.position, Vector2.left, Color.green);
            }


            if (hit.transform.gameObject.tag == "Player" && Mathf.Abs(Vector3.Distance(this.gameObject.transform.position, hit.transform.position)) <= range)
            {
                player.gameObject.GetComponent<CharacterScript2D>().GetStunned();
                Debug.Log("melee test idk");
            }
        }
        else
        {
            shootTimer++;
        }

        IEnumerator PlayAnim()
        {
            anim.SetInteger("Shoot", 1);
            yield return new WaitForSeconds(1);
            anim.SetInteger("Shoot", 0);
        }

    }
}
