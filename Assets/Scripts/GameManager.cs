using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int Health = 3;
    public static int Score = 0;
    public static int Coins = 0;
    public static int Keys = 0;
    private bool isGameOver = false;
    private bool isGameWon = false;

    [SerializeField] GameObject Enddoor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Health <= 0 && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            // Additional game over logic here
        }
        if (Keys == 3 && !isGameWon)
        {
            isGameWon = true;
            Debug.Log("You Win!");
            // Additional game win logic here
        }

    }
}
