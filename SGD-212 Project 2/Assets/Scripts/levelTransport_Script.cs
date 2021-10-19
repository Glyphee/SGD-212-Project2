using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelTransport_Script : MonoBehaviour
{
    public string levelName;
    public Animator circleTransitionController;
    public GameObject playerGO;
    public Image levelCompleteImage;

    public static levelTransport_Script J;

    AudioManager audioMan;

    void Start()
    {
        J = this;
        audioMan = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(AnimTransit());
    }

    public IEnumerator AnimTransit()
    {
        levelCompleteImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        // levelCompleteImage.gameObject.SetActive(false);

        circleTransitionController.SetTrigger("exitCircleTrigger");
        audioMan.Play("Walk Transition");

        float offset = 1 - AudioListener.volume;
        for (float vol = AudioListener.volume; vol >= 0; vol -= 0.1f) //lowers the volume for transition
        {
            AudioListener.volume = vol;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(offset); //always has a second to transition

        Debug.Log("Player has entered levelTransport. Transporting to: " + levelName);
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator RegularTransit(string temp)
    {
        circleTransitionController.SetTrigger("exitCircleTrigger");

        float offset = 1 - AudioListener.volume;
        for (float vol = AudioListener.volume; vol >= 0; vol -= 0.1f) //lowers the volume for transition
        {
            AudioListener.volume = vol;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(offset); //always has a second to transition

        SceneManager.LoadScene(temp);
    }
}
