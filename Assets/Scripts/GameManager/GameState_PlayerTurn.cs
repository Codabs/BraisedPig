using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_PlayerTurn : GameState
{
    public GameState_PlayerTurn (GameManager _currentContext, GameState_Factory _factory) : base (_currentContext, _factory)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("It's Player Turn");
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {

    }
    public override void CheckStateChange()
    {

    }
    public override void InitializeSubState()
    {
        if(UnitManager.Instance)
        if(UnitManager.Instance._unitSelected == null)
        {
            SetSubState(_states.NoAction());
        }
        else
        {
            SetSubState(_states.UnitChose());
        }
    }
    public override void AUnitIsClick(UnitScript unitScript)
    {

    }
    public override void ATileIsClick(BaseTile baseTile)
    {

    }
}
