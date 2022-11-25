using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill_3 : PassiveSkill
{

    [SerializeField]
    int maxCount;
    int currentCount;
    VFX vfx;
    List<Transform> bezierPos = new List<Transform>();
    List<Transform> afterPos = new List<Transform>();
    
    public override void UseSkill(GameObject obj)
    {
        if (currentCount < maxCount&&isActive)
        {
            Transform closeObj = null;
            Collider[] cols = Physics.OverlapSphere(obj.transform.position, 100, 1 << 7);
            if (cols[1])
            {
                foreach (Collider col in cols)
                {
                    if (col.gameObject.CompareTag("Enemy") && col.gameObject != obj && !afterPos.Contains(col.transform))
                    {
                        if (closeObj == null) closeObj = col.transform;
                        else if (Vector3.Distance(obj.transform.position, col.transform.position) < Vector3.Distance(obj.transform.position, closeObj.transform.position)) closeObj = col.transform;

                    }
                }


                if (closeObj != null)
                {
                    afterPos.Add(closeObj);
                    vfx = PoolManager.Instance.Pop("Lightning") as VFX;
                    vfx.GetComponentsInChildren<Transform>(bezierPos);
                    bezierPos.Remove(vfx.transform);
                    bezierPos.Remove(vfx.transform.Find("Lightning"));
                    bezierPos[0].position = obj.transform.position+Vector3.up;
                    bezierPos[1].position = Vector3.Lerp(obj.transform.position,closeObj.transform.position,0.3f)+Vector3.up;
                    bezierPos[2].position = Vector3.Lerp(obj.transform.position,closeObj.transform.position,0.6f)+Vector3.up;
                    bezierPos[3].position = closeObj.position+Vector3.up;

                    EnemyBass enemyBass = closeObj.GetComponent<EnemyBass>();
                    Material mat = closeObj.GetComponentInChildren<SkinnedMeshRenderer>().material;
                    mat.SetFloat("_FresnelPower", Mathf.Lerp(mat.GetFloat("_FresnelPower"), 0, 0.5f));
                    if (mat.GetFloat("_FresnelPower") <= 0.5f)
                    {
                        if (enemyBass is IExplosionable)
                        {
                            if (mat.GetFloat("_FresnelPower") <= 0.5f)
                            {
                                IExplosionable explosionable = (IExplosionable)enemyBass;
                                explosionable.Boom(0.03f, 5f, 0.1f);
                            }
                        }
                    }
                    currentCount++;
                    UseSkill(closeObj.gameObject);
                }
                else{
                    afterPos.Clear();
                    isActive = false;
                    return;
                }
            }
        }
        else {
            currentCount = 0;
            afterPos.Clear();
            isActive = false;
            return;
        }
    }
    
}
