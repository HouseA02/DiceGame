using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public EventArenaController eventArenaController;
    [SerializeField]
    public CombatData CombatData;
    [SerializeField]
    public Transform dieOrigin;
    [SerializeField]
    public Camera battleCamera;
    [SerializeField]
    GameObject battleLight;
    [SerializeField]
    GameObject[] battleUI;
    [SerializeField]
    Player player;
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
    [SerializeField]
    public List<Character> reinforcements = new List<Character>();
    bool canRoll;
    bool enemyRolled;
    public bool inBattle = false;
    [SerializeField]
    int rerolls;
    [SerializeField]
    TMP_Text rerollText;
    public UnityEvent gm_OnTurnStart = new UnityEvent();
    public UnityEvent gm_OnTurnEnd = new UnityEvent();
    [SerializeField]
    private GameObject mainCamera;

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
        activeHeroes.ForEach(h => h.isFirstRoll = true);
        Roll();
    }

    public void OnTurnEnd()
    {
        canRoll = false;
    }

    void StartTurn()
    {
        gm_OnTurnStart.Invoke();
        //activeHeroes.ForEach(h => h.OnTurnStart());
        //activeEnemies.ForEach(e => e.OnTurnEnd());
        OnTurnStart();
    }
    public void EndTurn()
    {
        gm_OnTurnEnd.Invoke();
        OnTurnEnd();
        //activeHeroes.ForEach(h  => h.OnTurnEnd());
        StartCoroutine(EnemyAct());
        //activeEnemies.ForEach(e => e.OnTurnEnd());
        //StartTurn();
    }

    public IEnumerator EnemyAct()
    {
        foreach(Enemy enemy in activeEnemies)
        {
            enemy.TakeAction();
            yield return new WaitForSeconds(0.5f);
        }
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
        foreach (Character hero in activeHeroes)
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
        if (activeEnemies.Count<=0)
        {
            Debug.Log("Win");
            inBattle = false;
            mainCamera.SetActive(true);
        }
        else if(activeHeroes.Count<=0)
        {
            Debug.Log("Lose");
            inBattle = false;
            mainCamera.SetActive(true);
        }
    }

    public void StartCombat(CombatEvent e)
    {
        mainCamera.SetActive(false);
        e.combatData.GetData(this);
        for (int i = 0; i < enemies.Count; i++)
        {
            Character enemyInstance = Instantiate(enemies[i]);
            enemyInstance.characterPanel = characterPanelsEnemy[i];
            enemyInstance.transform.position = enemyPositions[i].position;
            enemyInstance.transform.rotation = enemyPositions[i].rotation;
            enemyInstance.gameManager = this;
            enemyInstance.id = i;
            activeEnemies.Add(enemyInstance);
            enemyInstance.Initialise(enemyInstance);
            foreach (Character hero in activeHeroes)
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
            battleLight.SetActive(true);
        }
        foreach (GameObject element in battleUI)
        {
            element.SetActive(true);
        }
        inBattle = true;
        StartTurn();
        rerollText.text = new string($"Rerolls: {rerolls}");
        canRoll = true;
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
            heroInstance.id = i;
            activeHeroes.Add(heroInstance);
            heroInstance.Initialise(heroInstance);
        }
        player.gameManager = this;
        player.tempRelics.ForEach(r => player.AddRelic(r));
    }
}
