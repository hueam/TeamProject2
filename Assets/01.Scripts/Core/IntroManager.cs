using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider[] sliders;
    void Start()
    {
        float value;
        audioMixer.GetFloat("Master",out value);
        sliders[0].value = Mathf.InverseLerp(-80,20,value);
        audioMixer.GetFloat("BGM",out value);
        sliders[1].value = Mathf.InverseLerp(-80,20,value);
        audioMixer.GetFloat("SFX",out value);
        sliders[2].value = Mathf.InverseLerp(-80,20,value);
    }
    public void SetMasterSound(Slider slider){
        audioMixer.SetFloat("Master",Mathf.Lerp(-80,20,slider.value));
    }
    public void SetBGMSound(Slider slider){
        audioMixer.SetFloat("BGM",Mathf.Lerp(-80,20,slider.value));
    }
    public void SetSFXSound(Slider slider){
        audioMixer.SetFloat("SFX",Mathf.Lerp(-80,20,slider.value));
    }
    public void AppearPanel(Transform panel){
        Vector3 defalteScale = panel.localScale;
        Sequence seq = DOTween.Sequence();
        panel.gameObject.SetActive(true);
        seq.Append(panel.DOScale(defalteScale*0.5f,0.2f));
        seq.Append(panel.DOScale(defalteScale,0.2f));
    }
    public void ClosePanel(Transform panel){
        panel.gameObject.SetActive(false);
    }
    public void Exit(){
        Application.Quit();
    }
    public void GameStart(){
        SceneManager.LoadScene("mainGame");
    }

}
