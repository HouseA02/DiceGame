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
        rerolls = 3;
        enemyRolled = false;
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
        rerollText.text = new string($"Rerolls: {rerolls}");
        canRoll = true;
        isPlayerTurn = true;
    }
}
