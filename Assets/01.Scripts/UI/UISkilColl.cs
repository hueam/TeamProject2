using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkilColl : MonoBehaviour
{
    [SerializeField] private Image skilColl = null;
    private bool canSkil; //스킬을 사용할 수 있는 상태인가

    private void Update() {
        if (!canSkil) { //스킬을 사용했다면
            skilColl.fillAmount = 0; //쿨타임을 받아와 적용한다.
            if (skilColl.fillAmount >= 0) { //쿨타임이 0보다 크거나 같다면
                skilColl.fillAmount -= Time.deltaTime; //쿨타임에서 시간을 빼준다.
            }
            else {canSkil = true;} //아니라면(= 0초보다 작거나 같다면) 스킬을 사용할 수 있는 상태로 만든다.
        }
    }
}
