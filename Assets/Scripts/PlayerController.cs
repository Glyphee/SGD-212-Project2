using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //UI Management
    public float playerHealth = 100;
    public GameObject healthbarPanel;
    public GameObject sprintPanel;
    public GameObject gunPanel;
    public Sprite sprintOn;
    public Sprite sprintOff;
    public Sprite gun1;
    public Sprite gun2;
    public Sprite blank;
    public GameObject videoPlayer;

    //Player Appearance
    public GameObject gunSprite;
    public GameObject playerSprite;
    public Material playerUp;
    public Material playerDown;
    public Material playerLeft;
    public Material playerRight;
    public Material gun1mat;
    public Material gun2mat;
    public Material blankmat;

    //Movement
    CharacterController characterController;
    public float speed = 5.0f;
    Vector3 moveDirection = Vector3.zero;

    //Item Interaction
    public string handString = "";
    GameObject handGO = null;
    RaycastHit hit;
    bool hasDisk = false;

    //Weapont Interaction
    public GameObject bulletGO;
    public GameObject gunTip;
    Vector3 lookDirection;
    public GameObject gunParent;
    float lookAngle;
    Vector3 mousePos;

    //Audio
    public AudioSource nukeSound;
    public AudioSource musicSound;
    public AudioSource gunshotSound;
    public AudioSource hitSound;

    void Start()
    {
        //Movement
        characterController = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        //Regenerate Health
        if (playerHealth >= 100)
        {}
        else
        {
            playerHealth += 0.005f;
        }
        if (transform.position.y != 0.1f)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
    }

    void Update()
    {
        //Return to main menu
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("startScene");
        }

        //Set UI
        healthbarPanel.GetComponent<Image>().fillAmount = playerHealth * 0.01f;
        if (handString == "")
        {
            gunPanel.GetComponent<Image>().sprite = blank;
        }
        else if (handString == "gun1")
        {
            gunPanel.GetComponent<Image>().sprite = gun1;
        }
        else if (handString == "gun2")
        {
            gunPanel.GetComponent<Image>().sprite = gun2;
        }

        //Kill Player
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        //Movement
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
            sprintPanel.GetComponent<Image>().sprite = sprintOn;
        }
        else
        {
            sprintPanel.GetComponent<Image>().sprite = sprintOff;
            speed = 5f;
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);

        //Item Interaction
            //Pickup item
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    
                if (Physics.Raycast(ray, out hit))
                {
                    //Pickupable items
                    if (hit.transform.tag=="gun1"||hit.transform.tag=="gun2"||hit.transform.tag=="health1"||hit.transform.tag=="health2")
                    {
                        //Pick up if within distance and not holding anything
                        if (Vector3.Distance(transform.position, hit.transform.position) < 2)
                        {
                            if (handString == "")
                            {
                                handString = hit.transform.tag.ToString();
                                handGO = GameObject.FindGameObjectWithTag(hit.transform.tag);
                                hit.transform.position = new Vector3(100f,100f,100f);
                            }
                        }
                    }
                    else if (hit.transform.tag == "disk")
                    {
                        print("disk get");
                        hasDisk = true;
                        hit.transform.position = new Vector3(100f,100f,100f);
                    }
                    else if (hit.transform.tag == "nuke" && hasDisk == true)
                    {
                        hasDisk = false;
                        nukeSound.Play();
                        Invoke("PlayVideo", 10f);
                    }
                }
            }
            //Drop item
            if(handString != null && Input.GetKeyDown("f"))
            {
                handGO.transform.position = transform.position;
                handGO = null;
                handString = "";
            }
        
        //Weapon Interaction
        if(handString == "gun1" || handString == "gun2")
        {
            mousePos = Input.mousePosition;
            mousePos.z = 10;
            lookDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
            gunParent.transform.rotation = Quaternion.Euler(0f,lookAngle,0f);
            //Shoot when clicking
            if (Input.GetMouseButtonDown(0))
            {
                GameObject firedBullet = Instantiate(bulletGO, gunTip.transform.position, gunParent.transform.rotation);
                firedBullet.GetComponent<Rigidbody>().velocity = gunTip.transform.forward * 10f;
                gunshotSound.Play();
            }
        }

        //Player Appearence
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            playerSprite.GetComponent<Renderer>().material = playerRight;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSprite.GetComponent<Renderer>().material = playerLeft;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            playerSprite.GetComponent<Renderer>().material = playerUp;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            playerSprite.GetComponent<Renderer>().material = playerDown;
        }
        if (handString == "")
        {
            gunSprite.GetComponent<Renderer>().material = blankmat;
        }
        else if (handString == "gun1")
        {
            gunSprite.GetComponent<Renderer>().material = gun1mat;
        }
        else if (handString == "gun2")
        {
            gunSprite.GetComponent<Renderer>().material = gun2mat;
        }
    }

    //Collisions
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet1")
        {
            playerHealth -= 5;
            hitSound.Play();
        }
        if (collision.gameObject.tag == "bullet2")
        {
            playerHealth -= 10;
            hitSound.Play();
        }
        if (collision.gameObject.tag == "enemy")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    //Plays the video
    void PlayVideo()
    {
        musicSound.Stop();
        videoPlayer.SetActive(true);
        Invoke("NextLevel", 13f);
    }

    //Start Next Level
    void NextLevel()
    {
        if (SceneManager.GetActiveScene().name == "level01_meta")
        {
            SceneManager.LoadScene("level02_delta");
        }
        else if (SceneManager.GetActiveScene().name == "level02_delta")
        {
            SceneManager.LoadScene("level03_box");
        }
        else if (SceneManager.GetActiveScene().name == "level03_box")
        {
            SceneManager.LoadScene("winScene");
        }
    }
}
