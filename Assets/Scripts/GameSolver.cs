using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSolver : MonoBehaviour
{
    List<ChessPieceClass> pieces = new List<ChessPieceClass>();
    List<Tile[,]> solutions = new List<Tile[,]>();
    // Start is called before the first frame update
    void Start()
    {
        Solve();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Solve()
    {
        Debug.Log("The program will start calculating with a 3 x 3 grid with 2 Kings and 1 Rook?");
        int arrayX = 3;
        int arrayY = 3;
        Tile[,] board = BuildBoard(arrayX, arrayY);
        Debug.Log(board.GetLength(0) + " " + board.GetLength(1));
        int numKing = 2;
        int numRook = 1;
        for (int kingCounter = 0; kingCounter < numKing; kingCounter++)
        {
            pieces.Add(new ChessPieceClass("King"));
        }
        for (int rookCounter = 0; rookCounter < numRook; rookCounter++)
        {
            pieces.Add(new ChessPieceClass("Rook"));
        }
        Debug.Log(pieces.Count);
    }

    private void CycleArray(Tile[,] array, int piecesIndex)
    {
        Tile[,] tempArray = array;
        for (int i = 0; i < tempArray.GetLength(0); i++)
        {
            for(int j = 0; j< tempArray.GetLength(1); j++)
            {
                if (tempArray[i, j].placeble && !CheckForKills(tempArray,pieces[piecesIndex],new Vector2(i,j)))
                {
                    tempArray[i, j].placeble = false;
                    tempArray[i, j].pieceAttached = pieces[piecesIndex];
                    if(piecesIndex + 1 < pieces.Count)
                    {
                        CycleArray(tempArray, piecesIndex++);
                    }
                    else
                    {
                        solutions.Add(tempArray);
                    }
                    tempArray[i, j].placeble = true;
                    tempArray[i, j].pieceAttached = null;
                }
            }
        }
    }

    private bool CheckForKills(Tile[,] tiles, ChessPieceClass piece,Vector2 position)
    {
        bool canKill = false;
        switch (piece.pieceName)
        {
            case "King":
                canKill = KingKillCheck(piece.moveDir,tiles, position);
                break;
            case "Queen":
                canKill = DirectionalKillCheck(piece.moveDir, tiles, position);
                break;
            case "Bishop":
                canKill = DirectionalKillCheck(piece.moveDir, tiles, position);
                break;
            case "Rook":
                canKill = DirectionalKillCheck(piece.moveDir, tiles, position);
                break;
            case "Knight":
                canKill = KnightKillCheck(piece.moveDir, tiles, position);
                break;
        }
        return canKill;
    }

    private bool KingKillCheck(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        return true;
    }

    private bool KnightKillCheck(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        return true;
    }

    private bool DirectionalKillCheck(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        return true;
    }
    private static Tile[,] BuildBoard(int x, int y)
    {
        Tile[,] board = new Tile[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int p = 0; p < y; p++)
            {
                board[i, p] = new Tile();
                board[i, p].placeble = true;
            }
        }
        return board;
    }
}
public struct Tile
{
    public bool placeble;
    public ChessPieceClass pieceAttached;
}
