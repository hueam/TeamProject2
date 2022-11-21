using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<PoolableMono> pools = new List<PoolableMono>();
    [SerializeField]
    Transform playerStruct; 
    [SerializeField]
    float enemySpawnRange;
    EnemySpawner spawner;
    public static GameManager Instance;
    private void Awake() {
        if(GameManager.Instance == null){
            GameManager.Instance = this;
        }
        PoolManager.Instance = new PoolManager(transform);
        foreach(PoolableMono p in pools){
            PoolManager.Instance.CreatePool(p);
        }
        spawner = new EnemySpawner(playerStruct,enemySpawnRange);
    }
    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
            spawner.Spwan();
    }
}
