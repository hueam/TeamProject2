using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHP : MonoBehaviour
{
    SaveObject saveObj = new SaveObject();
    [SerializeField] private Image hpBar = null;

    private void Awake() {
        hpBar.fillAmount = saveObj.MaxHP;
    }

    private void Update() {
        hpBar.fillAmount = saveObj.currentHP;
    }
}
