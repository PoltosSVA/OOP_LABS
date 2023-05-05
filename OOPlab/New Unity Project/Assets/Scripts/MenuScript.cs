using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{

    public GameObject levelChanger;
    public GameObject exitPanel;
    private void Update()
    {
        if (levelChanger.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            levelChanger.SetActive(false);
        }
        else if(exitPanel.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        { 
            exitPanel.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            exitPanel.SetActive(false);
        }
    }

    public void OnClickStart()
    {
        levelChanger.SetActive(true);
    }

    public void OnClickExit()
    {
        exitPanel.SetActive(true);
    }
    public void OnClickExitYes()
    {
        Application.Quit();
    }

    public void OnClickExitNo()
    {
        exitPanel.SetActive(false);
    }

    public void LevelButtons(string level)
    {
        SceneManager.LoadScene(level);
    }
}
