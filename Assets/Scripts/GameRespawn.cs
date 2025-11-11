using UnityEngine;

public class GameReswan : MonoBehaviour
{

    [SerializeField] Transform threshold;
    [SerializeField] Transform PlayerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -3)
        {
            transform.position = PlayerPosition.position;
            GameManager.Health = GameManager.Health - 1;
        }
    }
}
