using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public AudioSource bgMenuSound;
    public GameObject howTo1Panel, howTo2Panel;

    void Start()
    {
        bgMenuSound.Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    public void Previous(){
        howTo1Panel.SetActive(true);
        howTo2Panel.SetActive(false);
    }

    public void Next(){
        howTo1Panel.SetActive(false);
        howTo2Panel.SetActive(true);
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
