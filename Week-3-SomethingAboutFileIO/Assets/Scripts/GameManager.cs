using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int score = 0;
    public int targetScore = 3;

    // Keys
    private const string DATA_DIR = "/Data/";
    private const string DATA_REC_FILE = "record.txt";

    private string DATA_FULL_REC_FILE_PATH;
    
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
            // - Why use High not high ?? -
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
            // if the record exist, pull the data
            // - Why not coding the "if not" part ?? -
            if (File.Exists(DATA_FULL_REC_FILE_PATH))
            {
                string fileContent = File.ReadAllText(DATA_FULL_REC_FILE_PATH);
                highScore = Int32.Parse(fileContent);
            }
            
            return highScore;
        }
        set
        {
            Debug.Log("New HighScore!");
            
            highScore = value;

            // if the directory path is not existing, create one
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }
            
            // Write the Highscore to a file
            // - Why high not High ?? -
            string fileContent = "" + highScore;
            File.WriteAllText(DATA_FULL_REC_FILE_PATH, fileContent);
        }
    }
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endText;

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
        // Set up full path
        DATA_FULL_REC_FILE_PATH = Application.dataPath + DATA_DIR + DATA_REC_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(score);
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            // Unseeing the end scene texts
            endText.enabled = false;
            
            scoreText.text = "Score: " + Score + "\n" + 
                             "\nThe firefighting champion put out " + HighScore + " fires!";
        }
        else
        {
            endText.enabled = true;
            scoreText.enabled = false;
            
            // Unseeing the normal score text
            
            
            if (Score >= HighScore)
            {
                endText.text = "You put out " + Score + " fire" + "\n" + 
                               "\nYou're the firefighting champion!!";
            }
            else
            {
                endText.text = "You put out " + Score + " fire" + "\n" + 
                               "\nThe firefighting champion put out " + HighScore + " fires!";
            }
        }

        if (score == targetScore)
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            targetScore = Mathf.RoundToInt(targetScore + targetScore * 1.5f);
            
            Debug.Log(targetScore);
        }
        
        // Press space key to delete high score record
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // This only delete the record file 
            // ... but not updating the game scene display
            File.Delete(DATA_FULL_REC_FILE_PATH);
        }
    }
}
