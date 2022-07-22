using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float delayTime = 2f;

    public void PlayGame()
    {
        Invoke("DelayedAction", delayTime);
    }

    void DelayedAction(){
       SceneManager.LoadScene(1);
    }

    public void ReMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
