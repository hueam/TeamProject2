using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Image settingPanel = null;

    private bool isSetting = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OpenSetting();
        }
    }

    public void OpenSetting() {
        isSetting = !isSetting;
        settingPanel.gameObject.SetActive(isSetting);
    }
}
