using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Compendium compendium;
    [SerializeField]
    private LootManager lootManager;
    [SerializeField]
    private Tutorial tutorial;
    public GameObject mapKey;
    [SerializeField]
    private LootScreen lootScreen;
    [SerializeField]
    private Button endTurn;
    [SerializeField]
    private StoryController storyController;
    public EventArenaController eventArenaController;
    [SerializeField]
    public CombatData combatData;
    [SerializeField]
    public Transform dieOrigin;
    [SerializeField]
    public Camera battleCamera;
    [SerializeField]
    GameObject battleLight;
    [SerializeField]
    GameObject[] battleUI;
    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    GameObject loseScreen;
    [SerializeField]
    public Player player;
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
    int thisTurnRolls = 0;
    public bool inBattle = false;
    [SerializeField]
    public int rerolls;
    [SerializeField]
    TMP_Text rerollText;
    public UnityEvent gm_OnBattleStart = new UnityEvent();
    public UnityEvent gm_OnTurnStart = new UnityEvent();
    public UnityEvent gm_OnTurnEnd = new UnityEvent();
    public UnityEvent gm_OnRoll = new UnityEvent();
    public UnityEvent gm_OnReroll = new UnityEvent();
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject webs;

    private void Start()
    {
        Initialise();
    }

    public void TryRoll()
    {
        if (canRoll)
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
            if (FindObjectsOfType<Entangle>().Length > 0)
            {
                webs.SetActive(true);
            }
            else
            {
                webs.SetActive(false);
            }
            gm_OnRoll.Invoke();
            if(thisTurnRolls > 0)
            {
                gm_OnReroll.Invoke();
            }
            thisTurnRolls++;
        }
        if (!enemyRolled)
        {
            foreach (Character character in activeEnemies.Where(e => e.isDead == false))
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
        thisTurnRolls = 0;
        activeHeroes.ForEach(h => h.GetComponent<Hero>().hasActed = false);
        if(FindObjectsOfType<Entangle>().Length > 0)
        {
            webs.SetActive(true);
        }
        else
        {
            webs.SetActive(false);
        }
        Roll();
    }

    public void OnTurnEnd()
    {
        canRoll = false;
    }

    public void StartTurn()
    {
        OnTurnStart();
        gm_OnTurnStart.Invoke();
        //activeHeroes.ForEach(h => h.OnTurnStart());
        //activeEnemies.ForEach(e => e.OnTurnEnd());
    }
    public void EndTurn()
    {
        endTurn.enabled = false;
        gm_OnTurnEnd.Invoke();
        OnTurnEnd();
        //activeHeroes.ForEach(h  => h.OnTurnEnd());
        StartCoroutine(EnemyAct());
        //activeEnemies.ForEach(e => e.OnTurnEnd());
        //StartTurn();
    }

    public IEnumerator EnemyAct()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i] != null)
            {
                activeEnemies[i].GetComponent<Enemy>().TakeAction();
                yield return new WaitForSeconds(0.5f);
            }
        }
        activeEnemies.ForEach(e => e.GetComponent<Enemy>().UpdateTarget(null));
        endTurn.enabled = true;
        /*foreach(Enemy enemy in activeEnemies)
        {
            if (enemy != null)
            {
                enemy.TakeAction();
                yield return new WaitForSeconds(0.5f);
            }
        }*/
        StartTurn();
    }

    public void OnDeath(Character character)
    {
        /*
        if (activeEnemies.Contains(character))
        {
            activeEnemies.Remove(character);
        }
        if (activeHeroes.Contains(character))
        {
            activeHeroes.Remove(character);
        }*/
        foreach (Character hero in activeHeroes)
        {
            hero.allies = new List<Character>();
            hero.allies.AddRange(activeHeroes.Where(h => h.isDead == false));
            hero.allies.Remove(hero);
            hero.enemies = new List<Character>();
            hero.enemies.AddRange(activeEnemies.Where(e => e.isDead == false));
        }
        foreach (Character enemy in activeEnemies)
        {
            enemy.allies = new List<Character>();
            enemy.allies.AddRange(activeEnemies.Where(e => e.isDead == false));
            enemy.allies.Remove(enemy);
            enemy.enemies = new List<Character>();
            enemy.enemies.AddRange(activeHeroes.Where(e => e.isDead == false));
        }
        if (!activeEnemies.Where(e => e.isDead == false).Any())
        {
            Debug.Log("Win");
            activeHeroes.ForEach(h => h.Cleanse());
            activeHeroes.ForEach(h => h.CleanUp());
            StartCoroutine(CombatWin());
        }
        else if (activeHeroes.Count <= 0)
        {
            Debug.Log("Lose");
            mainCamera.SetActive(true);
            inBattle = false;
            loseScreen.SetActive(true);
        }
    }

    IEnumerator CombatWin()
    {
        yield return new WaitForSeconds(1);
        activeHeroes.ForEach(h => h.ResetAbilities());
        if (FindAnyObjectByType<MapController>().GetComponent<MapController>().mapTime == 14)
        {
            winScreen.SetActive(true);
        }
        else
        {
            lootScreen.gameObject.SetActive(true);
            lootScreen.FindLoot(combatData);
        }
    }

    public void ReturnToMap()
    {
        if (tutorial.isComplete)
        {
            tutorial.currentFunc = 0;
            tutorial.nextMapFunc();
        }
        while(activeEnemies.Count > 0)
        {
            Destroy(activeEnemies[0].gameObject);
            activeEnemies.Remove(activeEnemies[0]);
        }
        foreach (GameObject element in battleUI)
        {
            element.SetActive(false);
        }
        mapKey.SetActive(true);
        mainCamera.SetActive(true);
        inBattle = false;
        lootScreen.gameObject.SetActive(false);
        foreach(CharacterPanel panel in characterPanelsEnemy)
        {
            panel.gameObject.SetActive(false);
        }
        SaveManager.SaveData(player);
    }
    
    public void StartCombat(CombatData combatData)
    {
        foreach(var item in FindObjectsOfType<AbilityDie>())
        {
            Destroy(item);
        }
        mapKey.SetActive(false);
        mainCamera.SetActive(false);
        combatData.GetData(this);
        this.combatData = combatData;
        activeEnemies.Clear();
        foreach (CharacterPanel panel in characterPanelsEnemy)
        {
            panel.gameObject.SetActive(false);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                ///
                if (!compendium.enemies.Contains(enemies[i]))
                {
                    compendium.AddEnemy(enemies[i].GetComponent<Enemy>());
                }
                Character enemyInstance = Instantiate(enemies[i]);
                enemyInstance.characterPanel = characterPanelsEnemy[i];
                enemyInstance.transform.position = enemyPositions[i].position;
                enemyInstance.transform.rotation = enemyPositions[i].rotation;
                enemyInstance.gameManager = this;
                enemyInstance.id = i;
                activeEnemies.Add(enemyInstance);
                enemyInstance.Initialise(enemyInstance);
            }
            else
            {
                characterPanelsEnemy[i].gameObject.SetActive(false);
            }
        }
        foreach (Character hero in activeHeroes)
        {
            hero.allies = new List<Character>();
            hero.allies.AddRange(activeHeroes);
            hero.allies.Remove(hero);
            hero.enemies = new List<Character>();
            hero.enemies.AddRange(activeEnemies);
            hero.SetResetAbilites();
        }
        foreach (Character enemy in activeEnemies)
        {
            enemy.allies = new List<Character>();
            enemy.allies.AddRange(activeEnemies);
            enemy.allies.Remove(enemy);
            enemy.enemies = new List<Character>();
            enemy.enemies.AddRange(activeHeroes);
            enemy.SetResetAbilites();
        }
        battleLight.SetActive(true);
        foreach (GameObject element in battleUI)
        {
            element.SetActive(true);
        }
        inBattle = true;
        gm_OnBattleStart.Invoke();
        if (!tutorial.isComplete)
        {
            StartTurn();
        }
        rerollText.text = new string($"Rerolls: {rerolls}");
        canRoll = true;
        DelayedStartCombat();
    }

    void DelayedStartCombat()
    {
        foreach (CharacterPanel panel in characterPanelsEnemy.Where(c => c.character != null))
        {
            panel.gameObject.SetActive(true);
            panel.Initialise(panel.character);
        }
        foreach (CharacterPanel panel in characterPanelsEnemy.Where(c => c.character == null))
        {
            panel.gameObject.SetActive(false);
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
            heroInstance.id = i;
            activeHeroes.Add(heroInstance);
            heroInstance.Initialise(heroInstance);
        }
        player.gameManager = this;
        activeHeroes.ForEach(hero => lootManager.AddFacePool(hero.GetComponent<Hero>().m_Class[0]));
    }
}
