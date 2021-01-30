using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GAME_STATE { READY, RUNNING, PAUSED, FINISHED };
    private GAME_STATE gameState;
    private float countdownTime = 3f;
    private float gameTime = 0f;
    public float GameTime
    {
        get { return gameTime;  }
    }
    private Timer timerComponent;
    private Countdown countdownComponent;
    private GAME_STATE gameStateOnUnpause = GAME_STATE.READY;


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
        if (gameState == GAME_STATE.PAUSED)
        {
            Time.timeScale = 1;
            MoveToNewState(gameStateOnUnpause);
        } else
        {
            Time.timeScale = 0;
            MoveToNewState(GAME_STATE.PAUSED);
        }
    }

    public void CompleteGame()
    {
        MoveToNewState(GAME_STATE.FINISHED);
    }

    public bool IsGameRunning()
    {
        return gameState == GAME_STATE.RUNNING;
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
        } else if(newState == GAME_STATE.FINISHED)
        {
            gameState = GAME_STATE.FINISHED;
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine("PauseBeforeChangingScene");
        }
    }

    private IEnumerator PauseBeforeChangingScene()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene("Finish");
    }
}
