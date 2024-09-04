using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class PlayerInventory : MonoBehaviour
{
    private GameManager gameManager;

    public bool isPlayer1;

    public int P1Points {get; private set;}
    public int P1NumberOfCrosses {get; private set;}
    public int P1NumberOfPotions {get; private set;}

    public int P2Points {get; private set;}
    public int P2NumberOfCrosses {get; private set;}
    public int P2NumberOfPotions {get; private set;}
    
    //for cross used state
    private bool isUsingCross;
    private float usingCrossEndTime;
    private float usingCrossDuration;
    private float currentTime;
    //cooldown
    public bool isCooldown;


    //forText
    public UnityEvent<PlayerInventory> PlayerInvStat;
    public GameObject p1OutOfAreaPanel, p2OutOfAreaPanel;
    private bool checkP1OutOfAreaPanel, checkP2OutOfAreaPanel;

    //Sound
    public AudioClip candySound, lollipopSound, skullSound, itemCollectSound, crossUseSound, potionUseSound;
    AudioSource audioSource;

    void Start()
    {
        gameManager = GameManager.instance;

        audioSource = GetComponent<AudioSource>();

        usingCrossDuration = 5.0f;

        checkP1OutOfAreaPanel = false;
        checkP2OutOfAreaPanel = false;
    }

    void Update()
    {
        currentTime = Time.time;
        checkUsingCrossState();   
    }

    //Candy
    public void CandyCollided(){
        if(isPlayer1){
            P1Points++;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(candySound, 0.7F);
        }else if(!isPlayer1){
            P2Points++;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(candySound, 0.7F);
        }
    }
    //Lollipop
    public void LollipopCollided(){
        if(isPlayer1){
            P1Points = P1Points + 5;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(lollipopSound, 0.7F);
        }else if(!isPlayer1){
            P2Points = P2Points + 5;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(lollipopSound, 0.7F);
        }
    }
    //Skull
    public void SkullCollided(){
        if(isPlayer1){
            P1Points = P1Points - 5;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(skullSound, 0.7F);
        }else if(!isPlayer1){
            P2Points = P2Points - 5;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(skullSound, 0.7F);
        }
    }
    //Cross function
    public void CrossCollected(){
        if(isPlayer1){
            P1NumberOfCrosses++;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(itemCollectSound, 0.7F);
        }else if(!isPlayer1){
            P2NumberOfCrosses++;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(itemCollectSound, 0.7F);
        }
    }
    public void CrossUsed(){
        if(isPlayer1){
            P1NumberOfCrosses--;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(crossUseSound, 0.7F);
            gameManager.p1CrossUsePanel.SetActive(true);
            gameManager.textCheckEndTime = gameManager.currentTime + 5.0f;
            gameManager.showP1CrossUsePanelPanel = true;
            isCooldown = true;
        }else if(!isPlayer1){
            P2NumberOfCrosses--;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(crossUseSound, 0.7F);
            gameManager.p2CrossUsePanel.SetActive(true);
            gameManager.textCheckEndTime = gameManager.currentTime + 5.0f;
            gameManager.showP2CrossUsePanelPanel = true;
            isCooldown = true;
        }
    }

    //Potion function
    public void PotionCollected(){
        if(isPlayer1){
            P1NumberOfPotions++;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(itemCollectSound, 0.7F);
        }else if(!isPlayer1){
            P2NumberOfPotions++;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(itemCollectSound, 0.7F);
        }
    }
    public void PotionUsed(){
        if(isPlayer1){
            P1NumberOfPotions--;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(potionUseSound, 0.7F);
        }else if(!isPlayer1){
            P2NumberOfPotions--;
            PlayerInvStat.Invoke(this);
            audioSource.PlayOneShot(potionUseSound, 0.7F);
        }
    }

    //Ghost
    public void GhostCollided(){
        if(isPlayer1){
            if(P1NumberOfCrosses > 0 && currentTime > usingCrossEndTime){
                isUsingCross = true;
                usingCrossEndTime = currentTime + usingCrossDuration;
                CrossUsed();
            } else if(!isUsingCross) {
                Destroy(gameObject);
            }
        }else if(!isPlayer1){
            if(P2NumberOfCrosses > 0 && currentTime > usingCrossEndTime){
                isUsingCross = true;
                usingCrossEndTime = currentTime + usingCrossDuration;
                CrossUsed();
            } else if(!isUsingCross) {
                Destroy(gameObject);
            }
        }
    }

    public void checkUsingCrossState(){
        currentTime = Time.time;
        if(currentTime > usingCrossEndTime){
            isUsingCross = false;
        }
    }

    //WallOutOfArea
    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "wall"){
            if(isPlayer1){
                p1OutOfAreaPanel.SetActive(true);
                checkP1OutOfAreaPanel = true;
            } 
            if(!isPlayer1){
                p2OutOfAreaPanel.SetActive(true);
                checkP2OutOfAreaPanel = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(isPlayer1){
            if(checkP1OutOfAreaPanel){
                p1OutOfAreaPanel.SetActive(false);
                checkP1OutOfAreaPanel = false;
            }
        }
        if(!isPlayer1){
            if(checkP2OutOfAreaPanel){
                p2OutOfAreaPanel.SetActive(false);
                checkP2OutOfAreaPanel = false;
            }
        }
        
    }
}
