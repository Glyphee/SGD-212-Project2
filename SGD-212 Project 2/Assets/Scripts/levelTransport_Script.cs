using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTransport_Script : MonoBehaviour
{
    public string levelName;
    public Animator circleTransitionController;
    //public Playercontroller pcont = new Playercontroller();

    public static levelTransport_Script J;

    AudioManager audioMan;

    void Start()
    {
        J = this;
        audioMan = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //DontDestroyOnLoad(pcont.playerParent);
        //DontDestroyOnLoad(pcont.slimeList[0]);
        //DontDestroyOnLoad(pcont.slimeList[1]);
        //DontDestroyOnLoad(pcont.slimeList[2]);
        StartCoroutine(AnimTransit());
    }

    public IEnumerator AnimTransit()
    {
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
        // print("Que imaginary transportation to " + levelName);
    }
}
