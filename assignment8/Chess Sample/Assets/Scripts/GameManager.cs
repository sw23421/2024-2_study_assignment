using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 프리팹들
    public GameObject TilePrefab;
    public GameObject[] PiecePrefabs;   // King, Queen, Bishop, Knight, Rook, Pawn 순
    public GameObject EffectPrefab;

    // 오브젝트의 parent들
    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    
    private MovementManager movementManager;
    private UIManager uiManager;
    
    public int CurrentTurn = 1; // 현재 턴 1 - 백, 2 - 흑
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];   // Tile들
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];    // Piece들

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);
        
        InitializeBoard();
    }

    void InitializeBoard()
    {
        // 8x8로 타일들을 배치
        // TilePrefab을 TileParent의 자식으로 생성하고, 배치함
        // Tiles를 채움
        // --- TODO ---
        for (int x = 0; x < Utils.FieldWidth; x++) {
            for (int y = 0; y < Utils.FieldHeight; y++) {
                GameObject tileObject = Instantiate(TilePrefab, TileParent);
                (int , int) Pos = (x, y);
                tileObject.transform.position = Utils.ToRealPos(Pos);

                Tile tile = tileObject.GetComponent<Tile>();
                tile.Set(Pos);

                Tiles[x, y] = tile;
            }

        }
        // ------

        PlacePieces(1);
        PlacePieces(2);
    }

    void PlacePieces(int direction)
    {
        // PlacePiece를 사용하여 Piece들을 적절한 모양으로 배치
        // --- TODO ---
        int otherRow = direction == 1 ? 0 : Utils.FieldHeight - 1;
        int pawnRow = direction == 1 ? 1 : Utils.FieldHeight - 2;

        PlacePiece(4, (0, otherRow), direction);
        PlacePiece(3, (1, otherRow), direction);
        PlacePiece(2, (2, otherRow), direction);
        PlacePiece(1, (3, otherRow), direction);
        PlacePiece(0, (4, otherRow), direction);
        PlacePiece(2, (5, otherRow), direction);
        PlacePiece(3, (6, otherRow), direction);
        PlacePiece(4, (7, otherRow), direction);

        for(int x = 0; x < Utils.FieldWidth; x++) {
            PlacePiece(5, (x, pawnRow), direction);
        }

        // ------
    }

    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        // Piece를 배치 후, initialize
        // PiecePrefabs의 원소를 사용하여 배치, PieceParent의 자식으로 생성
        // Pieces를 채움
        // 배치한 Piece를 리턴
        // --- TODO ---
        GameObject pieceObject = Instantiate(PiecePrefabs[pieceType], PieceParent);

        pieceObject.transform.position = Utils.ToRealPos(pos);
        SpriteRenderer sr = pieceObject.GetComponent<SpriteRenderer>();
        if(sr != null) {
            sr.sortingOrder = 1;
        }

        Piece piece = pieceObject.GetComponent<Piece>();
        if(piece == null) {
            Debug.LogError($"Piece component missing : {pieceType}");
            return null;
        }
        piece.initialize(pos, direction);
        Debug.Log($"Placing piece of type {pieceType} at board position {pos}, real position {Utils.ToRealPos(pos)}");
        Pieces[pos.Item1, pos.Item2] = piece;
        return piece;
        // ------
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        return movementManager.IsValidMove(piece, targetPos);
    }

    public void ShowPossibleMoves(Piece piece)
    {
        movementManager.ShowPossibleMoves(piece);
    }

    public void ClearEffects()
    {
        movementManager.ClearEffects();
    }


    public void Move(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMove(piece, targetPos)) return;
        
        // 해당 위치에 다른 Piece가 있다면 삭제
        // Piece를 이동시킴
        // --- TODO ---
        var targetPiece = Pieces[targetPos.Item1, targetPos.Item2];
        if (targetPiece != null) {
            Destroy(targetPiece.gameObject);
            Pieces[targetPos.Item1, targetPos.Item2] = null;
        }

        (int currentX, int currentY) = piece.MyPos;
        Pieces[currentX, currentY] = null;

        piece.MyPos = targetPos;
        piece.transform.position = Utils.ToRealPos(targetPos);
        Pieces[targetPos.Item1, targetPos.Item2] = piece;

        ClearEffects();

        ChangeTurn();
        // ------
    }

    void ChangeTurn()
    {
        // 턴을 변경하고, UI에 표시
        // --- TODO ---
        CurrentTurn = CurrentTurn == 1 ? 2 : 1;
        uiManager.UpdateTurn(CurrentTurn);
        // ------
    }
}
