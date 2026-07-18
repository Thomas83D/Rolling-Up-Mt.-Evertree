using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float accelerationTime, decelerationTime;

    private float acceleration, deceleration;
    private Rigidbody rb;

    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveRight = Input.GetAxis("Horizontal") * acceleration;

        Vector3 camRight = cam.right;
        camRight.y = 0;
        Vector3 rightRelatve = camRight * moveRight;
        Vector3 moveDir = rightRelatve;

        moveDir += -rb.linearVelocity;

        rb.AddForce(moveDir * deceleration, ForceMode.Force);
    }
}
