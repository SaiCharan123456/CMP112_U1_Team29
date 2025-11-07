using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 5f;
    [SerializeField] float gravityValue = -20f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] private float rotationSensitivity = 200f;
    [SerializeField] GameObject obstacle;

    public Vector3 playerVelocity;
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

        if (playerState.instance.health < 0)
        {
            Destroy(gameObject); //placeholder until a proper death sequence or procedure is created. probably just does this and then reloads the level after a while.
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.8f);
        isJumping = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) //couldn't get this to work as a regular collision, instead there is a tag around a box collider, simulating a hitbox.
        {
            playerState.instance.health -= 1;
            Debug.Log("Player collided with an obstacle!");
            Debug.Log(playerState.instance.health);
            playerVelocity *= -0.9f; //reverses the player's motion to move them out the way of the obstacle
        }
    }

}
