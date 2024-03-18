using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compendium : MonoBehaviour
{
    public List<Enemy> enemies;
    public List<StatusEffect> statuses;
    [SerializeField]
    private EnemyEntry p_enemyEntry;
    [SerializeField]
    private StatusEntry p_statusEntry;
    [SerializeField]
    private Transform enemyListParent;
    [SerializeField]
    private Transform statusListParent;

    public void AddEnemy(Enemy enemy)
    {
        //enemies.Add(enemy);
        EnemyEntry newEntry = Instantiate(p_enemyEntry, Vector2.zero, Quaternion.identity, enemyListParent);
        var rt = enemyListParent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 400);
        rt.transform.position -= new Vector3(0, 400f, 0);
        newEntry.Initialise(enemy);
    }

    public void AddStatus(StatusEffect statusEffect)
    {
        StatusEntry newEntry = Instantiate(p_statusEntry, Vector2.zero, Quaternion.identity, statusListParent);
        var rt = statusListParent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 200);
        rt.transform.position -= new Vector3(0, 200f, 0);
        newEntry.Initialise(statusEffect);
    }

    private void Start()
    {
        foreach (StatusEffect statusEffect in statuses)
        {
            AddStatus(statusEffect);
        }
        foreach(Enemy enemy in enemies)
        {
            AddEnemy(enemy);
        }
    }
}
