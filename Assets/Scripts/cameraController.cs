using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offset;
    private float xRotation;
    private float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 rotationChange = new Vector3(xRotation, yRotation, 0);
        transform.position = player.transform.position + offset + rotationChange;
    }

    void OnLook(InputValue value)
    {
        Vector2 lookAngle = value.Get<Vector2>();
        xRotation = lookAngle.x;
        yRotation = lookAngle.y;
        
    }

}
