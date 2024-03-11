using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat", menuName = "ScriptableObjects/Combat", order = 1)]
public class CombatData : ScriptableObject
{
    public enum CombatType
    {
        Normal,
        Elite,
        Boss
    }
    [SerializeField]public CombatType type;

    [SerializeField]
    public List<Character> enemies;
    public void GetData(GameManager gameManager)
    {
        gameManager.enemies.Clear();
        gameManager.enemies.AddRange(enemies);
    }
}
