using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public EnemyData enemy;
    public int amount;
    public XPos xPos;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public List<SpawnData> spawnData;
}
