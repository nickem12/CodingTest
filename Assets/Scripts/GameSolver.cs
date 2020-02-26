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
        CycleArray(board, 0);
        Debug.Log(solutions.Count);
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
                        tempArray = PlacePieceOnBoard(tempArray, pieces[piecesIndex], new Vector2(i, j));
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
                canKill = SetMovementAmountKillCheck(piece.moveDir,tiles, position);
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
                canKill = SetMovementAmountKillCheck(piece.moveDir, tiles, position);
                break;
        }
        return canKill;
    }

    private bool SetMovementAmountKillCheck(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        for(int i = 0; i < directions.Count; i++)
        {
            Vector2 temp = position + directions[i];
            if (temp.x >= 0 && temp.x < tiles.GetLength(0) && temp.y >= 0 && temp.y < tiles.GetLength(1))
            {
                if(tiles[(int)temp.x,(int)temp.y].pieceAttached != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool DirectionalKillCheck(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        for(int i = 0; i < directions.Count; i++)
        {
            bool outOfBounds = false;
            int multiplier = 1;
            while (!outOfBounds)
            {
                Vector2 temp = position + (directions[i] * multiplier);
                if(temp.x >= 0 && temp.x < tiles.GetLength(0) && temp.y >= 0 && temp.y < tiles.GetLength(1))
                {
                    if (tiles[(int)temp.x, (int)temp.y].pieceAttached != null)
                    {
                        return true;
                    }
                    else
                    {
                        multiplier++;
                    }
                }
                else
                {
                    outOfBounds = true;
                }
            }
        }
        return false;
    }

    private Tile[,] PlacePieceOnBoard(Tile[,] tiles, ChessPieceClass piece, Vector2 position)
    {
        Tile[,] tempArray = tiles;
        switch (piece.pieceName)
        {
            case "King":
                tempArray = SetMovementPiecePlacement(piece.moveDir, tempArray, position);
                break;
            case "Queen":
                tempArray = DirectionalPiecePlacement(piece.moveDir, tempArray, position);
                break;
            case "Bishop":
                tempArray = DirectionalPiecePlacement(piece.moveDir, tempArray, position);
                break;
            case "Rook":
                tempArray = DirectionalPiecePlacement(piece.moveDir, tempArray, position);
                break;
            case "Knight":
                tempArray = SetMovementPiecePlacement(piece.moveDir, tempArray, position);
                break;
        }
        return tempArray;
    }

    private Tile[,] SetMovementPiecePlacement(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        Tile[,] tempTiles = tiles;
        for (int i = 0; i < directions.Count; i++)
        {
            Vector2 temp = position + directions[i];
            if (temp.x >= 0 && temp.x < tiles.GetLength(0) && temp.y >= 0 && temp.y < tiles.GetLength(1))
            {
                tempTiles[(int)temp.x, (int)temp.y].placeble = false;
            }
        }
        return tempTiles;
    }

    private Tile[,] DirectionalPiecePlacement(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        Tile[,] tempTiles = tiles;
        for (int i = 0; i < directions.Count; i++)
        {
            bool outOfBounds = false;
            int multiplier = 1;
            while (!outOfBounds)
            {
                Vector2 temp = position + (directions[i] * multiplier);
                if (temp.x >= 0 && temp.x < tempTiles.GetLength(0) && temp.y >= 0 && temp.y < tempTiles.GetLength(1))
                {
                    tempTiles[(int)temp.x, (int)temp.y].placeble = false;
                    multiplier++;
                }
                else
                {
                    outOfBounds = true;
                }
            }
        }
        return tempTiles;
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
