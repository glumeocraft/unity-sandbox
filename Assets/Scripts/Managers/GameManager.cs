using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //UpdateGameState(GameState.SetUp);
        UpdateGameState(GameState.GenerateWorld);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SetUp:
                break;
            case GameState.GenerateWorld:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnUnits:
                Debug.Log("Game state update to spawn units started");
                UnitManager.Instance.SpawnTeam1();
                UnitManager.Instance.SpawnTeam2();
                UnitManager.Instance.SpawnTeam3();
                UpdateGameState(GameState.Move);
                break;
            case GameState.Move:
                break;
            case GameState.Fight:
                break;
        }

        Debug.Log($"Updated game state to {newState}");
        //only call if anything is subscribed to the event
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        SetUp,
        GenerateWorld,
        SpawnUnits,
        Move,
        Fight
    }
}
