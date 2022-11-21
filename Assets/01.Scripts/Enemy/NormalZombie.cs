using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalZombie : EnemyBass
{
    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _navMesh.speed = _enemyData.speed;
    }
    private void Update()
    {
        _navMesh.SetDestination(_target.position);
        
    }
}
