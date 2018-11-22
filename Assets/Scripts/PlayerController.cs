using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text winText;
    public float speed;
    public float sprintSpeed;
    public float jumpSpeed;
    public float gravity;
    public float mouseSensitivity;
    public float controllerSensitivity;
    public AudioClip[] steps;
    public float stepPeriod;

    private Transform cam;
    private CharacterController controller;
    private Vector3 moveDirection;
    private float rotX;
    private float rotY;
    private bool noclip;
    private int playerMask;
    private int wallMask;
    private Vector3 startPos;
    private AudioSource sound;
    private bool stepReady;

    // Use this for initialization
    void Start ()
    {
        sound = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        cam = transform.Find("Camera");
        stepReady = true;

        playerMask = LayerMask.NameToLayer("Player");
        wallMask = LayerMask.NameToLayer("Wall");

        startPos = transform.localPosition;
        winText.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Noclip"))
        {
            noclip = !noclip;
            Physics.IgnoreLayerCollision(playerMask, wallMask, noclip);
        }

        if (Input.GetButtonDown("Home"))
        {
            transform.localPosition = startPos;
            rotX = rotY = 0;
        }

        if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // controller aim
        rotX += Input.GetAxis("Controller X") * Time.deltaTime * 100 * controllerSensitivity;
        rotY += Input.GetAxis("Controller Y") * Time.deltaTime * 100 * controllerSensitivity;

        // mouse aim
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            rotX += Input.GetAxisRaw("Mouse X") * mouseSensitivity;

            rotY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        }

        // keep rotations within bounds
        rotX %= 360;
        rotY = Mathf.Clamp(rotY, -90, 90);

        // apply rotations
        transform.localRotation = Quaternion.AngleAxis(rotX, Vector3.up);
        cam.localRotation = Quaternion.AngleAxis(rotY, Vector3.right);

        if (controller.isGrounded)
        {
            // movement
            moveDirection = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")), 1);
            moveDirection = transform.TransformDirection(moveDirection);

            // multiply speed
            bool sprinting = Input.GetButton("Sprint");
            if (sprinting)
            {
                moveDirection *= sprintSpeed;
            }
            else
            {
                moveDirection *= speed;
            }

            // jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            // do step sounds
            Vector2 flatVelocity = new Vector2(controller.velocity.x, controller.velocity.z);
            if (flatVelocity.magnitude > 0 && stepReady)
            {
                sound.clip = steps[Random.Range(0, steps.Length)];
                sound.Play();
                float p = sprinting ? stepPeriod : sprintSpeed / speed * stepPeriod;
                StartCoroutine(StepWait(p));
            }
        }

        // Apply gravity
        moveDirection.y += (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    IEnumerator StepWait (float seconds)
    {
        stepReady = false;
        yield return new WaitForSeconds(seconds);
        stepReady = true;
    }

    void LateUpdate ()
    {
        if (Input.GetButtonDown("Fire1") && Cursor.lockState == CursorLockMode.Locked)
        {
            BallBehaviour ball = ObjectPooler.Instance.Pop("Ball").GetComponent<BallBehaviour>();
            ball.Throw(cam.transform.position, cam.transform.forward, controller.velocity);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Win"))
        {
            GameManager.Instance.Win();
        }
    }
}
