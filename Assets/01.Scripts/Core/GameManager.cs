using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<PoolableMono> pools = new List<PoolableMono>();
    public Transform playerStruct; 
    [SerializeField]
    float enemySpawnRange;
    EnemySpawner spawner;
    Player player;
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
        StartCoroutine(spawner.spawnDelay());
        player=GameObject.Find("Player").GetComponent<Player>();
    }
    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // public void NextStage(IEnumerator coru){
    //     StartCoroutine(coru);
    // }
}
