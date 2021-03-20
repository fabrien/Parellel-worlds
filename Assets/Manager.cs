using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject world1;
    public GameObject world2;
    private Grid _grid1;
    private Grid _grid2;

    public bool allowMovement;
    public Vector2Int direction;

    public float timer;
    private int _xInput;
    private int _yInput;

    
    // Start is called before the first frame update
    private void Start()
    {
        _grid1 = world1.GetComponent<Grid>();
        _grid2 = world2.GetComponent<Grid>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        UserInput.Update();

        if (_xInput == 0)
            _xInput = UserInput.Input.x;
        if (_yInput == 0)
            _yInput = UserInput.Input.y;

        direction.x = _xInput; 
        direction.y = _yInput; 
        allowMovement = AllowMovement(direction);
        if (timer < Settings.DeltaTime) return;
        _xInput = 0;
        _yInput = 0;
        timer = 0;
        _grid1.DeltaUpdate(direction);
        _grid2.DeltaUpdate(direction);
    }

    private bool AllowMovement(Vector2Int direction)
    {
        return !_grid1.WouldCollide(direction) && !_grid2.WouldCollide(direction);
    }
}
