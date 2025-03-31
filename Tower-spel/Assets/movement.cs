using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 moveDir;
    public float moveSpeed;
    public Vector3 bodyRotate;
    public float rotateSpeed;
    public Transform cam;
    public Vector3 camRotate;
    public float camRotateSpeed;
    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame 
    void Update()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");
        transform.Translate(moveDir * Time.deltaTime * moveSpeed);



        bodyRotate.y = Input.GetAxis("Mouse X");
        transform.Rotate(bodyRotate * Time.deltaTime * camRotateSpeed);


        camRotate.x = Input.GetAxis("Mouse Y");
        cam.Rotate(-camRotate * Time.deltaTime * camRotateSpeed);



    }
}
