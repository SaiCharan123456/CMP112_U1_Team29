using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject player;
    [SerializeField] GameObject MovingPlayer;
    [SerializeField] float speed = 5f;
    [SerializeField] float gravityValue = -20f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] private float rotationSensitivity = 200f;      
    [SerializeField] Camera playerCamera;

    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isJumping;

    private float yRotation;
    private float xRotation;

    Rigidbody rb;
    private Vector3 lastPosition;
    [SerializeField] float ballRadius = 1f;

    private AudioSource source;

    [SerializeField] AudioClip rollingClip;
    private bool ballSoundPlaying = false;

    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        gravityValue = -20;
        playerVelocity = Vector3.zero;
        lastPosition = transform.position;
        player.SetActive(true);
        MovingPlayer.SetActive(false);

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 1)
        {
            playerVelocity.y = 0f; // reset vertical velocity when grounded
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer && !isJumping)
        {
            isJumping = true;
            playerVelocity.y = jumpForce; // jump force applied once
            StartCoroutine(ResetJump());
        }


        playerVelocity.y += gravityValue * Time.deltaTime;

        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        Vector3 movement = (transform.right * movementX + transform.forward * movementY) * speed;

        controller.Move((-movement + playerVelocity) * Time.deltaTime);
        
        float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        yRotation += mouseX;

        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        xRotation += mouseScroll * rotationSensitivity; // Invert the scrolling if needed

        // Clamp the up/down rotation to prevent flipping the camera
        xRotation = Mathf.Clamp(xRotation, -80f, 30f); // Adjust the range if necessary


        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 180f, 0f);

        if (movement.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
            targetRotation *= Quaternion.Euler(0f, -90f, 0f); // adjust if model is oriented weirdly
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        Vector3 displacement = transform.position - lastPosition;
        float distance = displacement.magnitude;


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            player.SetActive(false);
            MovingPlayer.SetActive(true);
            // Find axis to rotate around (perpendicular to movement)
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, displacement.normalized);

            // Calculate rotation angle based on distance
            float rotationAngle = (distance / ballRadius) * Mathf.Rad2Deg;
            MovingPlayer.transform.Rotate(rotationAxis, rotationAngle, Space.World);

            if (!ballSoundPlaying)
            {
                source.loop = true;
                source.PlayOneShot(rollingClip, 0.3f);
            } 
            
            ballSoundPlaying = true;

        }
        else
        {
            MovingPlayer.SetActive(false);
            player.SetActive(true);

            ballSoundPlaying = false;
            source.loop = false;
            source.Stop();
        }
        lastPosition = transform.position;
    }


    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.8f);
        isJumping = false;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovingPlatform"))
        {
            currentPlatform = hit.collider.transform;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    void LateUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformDelta = currentPlatform.position - lastPlatformPosition;
            controller.Move(platformDelta);
            lastPlatformPosition = currentPlatform.position;
        }

        if (!groundedPlayer)
            currentPlatform = null;
    }
}
