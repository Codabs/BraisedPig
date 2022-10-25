using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitScript : MonoBehaviour
{
    //
    //Variable
    //

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private GameObject _outline;
    [SerializeField] private GameObject _outlineSelected;
    [SerializeField] private TrailRenderer _trail;

    /*[SerializeField]*/
    public BaseTile _tileOccupied;

    [SerializeField] private int _numberOfActionParTurn;
    [SerializeField] private float _speed;
    private readonly bool _isMoving;
    protected string faction;

    [SerializeField] Vector3 _unitOffset;

    //Animation
    [SerializeField] private string _idleAnim;

    [SerializeField] private string _upLeftAnim;
    [SerializeField] private string _upAnim;
    [SerializeField] private string _upRightAnim;

    [SerializeField] private string _downLeftAnim;
    [SerializeField] private string _downAnim;
    [SerializeField] private string _downRightAnim;

    [SerializeField] private string _leftAnim;
    [SerializeField] private string _rightAnim;

    //
    //Getters and Setters
    //
    public bool IsMoving { get { return _isMoving; } }
    public string Faction { get { return faction; } }
    public Vector3 Offset { get { return _unitOffset; } }
    //
    //MONOBEHAVIOUR
    //

    //Mouse
    private void OnMouseEnter()
    {
        _outline.SetActive(true);
    }
    private void OnMouseExit()
    {
        _outline.SetActive(false);
    }
    private void OnMouseDown()
    {
        Debug.Log("Click");
        GameManager.Instance.PlayerClickOnThisUnit(this);
    }

    //
    //Fonction
    //

    public IEnumerator MoveUnitInAPath(List<BaseTile> tiles)
    {
        _animator.Play("Walk_BlendTree");
        foreach (BaseTile tile in tiles)
        {
            var XDistance = _tileOccupied.x - tile.x;
            var YDistance = _tileOccupied.y - tile.y;
            //On joue l'animation
            _animator.SetFloat("moveX", XDistance);
            _animator.SetFloat("moveY", YDistance);
            SetSortingOrder(tile.SpriteRenderer.sortingOrder);
            transform.DOMove(tile.transform.position + _unitOffset, _speed);
            tile.PlayWalkSound();
            yield return new WaitForSeconds(_speed);
            _tileOccupied = tile;
            tile._outline.SetActive(false);
        }
        //_animator.Play("Idle_BlendTree");
    }
    public void Selection()
    {
        _outlineSelected.SetActive(true);
    }
    public void DeSelection()
    {
        _outlineSelected.SetActive(false);
        UnitManager.Instance._unitSelected = null;
    }
    public void SetSortingOrder(int _order)
    {
        _trail.sortingOrder = _order;
        _spriteRenderer.sortingOrder = _order + 1;
        _outline.GetComponent<SpriteRenderer>().sortingOrder = _order + 2;
        _outlineSelected.GetComponent<SpriteRenderer>().sortingOrder = _order + 3;
    }
}
