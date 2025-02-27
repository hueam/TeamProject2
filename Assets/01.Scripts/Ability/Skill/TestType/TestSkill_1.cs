using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill_1 : PassiveSkill
{
    Material mat;
    public override void UseSkill(GameObject hitObj = null)
    {
        Transform hitTrm = hitObj.transform;
        EnemyBase damageableObj = hitObj.GetComponent<EnemyBase>();
        mat = hitObj.GetComponentInChildren<SkinnedMeshRenderer>().material;
        mat.SetFloat("_FresnelPower", Mathf.Lerp(mat.GetFloat("_FresnelPower"), 0, 0.03f));
        if (mat.GetFloat("_FresnelPower") <= 0.5f)
        {
            if (damageableObj is IExplosionable)
            {
                IExplosionable explosionable = (IExplosionable)damageableObj;
                explosionable.Boom(0.03f, 5f, 0.1f);
            }
            damageableObj.Hit(0f);
        }
    }
}
