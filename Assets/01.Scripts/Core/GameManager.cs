using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<PoolableMono> pools = new List<PoolableMono>();
    public Transform playerStruct; 
    [SerializeField]
    float enemySpawnRange;
    EnemySpawner spawner;
    Player player;
    int stage;
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
        player=GameObject.Find("Player").GetComponent<Player>();
        NextStage();
    }
    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void NextStage(){
        stage++;
        StartCoroutine(spawner.spawnDelay(stage));
    }
}
