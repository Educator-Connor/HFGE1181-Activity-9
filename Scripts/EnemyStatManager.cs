using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class EnemyStatManager : MonoBehaviour
{
    [SerializeField]
    private EnemyStats statsTemplate;

    [SerializeField]
    private Stat[] localStats;

    private void Awake()
    {
        localStats = new Stat[statsTemplate.stats.Length];
        StartCoroutine(PopulateStats());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    public IEnumerator PopulateStats()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < statsTemplate.stats.Length; i++)
        {
            string name = statsTemplate.stats[i].statName;
            float value = statsTemplate.stats[i].statValue;
            localStats[i].statName = name;
            localStats[i].statValue = value;
        }
    }

    public Stat GetStat(string statChecker)
    {
        foreach(var item in localStats)
        {
            if (item.statName == statChecker)
            {
                return item;
            }
        }
        return null;
    }
}
