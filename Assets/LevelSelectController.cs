using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    

    public void backPressed()
    {
        AudioController.Instance.PlaySound(SoundFX.CLICK);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


    public void loadLevel(int levelNumber)
    {

        AudioController.Instance.PlaySound(SoundFX.CLICK);
        string levelToLoad = LevelManager.Instance.levelStrings[levelNumber];
        if (levelToLoad != null)
        {
            LevelManager.Instance.levelNumber = levelNumber;
            Debug.Log("Loading: " + levelToLoad);
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
        }
    }




}
