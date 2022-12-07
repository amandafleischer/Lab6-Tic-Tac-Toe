using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

//written by Hannah
//edits by Amanda for Instantiate parenting, single player mode (bot), and game over

namespace FleischerFouts.Lab6
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] List<GameObject> emptyCells = new List<GameObject>();
        List<GameObject> xCells = new List<GameObject>();
        List<GameObject> oCells = new List<GameObject>();

        [SerializeField] GameObject xChoosingPrefab;
        [SerializeField] GameObject oChoosingPrefab;
        [SerializeField] GameObject xPlacedPrefab;
        [SerializeField] GameObject oPlacedPrefab;
        [SerializeField] GameObject parent;
        //will be used later to tell if single player game is active (bot) 
        [SerializeField] GameObject singlePlayerUI;
        [SerializeField] GameObject MultiPlayerUI;
        [SerializeField] GameObject gameOverUI;

        //Text to update on score board
        [SerializeField] TMP_Text singlePlayer1;
        [SerializeField] TMP_Text botPlayer;
        [SerializeField] TMP_Text multiPlayer1;
        [SerializeField] TMP_Text multiPlayer2;
        [SerializeField] TMP_Text winner;





        private InputAction action;

        GameObject spotSelect;
        int cellPosition = 0;
        bool isXPiece = true;

        int xPoints = 0;
        int oPoints = 0;


        void Start()
        {
            spotSelect = Instantiate(xChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
        }

        public void Initialize(InputAction moveAction, InputAction placeAction)
        {
            moveAction.Enable();
            action = moveAction;
            action.performed += MovePiece;

            placeAction.Enable();
            action = placeAction;
            action.performed += PlacePiece;
        }

        private void MovePiece(InputAction.CallbackContext context)
        {
            Vector3 moveInput = context.ReadValue<Vector3>();
            Destroy(spotSelect);

            if (moveInput.x == 1)
            {
                cellPosition += 1;

                if (cellPosition >= emptyCells.Count)
                {
                    cellPosition = 0;
                }
            }
            else if (moveInput.x == -1)
            {
                cellPosition -= 1;

                if (cellPosition < 0)
                {
                    cellPosition = emptyCells.Count - 1;
                }
            }

            if (isXPiece)
            {
                spotSelect = Instantiate(xChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
            }
            else
            {
                //if in single player mode, no need to instantiate choosing prefab (bot)
                if (!singlePlayerUI.gameObject.activeSelf)
                {
                    spotSelect = Instantiate(oChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
                }
            }

        }

        private void PlacePiece(InputAction.CallbackContext context)
        {
            Destroy(spotSelect);

            if (isXPiece)
            {
                Instantiate(xPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
            }
            else
            {
                //if in single player mode, choose a random empty cell to place game piece (bot)
                if (singlePlayerUI.gameObject.activeSelf)
                {
                    cellPosition = UnityEngine.Random.Range(0, emptyCells.Count);
                    Instantiate(oPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);

                }
                else
                {
                    Instantiate(oPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
                }
            }


            if (isXPiece)
            {
                xCells.Add(emptyCells[cellPosition]);
            }
            else
            {
                oCells.Add(emptyCells[cellPosition]);
            }

            CheckForRow();
            isXPiece = !isXPiece;

            emptyCells.Remove(emptyCells[cellPosition]);

            cellPosition = 0;

            if (emptyCells.Count > 0)
            {
                if (isXPiece)
                { 
                    spotSelect = Instantiate(xChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
                }
                else
                {
                    //if in single player mode, no need to instantiate choosing prefab (bot)
                    if (!singlePlayerUI.gameObject.activeSelf)
                    {
                        spotSelect = Instantiate(oChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
                    }
                }
            }
            else
            {//display game over once there are no empty spaces left
                gameOverUI.gameObject.SetActive(true);
                //displays the winner
                if (xPoints > oPoints)
                { 
                    winner.text = "Player 1 Wins!";
                } else if (oPoints > xPoints)
                {
                    winner.text = "Player 2 Wins!";
                } else
                {
                    winner.text = "Tie Game!";
                }
                singlePlayerUI.gameObject.SetActive(false);
                MultiPlayerUI.gameObject.SetActive(false);
            }
        }

        private void CheckForRow()
        {
            List<GameObject> filledCells = new List<GameObject>();

            if (isXPiece)
            {
                filledCells = xCells;
            }
            else
            {
                filledCells = oCells;
            }


            if (filledCells.Count >= 3)
            {
                for (int x = 0; x < filledCells.Count - 2; x++)
                {
                    for (int y = x + 1; y < filledCells.Count - 1; y++)
                    {
                        string pieceOne = filledCells[x].name;
                        string pieceTwo = filledCells[y].name;
                        string newPiece = filledCells[filledCells.Count - 1].name;

                        CheckRowWin(pieceOne, pieceTwo, newPiece);
                        CheckColumnWin(pieceOne, pieceTwo, newPiece);
                        CheckLevelWin(pieceOne, pieceTwo, newPiece);
                        CheckDiagonalWin(pieceOne, pieceTwo, newPiece);
                    }
                }
            }
        }

        private void CheckRowWin(string one, string two, string three)
        {
            if (one[0] == two[0] && two[0] == three[0])
            {
                if (one[1] == two[1] && two[1] == three[1])
                {
                    //Debug.Log("" + one[0] + one[1] + one[2] + " " + two[0] + two[1] + two[2] + " " + three[0] + three[1] + three[2]);

                    if (isXPiece)
                    {
                        xPoints++;
                        Debug.Log("Row - X Points: " + xPoints);
                        if (singlePlayerUI.activeSelf)
                        {//updates score board for player 1
                            singlePlayer1.text = "Player 1 (X) - " + xPoints;
                        } else
                        {
                            multiPlayer1.text = "Player 1 (X) - " + xPoints;
                        }
                    }
                    else
                    {
                        oPoints++;
                        Debug.Log("Row - O Points: " + oPoints);
                        if (singlePlayerUI.activeSelf)
                        {//updates score board for player 2
                            botPlayer.text = "Bot (O) - " + oPoints;
                        }
                        else
                        {
                            multiPlayer2.text = "Player 2 (O) - " + oPoints;
                        }
                    }

                }
            }
        }

        private void CheckColumnWin(string one, string two, string three)
        {
            if (one[0] == two[0] && two[0] == three[0])
            {
                if (one[2] == two[2] && two[2] == three[2])
                {
                    if (isXPiece)
                    {
                        xPoints++;
                        Debug.Log("Column - X Points: " + xPoints);
                        if (singlePlayerUI.activeSelf)
                        {//updates score board for player 1
                            singlePlayer1.text = "Player 1 (X) - " + xPoints;
                        }
                        else
                        {
                            multiPlayer1.text = "Player 1 (X) - " + xPoints;
                        }
                    }
                    else
                    {
                        oPoints++;
                        Debug.Log("Column - O Points: " + oPoints);
                        if (singlePlayerUI.activeSelf)
                        {//updates score board for player 2
                            botPlayer.text = "Bot (O) - " + oPoints;
                        }
                        else
                        {
                            multiPlayer2.text = "Player 2 (O) - " + oPoints;
                        }
                    }

                }
            }
        }

        private void CheckLevelWin(string one, string two, string three)
        {
            if (one[1] == two[1] && two[1] == three[1])
            {
                if (one[2] == two[2] && two[2] == three[2])
                {
                    if (isXPiece)
                    {
                        xPoints++;
                        Debug.Log("Level - X Points: " + xPoints);
                        if (singlePlayerUI.activeSelf)
                        {//updates score board for player 1
                            singlePlayer1.text = "Player 1 (X) - " + xPoints;
                        }
                        else
                        {
                            multiPlayer1.text = "Player 1 (X) - " + xPoints;
                        }
                    }
                    else
                    {
                        oPoints++;
                        Debug.Log("Level - O Points: " + oPoints);
                        if (singlePlayerUI.activeSelf)
                        {//updates score board for player 2
                            botPlayer.text = "Bot (O) - " + oPoints;
                        }
                        else
                        {
                            multiPlayer2.text = "Player 2 (O) - " + oPoints;
                        }
                    }

                }
            }
        }

        private void CheckDiagonalWin(string one, string two, string three)
        {
            if ((one[0] == two[0] && two[0] == three[0]) || ((one[0] != two[0] && two[0] != three[0]) && one[0] != three[0]))
            {
                one = one.Substring(1);
                two = two.Substring(1);
                three = three.Substring(1);

                if (String.Equals(one, "22"))
                {
                    CheckCornersWin(two, three);
                }
                else if (String.Equals(two, "22"))
                {
                    CheckCornersWin(one, three);
                }
                else if (String.Equals(three, "22"))
                {
                    CheckCornersWin(two, one);
                }
            }
        }

        private void CheckCornersWin(string cornerOne, string cornerTwo)
        {
            bool point = false;
            if (String.Equals(cornerOne, "11") && String.Equals(cornerTwo, "33"))
            {
                point = true;
            }
            else if (String.Equals(cornerOne, "33") && String.Equals(cornerTwo, "11"))
            {
                point = true;
            }
            else if (String.Equals(cornerOne, "13") && String.Equals(cornerTwo, "31"))
            {
                point = true;
            }
            else if (String.Equals(cornerOne, "31") && String.Equals(cornerTwo, "13"))
            {
                point = true;
            }

            if (point)
            {
                if (isXPiece)
                {
                    xPoints++;
                    Debug.Log("Diag - X Points: " + xPoints);
                    if (singlePlayerUI.activeSelf)
                    {//updates score board for player 1
                        singlePlayer1.text = "Player 1 (X) - " + xPoints;
                    }
                    else
                    {
                        multiPlayer1.text = "Player 1 (X) - " + xPoints;
                    }
                }
                else
                {
                    oPoints++;
                    Debug.Log("Diag - O Points: " + oPoints);
                    if (singlePlayerUI.activeSelf)
                    {//updates score board for player 2
                        botPlayer.text = "Bot (O) - " + oPoints;
                    }
                    else
                    {
                        multiPlayer2.text = "Player 2 (O) - " + oPoints;
                    }
                }
            }
        }
    }
}
