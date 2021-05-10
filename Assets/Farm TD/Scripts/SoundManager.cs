using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource sfxAuidoSource;
    [SerializeField] private AudioSource backgroundAudioSource;
    public bool soundOn;
    public List<AudioClip> farmSFX;
    public List<AudioClip> shootSFX;
    public List<AudioClip> botKillSFX;
    public AudioClip sniperShotSFX;
    public AudioClip enemyShootSFX;
    public AudioClip enemyCloneSFX;
    public AudioClip coinPickSFX;
    public AudioClip healthPickSFX;
    public AudioClip tripleFirePickSFX;
    public AudioClip portalSFX;
    public AudioClip footstepSFX;
    public AudioClip buildingSFX;
    public AudioClip buildCompleteSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        soundOn = true;
        sfxAuidoSource = GetComponent<AudioSource>();
        //backgroundAudioSource =  GetComponent<AudioSource>();

        //if (backgroundAudioSource != null)
            //PlayMainMenuAudio();
    }
    public void PlaySFX(AudioClip audioClip)
    {
        sfxAuidoSource.PlayOneShot(audioClip);
    }

    public AudioClip GetRandomFarmSFX()
    {
        return farmSFX[Random.Range(0, farmSFX.Count)];
    }

    public AudioClip GetRandomShootSFX()
    {
        return shootSFX[Random.Range(0, shootSFX.Count)];
    }

    public AudioClip GetRandomKillSFX()
    {
        return botKillSFX[Random.Range(0, botKillSFX.Count)];
    }

    public void PlayBuildingSFX()
    {
        PlaySFX(buildingSFX);
    }

    public void PlayBuildingCompleteSFX()
    {
        PlaySFX(buildCompleteSFX);
    }
}
