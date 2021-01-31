using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image blackoutPanel;
    public Text storyText1;
    public Text storyText2;
    public Text storyText3;
    public Text storyText4;
    public enum GAME_STATE { READY, RUNNING, FINISHED };
    private GAME_STATE gameState;
    private float countdownTime = 0f;
    private float gameTime = 0f;
    public float GameTime
    {
        get { return gameTime;  }
    }
    private Timer timerComponent;


    // Start is called before the first frame update
    void Start()
    {
        timerComponent = GetComponent<Timer>();
        MoveToNewState(GAME_STATE.READY);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GAME_STATE.READY)
        {
            countdownTime += Time.deltaTime;
            if (countdownTime < 3.5f)
            {
                storyText1.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, countdownTime) * 2f);
                storyText2.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, (countdownTime - 0.75f) * 2f));
                storyText3.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, (countdownTime - 1.5f) * 1.5f));
                storyText4.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, (countdownTime - 2.5f) * 1.5f));
            }
            else
            {
                blackoutPanel.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, countdownTime - 3.5f));
                storyText1.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, countdownTime - 3.5f));
                storyText2.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, countdownTime - 3.5f));
                storyText3.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, countdownTime - 3.5f));
                storyText4.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, countdownTime - 3.5f));
            }

            if (countdownTime >= 4.5f)
            {
                countdownTime = 4.5f;
                MoveToNewState(GAME_STATE.RUNNING);
            }
        }
        else if (gameState == GAME_STATE.RUNNING)
        {
            gameTime += Time.deltaTime;
            timerComponent.UpdateTimerUI(gameTime);
        }
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
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
            Cursor.visible = false;
            timerComponent.SetUIEnabled(false);
            blackoutPanel.enabled = true;
            gameState = GAME_STATE.READY;
        } else if(newState == GAME_STATE.RUNNING)
        {
            Cursor.visible = false;
            timerComponent.SetUIEnabled(true);
            blackoutPanel.enabled = false;
            storyText1.enabled = false;
            storyText2.enabled = false;
            storyText3.enabled = false;
            gameState = GAME_STATE.RUNNING;
        } else if(newState == GAME_STATE.FINISHED)
        {
            Cursor.visible = true;
            gameState = GAME_STATE.FINISHED;
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine("PauseBeforeChangingScene");
        }
    }

    private IEnumerator PauseBeforeChangingScene()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(2);
    }
}