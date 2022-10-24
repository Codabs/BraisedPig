using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PathFinderManager : MonoBehaviour
{
    //
    //Variable
    //

    [SerializeField] private int Move_Diagonal_Cost;
    [SerializeField] private int Move_Forward_Cost;
    public static PathFinderManager Instance;
    [SerializeField] private BaseTile _targetTile;
    private List<BaseTile> _openList;
    private List<BaseTile> _closedList;

    //
    //GETTER AND SETTER
    //

    //public BaseTile TargetTile { set { _targetTile = value; } }

    //
    //MONOBEHAVIOUR
    //

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (BaseTile tile in PathFinding(UnitManager.Instance._unitSelected._tileOccupied, _targetTile))
            {
                if (tile == null) continue;
                tile.ShowPathFinding();
            }
        }
    }

    //
    //Fonction PUBLIC
    //

    public List<BaseTile> PathFinding(BaseTile startTile, BaseTile endTile)
    {
        if (endTile.IsWalkable == false) return null;
        _openList = new List<BaseTile> { startTile };
        _closedList = new List<BaseTile>();
        foreach (BaseTile _tile in GridManager.Instance.Tiles.Values)
        {
            if (_tile != null)
            {
                //Reset la grid
                _tile.G_Cost = int.MaxValue;
                _tile.CalculateFCost();
                _tile._cameFromTile = null;
            }
        }

        startTile.G_Cost = 0;
        startTile.H_Cost = CalculateDistanceCost(startTile, endTile);
        startTile.CalculateFCost();

        while (_openList.Capacity > 0)
        {
            BaseTile _currentTile = GetLowestFCostNode(_openList);
            if(_currentTile == endTile)
            {
                //This is the End
                return CalculatePath(endTile);
            }

            _openList.Remove(_currentTile);
            _closedList.Add(_currentTile);

            foreach (BaseTile neighborTile in GridManager.Instance.GetNeighborTiles(_currentTile))
            {
                if (_closedList.Contains(neighborTile)) continue;
                if (!neighborTile.IsWalkable)
                {
                    _closedList.Add(neighborTile);
                    continue;
                }
                float tentativeGCost = CalculateDistanceCost(_currentTile, neighborTile);
                if(tentativeGCost < neighborTile.G_Cost)
                {
                    neighborTile._cameFromTile = _currentTile;
                    neighborTile.G_Cost = tentativeGCost;
                    neighborTile.H_Cost = CalculateDistanceCost(neighborTile, endTile);
                    neighborTile.CalculateFCost();
                    if(!_openList.Contains(neighborTile))
                    {
                        _openList.Add(neighborTile);
                    }
                }

            }
        }
        //Theirs is no Path
        Debug.Log("NoPath");
        return null;
    }
    private int CalculateDistanceCost(BaseTile a, BaseTile b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remining = Mathf.Abs(xDistance - yDistance);
        return Move_Diagonal_Cost * Mathf.Min(xDistance, yDistance) + Move_Forward_Cost * remining;
    }
    private BaseTile GetLowestFCostNode(List<BaseTile> _tileList)
    {
        if (_tileList == null) return null;
        BaseTile lowestFCost = _tileList[0];
        if (_tileList.Count == 0 || _tileList.Count == -1) return lowestFCost;
        for (int i = 1; i < _tileList.Count; i++)
        {
            if (_tileList[i].F_Cost < lowestFCost.F_Cost)
            {
                lowestFCost = _tileList[i];
            }
        }
        return lowestFCost;
    }
    private List<BaseTile> CalculatePath(BaseTile _endTile)
    {
        List<BaseTile> path = new();
        path.Add(_endTile);
        BaseTile currentTile = _endTile;
        while(currentTile._cameFromTile != null)
        {
            path.Add(currentTile._cameFromTile);
            currentTile = currentTile._cameFromTile;
        }
        path.Reverse();
        return path;
    }
}
