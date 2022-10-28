using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //
    //Variable
    //

    public static UnitManager Instance;
    public UnitScript _unitSelected;
    [SerializeField] private List<GameObject> _herosUnit;
    [SerializeField] private int _herosCount = 1;
    [SerializeField] private List<GameObject> _ennemisUnit;
    [SerializeField] private int _ennemisCount = 0;
    private List<UnitScript> _herosInTheGrid = new();
    private List<UnitScript> _ennemisInTheGrid = new();

    //
    //GETTER AND SETTER
    //

    //
    //MONOBEHAVIOUR
    //

    private void Awake()
    {
        Instance = this;
    }

    //
    //FONCTION
    //

    /// <summary>
    /// Spawn The Heros
    /// </summary>
    public void SpawnHeros()
    {
        DestoyAllHeros();
        RemoveUnitSelectioned();
        for (int o = 0; o < _herosCount; o++)
        {
            var listOfSpawnedTile = GridManager.Instance.GetSpawnableTile();
            var spawnedHero = Instantiate(_herosUnit[0]);
            var randomTile = listOfSpawnedTile[Mathf.Abs(Random.Range(0, listOfSpawnedTile.Count))];
            var script = spawnedHero.GetComponent<UnitScript>();
            spawnedHero.transform.position = randomTile.transform.position + script.Offset;
            script._tileOccupied = randomTile;
            script.SetSortingOrder(randomTile.SpriteRenderer.sortingOrder);
            _herosInTheGrid.Add(spawnedHero.GetComponent<UnitScript>());
        }
    }
    public void DestoyAllHeros()
    {
        foreach (UnitScript hero in _herosInTheGrid)
        {
            Destroy(hero.gameObject);
        }
        _herosInTheGrid = new List<UnitScript>();
    }
    public void SpawnEnnemis()
    {
        DestroyAllEnnemis();
        RemoveUnitSelectioned();
        for (int o = 0; o < _ennemisCount; o++)
        {
            var listOfSpawnedTile = GridManager.Instance.GetSpawnableTile();
            var spawnedEnnemi = Instantiate(_ennemisUnit[0]);
            var randomTile = listOfSpawnedTile[Mathf.Abs(Random.Range(0, listOfSpawnedTile.Count))];
            var script = spawnedEnnemi.GetComponent<UnitScript>();
            spawnedEnnemi.transform.position = randomTile.transform.position + script.Offset;
            script._tileOccupied = randomTile;
            script.SetSortingOrder(randomTile.SpriteRenderer.sortingOrder);
            _ennemisInTheGrid.Add(spawnedEnnemi.GetComponent<UnitScript>());
        }
    }
    public void DestroyAllEnnemis()
    {
        foreach (UnitScript ennemi in _ennemisInTheGrid)
        {
            Destroy(ennemi.gameObject);
        }
        _ennemisInTheGrid = new List<UnitScript>();
    }
    public bool CanUnitBAttackUnitC(UnitScript unitB, UnitScript unitC)
    {
        var tileB = unitB._tileOccupied;
        var tileC = unitC._tileOccupied;
        var Distance = PathFinderManager.Instance.CalculateDistanceCost(tileB, tileC);
        if (Distance <= 40) return true;
        else return false;
    }
    private void RemoveUnitSelectioned()
    {
        if (_unitSelected != null) _unitSelected.DeSelection();
    }
}
