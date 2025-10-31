using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] GameObject player;
    [SerializeField] float speed = 5f;
    [SerializeField] float gravityValue = -20f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] private float rotationSensitivity = 200f;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isJumping;

    private float yRotation; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gravityValue = -20;
        playerVelocity = Vector3.zero; 
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
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        if (movement.sqrMagnitude > 0f)
        {
            player.transform.LookAt(transform.position + movement);
            player.transform.Rotate(0, -90, 0);
            player.transform.rotation.Normalize();
        }


    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.8f);
        isJumping = false;
    }

 
}
