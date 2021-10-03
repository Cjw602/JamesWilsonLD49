using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int levelNumber = 1;

    public string[] levelStrings;


    private void Awake()
    {
        //Singleton Pattern
        if (LevelManager.Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            LevelManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }


    public string getNextLevel()
    {
        levelNumber++;
        if (levelNumber <= levelStrings.Length - 1)
        {
            return levelStrings[levelNumber];
        }
        else
        {
            throw new Exception("Maximum level reached["+levelNumber+"]");
        }
    }



}
