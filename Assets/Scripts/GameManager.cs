using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource ClearLine,Background;
    public UnityEngine.UI.Slider slider;
    public float Volume = .7f;
    private void Awake() {
       instance = this;
        DontDestroyOnLoad(this);
        slider.value = Volume;
    }

    public void Play(){
        SceneManager.LoadScene(1);
    }

    public void Quit(){
        Application.Quit();
    }

    public void Pause(){
        Time.timeScale = 0;
    }

    public void resume(){
        Time.timeScale = 1;
    }

    public void DeleteLine(){
        ClearLine.Play();
    }

    public void SetRetardedVolume(float f){
        SetVolume(slider.value);
    }


    public void SetVolume(float f){
        Volume = f;
        Background.volume = f;
        ClearLine.volume = f;

    }
}
