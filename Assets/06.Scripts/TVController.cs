using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour
{
    public GameObject TVScreen; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            TVScreen.SetActive(true); 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            TVScreen.SetActive(false); 
        }
    }
}