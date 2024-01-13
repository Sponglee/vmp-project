using UnityEngine;


[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class Settings : ScriptableObject
{
    public int MaxTargetSize = 100;
    public float DeathDelay = 1f;

    public float PlayerMoveSpeed = 1f;
}