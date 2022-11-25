using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner 
{
    Transform _target;
    float _spawnRange;
    public EnemySpawner(Transform target, float range){
        _target = target;
        _spawnRange = range;
    }
    public void Spwan(){
        float ranAngle = Random.Range(0,360);
        Vector3 spawnPos = _target.position + new Vector3(Mathf.Cos(ranAngle)*_spawnRange,10,Mathf.Sin(ranAngle)*_spawnRange);
        EnemyBass eneny =PoolManager.Instance.Pop("BigZombie") as EnemyBass;
        eneny.transform.position = spawnPos;
    }
}
