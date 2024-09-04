using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject[] skullPrefab;
    public Transform[] placement;
    private float CurrentTime;
    private float NextTime;
    private float rate = 5.0f;

    void Update()
    {
        CurrentTime = Time.timeSinceLevelLoad;
        
        if (CurrentTime > NextTime) {                                      
            for (int i=0; i < skullPrefab.Length; i++){
                Instantiate(skullPrefab[i], placement[i].position, Quaternion.identity);
            }
        NextTime = NextTime + rate; 
        } 
    }

    public void OnTriggerEnter(Collider other){
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if(playerInventory != null){
            playerInventory.GhostCollided();
        }
    }
}
