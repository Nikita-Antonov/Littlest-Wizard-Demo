using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Object Reffrences")]
    public Quest quest;                         //Refrence to the Quest
    public Camera questCamera;                  //Refrence to the Camera that activates when the player is engaged in dialouge
    public GameObject startDialougeRequest;     //Reffrence to the UI to notify the player to start the dialouge
    public GameObject dialougeBox;              //Reffrence to the UI box where the dialouge is displayed
    [Space]
    
    [Header("Player Reffrences")]
    public CameraController playerCamera;       //Reffrence to the Player Camera
    public PlayerController playerController;   //Refrence to the Playert Controller
    public PlayerLogic playerLogic;             //Reffrence to the Player Logic Script
    [Space]

    [Header("Quest Range Collider")]
    public SphereCollider questCollider;        //Reffrence to the Collider that determines if the player is in range to dialouge

    public bool allowToDialouge;                //Value that allows the player to dialouge, is used so that the player cant read the dialouge over and over
    public GameObject QuestIcon;                //Game Object of the quest icon, for display that there is a Quest the NPC can give

    //Setting basic peramiters
    private void Start()
    {
        startDialougeRequest.SetActive(false);
        dialougeBox.SetActive(false);
        allowToDialouge = false;
        questCamera.gameObject.SetActive(false);
    }

    public void Update()
    {
        //Checkes if the player is allowed to dialouge
        if (allowToDialouge == true)
        {
            //Checks if the player pressed the X key to initiate the dialouge
            if (Input.GetKey(KeyCode.X))
            {
                //Stop Player From moving
                playerController.enabled = false;
                playerCamera.enabled = false;

                //Remove the collider to trigger the dialouge
                questCollider.enabled = false;
                questCamera.gameObject.SetActive(true);             //Enables the Quest Camera
                Cursor.lockState = CursorLockMode.None;             //Removes the Lock state on the cursor so the Player can progress through the Dialouge
                //Trigger the dialouge
                TriggerDialouge();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")               //Checks if the Player entered the Collider, and not an enemy or a projectile of some kind
        {
            startDialougeRequest.SetActive(true);           //Enables the Box asking the Player to Hit X to start the Dialouge
            allowToDialouge = true;                         //Upon Entering the collider, the Player Can dialouge
        }
       
    }

    //Function to trigger the dialouge function in the "Quest Manager"
    public void TriggerDialouge()
    {
        Debug.Log("Quest started from " + quest.questgiverName);        //Basic Debug to See who the quest was started from
        FindObjectOfType<QuestManager>().StartQuest(quest);             //Finds the Quest Manager and Initiates the Quest/Dialouge
        startDialougeRequest.SetActive(false);                          //Disables the Option for the player to start the Dialouge, becuase he is already in Dialouge
        dialougeBox.SetActive(true);                                    //Enables the Text Box for the Dialouge
        playerLogic.inDialouge = true;                                  //Tells the Player logic to Stop the player firing spells during dialouge
    }

    //This function enables all the the player controlls that where disabled during dialouge
    public void FinishDialouge()
    {
        allowToDialouge = false;                                        //Forbids the Player to dialouge with this NPC after finishing the Dialouge
        playerController.enabled = true;                                //Re-enables the Player controller for movement
        playerCamera.enabled = true;                                    //Re-enables the Player Camera to look around
        questCamera.gameObject.SetActive(false);                        //Turns off the camera for the Quest
        Cursor.lockState = CursorLockMode.Locked;                       //Locks the Cursor
        questCollider.enabled = false;                                  //Disables the requst to ask the player to start the quest
        dialougeBox.SetActive(false);                                   //Disables the Dialouge Box
        QuestIcon.gameObject.SetActive(false);                          //Disables the Icon displaying an available quest
        playerLogic.inDialouge = false;                                 //Allows the Player to cast spells and attacks

    }
}
