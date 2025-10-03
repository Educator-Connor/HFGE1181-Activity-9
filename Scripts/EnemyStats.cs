using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyStats : ScriptableObject
{
    public Stat[] stats;
}
