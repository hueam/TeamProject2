using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalZombie : EnemyBase
{
    bool isAttack;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _navMesh.speed = _enemyData.speed;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) < 7f && isAttack)
        {
            _target.GetComponent<IDamageable>().Hit(_enemyData.atk);
            _anim.SetTrigger("Attack");
            impulseSource.GenerateImpulse();
            audioSource.Play();
            isAttack = false;
        }
        if(!isAttack){
            _timer += Time.deltaTime;
            if(_timer > 3.5f){
                isAttack = true;
                _timer =0;
            }
        }
    }
}
