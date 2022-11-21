using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill_2 : ActiveSkill
{
    VFX vfx;
    public override void UseSkill(Transform maincam)
    {
        vfx = PoolManager.Instance.Pop("ActiveElectric") as VFX;
        Player player = gameObject.GetComponentInParent<Player>();
        vfx.transform.position = player._firePos.position;
        vfx.transform.LookAt(maincam.forward * player._dis * 3);
        Collider[] cols = Physics.OverlapBox(vfx.transform.position + player.transform.forward * 4, new Vector3(1, 1, 4), vfx.transform.rotation, LayerMask.GetMask("Any"));
        foreach (Collider col in cols)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                EnemyBass enemyBass = col.GetComponent<EnemyBass>();
                Material mat = col.GetComponentInChildren<SkinnedMeshRenderer>().material;
                mat.SetFloat("_FresnelPower", Mathf.Lerp(mat.GetFloat("_FresnelPower"), 0, 0.5f));
                if (mat.GetFloat("_FresnelPower") <= 0.5f)
                {
                    if (enemyBass is IExplosionable)
                    {
                        if(mat.GetFloat("_FresnelPower") <= 0.5f){
                            IExplosionable explosionable = (IExplosionable)enemyBass;
                            explosionable.Boom(0.03f, 5f, 0.1f);
                        }
                    }
                }
                enemyBass.Hit(2f);
            }
        }
    }
}
