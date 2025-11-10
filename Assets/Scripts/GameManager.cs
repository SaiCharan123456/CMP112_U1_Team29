using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int health = 3;
    public int score = 0;
    public int coins = 0;
    public int keys = 0;
    private bool isGameOver = false;
    private bool isGameWon = false;

    [SerializeField] GameObject Enddoor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0 && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            // Additional game over logic here
        }
        if (keys == 3 && !isGameWon)
        {
            isGameWon = true;
            Debug.Log("You Win!");
            // Additional game win logic here
        }

    }

}
