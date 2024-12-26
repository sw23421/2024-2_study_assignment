using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // 기본 이동
        int forwardDir = (PlayerDirection == 1) ? +1 : -1;

        
        int startRow = (PlayerDirection == 1) ? 1 : 6;

       
        if (MyPos.Item2 == startRow)
        {
           
            return new MoveInfo[]
            {
                new MoveInfo(0, forwardDir, 1),   // 1칸 전진
                new MoveInfo(0, forwardDir, 2),   // 2칸 전진
                new MoveInfo(1, forwardDir, 1),   // 대각선 공격(오른쪽)
                new MoveInfo(-1, forwardDir, 1)   // 대각선 공격(왼쪽)
            };
        }
        else
        {
            
            return new MoveInfo[]
            {
                new MoveInfo(0, forwardDir, 1),   // 1칸 전진
                new MoveInfo(1, forwardDir, 1),   // 대각선 공격(오른쪽)
                new MoveInfo(-1, forwardDir, 1)   // 대각선 공격(왼쪽)
            };
        }
    }
}