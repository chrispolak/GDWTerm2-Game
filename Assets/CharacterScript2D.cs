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
    public float dashDistance = 10;
    public float dashSpeed = 20;
    public Vector3 dashTarget;
    public float brakingMult = 0.5f;
    public float jumpForce = 1f;
    private Vector2 movement;
    private Rigidbody2D rb;
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
    private bool onWall = false;
    public float wallHangMax = 10f;
    private float wallHangTime = 0.0f;
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
        baseGrav = rb.gravityScale;
    }
    void DashFunc(int direction)
    {
        dashDir = direction;
        anim.SetInteger("Direction", direction);
        startTime = Time.time;
        dashTarget = new Vector3(transform.position.x+dashDistance*direction, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        dashing = true;
        if(specDashes > 0)
        {
            SpecialDash(direction);
        }
        else
        {
            NormalDash(direction);
        }
        anim.SetTrigger("Dash");
    }
    void NormalDash(int direction)
    {
    }
    void SpecialDash(int direction)
    {
        specDashes--;
        dashCharges[specDashes].SetActive(false);
        rb.AddForce(new Vector2(direction * dashSpeed, 0));
        //SpecialDash
    }
    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(0, 0);
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
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
            Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            if (mouse.x < playerScreenPoint.x)
            {
                DashFunc(-1);
            }
            else
            {
                DashFunc(1);
            }
        }

        if (onWall)
        {
            rb.gravityScale = 0;
            wallHangTime++;
            if(wallHangTime > wallHangMax)
            {
                onWall = false;
            }
        }
        else
        {

            rb.gravityScale = 10;
        }
        if (dashing)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * dashSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / dashDistance;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(transform.position, new Vector3(dashTarget.x, transform.position.y, transform.position.z), fractionOfJourney);

            lPressed = false;
            rPressed = false;
            if (Mathf.Abs(transform.position.x - dashTarget.x)<=0.2)
            {
                dashing = false;
                rb.velocity = new Vector3(dashDir*50, 0);
            }
        }
    }

    private void Move()
    {
        if(Mathf.Abs(rb.velocity.x) >= maxSpeed)
        {
            return;
        }
        if(movement.x > 0)
        {
            rb.AddForce(Vector2.right * acceleration, ForceMode2D.Impulse);
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
        if(rb.velocity.y >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            onWall = true;
        }
        dashing = false;
    }
    private void OnCollisionExit2D(Collision2D collistion)
    {
        if (onWall)
        {
            onWall = false;
        }
    }
}
