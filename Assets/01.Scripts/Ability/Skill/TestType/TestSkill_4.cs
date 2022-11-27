using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestSkill_4 : ActiveSkill
{
    VFX vfx;
    VisualEffect visualEffect;
    ElectricOrb orb;
    bool isAtiving;
    float chargingValue;
    float chargingTime;
    public override void UseSkill(Transform maincam,Player player,Action<GameObject> callback) 
    {
        if(isActive){
        if(isAtiving){
            orb.SetAction(callback);
            orb.ReleaseOrb((int)(chargingValue)*5);
            chargingValue = 0;
            visualEffect.SetFloat("OutSphereSize",visualEffect.GetFloat("OutSphereSize")*2);
            visualEffect.SetFloat("Size",0.1f);
            visualEffect.SetFloat("trailsSpawnRate",0.001f);
            isAtiving = false;
            isActive = false;
        }else {
            vfx = PoolManager.Instance.Pop("ActiveElectric_2") as VFX;
            orb = vfx.GetComponent<ElectricOrb>();
            orb.SetTarget(player.transform);
            visualEffect = vfx.GetComponent<VisualEffect>();
            isAtiving = true;
            }
        }
    }
    private void Update() {
        if(isAtiving)
        {
            chargingTime += Time.deltaTime;
            chargingValue += Time.deltaTime;
            if(chargingTime >= 5f){
                if(visualEffect.GetFloat("OutSphereSize")<0.9f){
                    visualEffect.SetFloat("OutSphereSize",visualEffect.GetFloat("OutSphereSize")+0.3f);
                    chargingTime = 0;
                }
            }
        }
        if(!isActive){
            timer += Time.deltaTime;
            UIManager.Instance.SetESkillCoolTime(timer <= 0 ? 0 : (coolTime - timer) / coolTime);
        }
        if(timer >= coolTime)
        {
            isActive = true;
            timer = 0;
        }
    }
}
