using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript2D : MonoBehaviour
{
    public int facing = 1;
    private Animator anim;
    public int specDashes = 3;
    public bool stopping = false;
    private float baseGrav;
    public bool dashing = false;
    public TrailRenderer trail;
    public bool isGrounded = true;
    public float acceleration = 0.5f;
    public Vector3 dashStartPos;
    public float dashSpeed = 20;
    public Vector3 dashTarget;
    public float brakingMult = 0.5f;
    public float jumpForce = 1f;
    GameObject targetSprite;
    private Vector2 movement;
    private Rigidbody2D rb;
    public GameObject dashTargSprite;
    private int stopCatchup = 0;
    public float maxSpeed = 10;
    private bool attacking = false;
    private float dashTimer;
    private float startTime;
    public float dashTimerEnd;
    private int dashDir = 0;
    private bool rPressed = false;
    private bool lPressed = false;
    public GameObject HUD;
    int i = 0;
    public Transform caster;
    private bool onWall = false;
    private bool catchingUp = false;
    public List<GameObject> dashCharges;
    public float attackRange = 4;
    public float stunTimer = 1;
    public bool stunned = false;
    private bool loop = false;

    private IEnumerator Stun()
    {
        stunned = true;   
        yield return new WaitForSeconds(stunTimer);
        stunned = false;

    }
    public void RegenSpecDash()
    {
        dashCharges[specDashes].SetActive(true);
        specDashes++;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HUD = GameObject.Find("HUD");
    }
    void DashFunc(Vector3 location)
    {
        dashStartPos = transform.position;
        if(location.x <= transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facing = -1;
        }
        if (location.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facing = 1;
        }
        startTime = Time.time;
        dashing = true;
        trail.enabled = true;
        anim.SetTrigger("Attack");
        //dashTarget = new Vector3(transform.position.x+dashDistance*direction, transform.position.y, transform.position.z);
        //transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        //rb.position = location;
        if (specDashes > 0)
        {
            SpecialDash(location);
        }
    }
    void NormalDash(Vector3 location)
    {
    }
    void SpecialDash(Vector3 location)
    {
        dashing = true;
        trail.enabled = true;
        anim.SetTrigger("Attack");
        specDashes--;
        dashCharges[specDashes].SetActive(false);
        //rb.AddForce((this.transform.position - location).normalized);
        //SpecialDash
    }
    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(0, 0);
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && Mathf.Abs(rb.velocity.y) <= 1)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            stopping = false;
            catchingUp = false;
        }
        movement.x = Input.GetAxis("Horizontal");
        if (movement.x != 0)
        {
            anim.SetBool("Running", true);
            Move();
        }
        if(movement.x == 0)
        {
            anim.SetBool("Running", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            bool nearEnemy = false;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Targeter")
                {
                    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        print(Vector3.Distance(hit.point, enemy.transform.position));
                        if (Vector3.Distance(hit.point, enemy.transform.position) <= 10)
                        {
                            nearEnemy = true;
                            dashTarget = enemy.transform.position;
                        }
                    }
                    if (!nearEnemy)
                        dashTarget = hit.point;
                }
                targetSprite = Instantiate(dashTargSprite, dashTarget, Quaternion.identity);
            }
        }
        if (Input.GetMouseButton(1))
        {
            bool nearEnemy = false;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Targeter")
                {
                    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        print(Vector2.Distance(hit.point, enemy.transform.position));
                        if (Vector3.Distance(hit.point, enemy.transform.position) <= 2)
                        {
                            nearEnemy = true;
                            dashTarget = enemy.transform.position;
                        }
                    }
                    if (!nearEnemy)
                        dashTarget = hit.point;
                }
            }
            targetSprite.transform.position = dashTarget;
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            DashFunc(dashTarget);
            Destroy(targetSprite);
        }
        if (dashing)
        {
            Attack();
            if (Vector3.Distance(transform.position, dashTarget) <= 5)
            {
                dashing = false;
                trail.enabled = false;
                rb.velocity = new Vector2(0, 0);

                Destroy(targetSprite);
            }
            rb.velocity = (dashTarget - transform.position).normalized * dashSpeed;
        }
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        if(rb.velocity.magnitude == 0)
        {
            dashing = false;
        }

        if (!dashing && (movement.x == 0))
        {
            anim.SetBool("Running", false);
        }
    }
    void Attack()
    {
        attacking = true;
        RaycastHit2D hit;
        anim.SetTrigger("Attack");
        if(facing == 1)
        {
             hit = Physics2D.Raycast(caster.position, Vector2.right);
        }
        else
        {
             hit = Physics2D.Raycast(caster.position, Vector2.left);
            Debug.DrawRay(caster.position, Vector2.left, Color.green);
            print("a");
        }
        hit = Physics2D.Raycast(caster.position, Vector2.right);
        print(hit.transform.gameObject);
        if (hit.transform.gameObject.tag == "Enemy" && Vector3.Distance(this.gameObject.transform.position, hit.transform.position) <= attackRange)
        {
            Destroy(hit.transform.gameObject);
            print("Attack");
            dashCharges[specDashes].SetActive(true);
            specDashes++;
        }
    }

    private void Move()
    {
        if (stunned == false && onWall == false)
        {
            if (movement.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.AddForce(Vector2.right * acceleration, ForceMode2D.Impulse);
                facing = 1;
            }
            else if (movement.x < 0)
            {
                rb.AddForce(Vector2.left * acceleration, ForceMode2D.Impulse);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                facing = -1;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Stun());
        }
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) < 0.5 && rb.velocity.y != 0)
            {
                onWall = true;
            }
            else
            {
                onWall = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collistion)
    {
    }
}
