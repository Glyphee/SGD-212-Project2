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

    public PersistentController persCont = new PersistentController();
    public static levelTransport_Script J;

    AudioManager audioMan;

    void Start()
    {
        J = this;
        audioMan = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks the slimeList[] on the player and adds one the repective PersistentController value
        for (int i = 0; i <= 2; i++)
        {
            if (playerGO.GetComponent<Playercontroller>().slimeList[i].gameObject.tag == "absorb")
            {
                persCont.GetComponent<PersistentController>().absorbCount++;
            }
            else if (playerGO.GetComponent<Playercontroller>().slimeList[i].gameObject.tag == "spike")
            {
                persCont.GetComponent<PersistentController>().spikeCount++;
            }
            else if (playerGO.GetComponent<Playercontroller>().slimeList[i].gameObject.tag == "crush")
            {
                persCont.GetComponent<PersistentController>().crushCount++;
            }
        }

        StartCoroutine(AnimTransit());

        persCont.levelChanged = true;
    }

    public IEnumerator AnimTransit()
    {
        levelCompleteImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        levelCompleteImage.gameObject.SetActive(false);

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
}
