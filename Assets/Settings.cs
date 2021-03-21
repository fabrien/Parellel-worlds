using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{ 
    public const float DeltaTime = 0.4f;

    private static readonly CellType[,] World1 = 
    {
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Obstacle, CellType.Player, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Door, CellType.Empty, CellType.Empty},
    };

    private static readonly CellType[,] World2 = 
    {
        {CellType.Empty, CellType.Player, CellType.Empty, CellType.Obstacle, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Key, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Obstacle, CellType.Obstacle, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
    };
    
    private static readonly CellType[,] World3 =
    {
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Player, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Obstacle, CellType.Obstacle, CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Obstacle, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Door, CellType.Empty, CellType.Empty, CellType.Empty},
    };
    
    private static readonly CellType[,] World4 =
    {
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Key, CellType.Obstacle, CellType.Empty, CellType.Obstacle},
        {CellType.Obstacle, CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Player, CellType.Empty, CellType.Obstacle, CellType.Empty},
        {CellType.Obstacle, CellType.Empty, CellType.Empty, CellType.Obstacle, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty},
        {CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty, CellType.Empty}
    };

    public static CellType[,] GetWorld(int index)
    {
        return index switch
        {
            1 => World1,
            2 => World2,
            3 => World3,
            4 => World4,
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "trying to get a non implemented world")
        };
    }

    public static Vector2Int GetPlayerPosFromIndex(int index)
    {
        return index switch
        {
            1 => new Vector2Int(1, 1),
            2 => new Vector2Int(1, 0),
            3 => new Vector2Int(3, 3),
            4 => new Vector2Int(3, 3),
            _ => throw new ArgumentOutOfRangeException(nameof(index), index, "trying to get a non implemented world's player pos")
        };
    }
}
