using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject key1, key2, key3;
    [SerializeField] GameObject heart0, heart1, heart2, heart3;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject pauseMenuUI;


    private bool isPaused = false;

    [SerializeField] float startTime = 300f;
    private float remainingTime;
    private bool isRunning = false;

    public static GameManager instance;
    public int health;
    public int score = 0;
    public int coins = 0;
    public int keys = 0;
    private bool isGameOver = false;
    private bool isGameWon = false;

    [SerializeField] GameObject Enddoor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 4;
        heart0.gameObject.SetActive(true);
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);

        remainingTime = startTime;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {

        if (isRunning)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isRunning = false;
                TimerEnded();
            }

            UpdateTimerDisplay();
        }

        scoreText.text = score.ToString();
        coinText.text = coins.ToString();

        switch (health)
        {
            case 4:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 3:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 2:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart0.gameObject.SetActive(true);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart0.gameObject.SetActive(false);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            //default:
            //    heart0.gameObject.SetActive(false);
            //    heart1.gameObject.SetActive(false);
            //    heart2.gameObject.SetActive(false);
            //    heart3.gameObject.SetActive(false);
            //    break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (health <= 0 && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            //Additional game over logic here
        }
        if (keys == 3 && !isGameWon)
        {
            isGameWon = true;
            Debug.Log("You Win!");
            // Additional game win logic here
        }

    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show pause UI
        Time.timeScale = 0f;         // Freeze time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide pause UI
        Time.timeScale = 1f;          // Resume time
        isPaused = false;
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        remainingTime = startTime;
        UpdateTimerDisplay();
    }

    void TimerEnded()
    {
            
    }
}
