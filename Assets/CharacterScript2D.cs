using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript2D : MonoBehaviour
{
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
        rb = GetComponent<Rigidbody2D>();
        baseGrav = rb.gravityScale;
    }
    void DashFunc(int direction)
    {
        startTime = Time.time;
        dashTarget = new Vector3(transform.position.x+dashDistance, transform.position.y, transform.position.z);
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

        //Right Movement
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * acceleration, ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.AddForce(-rb.velocity * brakingMult * rb.mass, ForceMode2D.Impulse);
            if(!dashing)
                rPressed = true;
            else
            {
                rPressed = false;
            }
        }

        //Left Movement
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * acceleration, ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.AddForce(-rb.velocity * brakingMult * rb.mass, ForceMode2D.Impulse);
            lPressed = true;
        }

        //Cap Speed
        if(Mathf.Abs(rb.velocity.x) >= maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        //Dash
        if(rPressed || lPressed)
        {
            if(dashTimer >= dashTimerEnd)
            {
                rPressed = false;
                lPressed = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.D) && rPressed)
        {
            DashFunc(1);
            rPressed = false;
        }
        if (Input.GetKeyDown(KeyCode.A) && lPressed)
        {
            DashFunc(-1);
            lPressed = false;
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
            transform.position = Vector3.Lerp(transform.position, dashTarget, fractionOfJourney);

            lPressed = false;
            rPressed = false;
            if (transform.position == dashTarget)
            {
                dashing = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.velocity.y >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            onWall = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collistion)
    {
        if (onWall)
        {
            onWall = false;
        }
    }
}
