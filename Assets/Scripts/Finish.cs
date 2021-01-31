using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public Text winTimeText;
    private GameObject gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager");
        Debug.Log(gm.ToString());

        if(gm)
        {
            var gmComp = gm.GetComponent<GameManager>();
            var minutes = Mathf.FloorToInt(gmComp.GameTime / 60);
            var seconds = Mathf.FloorToInt(gmComp.GameTime % 60);

            winTimeText.text = string.Format("You won in {0:00}:{1:00}!", minutes, seconds);
            Destroy(gm);
        }
    }

    public void OnPlayGameClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene(0);
    }
}
