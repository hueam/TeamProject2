using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigZomble : EnemyBass
{
    bool isAttack;
    Rigidbody rigid;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>(); 
        _anim = GetComponent<Animator>();
        _navMesh.speed = _enemyData.speed;
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position,_target.position)<3f&&isAttack){
            _target.GetComponent<IDamageable>().Hit(_enemyData.atk);
            _anim.SetTrigger("Attack");
            StartCoroutine(Jump());
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
    IEnumerator Jump(){
        yield return new WaitForSeconds(0.5f);
        rigid.AddForce(Vector3.up*3,ForceMode.Impulse);
    }
}
