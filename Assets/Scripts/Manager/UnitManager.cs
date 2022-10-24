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
    [SerializeField] private List<GameObject> _ennemisUnit;
    private readonly List<UnitScript> _herosUnits = new();
    private readonly List<UnitScript> _ennemisUnits = new();

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

    public void SpawnHeros()
    {
        int _herosCount = 1;

        for (int o = 0; o < _herosCount; o++)
        {
            var listOfSpawnedTile = GridManager.Instance.GetSpawnableTile();
            var spawnedHero = Instantiate(_herosUnit[0]);
            var randomTile = listOfSpawnedTile[Mathf.Abs(Random.Range(0, listOfSpawnedTile.Count))];
            var script = spawnedHero.GetComponent<UnitScript>();
            spawnedHero.transform.position = randomTile.transform.position + script.Offset;
            script._tileOccupied = randomTile;
            script.SetSortingOrder(randomTile.SpriteRenderer.sortingOrder);
            _herosUnits.Add(spawnedHero.GetComponent<UnitScript>());
        }
    }
    public void DestoyAllHeros()
    {
        foreach (UnitScript hero in _herosUnits)
        {
            _herosUnits.Remove(hero);
            Destroy(hero.gameObject);
        }
        //_herosUnits.RemoveAt();
    }
    public void SpawnEnnemis()
    {
        int _ennemisCount = 0;

        for (int o = 0; o < _ennemisCount; o++)
        {
            var listOfSpawnedTile = GridManager.Instance.GetSpawnableTile();
            if (listOfSpawnedTile == null) return;
            var spawnedEnnemi = Instantiate(_ennemisUnit[0]);
            var randomTile = listOfSpawnedTile[Mathf.Abs(Random.Range(0, listOfSpawnedTile.Count))];
            spawnedEnnemi.transform.position = randomTile.transform.position;
            spawnedEnnemi.GetComponent<UnitScript>()._tileOccupied = randomTile;
            _ennemisUnits.Add(spawnedEnnemi.GetComponent<UnitScript>());
        }
    }
}
