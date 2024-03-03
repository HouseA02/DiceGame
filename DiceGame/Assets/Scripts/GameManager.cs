using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CharacterPanel[] characterPanelsFriendly;
    [SerializeField]
    CharacterPanel[] characterPanelsEnemy;
    [SerializeField]
    Transform[] heroPositions;
    [SerializeField]
    Transform[] enemyPositions;
    [SerializeField]
    public List<Character> heroes = new List<Character>();
    [SerializeField]
    public List<Character> enemies = new List<Character>();
    [SerializeField]
    public List<Character> activeHeroes = new List<Character>();
    [SerializeField]
    public List<Character> activeEnemies = new List<Character>();
    bool isPlayerTurn;
    bool canRoll;
    bool enemyRolled;
    [SerializeField]
    int rerolls;
    [SerializeField]
    TMP_Text rerollText;

    private void Start()
    {
        Initialise();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canRoll)
        {
            Roll();
        }
    }

    public void Roll()
    {
        if (rerolls > 0)
        {
            foreach (Character character in activeHeroes)
            {
                character.Roll();
            }
            rerolls--;
            rerollText.text = new string($"Rerolls: {rerolls}");
        }
        if (!enemyRolled)
        {
            foreach (Character character in activeEnemies)
            {
                character.Roll();
            }
            enemyRolled = true;
        }
    }

    private void OnTurnStart()
    {
        canRoll = true;
        rerolls = 3;
        enemyRolled = false;
        Roll();
    }

    public void OnTurnEnd()
    {
        canRoll = false;
    }

    void StartTurn()
    {
        activeHeroes.ForEach(h => h.OnTurnStart());
        activeEnemies.ForEach(e => e.OnTurnEnd());
        OnTurnStart();
    }
    public void EndTurn()
    {
        OnTurnEnd();
        activeHeroes.ForEach(h  => h.OnTurnEnd());
        activeEnemies.ForEach(e => e.OnTurnEnd());
        StartTurn();
    }

    public void OnDeath(Character character)
    {
        if (activeEnemies.Contains(character))
        {
            activeEnemies.Remove(character);
        }
        if(activeHeroes.Contains(character))
        {
            activeHeroes.Remove(character);
        }
    }
    void Initialise()
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            Character heroInstance = Instantiate(heroes[i]);
            heroInstance.characterPanel = characterPanelsFriendly[i];
            heroInstance.transform.position = heroPositions[i].position;
            heroInstance.transform.rotation = heroPositions[i].rotation;
            heroInstance.gameManager = this;
            activeHeroes.Add(heroInstance);
            heroInstance.Initialise(heroInstance);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            Character enemyInstance = Instantiate(enemies[i]);
            enemyInstance.characterPanel = characterPanelsEnemy[i];
            enemyInstance.transform.position = enemyPositions[i].position;
            enemyInstance.transform.rotation = enemyPositions[i].rotation;
            enemyInstance.gameManager = this;
            activeEnemies.Add(enemyInstance);
            enemyInstance.Initialise(enemyInstance);
        }
        foreach(Character hero in activeHeroes) 
        {
            hero.allies = new List<Character>();
            hero.allies.AddRange(activeHeroes);
            hero.allies.Remove(hero);
            hero.enemies = new List<Character>();
            hero.enemies.AddRange(activeEnemies);
        }
        foreach (Character enemy in activeEnemies)
        {
            enemy.allies = new List<Character>();
            enemy.allies.AddRange(activeEnemies);
            enemy.allies.Remove(enemy);
            enemy.enemies = new List<Character>();
            enemy.enemies.AddRange(activeHeroes);
        }
        StartTurn();
        rerollText.text = new string($"Rerolls: {rerolls}");
        canRoll = true;
        isPlayerTurn = true;
    }
}
