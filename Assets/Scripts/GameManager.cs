using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action OnStateChanged;
    public Action OnGamePaused;
    public Action OnGameUnpaused;

    private enum STATE
    {
        WAITINGTOSTART,
        COUNTDOWNTOSTART,
        GAMEPLAYING,
        GAMEOVER,
    }

    [SerializeField] private STATE state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 1f;
    private float gamePlayTimer;
    [SerializeField] private float gamePlayTimerMax = 60f;
    private bool isGamePaused = false;



    private void Awake()
    {
        Instance = this;
        state = STATE.WAITINGTOSTART;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        // StartCoroutine(Tutorial_OnInteractAction());

        //debug trigger game start automatically
        state = STATE.COUNTDOWNTOSTART;
        OnStateChanged?.Invoke();
    }

    private IEnumerator Tutorial_OnInteractAction()
    {
        yield return new WaitForSeconds(3f);
        GameInput_OnInteractAction();
    }

    private void GameInput_OnInteractAction()
    {
        if (state == STATE.WAITINGTOSTART)
        {
            state = STATE.COUNTDOWNTOSTART;
            OnStateChanged?.Invoke();
        }
    }

    private void GameInput_OnPauseAction()
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case STATE.WAITINGTOSTART:
                // waitingToStartTimer -= Time.deltaTime;
                // if(waitingToStartTimer < 0f)
                // {
                //    state = STATE.COUNTDOWNTOSTART;
                //    OnStateChanged?.Invoke();
                // }
                break;
            case STATE.COUNTDOWNTOSTART:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = STATE.GAMEPLAYING;
                    gamePlayTimer = gamePlayTimerMax;
                    OnStateChanged?.Invoke();
                }
                break;
            case STATE.GAMEPLAYING:
                gamePlayTimer -= Time.deltaTime;
                if (gamePlayTimer < 0f)
                {
                    state = STATE.GAMEOVER;
                    OnStateChanged?.Invoke();
                }
                break;
            case STATE.GAMEOVER:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == STATE.GAMEPLAYING;
    }

    public bool IsCountDownToStartActive()
    {
        return state == STATE.COUNTDOWNTOSTART;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == STATE.GAMEOVER;
    }

    public float GetGamePlayTimerNormalized()
    {
        return 1 - (gamePlayTimer / gamePlayTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }
}
