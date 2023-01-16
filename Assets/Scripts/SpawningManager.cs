using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] private Transform _startingPoint;

    [SerializeField] private GameObject _EnemyPrefab;
    [SerializeField] private List<GameObject> _EnemyPool;
    [SerializeField] private List<GameObject> _InactiveEnemyPool;
    [SerializeField]private int _MaxEnemies = 100;
    [SerializeField] private float _spawnMinTime = 3f;
    [SerializeField] private float _spawnMaxTime = 6f;
    private int NumEnemiesSpawned = 0;

    private float _spawnTimer = -1;
    private static SpawningManager _instance;

    public static SpawningManager Instance
    {
        get{
            if (_instance == null)
            {
                _instance = new SpawningManager();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateEnemyPool();
    }

    private void GenerateEnemyPool()
    {
        for (int i = 0; i < _MaxEnemies; i++)
        {
            GameObject go = Instantiate(_EnemyPrefab, _startingPoint.transform.position,Quaternion.identity,this.transform);
            go.SetActive(false);
            _EnemyPool.Add(go);
        }
    }

    private GameObject RequestEnemy()
    {
        foreach (var enemy in _EnemyPool)
        {
            if (enemy.activeInHierarchy == false)
            {
                enemy.transform.position = _startingPoint.position;
                EnemyAI eAI = enemy.GetComponent<EnemyAI>();
                
                enemy.SetActive(true);
                return enemy;
            }
        }
        Debug.LogError("We ran out of enemies?");
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (_spawnTimer < Time.time)
        {
            _spawnTimer = Time.time + Random.Range(_spawnMinTime,_spawnMaxTime);
            NumEnemiesSpawned++;
            if (NumEnemiesSpawned <= 100)
            {
                RequestEnemy();
            }
        }
    }
}
