using UnityEngine;


[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/SpawnerData", order = 1)]
public class SpawnerData : ScriptableObject
{
    public float SpawnRadius;
    public float SpawnTime = 1f;
    public int SpawnWaveAmount = 1;
    public GameObject[] enemyPrefabs;
    
}
