using System;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.WSA;

public static class UserInput
{
    public static Vector2Int Input;
    
    public static void Update()
    {
        var horizontal = Horizontal.None;
        var vertical = Vertical.None;

        if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
            vertical = Vertical.Down;
        else if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
            vertical = Vertical.Up;
        if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            horizontal = Horizontal.Right;
        else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            horizontal = Horizontal.Left;
        Input = InputToVector(horizontal, vertical);
    }

    private static Vector2Int InputToVector(Horizontal horizontal, Vertical vertical)
    {
        var x = horizontal switch
        {
            Horizontal.Left => -1,
            Horizontal.Right => 1,
            _ => 0
        };
        var y = vertical switch
        {
            Vertical.Up => 1,
            Vertical.Down => -1,
            _ => 0
        };
        return new Vector2Int(x, y);
    }
}


public enum Horizontal
{
    None,
    Right,
    Left
}

public enum Vertical
{
    None,
    Up,
    Down
}