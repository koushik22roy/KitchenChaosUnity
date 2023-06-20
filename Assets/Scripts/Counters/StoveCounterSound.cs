using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private float warningSoundTimer;
    private bool warningSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(IHasProgress.OnProgressChangedEvent obj)
    {
        float burnShowProgressAmount = .5f;
        warningSound = stoveCounter.IsFried() && obj.progressNormalized >= burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(StoveCounter.STATE obj)
    {
        bool playSound = obj == StoveCounter.STATE.FRYING || obj == StoveCounter.STATE.FRIED;
        Debug.Log("OBJ:  " + obj);

        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void Update()
    {
        if (warningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
