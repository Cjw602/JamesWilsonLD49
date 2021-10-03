using UnityEngine;
public enum SoundFX
{
    EXP1,
    EXP2,
    HURT1,
    JUMP1,
    JUMP2,
    LOSE1,
    WIN1,
    ALTAR,
    CLICK,
    SHUFFLE,
    TALKING
}

public class AudioController : MonoBehaviour
{
    public AudioClip Explosion1;
    public AudioClip Explosion2;
    public AudioClip Hurt1;
    public AudioClip Jump1;
    public AudioClip Jump2;
    public AudioClip Lose1;
    public AudioClip Win1;
    public AudioClip altar;
    public AudioClip click;
    public AudioClip shuffle;
    public AudioClip talking;





    public AudioSource source;
    public static AudioController Instance;

    void Awake()
    {
        if (AudioController.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            AudioController.Instance = this;
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }



    private void Start()
    {

    }

    public void PlaySound(SoundFX sound)
    {
        switch (sound)
        {
            case SoundFX.EXP1:
                source.PlayOneShot(Explosion1);
                break;
            case SoundFX.EXP2:
                source.PlayOneShot(Explosion2);
                break;
            case SoundFX.HURT1:
                source.PlayOneShot(Hurt1);
                break;
            case SoundFX.JUMP1:
                source.PlayOneShot(Jump1);
                break;
            case SoundFX.JUMP2:
                source.PlayOneShot(Jump2);
                break;
            case SoundFX.LOSE1:
                source.PlayOneShot(Lose1);
                break;
            case SoundFX.WIN1:
                source.PlayOneShot(Win1);
                break;
            case SoundFX.ALTAR:
                source.PlayOneShot(altar);
                break;
            case SoundFX.CLICK:
                source.PlayOneShot(click);
                break;
            case SoundFX.SHUFFLE:
                source.PlayOneShot(shuffle);
                break;
            case SoundFX.TALKING:
                source.PlayOneShot(talking);
                break;
            default:
                break;
        }
    }
}