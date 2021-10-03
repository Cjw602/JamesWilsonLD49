using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public void PlayClicked()
    {
        AudioController.Instance.PlaySound(SoundFX.CLICK);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void levelsClicked()
    {
        AudioController.Instance.PlaySound(SoundFX.CLICK);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");

    }


}
