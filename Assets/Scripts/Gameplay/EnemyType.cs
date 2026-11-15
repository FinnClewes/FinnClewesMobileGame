using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy Type")]
public class EnemyType : ScriptableObject
{
    public string displayName = "Slime";
    public int maxHealth = 10;
    public int damage = 2;
    public int coinReward = 25;
    public Sprite sprite;
}