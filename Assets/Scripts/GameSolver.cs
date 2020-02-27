using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSolver
{
    List<ChessPieceClass> pieces = new List<ChessPieceClass>();
    List<Tile[,]> possibilities = new List<Tile[,]>();

    public List<Tile[,]> solutions = new List<Tile[,]>();
    public int arraySizeX;
    public int arraySizeY;
    public int numKing = 0;
    public int numQueen = 0;
    public int numBishop = 0;
    public int numRook = 0;
    public int numKnight = 0;

    public void Solve()
    {
        Tile[,] board = BuildBoard(arraySizeX, arraySizeY);
        CreateChessPieces();
        CycleArray(board, 0);
        RemoveDuplicates();  
    }

    private void RemoveDuplicates()
    {
        for(int i = 0; i < possibilities.Count; i++)
        {
            bool duplicate = false;
            for(int j = 0; j < i; j++)
            {
                if (CompareTwoArrays(possibilities[j],possibilities[i]))
                {
                    duplicate = true;
                    break;
                }
            }
            if (!duplicate)
            {
                solutions.Add(possibilities[i]);
            }
        }
    }

    private bool CompareTwoArrays(Tile[,] firstArray,Tile[,] secondArray)
    {
        for(int i = 0; i < firstArray.GetLength(0); i++)
        {
            for (int j = 0; j < firstArray.GetLength(1); j++)
            {
                if(firstArray[i,j].pieceAttached == null && secondArray[i, j].pieceAttached != null)
                {
                    return false;
                }
                if (firstArray[i, j].pieceAttached != null && secondArray[i, j].pieceAttached == null)
                {
                    return false;
                }
                if (firstArray[i, j].pieceAttached == null && secondArray[i, j].pieceAttached == null)
                {
                    break;
                }
                else if(firstArray[i, j].pieceAttached.pieceName != secondArray[i, j].pieceAttached.pieceName)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void CreateChessPieces()
    {
        int counter = 0;
        for (counter = 0; counter < numKing; counter++)
        {
            pieces.Add(new ChessPieceClass("King"));
        }
        for (counter = 0; counter < numQueen; counter++)
        {
            pieces.Add(new ChessPieceClass("Queen"));
        }
        for (counter = 0; counter < numBishop; counter++)
        {
            pieces.Add(new ChessPieceClass("Bishop"));
        }
        for (counter = 0; counter < numRook; counter++)
        {
            pieces.Add(new ChessPieceClass("Rook"));
        }
        for (counter = 0; counter < numKnight; counter++)
        {
            pieces.Add(new ChessPieceClass("Knight"));
        }
    }

    private void CycleArray(Tile[,] array, byte piecesIndex)
    {
        Tile[,] tempArray = (Tile[,])array.Clone();
        byte arrayIndex = piecesIndex;
        for (byte i = 0; i < tempArray.GetLength(0); i++)
        {
            for(byte j = 0; j< tempArray.GetLength(1); j++)
            {
                if (tempArray[i, j].placeble && !CheckForKills(tempArray,pieces[arrayIndex],new Vector2(i,j)))
                {
                    tempArray[i, j].placeble = false;
                    tempArray[i, j].pieceAttached = pieces[arrayIndex];
                    
                    if(arrayIndex + 1 < pieces.Count)
                    {
                        CycleArray(PlacePieceOnBoard(tempArray, pieces[arrayIndex], new Vector2(i, j)), (byte)(arrayIndex + 1));
                    }
                    else
                    {
                        Tile[,] addArray = (Tile[,])tempArray.Clone();
                        possibilities.Add(addArray);
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
        if(piece.pieceName == "King" || piece.pieceName == "Knight")
        {
            canKill = SetMovementAmountKillCheck(piece.moveDir, tiles, position);
        }
        else
        {
            canKill = DirectionalKillCheck(piece.moveDir, tiles, position);
        }
        return canKill;
    }

    private bool SetMovementAmountKillCheck(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        for(byte i = 0; i < directions.Count; i++)
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
        for(byte i = 0; i < directions.Count; i++)
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
        Tile[,] tempArray = (Tile[,])tiles.Clone();
        if(piece.pieceName == "King"||piece.pieceName == "Knight")
        {
            tempArray = SetMovementPiecePlacement(piece.moveDir, tempArray, position);
        }
        else
        {
            tempArray = DirectionalPiecePlacement(piece.moveDir, tempArray, position);
        }
        return tempArray;
    }

    private Tile[,] SetMovementPiecePlacement(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        Tile[,] tempTiles = (Tile[,])tiles.Clone(); 
        for (byte i = 0; i < directions.Count; i++)
        {
            Vector2 temp = position + directions[i];
            if (temp.x >= 0 && temp.x < tempTiles.GetLength(0) && temp.y >= 0 && temp.y < tempTiles.GetLength(1))
            {
                tempTiles[(int)temp.x, (int)temp.y].placeble = false;
            }
        }
        return tempTiles;
    }

    private Tile[,] DirectionalPiecePlacement(List<Vector2> directions, Tile[,] tiles, Vector2 position)
    {
        Tile[,] tempTiles = (Tile[,])tiles.Clone();
        for (byte i = 0; i < directions.Count; i++)
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
