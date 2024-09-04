using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    PlayerMovement playerMovement1, playerMovement2;
    PlayerInventory playerInventory1, playerInventory2;
    DetectPlayer detectPlayer2, detectPlayer1;
    public GameObject Player1, Player2;

    //for potion used state
    private float usingPotionEndTime;
    private float usingPotionDuration;
    public float currentTime;

    //for text
    public GameObject p1UsePotionPanel, p2UsePotionPanel;
    public GameObject p1CheckP2PotionStatusPanel, p2CheckP1PotionStatusPanel;
    public GameObject P1NoPotionPanel, P2NoPotionPanel;
    public GameObject p1CheckP2RangePanel, p2CheckP1RangePanel;
    public GameObject p1WinPanel, p2WinPanel;
    public GameObject p1CrossUsePanel, p2CrossUsePanel;
    public Image p1CrossCooldown, p2CrossCooldown;
    public GameObject p1SlowEffect, p2SlowEffect;

    public float textCheckEndTime;

    private bool checkP2StatusPanel, checkP1StatusPanel;

    private bool checkP1NoPotionPanel, checkP2NoPotionPanel;

    private bool checkP2RangePanel, checkP1RangePanel;

    public bool showP1CrossUsePanelPanel, showP2CrossUsePanelPanel;

    private bool playsound;

    //Sound
    public AudioClip endSound;
    AudioSource audioSource;
    
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else if(instance != null) {
            Destroy(gameObject);
            return;
        }

        playerMovement1 = Player1.GetComponent<PlayerMovement>();
        playerMovement2 = Player2.GetComponent<PlayerMovement>();

        playerInventory1 = Player1.GetComponent<PlayerInventory>();
        playerInventory2 = Player2.GetComponent<PlayerInventory>();

        detectPlayer2 = Player1.GetComponent<DetectPlayer>();
        detectPlayer1 = Player2.GetComponent<DetectPlayer>();
    }

    void Start()
    {
        checkP2StatusPanel = false;
        checkP1StatusPanel = false;
        checkP1NoPotionPanel = false;
        checkP2NoPotionPanel = false;
        checkP2RangePanel = false;
        checkP1RangePanel = false;
        usingPotionDuration = 10.0f;
        audioSource = GetComponent<AudioSource>();
        p1WinPanel.SetActive(false);
        p2WinPanel.SetActive(false);
    }

    void Update()
    {
        currentTime = Time.time;
        checkPlayer();
        checkPoints();      
        usePotion();
        checkPotionState();
        checkCoolDownCross();
        disableText();
        ExitGame();
    }

    void checkPlayer(){
        if(Player1 == null && Player2 != null){
            if(!playsound)
            {
                audioSource.PlayOneShot(endSound, 0.7F);
                playsound = true;
            }
            p2WinPanel.SetActive(true);
        }
        else if(Player2 == null && Player1 != null){
            if(!playsound)
            {
                audioSource.PlayOneShot(endSound, 0.7F);
                playsound = true;
            }
            p1WinPanel.SetActive(true);
        }
    }

    void checkPoints(){
        if(playerInventory1.P1Points>=100){
            if(!playsound)
            {
                audioSource.PlayOneShot(endSound, 0.7F);
                playsound = true;
            }
            p1WinPanel.SetActive(true);
        }
        else if(playerInventory2.P2Points>=100){
            if(!playsound)
            {
                audioSource.PlayOneShot(endSound, 0.7F);
                playsound = true;
            }
            p2WinPanel.SetActive(true);
        }
    }

    void usePotion(){
        if(Input.GetKeyDown(KeyCode.R)){
            if(detectPlayer2.canSeePlayer){
                if(playerInventory1.P1NumberOfPotions > 0 && playerMovement2.speed == 6.0f){
                    playerInventory1.PotionUsed();
                    playerMovement2.speed = 3.0f;
                    p2SlowEffect.SetActive(true);
                    p1UsePotionPanel.SetActive(true);
                    p1CheckP2RangePanel.SetActive(false);
                    usingPotionEndTime = currentTime + usingPotionDuration;
                } else if(playerInventory1.P1NumberOfPotions > 0 && playerMovement2.speed == 3.0f){ 
                    textCheckEndTime = currentTime + 3.0f;
                    p1CheckP2PotionStatusPanel.SetActive(true);
                    p1CheckP2RangePanel.SetActive(false);
                    checkP2StatusPanel = true;
                } else if(playerInventory1.P1NumberOfPotions == 0){
                    textCheckEndTime = currentTime + 3.0f;
                    P1NoPotionPanel.SetActive(true);
                    p1CheckP2RangePanel.SetActive(false);
                    checkP1NoPotionPanel = true;
                }
            }else{
                p1UsePotionPanel.SetActive(false);
                P1NoPotionPanel.SetActive(false);
                p1CheckP2RangePanel.SetActive(true);
                textCheckEndTime = currentTime + 3.0f;
                checkP2RangePanel = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Slash)){
            if(detectPlayer1.canSeePlayer){
                if(playerInventory2.P2NumberOfPotions > 0 && playerMovement1.speed == 6.0f){
                    playerInventory2.PotionUsed();
                    playerMovement1.speed = 3.0f;
                    p1SlowEffect.SetActive(true);
                    p2UsePotionPanel.SetActive(true);
                    p2CheckP1RangePanel.SetActive(false);
                    usingPotionEndTime = currentTime + usingPotionDuration;
                } else if(playerInventory1.P1NumberOfPotions > 0 && playerMovement1.speed == 3.0f){
                    textCheckEndTime = currentTime + 3.0f;
                    p2CheckP1PotionStatusPanel.SetActive(true);
                    p2CheckP1RangePanel.SetActive(false);
                    checkP1StatusPanel = true;
                } else if(playerInventory2.P2NumberOfPotions == 0){
                    textCheckEndTime = currentTime + 3.0f;
                    P2NoPotionPanel.SetActive(true);
                    p2CheckP1RangePanel.SetActive(false);
                    checkP2NoPotionPanel = true;
                }
            }else{
                p2UsePotionPanel.SetActive(false);
                P2NoPotionPanel.SetActive(false);
                p2CheckP1RangePanel.SetActive(true);
                textCheckEndTime = currentTime + 3.0f;
                checkP1RangePanel = true;
            }
        }
    }

    void checkPotionState(){
        currentTime = Time.time;
        if(playerMovement1.speed == 3.0f){
            if(currentTime > usingPotionEndTime){
                playerMovement1.speed = 6.0f;
                p1SlowEffect.SetActive(false);
                p2UsePotionPanel.SetActive(false);
            }
        }
        if(playerMovement2.speed == 3.0f){
            if(currentTime > usingPotionEndTime){
                playerMovement2.speed = 6.0f;
                p2SlowEffect.SetActive(false);
                p1UsePotionPanel.SetActive(false);
            }
        }
    }

    void checkCoolDownCross(){
        if(playerInventory1.isCooldown){
            p1CrossCooldown.fillAmount -=1 / 5.0f * Time.deltaTime;
            p1CrossCooldown.gameObject.SetActive(true);

            if(p1CrossCooldown.fillAmount == 0){
                p1CrossCooldown.fillAmount = 1;
                playerInventory1.isCooldown = false;
                p1CrossCooldown.gameObject.SetActive(false);
            }
        }
        if(playerInventory2.isCooldown){
            p2CrossCooldown.fillAmount -=1 / 5.0f * Time.deltaTime;
            p2CrossCooldown.gameObject.SetActive(true);

            if(p2CrossCooldown.fillAmount == 0){
                p2CrossCooldown.fillAmount = 1;
                playerInventory2.isCooldown = false;
                p2CrossCooldown.gameObject.SetActive(false);
            }
        }
    }

    void disableText(){
        currentTime = Time.time;
        if(checkP2StatusPanel){
            if(currentTime > textCheckEndTime){
                p1CheckP2PotionStatusPanel.SetActive(false);
                checkP2StatusPanel = false;
            }
        }
        if(checkP1StatusPanel){
            if(currentTime > textCheckEndTime){
                p2CheckP1PotionStatusPanel.SetActive(false);
                checkP1StatusPanel = false;
            }
        }
        if(checkP1NoPotionPanel){
            if(currentTime > textCheckEndTime){
                P1NoPotionPanel.SetActive(false);
                checkP1NoPotionPanel = false;
            }
        }
        if(checkP2NoPotionPanel){
            if(currentTime > textCheckEndTime){
                P2NoPotionPanel.SetActive(false);
                checkP2NoPotionPanel = false;
            }
        }
        if(checkP2RangePanel){
            if(currentTime > textCheckEndTime){
                p1CheckP2RangePanel.SetActive(false);
                checkP2RangePanel = false;
            }
        }
        if(checkP1RangePanel){
            if(currentTime > textCheckEndTime){
                p2CheckP1RangePanel.SetActive(false);
                checkP1RangePanel = false;
            }
        }
        if(showP1CrossUsePanelPanel){
            if(currentTime > textCheckEndTime){
                p1CrossUsePanel.SetActive(false);
                showP1CrossUsePanelPanel = false;
            }
        }
        if(showP2CrossUsePanelPanel){
            if(currentTime > textCheckEndTime){
                p2CrossUsePanel.SetActive(false);
                showP2CrossUsePanelPanel = false;
            }
        }
    }

    public void BackToMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
