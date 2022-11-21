using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBass : PoolableMono,IExplosionable,IDamageable
{
    [SerializeField]
    protected EnemySO _enemyData;
    protected NavMeshAgent _navMesh;
    protected Animator _anim;
    public Material _mat;
    public Transform _target;

    float _currentHP;


    public void Boom(float value,float explosionDis,float Damage)
    {
        VFX vfx = PoolManager.Instance.Pop("ElectricBoom") as VFX;
        vfx.transform.position = transform.position;
        Collider[] cols = Physics.OverlapSphere(transform.transform.position, explosionDis, 1 << 7);
        _mat.SetFloat("_FresnelPower", 100f);
        foreach (Collider col in cols)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                EnemyBass damageable = col.GetComponent<EnemyBass>();
                float dis = Vector3.Distance(transform.position, col.transform.position);
                damageable.Hit(Mathf.Clamp(5 - dis, 0.1f, 5f));
                if (damageable is IExplosionable && col.gameObject != gameObject)
                {
                    if(damageable._mat.GetFloat("_FresnelPower") <= 0.5f){
                        damageable.Boom(0.5f, explosionDis, Damage);
                    }

                }
            }
        }
    }
    public void Hit(float Damage)
    {
        _currentHP -= Damage;
        Debug.Log(_currentHP);
        if (_currentHP <= 0)
        {
            PoolManager.Instance.Push(this);
        }
    }
    public override void Reset() {
        _currentHP =_enemyData.maxHP;
        _mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _mat.SetFloat("_FresnelPower",100f);
    }

}
