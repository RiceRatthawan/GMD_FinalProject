using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSpace : MonoBehaviour
{
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            Destroy(other.gameObject);
        }
    }
}
