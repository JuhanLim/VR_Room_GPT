using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    // Reference to the dialogue UI gameObject
    [SerializeField] private GameObject dialouge;
    //public GameObject dialogueUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        
            dialouge.SetActive(true);
        }
        //dispaly cursor 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
        
    //         dialouge.SetActive(false);
    //     }
    //     //dispaly cursor 
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    // }

    public void Recover()
    {
        dialouge.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}



