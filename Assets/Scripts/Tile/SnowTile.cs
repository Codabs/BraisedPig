using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTile : BaseTile
{
    [SerializeField] private Color _baseColor, _offsetColor;

    public override void Init(int x, int y)
    {
        //var isOffSet = (x % 2 == 0 && y % 2 != 0) || (y % 2 == 0 && x % 2 != 0);
        //spriteRenderer.color = isOffSet ? _offsetColor : _baseColor;
    }
}
