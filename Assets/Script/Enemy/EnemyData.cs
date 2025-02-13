using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Enemy enemyID;

    public int hp;
    public float speed;
    public int damage;
    public int range;
    public float attackPeriod;

    public GameObject enemyPrefab;
    public Sprite IdleSprite;
}