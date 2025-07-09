using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player pool")]
    [SerializeField] private GameObject playersPool;
    [Space]
    [SerializeField] private GameObject foog;
    
    private Player currentPlayer;
    private Queue<Player> players = new Queue<Player>();
    private bool isActive = false;
    private uint actionPoints;
    public delegate void ChangeActionPoint(uint value);
    public event ChangeActionPoint OnChangeActionPoint;

    public bool IsActive { get => isActive; private set => isActive = value; }
    public Player CurrentPlayer { get => currentPlayer; }
    public uint ActionPoints
    {
        get => actionPoints;
        set { actionPoints = value; if (OnChangeActionPoint != null) OnChangeActionPoint(value); }
    }

    void Start()
    {
        StartTurn();
    }

    // Update is called once per frame
    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void FinishAction()
    {
        IsActive = true;
        if (ActionPoints == 0)
        {
            NextPlayer();
        }
    }
    public void NextPlayer()
    {
        if (players.Count > 0)
        {
            currentPlayer = players.Dequeue();
            currentPlayer.Init();
            ActionPoints = currentPlayer.ActionsPoints;
            Debug.Log("Start action for " + currentPlayer);
        }
        else
        {
            Debug.Log("All players finish action. Zombie turn");
            StartCoroutine(EndTurnCorutine());
        }
    }

    public void StartAction()
    {
        IsActive = false;
        ActionPoints--;
    }


    private IEnumerator EndTurnCorutine()
    {
        foog.SetActive(true);
        IsActive = false;
        yield return new WaitForSeconds(10);
        foog.SetActive(false);
        IsActive = true;
        StartTurn();
    }

    private void StartTurn()
    {
        if (players.Count == 0)
        {
            foreach (Player player in playersPool.GetComponentsInChildren<Player>())
            {
                players.Enqueue(player);
            }
            Debug.Log("Player initialization finished: " + players.Count);
        }
        else
        {
            Debug.LogError("Players queue not empty: " + players.Count);
        }
        NextPlayer();
        IsActive = true;
    }   
}
