using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    private SoundManager() { }
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("SoundManager");
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private int audioSourcePoolSize = 10;
    private List<AudioSource> audioSourcePool;

    [Header("All Sounds")]
    [Header("Building Placement")]
    [SerializeField] private List<AudioClip> pickUpBuilding;
    [SerializeField] private List<AudioClip> pickUpRumble;
    [SerializeField] private List<AudioClip> placeBuilding;
    [SerializeField] private List<AudioClip> rotateBuilding;
    [Header("Enemy Sounds")]
    [SerializeField] private List<AudioClip> enemySpawn;
    [SerializeField] private List<AudioClip> enemyDeath;
    [Header("Towers")]
    [SerializeField] private List<AudioClip> shotAoe;
    [SerializeField] private List<AudioClip> shotFireRate;
    [SerializeField] private List<AudioClip> shotBallista;
    [Header("UI")]
    [SerializeField] private List<AudioClip> buttonClick;
    [SerializeField] private List<AudioClip> trashCan;

    private void Awake()
    {
        InitializeAudioSourcePool();
    }

    private void InitializeAudioSourcePool()
    {
        audioSourcePool = new List<AudioSource>();

        for (int i = 0; i < audioSourcePoolSize; i++)
        {
            GameObject audioSourceObj = new GameObject("AudioSource_" + i);
            audioSourceObj.transform.SetParent(transform);
            AudioSource audioSource = audioSourceObj.AddComponent<AudioSource>();
            audioSourcePool.Add(audioSource);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        GameObject audioSourceObj = new GameObject("AudioSource_" + audioSourcePool.Count);
        audioSourceObj.transform.SetParent(transform);
        AudioSource newAudioSource = audioSourceObj.AddComponent<AudioSource>();
        audioSourcePool.Add(newAudioSource);
        return newAudioSource;
    }

    private void PlayRandomSoundFromList(List<AudioClip> soundList)
    {
        if (soundList.Count > 0)
        {
            AudioSource audioSource = GetAvailableAudioSource();
            audioSource.clip = soundList[Random.Range(0, soundList.Count)];
            audioSource.Play();
        }
    }

    #region Placement
    public void PlayPickUp()
    {
        PlayRandomSoundFromList(pickUpBuilding);
    }
    public void PlayPickUpRumble()
    {
        PlayRandomSoundFromList(pickUpRumble);
    }
    public void PlayPlaceBuilding()
    {
        PlayRandomSoundFromList(placeBuilding);
    }
    public void PlayRotateBuilding()
    {
        PlayRandomSoundFromList(rotateBuilding);
    }
    #endregion

    #region Enemies
    public void PlayEnemySpawn()
    {
        PlayRandomSoundFromList(enemySpawn);
    }
    public void PlayEnemyDeath()
    {
        PlayRandomSoundFromList(enemyDeath);
    }
    #endregion

    #region Towers
    public void PlayShotAoe()
    {
        PlayRandomSoundFromList(shotAoe);
    }
    public void PlayShotFireRate()
    {
        PlayRandomSoundFromList(shotFireRate);
    }
    public void PlayShotBallista()
    {
        PlayRandomSoundFromList(shotBallista);
    }
    #endregion

    #region UI
    public void PlayButtonClick()
    {
        PlayRandomSoundFromList(buttonClick);
    }
    public void PlayTrashCan()
    {
        PlayRandomSoundFromList(trashCan);
    }
    #endregion
}
