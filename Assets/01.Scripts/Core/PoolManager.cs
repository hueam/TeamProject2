using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance;

    private Dictionary<string, Pool<PoolableMono>> _pools = new Dictionary<string, Pool<PoolableMono>>();

    private Transform _trmPrarent;

    public PoolManager(Transform trmParent)
    {
        _trmPrarent = trmParent;
    }

    public void CreatePool(PoolableMono prefab, int cnt = 10)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, _trmPrarent, cnt);
        _pools.Add(prefab.gameObject.name, pool);
    }
    public PoolableMono Pop(string prefabName)
    {
        if (_pools.ContainsKey(prefabName) == false)
        {
            Debug.LogError("Prefab dosent exist on poolList");
            return null;
        }
        PoolableMono item = _pools[prefabName].Pop();
        item.Reset();
        return item;
    }
    public void Push(PoolableMono obj)
    {
        _pools[obj.name].Push(obj);
    }
}
