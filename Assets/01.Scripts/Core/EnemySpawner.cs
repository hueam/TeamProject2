using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner 
{
    Transform _target;
    float _spawnRange;
    int spawnValue;
    // public int Stage{
    //     get => Stage;
    //     set{
    //         Stage = value;
    //         spawnValue = value + 10;
    //     }
    // }
    public EnemySpawner(Transform target, float range){
        _target = target;
        _spawnRange = range;
    }
    public void Spwan(){
        float ranAngle = Random.Range(0,360);
        Vector3 spawnPos = _target.position + new Vector3(Mathf.Cos(ranAngle)*_spawnRange,10,Mathf.Sin(ranAngle)*_spawnRange);
        EnemyBass eneny = Random.Range(1f, 11f) < 0.5f ? PoolManager.Instance.Pop("BigZombie") as EnemyBass : PoolManager.Instance.Pop("Zombie") as EnemyBass;
        eneny.transform.position = spawnPos;
    }
    public IEnumerator spawnDelay(){
        while(true){
        // for (int i = 0; i < spawnValue; i++)
        // {
            yield return new WaitForSeconds(0.5f);
            Spwan();
        }
        // }
        // GameManager.Instance.NextStage(NextStage());
    }
    // IEnumerator NextStage(){
    //     yield return new WaitForSeconds(10f);
    //     Stage++;
    // }
}
