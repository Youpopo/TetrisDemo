using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SceneFunction : MonoBehaviour
{
    public GameObject HUD,Menu;
    public UnityEngine.UI.Slider slider;
    public TMP_Text Score;
    public static SceneFunction inst;
    float score = 0;
    private void Awake() {
        inst = this;
        slider.value = GameManager.instance.Volume;
    }
    public void Pause(){
        GameManager.instance.Pause();
        HUD.SetActive(false);
        Menu.SetActive(true);
    }

    public void Resume(){
        GameManager.instance.resume();
        Menu.SetActive(false);
        HUD.SetActive(true);
    }

    public void ChangeVolume(float f){
        GameManager.instance.SetVolume(slider.value);
    }

    public void UpdateScore(){
        score += 100;
        Score.text = "Score : "+score;
    }
}
