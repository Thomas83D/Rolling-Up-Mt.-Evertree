using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;

    public Transform cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveRight = Input.GetAxis("Horizontal") * speed;

        Vector3 camRight = cam.right;
        camRight.y = 0;
        Vector3 rightRelatve = camRight * moveRight;
        Vector3 moveDir = rightRelatve;

        rb.AddForce(moveDir, ForceMode.Force);
    }
}
