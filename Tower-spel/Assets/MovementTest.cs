using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float moveSpeed;
    public float sprintMultiplier = 1.5f;
    public float crouchSpeedMultiplier = 0.5f;
    public float rotateSpeed;
    public float camRotateSpeed;
    public Transform cam;
    public Vector3 jumpForce;
    public float raycastDistance;

    public float wallRunSpeed = 5f;
    public float wallRunDuration = 2f;
    public LayerMask wallLayer;
    private bool isWallRunning = false;
    private float wallRunTimer = 0f;
    private Vector3 wallNormal;

    private Rigidbody rb;
    private bool isCrouching = false;
    private float originalHeight;
    private float crouchHeight = 0.5f;
    private Animator anim;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        originalHeight = GetComponent<CapsuleCollider>().height;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;
        float currentMoveSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveSpeed *= sprintMultiplier;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }

        if (isWallRunning)
        {
            WallRun();
        }
        else
        {
            float currentSpeed = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            //Debug.Log(currentSpeed);
            anim.SetFloat("Speed", currentSpeed);
            //rb.AddForce(transform.forward * (currentMoveSpeed * 1f));
            //rb.velocity = moveDir * currentMoveSpeed;
            Vector3 direction = transform.TransformDirection(moveDir);
            Vector3 velocity = direction * (currentMoveSpeed * moveDir.magnitude);
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;

            //transform.Translate(moveDir * Time.deltaTime * currentMoveSpeed);
        }

        float bodyRotate = Input.GetAxis("Mouse X");
        Vector3 bodyRot = new Vector3();
        //bodyRot.y = bodyRotate * Time.deltaTime * rotateSpeed;
        //transform.Rotate(bodyRot);
        Vector3 rotateVelocity = new Vector3(0, bodyRotate * 100f, 0);
        Quaternion deltaRotation = Quaternion.Euler(rotateVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        float camRotate = Input.GetAxis("Mouse Y");
        Vector3 camRot = new Vector3();
        camRot.x = -camRotate * Time.deltaTime * camRotateSpeed;
        cam.Rotate(camRot);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Jump()
    {
        anim.SetTrigger("Jump");
        //rb.velocity += jumpForce;
        rb.AddForce((jumpForce.y * transform.up), ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            GetComponent<CapsuleCollider>().height = crouchHeight;
            moveSpeed *= crouchSpeedMultiplier;
        }
        else
        {
            GetComponent<CapsuleCollider>().height = originalHeight;
            moveSpeed /= crouchSpeedMultiplier;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            wallNormal = collision.contacts[0].normal;
            StartWallRun();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            
            EndWallRun();
        }
    }

    void StartWallRun()
    {
        if (!isWallRunning)
        {
            isWallRunning = true;
            wallRunTimer = wallRunDuration;
            rb.useGravity = false;
        }
    }

    void WallRun()
    {
        Vector3 alongWall = Vector3.Cross(wallNormal, Vector3.up);
        float dot = Vector3.Dot(alongWall, transform.forward);
        if (dot < 0)
            alongWall = -alongWall;

        Vector3 wallRunVelocity = alongWall * wallRunSpeed;
        wallRunVelocity.y = rb.velocity.y;
        rb.velocity = wallRunVelocity;

        if (Input.GetButtonDown("Jump"))
        {
            Vector3 wallJumpForce = wallNormal * 5f + Vector3.up * 5f;
            rb.velocity = wallJumpForce;
            EndWallRun();
        }

        wallRunTimer -= Time.deltaTime;
        if (wallRunTimer <= 0)
        {
            EndWallRun();
        }
    }

    void EndWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;
    }

   
}
