using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBass : PoolableMono,IExplosionable,IDamageable
{
    [SerializeField]
    public int hel;

    [SerializeField]
    protected EnemySO _enemyData;
    protected NavMeshAgent _navMesh;
    protected Animator _anim;
    public Material _mat;
    public Transform _target;
    protected AudioSource audioSource;
    protected CinemachineImpulseSource impulseSource;


    protected float _timer;
    float _currentHP;

    private void Awake() {
        _target = GameManager.Instance.playerStruct;
        audioSource = GetComponent<AudioSource>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
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
        if (_currentHP <= 0)
        {
            PoolManager.Instance.Push(this);
            if (gameObject.name == "BigZombie") {
                SaveObject saveObject = _target.GetComponent<SaveObject>();
                if (saveObject.currentHP < saveObject.MaxHP) saveObject.currentHP += hel;
                if (saveObject.currentHP >= saveObject.MaxHP) saveObject.currentHP = saveObject.MaxHP;
            }
        }
    }
    public override void Reset() {
    }
    public void Init(int stage){
        _currentHP =_enemyData.maxHP+stage;
        _mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _navMesh = GetComponent<NavMeshAgent>();
        _mat.SetFloat("_FresnelPower",100f);
        _navMesh.destination = _target.position;
    }

}
