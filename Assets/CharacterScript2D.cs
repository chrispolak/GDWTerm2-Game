using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript2D : MonoBehaviour
{
    private Animator anim;
    public int specDashes = 3;
    public bool stopping = false;
    private float baseGrav;
    public bool dashing = false;
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
    int i = 0;
    private bool catchingUp = false;
    public List<GameObject> dashCharges;
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
    }
    void DashFunc(Vector3 location)
    {
        dashStartPos = transform.position;
        startTime = Time.time;
        //dashTarget = new Vector3(transform.position.x+dashDistance*direction, transform.position.y, transform.position.z);
        //transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        //rb.position = location;
        if(specDashes > 0)
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
        anim.SetTrigger("Dash");
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
        if(movement.x != 0)
        {
            anim.SetBool("Running", true);
            Move();
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
        }
        if (dashing)
        {

            if (Vector3.Distance(transform.position, dashTarget) <= 5)
            {
                dashing = false;
                rb.velocity = new Vector2(0, 0);

                Destroy(targetSprite);
            }
            rb.velocity = (dashTarget - transform.position).normalized * dashSpeed;
        }
    }

    private void Move()
    {
        if((rb.velocity.x >= 0 && movement.x <= 0) || (rb.velocity.x <= 0 && movement.x >= 0))
        {
            rb.velocity = new Vector2(0, 0);
        }
        if(Mathf.Abs(rb.velocity.x) >= maxSpeed)
        {
            return;
        }
        if(movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.AddForce(Vector2.right * acceleration, ForceMode2D.Impulse);
            anim.SetBool("Running", false);
        }
        if (movement.x < 0)
        {
            rb.AddForce(Vector2.left * acceleration, ForceMode2D.Impulse);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("Running", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private void OnCollisionExit2D(Collision2D collistion)
    {
    }
}
