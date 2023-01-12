using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager :MonoBehaviour
{   
    [SerializeField]
    AudioMixer audioMixer;

    Transform canvas;
    Slider _hpBar;
    Image _skilE;
    Image _skilQ;
    Vector3 defaultPos;
    AudioMixer mixer;
    Player _player;
    RectTransform settingPanel;
    [SerializeField]
    RectTransform closeImage;
    [SerializeField]
    TextMeshProUGUI stageTxt;

    public static UIManager Instance;
    
    private void Start(){
        _player = GameObject.Find("Player").GetComponent<Player>();
        mixer = audioMixer;
        canvas = GameObject.Find("Canvas").transform;
        RectTransform _playerPanel = canvas.Find("PlayerPanel").GetComponent<RectTransform>();
        _hpBar = _playerPanel.Find("HPBar").GetComponent<Slider>();
        defaultPos = _hpBar.transform.position;
        _hpBar.value = 1;
        RectTransform _skillPanel = _playerPanel.Find("Skills").GetComponent<RectTransform>();
        _skilE = _skillPanel.Find("SkilE").Find("SkilECool").GetComponent<Image>();
        _skilQ = _skillPanel.Find("SkilQ").Find("SkilQCool").GetComponent<Image>();
        settingPanel = canvas.Find("SettingPanel").GetComponent<RectTransform>();
        RectTransform mousePanel = settingPanel.GetChild(0).GetComponent<RectTransform>();
        RectTransform soundSetting = settingPanel.GetChild(1).GetComponent<RectTransform>();
        Slider[]sliders = soundSetting.GetComponentsInChildren<Slider>();
        float value;
        audioMixer.GetFloat("Master",out value);
        sliders[0].value = Mathf.InverseLerp(-80,20,value);
        audioMixer.GetFloat("BGM",out value);
        sliders[1].value = Mathf.InverseLerp(-80,20,value);
        audioMixer.GetFloat("SFX",out value);
        sliders[2].value = Mathf.InverseLerp(-80,20,value);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(settingPanel.gameObject.activeSelf){
                settingPanel.gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _player.isSetting = false;
            }
            else if(!settingPanel.gameObject.activeSelf){
                settingPanel.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _player.isSetting = transform;
            }
            
        }    
    }
    public void AppearStageTxt(int stage){
        stageTxt.text = $"{stage} Stage";
        Vector3 defaultPos = stageTxt.transform.position;
        Sequence seq = DOTween.Sequence();
        seq.Append(stageTxt.transform.DOMove(defaultPos + Vector3.down*100,1f));
        seq.Join(stageTxt.transform.DOScale(Vector3.one* 2,1f));
        seq.Append(stageTxt.transform.DOMove(defaultPos,1f));
        seq.Join(stageTxt.transform.DOScale(Vector3.one,1f));
    }
    public void SetHPBar(float value){
        _hpBar.value = value;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_hpBar.transform.DOMove(defaultPos+Vector3.up*11,0.05f)); 
        sequence.Append(_hpBar.transform.DOMove(defaultPos+Vector3.down*10,0.1f)); 
        sequence.Append(_hpBar.transform.DOMove(defaultPos,0.05f)); 
    }
    public void SetESkillCoolTime(float value){
        _skilE.fillAmount = value;
    }
    public void SetQSkillCoolTime(float value){
        _skilQ.fillAmount = value;
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
    public void SetMouseDPI(Slider slider){
        _player.dpi = Mathf.Lerp(0.1f,10,slider.value);
    }
    public void GameOver(){
        Sequence seq = DOTween.Sequence();
        seq.Append(closeImage.DOMove(new Vector2(closeImage.position.x,0),0.5f));
        seq.Append(closeImage.DOMove(new Vector2(closeImage.position.x,10),0.2f));
        seq.Append(closeImage.DOMove(new Vector2(closeImage.position.x,0),0.1f));
        seq.OnComplete(()=>SceneManager.LoadScene("Intro"));
        
    }
    
}
