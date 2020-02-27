using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject inputField;
    public Text uiText;
    public Text titleText;
    public GameObject board;
    int numberOfInputs;
    GameSolver solver = new GameSolver();
    string[] titleStrings =  new string[7];
    // Start is called before the first frame update
    void Start()
    {
        titleStrings[0] = "Array x size?";
        titleStrings[1] = "Array y size?";
        titleStrings[2] = "Num of Kings?";
        titleStrings[3] = "Num of Queens?";
        titleStrings[4] = "Num of Bishop?";
        titleStrings[5] = "Num of Rook?";
        titleStrings[6] = "Num of Knight?";
        titleText.text = titleStrings[0];
        numberOfInputs = 0;
    }

    public void GetTextInput()
    {
        switch (numberOfInputs)
        {
            case 0:
                solver.arraySizeX = int.Parse(uiText.text);
                break;
            case 1:
                solver.arraySizeY = int.Parse(uiText.text);
                break;
            case 2:
                solver.numKing = int.Parse(uiText.text);
                break;
            case 3:
                solver.numQueen = int.Parse(uiText.text);
                break;
            case 4:
                solver.numBishop = int.Parse(uiText.text);
                break;
            case 5:
                solver.numRook = int.Parse(uiText.text);
                break;
            case 6:
                solver.numKnight = int.Parse(uiText.text);
                break;
        }
        numberOfInputs++;
        if(numberOfInputs<titleStrings.Length)
        {
            titleText.text = titleStrings[numberOfInputs];
        }
        else
        {
            inputField.SetActive(false);
            solver.Solve();
        }
        DisplaySolutions();
    }

    void DisplaySolutions()
    {
        for(int i = 0; i < solver.solutions.Count; i++)
        {
            GameObject tempBoard = Instantiate(board);
            tempBoard.GetComponent<GameBoardDisplay>().CreateGameBoard(new Vector3(4,-2,0), solver.solutions[i], i, solver.solutions.Count);
        }
    }
}
