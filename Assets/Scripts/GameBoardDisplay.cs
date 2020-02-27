using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardDisplay : MonoBehaviour
{
    public GameObject tile;
    public GameObject KingTile;
    public GameObject QueenTile;
    public GameObject BishopTile;
    public GameObject RookTile;
    public GameObject KnightTile;
    Vector3 newStartPosition;
    Vector3 resetPosition;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGameBoard(Vector3 position,Tile[,] boardData, int boardNumber, int numberOfBoards)
    {
        GameObject tempTile = null;
        for (int i = 0; i < boardData.GetLength(0); i++)
        {
            for(int j = 0; j< boardData.GetLength(1); j++)
            {
                
                if (boardData[i, j].pieceAttached == null)
                {
                    tempTile = Instantiate(tile);
                }
                else
                {
                    switch (boardData[i, j].pieceAttached.pieceName)
                    {
                        case "King":
                            tempTile = Instantiate(KingTile);
                            break;
                        case "Queen":
                            tempTile = Instantiate(QueenTile);
                            break;
                        case "Bishop":
                            tempTile = Instantiate(BishopTile);
                            break;
                        case "Rook":
                            tempTile = Instantiate(RookTile);
                            break;
                        case "Knight":
                            tempTile = Instantiate(KnightTile);
                            break;
                    }
                }
                tempTile.transform.parent = this.transform;
                tempTile.transform.position = new Vector3(1.25f * i, 1.25f * j, 0);
            }
        }

        if (boardNumber > 1)
        {
            transform.position = new Vector3(position.x * (boardData.GetLength(0) * boardNumber), position.y, position.z);
        }
        else
        {
            transform.position = position;
        }

        resetPosition = new Vector3(10 + (boardData.GetLength(0) * 1.25f), transform.position.y, transform.position.z);
        newStartPosition = new Vector3(-10 - (boardData.GetLength(0) * 1.25f * numberOfBoards), transform.position.y, transform.position.z);
    }
}
