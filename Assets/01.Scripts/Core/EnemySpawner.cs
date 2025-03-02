using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner 
{
    Transform _target;
    float _spawnRange;
    int spawnValue;
    int stage;
    public EnemySpawner(Transform target, float range){
        _target = target;
        _spawnRange = range;
        spawnValue = 10;

    }
    public void Spwan(int num){
        float ranAngle = Random.Range(0,360);
        Vector3 spawnPos = _target.position + new Vector3(Mathf.Cos(ranAngle)*_spawnRange,10,Mathf.Sin(ranAngle)*_spawnRange);
        EnemyBase enemy = null;
        enemy = num%5 == 0?PoolManager.Instance.Pop("BigZombie") as EnemyBase : PoolManager.Instance.Pop("Zombie") as EnemyBase;
        enemy.Init(stage);
        enemy.transform.position = spawnPos;
    }
    public IEnumerator spawnDelay(int stage){
        this.stage = stage;
        while(true){
            for (int i = 0; i < spawnValue+stage; i++)
            {
                yield return new WaitForSeconds(Random.Range(1f,3f));
                Spwan(i);
            }
            yield return new WaitForSeconds(10f);
            GameManager.Instance.NextStage();
        }
    }
    // IEnumerator NextStage(){
    //     yield return new WaitForSeconds(10f);
    //     Stage++;
    // }
}
