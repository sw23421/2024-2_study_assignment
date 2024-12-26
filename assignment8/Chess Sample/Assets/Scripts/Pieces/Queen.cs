using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
        {
            // 직선 방향
            new MoveInfo(1, 0, 8),  
            new MoveInfo(-1, 0, 8),  
            new MoveInfo(0, 1, 8),  
            new MoveInfo(0, -1, 8),  

            // 대각선 방향
            new MoveInfo(1, 1, 8),   
            new MoveInfo(-1, 1, 8),  
            new MoveInfo(1, -1, 8),  
            new MoveInfo(-1, -1, 8)  
        };
        // ------
    }
}