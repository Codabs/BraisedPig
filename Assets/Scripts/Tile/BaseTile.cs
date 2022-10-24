using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using FMODUnity;


public abstract class BaseTile : MonoBehaviour
{
    //---------------
    //VARIABLE
    //----------------

    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] public GameObject _outline;
    [SerializeField] private bool _isWalkable;
    [SerializeField] private TextMeshPro G_Text;
    [SerializeField] private TextMeshPro H_Text;
    [SerializeField] private TextMeshPro F_Text;
    [SerializeField] private string _walkSoundFmodBank;
    //private readonly List<BaseTile> _pathToMe = new();

    public int x;
    public int y;

    //PathFinding A*
    [SerializeField] private float _gCost = 0;
    [SerializeField] private float _hCost = 0;
    [SerializeField] private float _fCost = 0;
    public BaseTile _cameFromTile;

    //----------------
    //GETTER AND SETTER
    //----------------

    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    public float G_Cost { get { return _gCost; } set { _gCost = value; } }
    public float H_Cost { get { return _hCost; } set { _hCost = value; } }
    public float F_Cost { get { return _fCost; } }
    public bool IsWalkable { get { return _isWalkable; } }

    //----------------
    //MONOBEHAVIOUR
    //----------------

    private void OnMouseEnter()
    {
        _outline.SetActive(true);
        GridManager.Instance.TileSelectioned = this;
    }

    private void OnMouseExit()
    {
        _outline.SetActive(false);
    }

    private void OnMouseDown()
    {
        GameManager.Instance.PlayerClickOnThisTile(this);
    }

    //------------------
    //PUBLIC FONCTION
    //------------------
    public virtual void Init(int x, int y)
    {
        //Fonction utiliser quand la tile est créée
    }

    [Button] public void ShowPathFinding()
    {
        G_Text.text = _gCost.ToString();
        H_Text.text = _hCost.ToString();
        F_Text.text = _fCost.ToString();
        spriteRenderer.color = Color.black;
    }

    public void CalculateFCost()
    {
        _fCost = _gCost + _hCost;
    }

    public void PlayWalkSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_walkSoundFmodBank);
    }
}
