using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject helpPanel;
    bool creditPanelOn = false;
    bool helpPanelOn = false;

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("level01_meta");
    }

    public void OnCreditButtonClick()
    {
        if (creditPanelOn == false)
        {
            creditPanel.SetActive(true);
            creditPanelOn = true;
        }
        else if (creditPanelOn == true)
        {
            creditPanel.SetActive(false);
            creditPanelOn = false;
        }
        else
        {
            print("what?");
        }
    }

    public void OnHelpButtonClick()
    {
        if (helpPanelOn == false)
        {
            helpPanel.SetActive(true);
            helpPanelOn = true;
        }
        else if (helpPanelOn == true)
        {
            helpPanel.SetActive(false);
            helpPanelOn = false;
        }
        else
        {
            print("what?");
        }
    }
}
