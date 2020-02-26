using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceClass
{
    public List<Vector2> moveDir = new List<Vector2>();
    public string pieceName;

    public ChessPieceClass(string name)
    {
        switch (name)
        {
            case "King":
                BuildDiagonal();
                BuildStraights();
                pieceName = "King";
                break;
            case "Queen":
                BuildDiagonal();
                BuildStraights();
                pieceName = "Queen";
                break;
            case "Bishop":
                BuildDiagonal();
                pieceName = "Bishop";
                break;
            case "Rook":
                BuildStraights();
                pieceName = "Rook";
                break;
            case "Knight":
                BuildKnight();
                pieceName = "Knight";
                break;
        }
    }

    void BuildDiagonal()
    {
        moveDir.Add(new Vector2(1, 1));
        moveDir.Add(new Vector2(-1, 1));
        moveDir.Add(new Vector2(-1, -1));
        moveDir.Add(new Vector2(1, -1));
    }
    void BuildStraights()
    {
        moveDir.Add(new Vector2(0, 1));
        moveDir.Add(new Vector2(0, -1));
        moveDir.Add(new Vector2(1, 0));
        moveDir.Add(new Vector2(-1, 0));
    }
    void BuildKnight()
    {
        moveDir.Add(new Vector2(2, 1));
        moveDir.Add(new Vector2(2, -1));
        moveDir.Add(new Vector2(1, 2));
        moveDir.Add(new Vector2(-1, 2));
        moveDir.Add(new Vector2(-2, 1));
        moveDir.Add(new Vector2(-2, -1));
        moveDir.Add(new Vector2(1, -2));
        moveDir.Add(new Vector2(-1, -2));
    }

}
