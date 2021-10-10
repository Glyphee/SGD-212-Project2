using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTransport_Script : MonoBehaviour
{
    public string levelName;
    public Animator circleTransitionController;
    public GameObject playerGO;
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
        circleTransitionController.SetTrigger("exitCircleTrigger");
        audioMan.Play("Walk Transition");

        yield return new WaitForSeconds(1);

        Debug.Log("Player has entered levelTransport. Transporting to: " + levelName);
        SceneManager.LoadScene(levelName);
        // print("Que imaginary transportation to " + levelName);
    }
}
