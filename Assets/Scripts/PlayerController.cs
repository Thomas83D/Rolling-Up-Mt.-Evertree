using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float maxSlopeSpeed;
    public float slopeForce;
    public float jumpHeight;
    public float fallVelocity;
    public float accelerationTime, decelerationTime;

    private float acceleration, deceleration;
    private bool isGrounded;
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 surfaceNormal;

    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveRight = Input.GetAxis("Horizontal");

        Vector3 camRight = cam.right;
        camRight.y = 0;
        Vector3 rightRelatve = camRight * moveRight;
        Vector3 moveDir = rightRelatve;

        velocity = new Vector3(moveDir.x, 0, moveDir.z).normalized;

        Jump();
    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero && IsGrounded())
        {
            rb.AddForce(velocity * acceleration, ForceMode.Force);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        else
        {
            if(rb.linearVelocity.magnitude > 0.01f && IsGrounded())
            {
                rb.AddForce(-rb.linearVelocity.x * deceleration,0,-rb.linearVelocity.z * deceleration, ForceMode.Force);
            }
            OnSlope();
        }
    }
    private bool IsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.8f);
        surfaceNormal = hit.normal;
        return isGrounded;
    }

    private void OnSlope()
    {
        if (IsGrounded())
        {
            if (surfaceNormal != Vector3.down)
            {
                Vector3 slopeDownDirection = Vector3.ProjectOnPlane(Vector3.down, surfaceNormal).normalized;

                if (rb.angularVelocity.magnitude < maxSlopeSpeed)
                {
                    rb.AddForce(slopeDownDirection * slopeForce, ForceMode.Acceleration);
                }
            }
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z);
        }
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * fallVelocity, rb.linearVelocity.z);
        }
    }
}