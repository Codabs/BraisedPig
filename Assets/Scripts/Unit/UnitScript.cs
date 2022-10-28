using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FMOD;

public class UnitScript : MonoBehaviour
{
    //
    //Variable
    //

    [SerializeField] private Animator _animator;
    [SerializeField] private List<SpriteRenderer> _spriteRenderers;

    [SerializeField] private GameObject _outline;
    [SerializeField] private GameObject _outlineSelected;
    [SerializeField] private TrailRenderer _trail;

    public BaseTile _tileOccupied;

    [SerializeField] private int _numberOfActionParTurn;
    [SerializeField] private float _speed;
    [SerializeField] private string _attackSound = "event:/UnitSound/Flamethrower";
    private readonly bool _isMoving;
    protected string faction;

    [SerializeField] Vector3 _unitOffset;

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/MouseSound/MouseOver");
    }
    private void OnMouseExit()
    {
        _outline.SetActive(false);
    }
    private void OnMouseDown()
    {
        GameManager.Instance.PlayerClickOnThisUnit(this);
        FMODUnity.RuntimeManager.PlayOneShot("event:/MouseSound/MouseClick");
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
        _animator.Play("Idle_BlendTree");
    }
    public void AttackOtherUnit(UnitScript unitAttacked)
    {
        //Turn in direction of the unit attacked
        _animator.SetFloat("moveX", 0);
        _animator.SetFloat("moveY", 0);
        //Play Animation
        _animator.Play("Attack_BlenTree");
    }
    public void DestroyInStyle()
    {
        Destroy(gameObject);
    }
    public void StopAttacking()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_attackSound);
        _animator.Play("Idle_BlendTree");
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
        foreach(SpriteRenderer _spriteRenderer in _spriteRenderers)
            _spriteRenderer.sortingOrder = _order + 1;
        _outline.GetComponent<SpriteRenderer>().sortingOrder = _order + 2;
        _outlineSelected.GetComponent<SpriteRenderer>().sortingOrder = _order + 3;
    }
}
