using Anthill.Inject;
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
    private SpawnerData _spawnerData;


    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<EnemySpawnSystem>(this);
        _spawnerFilter = this.World.Filter.With<EnemySpawnComponent>().Build();
        _playerFilter = this.World.Filter.With<PlayerTagComponent>().Build();
        _spawnerData = GameSettings.GetReference<SpawnerData>();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;


        foreach (var entity in _spawnerFilter)
        {
            ref var enemySpawnComponent = ref entity.GetComponent<EnemySpawnComponent>();

            if (!enemySpawnComponent.IsInitialized)
            {
                enemySpawnComponent.SpawnTimer = _spawnerData.SpawnTime;
                enemySpawnComponent.IsInitialized = true;
                return;
            }


            enemySpawnComponent.SpawnTimer += deltaTime;
            if (enemySpawnComponent.SpawnTimer >= _spawnerData.SpawnTime / _spawnerData.SpawnWaveAmount)
            {
                enemySpawnComponent.SpawnTimer = 0f;
                enemySpawnComponent.SpawnedUnitsAmount++;
                SpawnEnemy(enemySpawnComponent);
            }


            if (enemySpawnComponent.SpawnedUnitsAmount >= _spawnerData.SpawnWaveAmount)
            {
                enemySpawnComponent.SpawnTimer = 0f;
                enemySpawnComponent.SpawnedUnitsAmount = 0;
            }
        }
    }

    private void SpawnWave(EnemySpawnComponent aSpawner)
    {
        for (int i = 0; i < _spawnerData.SpawnWaveAmount; i++)
        {
            SpawnEnemy(aSpawner);
        }
    }

    private void SpawnEnemy(EnemySpawnComponent aSpawner)
    {
        ObjectFactory.CreateObject(GetEnemyToSpawn(_spawnerData), aSpawner.SpawnerTransform,
            GetPositionToSpawn(_spawnerData));
    }

    private GameObject GetEnemyToSpawn(SpawnerData aSpawnData)
    {
        return aSpawnData.enemyPrefabs[Random.Range(0, aSpawnData.enemyPrefabs.Length)];
    }

    private Vector3 GetPositionToSpawn(SpawnerData aSpawnData)
    {
        ref var transformComponent = ref _playerFilter.First().GetComponent<TransformComponent>();

        float angle = Random.Range(0f, 2f * Mathf.PI);

        float x = aSpawnData.SpawnRadius * Mathf.Cos(angle);
        float z = aSpawnData.SpawnRadius * Mathf.Sin(angle);

        return transformComponent.Transform.position + new Vector3(x, 0, z);
        ;
    }
}