using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GAME_STATE { READY, RUNNING, PAUSED, FINISHED };
    private GAME_STATE gameState;
    private float countdownTime = 3f;
    private float gameTime = 0f;
    private Timer timerComponent;
    private Countdown countdownComponent;
    private GAME_STATE gameStateOnUnpause;


    // Start is called before the first frame update
    void Start()
    {
        timerComponent = GetComponent<Timer>();
        countdownComponent = GetComponent<Countdown>();
        MoveToNewState(GAME_STATE.READY);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GAME_STATE.READY)
        {
            countdownTime -= Time.deltaTime;
            countdownComponent.UpdateCountdownUI(countdownTime);

            if(countdownTime <= 0)
            {
                countdownTime = 0;
                MoveToNewState(GAME_STATE.RUNNING);
            }
        }
        else if (gameState == GAME_STATE.RUNNING)
        {
            gameTime += Time.deltaTime;
            timerComponent.UpdateTimerUI(gameTime);
        }
    }

    public void PauseGame()
    {
        if (!(gameState == GAME_STATE.PAUSED))
        {
            Time.timeScale = 0;
            MoveToNewState(GAME_STATE.PAUSED);
        } else
        {
            Time.timeScale = 1;
            MoveToNewState(gameStateOnUnpause);
        }
    }

    private void MoveToNewState(GAME_STATE newState)
    {
        if(newState == GAME_STATE.READY)
        {
            timerComponent.SetUIEnabled(false);
            countdownComponent.SetUIEnabled(true);
            gameState = GAME_STATE.READY;
        } else if(newState == GAME_STATE.RUNNING)
        {
            timerComponent.SetUIEnabled(true);
            countdownComponent.SetUIEnabled(false);
            gameState = GAME_STATE.RUNNING;
        } else if(newState == GAME_STATE.PAUSED)
        {
            gameStateOnUnpause = gameState;
            gameState = GAME_STATE.PAUSED;
        }
    }
}
