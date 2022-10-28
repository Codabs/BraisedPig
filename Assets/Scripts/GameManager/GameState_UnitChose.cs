using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_UnitChose : GameState
{

    //
    //VARIABLE
    //

    //
    //FONCTION PUBLIC
    //
    public GameState_UnitChose(GameManager _currentContext, GameState_Factory _factory) : base(_currentContext, _factory)
    {
        InitializeSubState();
    }
    public override void EnterState()
    {
        //Change Camera Settings
        //Centrer sur l'unité Selectioner

        //Change le mode de la cam

        Debug.Log("It's Player Turn");

    }
    public override void UpdateState()
    {
        SetPath();
        GridManager.Instance.ErasedOutLineTile();
        if (_ctx.path != null)
            _ctx.ShowPathToTile(_ctx.path);
    }
    public override void ExitState()
    {
        Debug.Log("The Unit Is Doing Somethings");
    }
    public override void CheckStateChange()
    {

    }
    public override void InitializeSubState()
    {

    }
    public override void AUnitIsClick(UnitScript unitScript)
    {
        //De quel faction est cette Unité 
        string faction = unitScript.Faction;
        //Est-ce une faction allié ?
        if (UnitManager.Instance._unitSelected.Faction == faction)
        {
            //Changer l'unit selectioner
            UnitManager.Instance._unitSelected.DeSelection();
            UnitManager.Instance._unitSelected = unitScript;
            unitScript.Selection();
        }
        else
        {
            //Can I Attaque this ennemi
            if (UnitManager.Instance.CanUnitBAttackUnitC(UnitManager.Instance._unitSelected, unitScript))
            {
                //if yes, attaque the ennemis
                _ctx._unitAttacked = unitScript;
                Debug.Log("SwitchState(_states.StartGame())");
            }
        }
    }
    public override void ATileIsClick(BaseTile baseTile)
    {
        SetPath();
        if (_ctx.path == null) return;
        SwitchState(_states.UnitIsMoving());
    }

    //
    //PRIVATE FONCTION
    //
    private void SetPath()
    {
        _ctx.unit = UnitManager.Instance._unitSelected;
        _ctx.tile = GridManager.Instance.TileSelectioned;
        _ctx.path = PathFinderManager.Instance.PathFinding(_ctx.unit._tileOccupied, _ctx.tile);
    }
}
