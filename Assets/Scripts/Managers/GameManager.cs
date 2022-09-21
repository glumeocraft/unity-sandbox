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
                MenuManager.Instance.EnableEndMoveButton();
                break;
            case GameState.Fight:
                MenuManager.Instance.DisableEndMoveButton();
                break;
        }

        Debug.Log($"Updated game state to {newState}");
        //only call if anything is subscribed to the event
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        SetUp = 1,
        GenerateWorld = 2,
        SpawnUnits = 3,
        Move = 4,
        Fight = 5
    }

    public void EndMove()
    {
        UpdateGameState(GameState.Fight);
    }

    public void Restart()
    {
        GridManager.Instance.DestoryGrid();
        UpdateGameState(GameState.GenerateWorld);
    }
}
