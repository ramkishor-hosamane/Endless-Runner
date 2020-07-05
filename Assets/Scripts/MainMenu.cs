using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image BlackScreen;

    public GameObject blkimg;
    public Animator anim;
    public bool fadeout = true;

    public bool fade = true;
    public void changeScene(string scene_name){
        blkimg.SetActive(true);       
        Invoke("StartGame",1.0f);

    }

    public  void QuitGame () {
        Application.Quit();
        Debug.Log("Game is exiting");
         }
    // Start is called before the first frame update
    void Start()
    {
    }


void StartGame(){
            Application.LoadLevel("Game");

}

}