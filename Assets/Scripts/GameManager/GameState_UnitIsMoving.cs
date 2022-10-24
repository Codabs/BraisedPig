using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_UnitIsMoving : GameState
{
    public GameState_UnitIsMoving(GameManager _currentContext, GameState_Factory _factory) : base(_currentContext, _factory)
    {
        InitializeSubState();
    }
    public override void EnterState()
    {
        //Camera focus the unit
        _ctx.MoveUnit(_ctx.unit, _ctx.path);
        Debug.Log("A Unit Is Moving");
    }
    public override void UpdateState()
    {
        CheckStateChange();
    }
    public override void ExitState()
    {
        Debug.Log("A Unit has finised of moving");
        //Camera unfocus the unit
    }
    public override void CheckStateChange()
    {
        if(_ctx.unit._tileOccupied == _ctx.tile)
        {
            _ctx.unit.DeSelection();
            SwitchState(_states.NoAction());
        }
    }
    public override void InitializeSubState()
    {

    }
    public override void AUnitIsClick(UnitScript unitScript)
    {

    }
    public override void ATileIsClick(BaseTile baseTile)
    {

    }
}
