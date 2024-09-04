using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public void OnTriggerEnter(Collider other){
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if(playerInventory != null){
            playerInventory.CandyCollided();
            gameObject.SetActive(false);
        }
    }
}
