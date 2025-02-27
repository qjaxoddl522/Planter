using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Enemy enemyID;

    public int hp;
    public float speed;
    public int damage;
    public float range;
    public float attackPeriod;

    public GameObject enemyPrefab;
    public Sprite idleSprite;
    public DescriptionData description;
}