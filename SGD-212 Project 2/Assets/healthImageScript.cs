using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthImageScript : MonoBehaviour
{
    public static healthImageScript instance;

    public Image[] healthImages;
    private Image currentImage;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentImage = healthImages[0];
    }

    public void ChangeHealthImage(int temp)
    {
        currentImage.gameObject.SetActive(false);
        healthImages[temp].gameObject.SetActive(true);
        currentImage = healthImages[temp];
    }
}
