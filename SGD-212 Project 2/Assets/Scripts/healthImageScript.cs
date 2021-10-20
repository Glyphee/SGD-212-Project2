using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthImageScript : MonoBehaviour
{
    public static healthImageScript instance;

    public Image[] healthImages;
    private Image currentImage;

    public int playerHealth = 3;

    void Start()
    {
        instance = this;
        
        currentImage = healthImages[playerHealth]; // sets starting health image
        // Debug.Log("Current health image " + currentImage);
    }

    void Update()
    {
        CheckHealth();
    }

    public void ChangeHealthImage() // changes the health image to what ever the health is at that moment
    {                               // (element-0 = 0/3, element-1 = 1/3, element-2 = 2/3, element-3 = 3/3)
        if(playerHealth <= 0)
        {
            currentImage.gameObject.SetActive(false);
            healthImages[0].gameObject.SetActive(true);
        }
        else
        {
            currentImage.gameObject.SetActive(false);
            healthImages[playerHealth].gameObject.SetActive(true);
            currentImage = healthImages[playerHealth];
        }
        //Debug.Log("Current health image " + currentImage);
        // currentImage.gameObject.SetActive(false);
        // healthImages[playerHealth].gameObject.SetActive(true);
        // currentImage = healthImages[playerHealth];
    }

    public void CheckHealth() // checks to see when health hits 0 or under and starts player death sequence
    {
        if(playerHealth <= 0)
        {
            ChangeHealthImage();
            Debug.Log("Starting GameOver");
            StartCoroutine(Playercontroller.P.GameOver());
            this.gameObject.GetComponent<healthImageScript>().enabled = false;
        }
    }
}
