using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour, IDamageable
{
    public float MaxHP;
    public float currentHP;
    private void Start() {
        currentHP = MaxHP;
    }
    public void Hit(float Damage)
    {
        currentHP -= Damage;
        UIManager.Instance.SetHPBar(currentHP/MaxHP);
        if(currentHP <= 0){
            UIManager.Instance.GameOver();
        }
    }
}
