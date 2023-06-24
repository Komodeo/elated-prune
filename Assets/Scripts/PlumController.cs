using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector3 vel;
    public Rigidbody rb;
    public Transform global;

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
    }

    private void FixedUpdate()
    {
        vel = rb.velocity;

        ApplyGravity();

        rb.velocity = vel;
        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -19);
    }

    private void Controller()
    {
        if (Input.GetKey(KeyCode.Space) && autoJump && readyToJump && onGround)
        {
            readyToJump = false;

            Jump();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !autoJump && readyToJump && onGround)
        {
            readyToJump = false;

            Jump();
        }

        else if (inWater)
        {
            print("Game Over");
        }
    }

    private void Jump()
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
                Reset();
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
        if (Input.GetKey(KeyCode.Space))
        {
            vel.y -= marioJumpGravity * Time.deltaTime;
        }

        else
        {
            vel.y -= normalGravity * Time.deltaTime;
        }
    }
}
