using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour
{
    public string[] objectScript;
    // public string startingDialogue;
    // public string currentLevel;

    private int dialogueLength;

    // Level_1: "Uh oh, seems like I manged to get myself lost in a dungeon. I need to get out. Huh? Is that... a slime?"
    // Level_2: "Whew, one step closer to the exit. Let's go, my slime friends!"
    // Level_3: "There's more rubble in the area, but that means the exit is close!"

    private void StartDialogue()
    {
        StartCoroutine(MessageScript.J.MultipleMessages(objectScript));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // StartCoroutine(MultiDialogue());
            StartDialogue();
            //Debug.Log("This dialogue object turned off");
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
