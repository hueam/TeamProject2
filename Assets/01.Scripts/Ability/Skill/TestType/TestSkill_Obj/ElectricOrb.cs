using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ElectricOrb : MonoBehaviour
{
    Transform target;
    Action<GameObject> callback;
    bool isTrigger;
    VisualEffect effect;
    List<Transform> afterPos = new List<Transform>();
    int value;
    VFX visual;
    private void Awake() {
        effect = GetComponent<VisualEffect>();
        visual = GetComponent<VFX>();
    }
    private void Reset() {
        
       
    }
    // Update is called once per frame
    void Update()
    {
        if(isTrigger)
        {
            transform.position = target.position+Vector3.up*1.3f;
            Player player = target.GetComponent<Player>();
            transform.rotation = Quaternion.LookRotation(player._mainCam.transform.forward*player._dis*3);
        }
        else
        {
            transform.position += transform.forward*Time.deltaTime *10;
            Collider[] cols =Physics.OverlapSphere(transform.position,effect.GetFloat("OutSphereSize")/2,1<<7);
            foreach(Collider col in cols){
                if(col.gameObject.CompareTag("Enemy")){
                    if(value >0){
                        List<Transform> bezierPos = new List<Transform>();
                        VFX vfx = PoolManager.Instance.Pop("Lightning") as VFX;
                        vfx.GetComponentsInChildren<Transform>(bezierPos);
                        bezierPos.Remove(vfx.transform);
                        bezierPos.Remove(vfx.transform.Find("Lightning"));
                        bezierPos[0].position = transform.position;
                        bezierPos[1].position = Vector3.Lerp(transform.position, col.transform.position, 0.3f);
                        bezierPos[2].position = Vector3.Lerp(transform.position, col.transform.position, 0.6f);
                        bezierPos[3].position = col.transform.position;

                        EnemyBase enemyBase = col.GetComponent<EnemyBase>();
                        enemyBase.Hit(2f);
                        callback?.Invoke(col.gameObject);
                        value--;
                    }
                    else 
                    {
                        VFX vfx = PoolManager.Instance.Pop("ElectricBoom") as VFX;
                        vfx.transform.position = transform.position;
                        Collider[] colliders = Physics.OverlapSphere(transform.transform.position,effect.GetFloat("OutSphereSize"), 1 << 7);
                        foreach (Collider collider in colliders){
                            if (col.gameObject.CompareTag("Enemy"))
                            {
                                EnemyBase damageable = col.GetComponent<EnemyBase>();
                                float dis = Vector3.Distance(transform.position, collider.transform.position);
                                damageable.Hit(Mathf.Clamp(5 - dis, 0.1f, 5f));
                            }
                        }
                        visual.StopVFX();
                    }
                } 
                // if (col.gameObject.CompareTag("Player")){
                //     VFX vfx = PoolManager.Instance.Pop("ElectricBoom") as VFX;
                //         vfx.transform.position = transform.position;
                //         Collider[] colliders = Physics.OverlapSphere(transform.transform.position,effect.GetFloat("OutSphereSize"), 1 << 7);
                //         foreach (Collider collider in colliders){
                //             if (col.gameObject.CompareTag("Enemy"))
                //             {
                //                 EnemyBass damageable = col.GetComponent<EnemyBass>();
                //                 float dis = Vector3.Distance(transform.position, collider.transform.position);
                //                 damageable.Hit(Mathf.Clamp(5 - dis, 0.1f, 5f));
                //             }
                //         }
                //         visual.StopVFX();
                // }
            }
        }
    }
    public void Disappear(){

    }
    public void SetAction(Action<GameObject> _callback){
        callback = _callback;
    }
    public void SetTarget(Transform _target){
         effect.SetFloat("OutSphereSize",0.3f);
        effect.SetFloat("Size",1f);
        effect.SetFloat("trailsSpawnRate",1f);
        target = _target;
        isTrigger = true;
    }
    public void ReleaseOrb(int value){
        this.value = value;
        isTrigger = false;
    }
}
