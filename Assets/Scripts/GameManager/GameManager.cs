using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //
    //VARIABLE
    //

    public static GameManager Instance;
    public GameState _currentGameState;
    public GameState_Factory _states;

    //Units Actions
    public UnitScript unit;
    public BaseTile tile;
    public List<BaseTile> path;
    //

    //
    //MONOBEHAVIOUR
    //

    private void Start()
    {
        //Manager Permanent
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        //Create all other states
        _states = new GameState_Factory(this);

        //First State
        _currentGameState = _states.StartGame();

        _currentGameState.EnterState();
    }

    private void Update()
    {
        _currentGameState.UpdateStates();
    }

    //
    //FONCTION
    //

    public void PlayerClickOnThisUnit(UnitScript unitClick)
    {
        _currentGameState.SubState.AUnitIsClick(unitClick);
    }
    public void ShowPathToTile(List<BaseTile> path)
    {
        //On active outline pour monter le chemin que va prendre l'uniter
        foreach(BaseTile tile in path)
        {
            tile._outline.SetActive(true);
        }
    }
    public void PlayerClickOnThisTile(BaseTile tileClick)
    {
        _currentGameState.SubState.ATileIsClick(tileClick);
    }
    public void MoveUnit(UnitScript unit, List<BaseTile> path)
    {
        StartCoroutine(unit.MoveUnitInAPath(path));
    }
}
