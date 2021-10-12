using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour
{
    public static MessageScript J;

    public GameObject messagePanel;
    public Text messageText;
    public GameObject promptPanel;
    public int messageTime;

    void Start()
    {
        J = this; // defines public static as this object
    }

    public void StartMessage(string desiredMessage1) // drags string from where this is called -> desiredMessage2 in SummonMessageBubble()
    {
        StartCoroutine(SummonMessageBubble(desiredMessage1));
        
    }

    private IEnumerator SummonMessageBubble(string desiredMessage2) // Call from anywhere and set perameters to summon the messagePanel w/text, then off after 3 seconds
    {
        Debug.Log("message bubble on");
        messagePanel.SetActive(true);
        messageText.text = desiredMessage2;

        yield return new WaitForSeconds(messageTime);

        messageText.text = "Temp Text";
        messagePanel.SetActive(false);
        Debug.Log("message bubble off");
    }

    public IEnumerator MultipleMessages(string[] tempArray) // Similar to above but for multiple messages
    {
        TurnOnBubble();

        for(int n = 0; n < tempArray.Length; n++)
        {
            messageText.text = tempArray[n];
            yield return new WaitForSeconds(messageTime);
        }

        TurnOffBubble();
    }

    public void TurnOnBubble()
    {
        Debug.Log("message bubble on");
        messagePanel.SetActive(true);
    }

    public void TurnOffBubble()
    {
        Debug.Log("message bubble off");
        messagePanel.SetActive(false);
    }

    public void SummonPrompt() // turns on promptPanel
    {
        promptPanel.gameObject.SetActive(true);

        Debug.Log("turned on prompt panel");
    }
    public void DismissPrompt() // turns off promptPanel
    {
        promptPanel.gameObject.SetActive(false);

        Debug.Log("turned off prompt panel");
    }
}
