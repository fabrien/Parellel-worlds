using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public class Grid : MonoBehaviour
{
    public Vector2Int dimensions;
    public Manager manager;
    
    private Cell[,] _cells;
    private Vector2 _cellSize;
    private readonly CellType[,] _types =
    {
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Player, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Obstacle, CellType.Empty},
        {CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Door, CellType.Empty, CellType.Empty},
    };

    private Vector2Int _playerPos = new Vector2Int(0, 1);
    private float _timer = 0;
    private const float DeltaTime = Settings.DeltaTime;

    private int _xInput;
    private int _yInput;

    private void Start()
    {
        var go = gameObject;
        
        _cells = new Cell[dimensions.x, dimensions.y];
        Vector2 scale =  go.transform.localScale;
        _cellSize = scale / dimensions;
        for (var x = 0; x < dimensions.x ; ++x) {
            for (var y = 0; y < dimensions.y; ++y) {
                _cells[x, y] = new Cell(CoordToPos(x, y), _types[y, x]);
            }
        }
    }

    private Vector2 CoordToPos(Vector2Int coord)
    {
        // var pos = sprite.rect.position - sprite.rect.size / 2;
        var pos = (Vector2)transform.position - new Vector2(4f, 4f);
        return pos + coord * _cellSize;
    }

    private Vector2 CoordToPos(int x, int y)
    {
        return CoordToPos(new Vector2Int(x, y));
    }

    private void MovePlayer(Vector2Int direction)
    {
        var (x, y) = (_playerPos.x, _playerPos.y);
        _playerPos += direction;
        var (newX, newY) = (_playerPos.x, _playerPos.y);

        _cells[x, y].Destroy();
        _cells[x, y] = new Cell(CoordToPos(x, y), CellType.Empty);
        _cells[newX, newY].Destroy();
        _cells[newX, newY] = new Cell(CoordToPos(newX, newY), CellType.Player);
    }

    private bool OutOfBounds(Vector2Int pos)
    {
        return 0 > pos.x || pos.x >= dimensions.x || 0 > pos.y || pos.y >= dimensions.y;
    }

    public bool WouldCollide(Vector2Int direction)
    {
        var newPos = _playerPos + direction;
        return OutOfBounds(newPos) || _cells[newPos.x, newPos.y].CellType == CellType.Obstacle;
    }
    
    public void DeltaUpdate(Vector2Int direction)
    {
        if (direction.x == 0 && direction.y == 0) return;
        if (manager.allowMovement)
            MovePlayer(direction);
    }
    
    // private static int NonZero(float f)
    // {
    //     if (f > 0)
    //         return 1;
    //     if (f < 0)
    //         return -1;
    //     return 0;
    // }
}

internal class Cell
{
    public readonly CellType CellType;
    private static readonly Sprite CellSprite =  UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
    
    private GameObject _cell;
    private Vector2 _position;
    
    public Cell(Vector2 position, CellType cellType)
    {
        _position = position;
        CellType = cellType;
        _cell = new GameObject();
        _cell.transform.position = _position;
        var spriteR = _cell.AddComponent<SpriteRenderer>();
        spriteR.sprite = CellSprite;
        spriteR.color = TypeToColor(this.CellType);
        spriteR.renderingLayerMask = 1;
    }

    private static Color TypeToColor(CellType ct)
    {
        return ct switch
        {
            CellType.Door => Color.red,
            CellType.Key => Color.blue,
            CellType.Empty => Color.black,
            CellType.Obstacle => Color.green,
            CellType.Player => Color.white,
            _ => throw new ArgumentOutOfRangeException(nameof(ct), ct, "null")
        };
    }
    
    ~Cell()
    {
        Destroy();
    }

    public void Destroy()
    {
        Object.Destroy(_cell);
    }
}

internal enum CellType
{
    Empty,
    Player,
    Key,
    Door,
    Obstacle
}
//
// string ToString(CellType ct)
// {
//     return ct switch
//     {
//         CellType.Empty => "Empty",
//         CellType.Player => "Player",
//         CellType.Key => "Key",
//         CellType.Door => "Door",
//         CellType.Obstacle => "Obstacle",
//         _ => throw new ArgumentOutOfRangeException(nameof(ct), ct, null)
//     };
// }