using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX : PoolableMono
{
    [SerializeField]
    float time;
    VisualEffect vfx;
    private void Start() {
        
    }
    IEnumerator Effect(){
        yield return new WaitForSeconds(time);
        PoolManager.Instance.Push(this);
    }
    public override void Reset()
    {
        vfx = GetComponentInChildren<VisualEffect>();
        vfx.Play();
        StartCoroutine(Effect());
    }
}
