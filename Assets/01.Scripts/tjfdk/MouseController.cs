using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Slider mouseSlider = null;

    private void Update() {
        //마우스 회전 스피드 = mouseSlider.value;
    }
}
