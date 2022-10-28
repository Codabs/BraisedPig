using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_NoAction : GameState
{
    public GameState_NoAction(GameManager _currentContext, GameState_Factory _factory) : base(_currentContext, _factory)
    {
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("The Player Is Doing Nothing");
    }
    public override void UpdateState()
    {
    }
    public override void ExitState()
    {
        Debug.Log("The Player Chose A Unit");
    }
    public override void CheckStateChange()
    {
    }
    public override void InitializeSubState()
    {

    }
    public override void AUnitIsClick(UnitScript unitScript)
    {
        //if (unitScript.Faction == "hero" && _ctx.currentState == _states.ItsPlayerTurn())
        //{
            UnitManager.Instance._unitSelected = unitScript;
            unitScript.Selection();
            SwitchState(_states.UnitChose());
        //}
    }
    public override void ATileIsClick(BaseTile baseTile)
    {

    }
}
