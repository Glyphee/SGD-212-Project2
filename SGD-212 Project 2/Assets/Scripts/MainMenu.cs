using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Logic for the main menu but can be used for general use, by Josiah
/// </summary>

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject helpPanel; //main help panel
    [SerializeField] GameObject[] helpPage; //multiple help panel pages
    [SerializeField] GameObject creditsPanel;
    
    [SerializeField] Slider volumeSlider;
    [SerializeField] Image volumeIcon;
    [SerializeField] Sprite[] speakerIcons;

    bool isHelpToggle = false;
    bool isCreditsToggle = false;
    int currentHelpPage = 0;

    public Animator circleTransitionController;

    AudioManager audioMan;

    private void Start()
    {
        helpPanel.SetActive(false);

        foreach (GameObject page in helpPage)
        {
            page.SetActive(false);
        }

        helpPage[currentHelpPage].SetActive(true);

        creditsPanel.SetActive(false);
        audioMan = GetComponent<AudioManager>();
        audioMan.Play("BGM");

        if(PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1); //resets the volume to 1, comment this out if you want to keep player prefs
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }       
    }

    public void OnPlayButtonClick(string level)
    {
        // levelTransport_Script.J.StartButtonTransit();

        StartCoroutine(ButtonTransit(level));

        
    }

    private IEnumerator ButtonTransit(string level2)
    {
        circleTransitionController.SetTrigger("exitCircleTrigger");
        float offset = 1 - AudioListener.volume;
        for (float vol = AudioListener.volume; vol >= 0; vol -= 0.1f) //lowers the volume for transition
        {
            AudioListener.volume = vol;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(offset); //always has a second to transition

        SceneManager.LoadScene(level2);
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

    public void ChangeVolume() //changes volume to slider value
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);

        if(volumeSlider.value == 0)
        {
            volumeIcon.sprite = speakerIcons[1];
        }
        else
        {
            volumeIcon.sprite = speakerIcons[0];
        }
    }
    float volume;
    bool isMute = false;
    public void OnMuteButtonClick() //toggles mute button
    {
        if (!isMute)
        {
            volume = AudioListener.volume;
            AudioListener.volume = 0;
            volumeSlider.value = 0;
            isMute = true;
        }
        else
        {
            AudioListener.volume = volume;
            volumeSlider.value = volume;
            isMute = false;
        }
    }
}
