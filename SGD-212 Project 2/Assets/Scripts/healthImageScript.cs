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
        
        currentImage = healthImages[playerHealth];
        // Debug.Log("Current health image " + currentImage);
    }

    void Update()
    {
        CheckHealth();
    }

    public void ChangeHealthImage()
    {
        currentImage.gameObject.SetActive(false);
        healthImages[playerHealth].gameObject.SetActive(true);
        currentImage = healthImages[playerHealth];
        //Debug.Log("Current health image " + currentImage);
    }

    public void CheckHealth()
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
