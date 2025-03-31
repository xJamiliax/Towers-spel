using UnityEngine;

public class SpelMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float camRotateSpeed;
    public Transform cam;
    public Vector3 jumpForce;
    public float raycastDistance;

    private Rigidbody rb;

    void Start()
    {
        // Haal de Rigidbody-component van de speler op
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        // Beweging
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(horizontal, 0, vertical);
        transform.Translate(moveDir * Time.deltaTime * moveSpeed);

        // Rotatie van het lichaam
        float bodyRotate = Input.GetAxis("Mouse X");
        Vector3 bodyRot = new Vector3();
        bodyRot.y = bodyRotate * Time.deltaTime * rotateSpeed;
        transform.Rotate(bodyRot);

        // Camera rotatie
        float camRotate = Input.GetAxis("Mouse Y");
        Vector3 camRot = new Vector3();
        camRot.x = -camRotate * Time.deltaTime * camRotateSpeed;
        cam.Rotate(camRot);

        // Springen
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }


        // Controleer of de speler de Escape toets indrukt om de cursor weer vrij te geven
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    void Jump()
    {
        rb.velocity += jumpForce;
    }

    bool IsGrounded()
    {
        // Controleer of de speler zich dicht bij de grond bevindt
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }

}
