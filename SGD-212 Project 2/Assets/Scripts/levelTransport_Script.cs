using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTransport_Script : MonoBehaviour
{
    public string levelName;
    public Animator circleTransitionController;

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
        circleTransitionController.SetTrigger("exitCircleTrigger");
        audioMan.Play("Walk Transition");

        yield return new WaitForSeconds(1);

        Debug.Log("Player has entered levelTransport. Transporting to: " + levelName);
        SceneManager.LoadScene(levelName);
        // print("Que imaginary transportation to " + levelName);
    }
}
