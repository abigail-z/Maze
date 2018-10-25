using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float gravity;
    private Transform cam;
    private CharacterController controller;
    private Vector3 moveDirection;
    private float rotX;
    private float rotY;
    private bool noclip;
    private int playerMask;
    private int wallMask;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        cam = transform.Find("Camera");

        playerMask = LayerMask.NameToLayer("Player");
        wallMask = LayerMask.NameToLayer("Wall");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("g"))
        {
            noclip = !noclip;
            Physics.IgnoreLayerCollision(playerMask, wallMask, noclip);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }

        // mouse aim
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            rotX += Input.GetAxis("Mouse X");
            rotX %= 360;
            transform.localRotation = Quaternion.AngleAxis(rotX, Vector3.up);

            rotY += Input.GetAxis("Mouse Y");
            rotY = Mathf.Clamp(rotY, -90, 90);
            cam.localRotation = Quaternion.AngleAxis(rotY, Vector3.left);
        }

        if (controller.isGrounded)
        {
            // movement
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            // jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y + (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
