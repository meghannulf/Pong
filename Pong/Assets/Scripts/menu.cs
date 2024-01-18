using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pause_Menu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI;
    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(gameIsPaused){
                Resume();
            } else {
                Pause();
            }
        } 
    }
    public void Resume(){
        PauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause(){
        PauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
}


