using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject pause_panel;
    public GameObject fuel_progress_bar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pause_panel.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pause_panel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else if (pause_panel.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            pause_panel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else if (fuel_progress_bar.transform.localScale.x <= 0)
        {
            pause_panel.SetActive(true);
            isPaused = true;
        }
    }

    public void PauseButtonOn()
    {
        pause_panel.SetActive(true);
        Time.timeScale = 0;
        isPaused=true;
    }

    public void ContinueButtonOn()
    {
        pause_panel.SetActive(false);
        Time.timeScale = 1;
        isPaused=false;
    }

    public void GoToMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Classic");
        
    }

}
