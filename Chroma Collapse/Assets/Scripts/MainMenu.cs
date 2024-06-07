using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=-GWjA6dixV4

public class MainMenu : MonoBehaviour
{
    public AudioSource sound;

    public void PlayGame(){
        sound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
