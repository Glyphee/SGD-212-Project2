using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Logic for the main menu but can be used for general use, by Josiah
/// </summary>

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject helpPanel; //main help panel
    [SerializeField] GameObject[] helpPage; //multiple help panel pages
    [SerializeField] GameObject creditsPanel;

    bool isHelpToggle = false;
    bool isCreditsToggle = false;
    int currentHelpPage = 0;

    private void Start()
    {
        helpPanel.SetActive(false);

        foreach (GameObject page in helpPage)
        {
            page.SetActive(false);
        }

        helpPage[currentHelpPage].SetActive(true);

        creditsPanel.SetActive(false);
    }

    public void OnPlayButtonClick(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void OnHelpButtonClick() //toggles panel, prevents overlapping with credits panel
    {
        if (isCreditsToggle)
        {
            creditsPanel.SetActive(false);
            isCreditsToggle = false;
        }

        if (!isHelpToggle)
        {
            helpPanel.SetActive(true);
            isHelpToggle = true;           
        }
        else
        {
            helpPanel.SetActive(false);
            isHelpToggle = false;
        }
    }

    public void HelpButtonPageTurn(bool nextPage)
    {
        helpPage[currentHelpPage].SetActive(false);

        if (nextPage) //next page
        {
            if(currentHelpPage != helpPage.Length - 1)
            {                                
                currentHelpPage++;               
            }
            else //set back to the beginning
            {                
                currentHelpPage = 0;                               
            }
        }
        else //previous page
        {
            if(currentHelpPage != 0)
            {                              
                currentHelpPage--;                
            }
            else //set back to the end
            {                
                currentHelpPage = helpPage.Length - 1;                
            }
        }

        helpPage[currentHelpPage].SetActive(true);
    }

    public void OnCreditsButtonClick()
    {
        if (isHelpToggle)
        {
            helpPanel.SetActive(false);
            isHelpToggle = false;
        }

        if (!isCreditsToggle)
        {
            creditsPanel.SetActive(true);
            isCreditsToggle = true;
        }
        else
        {
            creditsPanel.SetActive(false);
            isCreditsToggle = false;
        }
    }


    public void OnQuitButtonClick()
    {
        Application.Quit();
        Debug.Log("Quit button pressed");
    }
}
