using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTransport_Script : MonoBehaviour
{
    public string levelName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has entered levelTransport. Transporting to: " + levelName);

        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(levelName);
        }
    }
}