using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    [SerializeField]
    private Player player;
    [SerializeField]
    private List<Character> enemies = new List<Character>();


    private bool m_isPlayerTurn = true;
    private bool m_isCombatActive = true;

    public static CombatManager instance;



    public Player Player { get => player; set => player = value; }
    public List<Character> Enemies { get => enemies; set => enemies = value; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(StartCombat());
    }


    private IEnumerator StartCombat()
    {
        yield return Initialize();
        yield return Combat();
    }

    private IEnumerator Initialize()
    {
        yield return player.Initialize();
        foreach (var enemy in enemies)
        {
            yield return enemy.Initialize();
        }

    }

    private IEnumerator Combat()
    {
        while (m_isCombatActive)
        {
            StartCoroutine(enemies[0].Draw());
            yield return player.Draw();


            yield return PlayerTurn();
            yield return new WaitForSeconds(0.5f);


            yield return EnemyTurn();
            yield return new WaitForSeconds(0.5f);

            yield return enemies[0].Draw();
        }
    }

    public void EndTurn()
    {
        m_isPlayerTurn = false;
    }

    private IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(0.5f);
        m_isPlayerTurn = true;

        yield return player.StartTurnCoroutine();




        yield return new WaitUntil(() => !m_isPlayerTurn);

        yield return player.EndTurnCoroutine();
    }

    private IEnumerator EnemyTurn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            yield return enemies[i].StartTurnCoroutine();

            yield return enemies[i].Turn();

            yield return enemies[i].EndTurnCoroutine();



        }


    }

    public void CheckEnemyList()
    {
        if (enemies.Count == 0)
        {
            m_isCombatActive = false;
            ShowRewardScreen();
        }
    }

    private void ShowRewardScreen()
    {
        PopUpManager.Instance.ShowRewardPopup();
    }
}
