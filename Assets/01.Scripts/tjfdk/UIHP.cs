using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHP : MonoBehaviour
{
    SaveObject saveObj = new SaveObject();
    [SerializeField] private Image hpSlider = null;

    private void Awake() {
        hpSlider.fillAmount = saveObj.MaxHP;
    }

    private void Update() {
        hpSlider.fillAmount = saveObj.currentHP;
    }
}
