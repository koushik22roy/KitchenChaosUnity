using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MUSIC";

    private AudioSource audioSource;
    private float volume;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .5f);
        audioSource.volume = volume;
    }

    public void ChangeVolume(float value)
    {
        //volume += .1f;
        //if (volume > 1f)
        //    volume = 0f;

        //audioSource.volume = volume;

        volume = value;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME,volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
