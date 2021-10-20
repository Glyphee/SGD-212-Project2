using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] GameObject pauseMenuGO;

    AudioManager audioMan;

    private void Start()
    {
        pauseMenuGO.SetActive(false);
        audioMan = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        audioMan.Play("Click");
        pauseMenuGO.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        audioMan.Play("Click2");
        pauseMenuGO.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OnMenuClick()
    {
        audioMan.Play("Click2");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Frontend");
    }
    public void OnQuitButtonClick()
    {
        audioMan.Play("Click2");
        Application.Quit();
    }
}
