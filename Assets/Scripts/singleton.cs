using UnityEngine;

public class playerState : MonoBehaviour
{
    public static playerState instance;
    public int health = 10;
    public int score = 0;
    public int keysCollected = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
