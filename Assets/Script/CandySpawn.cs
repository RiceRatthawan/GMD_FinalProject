using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawn : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] scorePrefab;
    public Transform[] placement;
    private float currentTime;
    private float nextTime;
    public float rate;

    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.timeSinceLevelLoad;
        
        if (currentTime > nextTime) {                               
            for (int i=0; i < scorePrefab.Length; i++){
                Instantiate(scorePrefab[i], placement[i].position, Quaternion.identity);
            }
            nextTime = nextTime + rate; 
        } 
        
    }

}
