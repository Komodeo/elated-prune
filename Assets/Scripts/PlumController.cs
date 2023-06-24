using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlumController : MonoBehaviour
{
    public bool autoJump = false;
    public float jumpForce;
    bool readyToJump = true;
    public bool onGround;
    public float slopeLimit;
    public bool inWater = false;
    public bool marioJump = false;
    public float normalGravity;
    public float marioJumpGravity;
    public float shrunkScale;
    public float inflatedScale;
    public bool released = false;
    public float XSpeedLimit;
    public float YSpeedLimit;
    private Vector3 vel;
    public Rigidbody rb;
    public Transform global;

    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
        UI();
    }

    private void UI()
    {
        text.text = "x velocity = " + vel.x + "\ny velocity = " + vel.y;
    }

    private void FixedUpdate()
    {
        vel = rb.velocity;

        ApplyGravity();

        rb.velocity = vel;
    }

    private void Controller()
    {
        if (Input.GetKey(KeyCode.Space) && autoJump && readyToJump && onGround)
        {
            readyToJump = false;

            jump();
        }

        else if (Input.GetKeyUp(KeyCode.Space) && !autoJump)
        {
            released = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !autoJump && readyToJump && onGround)
        {
            readyToJump = false;

            jump();
        }

        else if (inWater)
        {
            print("Game Over");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Shrink();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Normalize();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Inflate();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Normalize();
        }
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(global.transform.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision other)
    {
        // Check if any of the contacts has acceptable floor angle
        foreach (ContactPoint contact in other.contacts)
        {
            if (contact.normal.y > Mathf.Sin(slopeLimit * (Mathf.PI / 180f) + Mathf.PI / 2f))
            {
                onGround = true;
                if (autoJump || released)
                {
                    Reset();
                }
                released = false;
                return;
            }
        }
    }

    private void Reset()
    {
        readyToJump = true;
    }

    private void ApplyGravity()
    {
        if (Input.GetKey(KeyCode.Space) && marioJump)
        {
            vel.y -= marioJumpGravity * Time.deltaTime;
        }

        else
        {
            vel.y -= normalGravity * Time.deltaTime;
        }
    }

    private void Shrink()
    {
        transform.localScale = new Vector3(shrunkScale, shrunkScale, shrunkScale);
    }

    private void Inflate()
    {
        transform.localScale = new Vector3(inflatedScale, inflatedScale, inflatedScale);
    }

    private void Normalize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
