using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    //Singleton
    [HideInInspector]
    public static Game Instance;


    //Game Variables
    public bool running = false;
    public bool levelCompleted = false;

    public System.Random rand;
    
    public Camera mainCam;

    //GameObjects
    public GameObject NPCHolder;
    public GameObject pauseMenu;
    public GameObject dialogePrefab;

    //Jumps
    public int jumpsAllowed;
    private int currentJumps = 0;
    [SerializeField]
    private JumpUIController jumpUI;



    private void Awake()
    {
        //Singleton Pattern
        if (Game.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Game.Instance = this;
        }


        mainCam = Camera.main;
        rand = new System.Random((int)System.DateTime.Now.TimeOfDay.TotalSeconds);

    }

    internal void resetJumps()
    {
        currentJumps = 0;
        //Update UI
        jumpUI.updateJumps(currentJumps);
        AudioController.Instance.PlaySound(SoundFX.ALTAR);
    }

    public void CompleteLevel()
    {
        Debug.Log("Level Complete");
        running = false;
        levelCompleted = true;
        Invoke("loadNextLevel", 1);
        AudioController.Instance.PlaySound(SoundFX.WIN1);

    }

    private void loadNextLevel()
    {
        try
        {
            string nextLevelString = LevelManager.Instance.getNextLevel();
            Debug.Log("Loading " + nextLevelString);
            SceneManager.LoadScene(nextLevelString);
        }
        catch (Exception)
        {
            Debug.Log("No more levels");
            endCutscene();
        }
    }

    public void pausedPressed()
    {
        if (!pauseMenu.active)
        {
            pauseMenu.SetActive(true);
            running = false;
            AudioController.Instance.PlaySound(SoundFX.CLICK);
        }
        else
        {
            AudioController.Instance.PlaySound(SoundFX.CLICK);
            pauseMenu.SetActive(false);
            running = true;
        }
    }

    public void resumePressed()
    {
        AudioController.Instance.PlaySound(SoundFX.CLICK);
        pauseMenu.SetActive(false);
        running = true;
    }

    public void levelPressed()
    {
        AudioController.Instance.PlaySound(SoundFX.CLICK);
        SceneManager.LoadScene("LevelSelect");

    }

    public void quitPressed()
    {
        AudioController.Instance.PlaySound(SoundFX.CLICK);
        SceneManager.LoadScene("MainMenu");
    }


    public void endCutscene()
    {
        mainCam.GetComponent<UnstableCamera>().deathShake();
        AudioController.Instance.PlaySound(SoundFX.EXP1);
        Invoke("quitPressed", 4);
    }








    public IEnumerator DeathWaitReset(float time)
    {
        if (levelCompleted)
        {
            yield break;
        }
        Debug.Log("Waiting");
        yield return new WaitForSeconds(time);
        if (!levelCompleted)
        {
            Game.Instance.reloadCurrentLevel();
        }
    }

    public void reloadCurrentLevel()
    {
        Debug.Log("Level Reloaded");
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void landedOnGround()
    {

        if (jumpUI.gameObject.active)
        {
            currentJumps++;
            if (currentJumps == jumpsAllowed)
            {
                //currentJumps = 1;
                //Trigger Rumble
                Debug.Log("RUMBLE");
                running = false;
                mainCam.GetComponent<UnstableCamera>().deathShake();
                AudioController.Instance.PlaySound(SoundFX.EXP1);
                StartCoroutine(DeathWaitReset(4));
            }
            else
            {
                AudioController.Instance.PlaySound(SoundFX.EXP2);
                mainCam.GetComponent<UnstableCamera>().cameraShake();
            }

            //Update UI
            jumpUI.updateJumps(currentJumps);
        }
        

    }

    public void alertNPCDeath(Vector2 deathLocation)
    {
        if (NPCHolder != null)
        {
            foreach (Transform npc in NPCHolder.transform)
            {
                npc.GetComponent<NPC>().ReactToDealth(deathLocation);
            }
        }
    }

    public void alertPlayerJump(Vector2 jumpLocation)
    {
        if (NPCHolder != null)
        {
            foreach (Transform npc in NPCHolder.transform)
            {
                npc.GetComponent<NPC>().ReactToJump(jumpLocation);
            }
        }
    }

    public void startLevel()
    {
        Debug.Log("Level Started");
        running = true;
    }
}
