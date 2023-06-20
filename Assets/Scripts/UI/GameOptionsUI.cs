using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameOptionsUI : MonoBehaviour
{
    public static GameOptionsUI Instance { get; private set; }

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Button closeButton;


    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        soundSlider.onValueChanged.AddListener(OnSliderSoundValueChanged);
        musicSlider.onValueChanged.AddListener(OnSliderMusicValueChanged);

        closeButton.onClick.AddListener(()=> { Hide(); onCloseButtonAction(); });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += Instance_OnGamePaused;
        Hide();
    }

    private void OnEnable()
    {
        soundSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();
    }

    private void Instance_OnGamePaused()
    {
        Hide();
    }

    private void OnSliderSoundValueChanged(float value)
    {
        SoundManager.Instance.ChangeVolume(value);
    }

    private void OnSliderMusicValueChanged(float val)
    {
        MusicManager.Instance.ChangeVolume(val);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
