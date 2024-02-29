using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CharacterPanel[] characterPanelsFriendly;
    [SerializeField]
    CharacterPanel[] characterPanelsEnemy;
    [SerializeField]
    List<Character> heroes = new List<Character>();
    [SerializeField]
    List<Character> enemies = new List<Character>();
    bool isPlayerTurn;
    bool canRoll;

    private void Awake()
    {
        Initialise();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canRoll)
        {
            Roll();
        }
    }

    public void Roll()
    {
        foreach (Character character in heroes)
        {
            Debug.Log(character);
            character.Roll();
        }
    }
    void Initialise()
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            Debug.Log(i);
            characterPanelsFriendly[i].gameObject.SetActive(true);
            characterPanelsFriendly[i].Initialise(heroes[i]);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            characterPanelsEnemy[i].gameObject.SetActive(true);
            characterPanelsEnemy[i].Initialise(enemies[i]);
        }
        canRoll = true;
        isPlayerTurn = true;
    }
}
