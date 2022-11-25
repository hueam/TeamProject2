using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour, IDamageable
{
    [SerializeField]int MaxHP;
    float currentHP;
    private void Start() {
        currentHP = MaxHP;
    }
    public void Hit(float Damage)
    {
        currentHP -= Damage;
        if(currentHP <= 0){
            Debug.Log("님 망함 ㅋㅋ");
        }
    }
}
