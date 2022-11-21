using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Laser : Weapon
{
    
    VisualEffect vfx;

    public override void Attack(LineRenderer lineR,Transform _firePos,Transform _mainCam,float _dis, LayerMask _objLayer,Action<GameObject> callback = null)
    {
        lineR.SetPosition(0,_firePos.transform.position);
        if(Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out RaycastHit raycastHit, _dis*3, _objLayer)){
            if(raycastHit.transform.gameObject.CompareTag("Enemy")&&attackable){
                GameObject hitObj = raycastHit.transform.gameObject;
                EnemyBass damageable = hitObj.GetComponent<EnemyBass>();
                damageable.Hit(weaponData.Atk);
                callback?.Invoke(hitObj);
                attackable = false;
            }
            lineR.SetPosition(1,raycastHit.point);
        }
        else lineR.SetPosition(1,_mainCam.transform.position+_mainCam.transform.forward*_dis*3);
    }

    private void Update() {
        if (!attackable)
        {
            if (timer > weaponData.delay)
            {
                attackable = true;
                timer = 0;
            }
            else
            {
            timer += Time.deltaTime;
            }
        }
    }

    public override bool CheckisAttack()
    {
        return attackable;
    }

    
}
