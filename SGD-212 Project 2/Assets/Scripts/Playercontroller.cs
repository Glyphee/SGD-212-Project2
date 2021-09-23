using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] private string[] slimearray = new string[] { null, null, null};
    [SerializeField] private GameObject playerGO;
    [SerializeField] private int levelID = 1;
    [SerializeField] private string slimeInHand = null;
    [SerializeField] private GameObject normSlime;
    [SerializeField] private GameObject spikeSlime;
    [SerializeField] private GameObject expandSlime;
    public SlimeController slimecontroller0;
    public SlimeController slimecontroller1;
    public SlimeController slimecontroller2;

    private Vector3 mousePos;
    private Vector3 lookDirection;
    private float lookAngle;

    public CharacterController characterController;
    [SerializeField] private float speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    public GameObject slime0;
    public GameObject slime1;
    public GameObject slime2;
    

    void Start()
    {
        if (levelID == 1)
        {
            slimearray[0] = "norm";
            slimecontroller0.slimeType = slimearray[0];
            slimearray[1] = "norm";
            slimecontroller1.slimeType = slimearray[1];
            slimearray[2] = "norm";
            slimecontroller2.slimeType = slimearray[2];
        }
    }

    void Update()
    {
        //Move the player
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);
        
        //Following the mouse
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        lookDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        playerGO.transform.rotation = Quaternion.Euler(0f, lookAngle + 90f, 0f);

        //Cycles the slimes when the player presses space
        if (Input.GetKeyDown("space") == true)
        {
            CycleSlimes();
        }
    }

    void CycleSlimes()
    {
        string temp;
        temp = slimeInHand;
        slimeInHand = slimearray[0];
        switch (slimeInHand)
        {
            case "norm":
                normSlime.SetActive(true);
                spikeSlime.SetActive(false);
                expandSlime.SetActive(false);
                break;
            case "spike":
                normSlime.SetActive(false);
                spikeSlime.SetActive(true);
                expandSlime.SetActive(false);
                break;
            case "expand":
                normSlime.SetActive(false);
                spikeSlime.SetActive(false);
                expandSlime.SetActive(true);
                break;
            case null:
                normSlime.SetActive(false);
                spikeSlime.SetActive(false);
                expandSlime.SetActive(false);
                break;
        }
        slimearray[0] = slimearray[1];
        slimecontroller0.slimeType = slimearray[1];
        slimearray[1] = slimearray[2];
        slimecontroller1.slimeType = slimearray[2];
        slimearray[2] = temp;
        slimecontroller2.slimeType = temp;
    }
}
