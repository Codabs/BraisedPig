using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState_StartGame : GameState
{
    public GameState_StartGame(GameManager _currentContext, GameState_Factory _states) : base(_currentContext, _states)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        Debug.Log("The Game has Started");
        //On génére un Grid
        GridManager.Instance.GenerateGrid();
        //On Ajout Les Héros
        UnitManager.Instance.SpawnHeros();
        //On Ajout les Ennemis
        UnitManager.Instance.SpawnEnnemis();
        //On Donne le Tours au Joueur
        SwitchState(_states.PlayerTurn());
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {
        Debug.Log("Lancement de la partie fini");
    }
    public override void CheckStateChange()
    {

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
