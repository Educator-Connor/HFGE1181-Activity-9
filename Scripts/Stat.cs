using System;
using UnityEngine;

[Serializable]
public class Stat
{
    public string statName;
    public float statValue;

    public void PopulateStat(string statName, float statValue)
    {
        this.statName = statName;
        this.statValue = statValue;
    }
}
