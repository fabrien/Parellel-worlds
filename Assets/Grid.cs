using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Grid : MonoBehaviour
{
    private Vector2Int _dimensions;
    public Manager manager;
    
    private Cell[,] _cells;
    private Vector2 _cellSize;

    public int worldTypeIdx;

    private CellType[,] _types;

    private Vector2Int _playerPos;

    private bool _isVisible = true;
    private SpriteRenderer _spriteRenderer;
    public AudioClip Walk;

    private void Start()
    {
        var go = gameObject;
        _spriteRenderer = go.GetComponent<SpriteRenderer>();
        
        _types = Settings.GetWorld(worldTypeIdx);
        _dimensions = new Vector2Int(_types.GetLength(1), _types.GetLength(0));
        _cells = new Cell[_dimensions.x, _dimensions.y];
        _playerPos = Settings.GetPlayerPosFromIndex(worldTypeIdx);
        
        Vector2 scale =  go.transform.localScale;
        _cellSize = scale / _dimensions;
        for (var x = 0; x < _dimensions.x ; ++x) {
            for (var y = 0; y < _dimensions.y; ++y) {
                _cells[x, y] = new Cell(CoordToPos(x, y), _types[y, x]);
            }
        }
    }

    private Vector2 CoordToPos(Vector2Int coord)
    {
        var sprite = _spriteRenderer.sprite;
        var pos = (Vector2)transform.position - (sprite.rect.size / sprite.pixelsPerUnit) * gameObject.transform.localScale / 2;
        // var pos = (Vector2)transform.position - new Vector2(4f, 4f);
        return pos + coord * _cellSize + _cellSize / 2;
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

        if (_cells[newX, newY].cellType == CellType.Key)
            manager.doorLocked = false;
        if (_cells[newX, newY].cellType == CellType.Door && !manager.doorLocked)
            manager.CompleteLevel();
        _cells[x, y].Destroy();
        _cells[x, y] = new Cell(CoordToPos(x, y), CellType.Empty, _isVisible);
        _cells[newX, newY].Destroy();
        _cells[newX, newY] = new Cell(CoordToPos(newX, newY), CellType.Player, _isVisible);
        AudioSource.PlayClipAtPoint(Walk, transform.position);
    }

    private bool OutOfBounds(Vector2Int pos)
    {
        return 0 > pos.x || pos.x >= _dimensions.x || 0 > pos.y || pos.y >= _dimensions.y;
    }

    public bool WouldCollide(Vector2Int direction)
    {
        var newPos = _playerPos + direction;
        if (OutOfBounds(newPos))
            return true;
        var endCellType = _cells[newPos.x, newPos.y].cellType;
        return endCellType == CellType.Obstacle || (endCellType == CellType.Door && manager.doorLocked);
    }
    
    public void DeltaUpdate(Vector2Int direction)
    {
        if (direction.x == 0 && direction.y == 0) return;
        if (manager.allowMovement)
            MovePlayer(direction);
    }

    public void TriggerVisibility()
    {
        _isVisible = !_isVisible;
        foreach (var cell in _cells) {
            cell.VisibilityTrigger();
        }
    }
}

internal class Cell
{
    public readonly CellType cellType;
    // private static readonly Sprite CellSprite =  UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

    private readonly GameObject _cell;
    private readonly SpriteRenderer _renderer;

    public Cell(Vector2 position, CellType cellType, bool visible=true)
    {
        this.cellType = cellType;
        _cell = new GameObject();
        _cell.transform.position = position;
        _renderer = _cell.AddComponent<SpriteRenderer>();
        _renderer.sprite = chooseSprite(cellType);
        _renderer.renderingLayerMask = 1;
        if (!visible) _renderer.enabled = false;
    }

    private static Sprite chooseSprite (CellType ct)
    {
        return ct switch
        {
            CellType.Door => Resources.Load<Sprite>("Sprites/Door"),
            CellType.Key => Resources.Load<Sprite>("Sprites/key"),
            CellType.Empty => Resources.Load<Sprite>("Sprites/path"),
            CellType.Obstacle => Resources.Load<Sprite>("Sprites/obstacle"),
            CellType.Player => Resources.Load<Sprite>("Sprites/player"),
            _ => throw new ArgumentOutOfRangeException(nameof(ct), ct, "null")
        };
    }

 /*   private static Color TypeToColor(CellType ct)
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
    }*/

    public void VisibilityTrigger()
    {
        _renderer.enabled = !_renderer.enabled;
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