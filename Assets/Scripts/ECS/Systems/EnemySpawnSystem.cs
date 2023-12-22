using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemySpawnSystem))]
public sealed class EnemySpawnSystem : UpdateSystem
{
    private Filter _spawnerFilter;
    private Filter _playerFilter;


    public override void OnAwake()
    {
        _spawnerFilter = this.World.Filter.With<EnemySpawnComponent>().Build();
        _playerFilter = this.World.Filter.With<PlayerTagComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in _spawnerFilter)
        {
            ref var enemySpawnComponent = ref entity.GetComponent<EnemySpawnComponent>();

            enemySpawnComponent.SpawnTimer += deltaTime;

            if (enemySpawnComponent.SpawnTimer >= enemySpawnComponent.SpawnerData.SpawnTime)
            {
                enemySpawnComponent.SpawnTimer = 0f;

                SpawnWave(enemySpawnComponent);
            }
        }
    }

    private void SpawnWave(EnemySpawnComponent aSpawner)
    {
        for (int i = 0; i < aSpawner.SpawnerData.SpawnWaveAmount; i++)
        {
            SpawnEnemy(aSpawner);
        }
    }
    
    private void SpawnEnemy(EnemySpawnComponent aSpawner)
    {
        ObjectFactory.CreateObject(GetEnemyToSpawn(aSpawner.SpawnerData),
            GetPositionToSpawn(aSpawner.SpawnerData), aSpawner.SpawnerTransform);
    }
    
    private GameObject GetEnemyToSpawn(SpawnerData aSpawnData)
    {
        return aSpawnData.enemyPrefabs[Random.Range(0, aSpawnData.enemyPrefabs.Length)];
    }

    private Vector3 GetPositionToSpawn(SpawnerData aSpawnData)
    {
        ref var playerTag = ref _playerFilter.First().GetComponent<PlayerTagComponent>();

        float angle = Random.Range(0f, 2f * Mathf.PI);
        
        float x = aSpawnData.SpawnRadius * Mathf.Cos(angle);
        float z = aSpawnData.SpawnRadius * Mathf.Sin(angle);
        
        return playerTag.Transform.position + new Vector3(x, 0, z);;
    }
}