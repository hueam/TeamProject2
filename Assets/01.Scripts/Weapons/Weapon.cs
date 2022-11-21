using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon:MonoBehaviour
{
    public abstract void Attack(LineRenderer lineR,Transform _firePos,Transform _mainCam,float _dis, LayerMask _objLayer, Action<GameObject> callback = null);
    public abstract bool CheckisAttack();


    [SerializeField]
    protected WeaponSO weaponData;
    protected float timer;
    protected bool attackable = true;
}
