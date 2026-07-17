using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public GameObject target;
    public Rigidbody playerRb;
    private Vector3 cameraOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        cameraOffset = new Vector3(0,0,-6);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + cameraOffset;
        transform.LookAt(target.transform);
    }

    private void Update()
    {
        HandleCameraRotation();
    }

    public void HandleCameraRotation()
    {
        float rotation = 90;

        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Camera Rotated 90 degrees");
            cameraOffset = Quaternion.AngleAxis(rotation, Vector3.down) * cameraOffset;
        }
        else if(Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Camera Rotated -90 degrees"); 
            cameraOffset = Quaternion.AngleAxis(rotation, Vector3.up) * cameraOffset;
        }
    }
}
