using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill :MonoBehaviour
{
    public bool isActive;
    public float timer;
    public float coolTime;
    private void Update() {
        if(!isActive){
            timer += Time.deltaTime;
        }
        if(timer >= coolTime)
        {
            isActive = true;
            timer = 0;
        }

    }
}