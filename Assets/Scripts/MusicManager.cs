using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{


    public AudioSource source;
    public static MusicManager Instance;
    public AudioClip mainTheme;

    void Awake()
    {
        if (MusicManager.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            MusicManager.Instance = this;
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }


    private void Start()
    {
        source.clip = mainTheme;
        source.Play();
    }

}
