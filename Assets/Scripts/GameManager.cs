using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    // Trong Unity thì làm như này là phổ biến và phuf hợp với cách Unity quản lí đối tượng.
    
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
    
    

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // Không bị phá huỷ khi chuyển scene
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        NewGame(); 
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        if (world == 1 && stage == 10)
        {
            LoadLevel(world + 1, 1);
        }
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;
        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        
        NewGame();
        //SceneManager.LoadScene($"{world}_GameOver");
    }

    public void AddCoin()
    {
        coins += 1;
        if (coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }
    public void AddLife()
    {
        lives += 1;
    }
}
