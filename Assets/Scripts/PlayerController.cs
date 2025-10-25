using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float speed = 5;
    [SerializeField] float playerSpeed;
    [SerializeField] float gravityValue;
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] bool isJumping;

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravityValue = -20;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            isJumping = true;
            playerVelocity.y += 20;
            
            StartCoroutine(ResetJump());
        }



        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.8f);
        isJumping = false;
    }

    void FixedUpdate()
    {

        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movementX, 0.0f, movementY) * speed * Time.deltaTime;

        transform.position -= movement;

    }
}
