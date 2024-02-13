using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int score = 0;
    public int targetScore = 3;

    // Property
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            Debug.Log("Score changed");
            
            score = value;
            
            // Update the high score
            // - Why use High not high-
            if (score > HighScore)
            {
                HighScore = score;
            }
        }
    }
    
    // High score
    public int highScore = 0;

    public int HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;
        }
    }
    
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(score);
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            scoreText.text = "Score: " + Score + "\n" + 
                             "\nThe firefighting champion put out " + HighScore + " fires!";
        }
        else
        {
            if (Score >= HighScore)
            {
                scoreText.text = "You put out " + Score + " fire" + "\n" + 
                                 "\nYou're the firefighting champion!!";
            }
            else
            {
                scoreText.text = "You put out " + Score + " fire" + "\n" + 
                                 "\nThe firefighting champion put out" + HighScore + " fires!";
            }
        }

        if (score == targetScore)
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            targetScore = Mathf.RoundToInt(targetScore + targetScore * 1.5f);
        }
    }
}
