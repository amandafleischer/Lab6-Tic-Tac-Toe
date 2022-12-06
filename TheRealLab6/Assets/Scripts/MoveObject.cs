using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//written by Hannah
//edits by Amanda for Instantiate parenting, single player mode (bot), and game over

namespace FleischerFouts.Lab6
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] List<GameObject> emptyCells = new List<GameObject>();
        List<GameObject> filledCells = new List<GameObject>();

        [SerializeField] GameObject xChoosingPrefab;
        [SerializeField] GameObject oChoosingPrefab;
        [SerializeField] GameObject xPlacedPrefab;
        [SerializeField] GameObject oPlacedPrefab;
        [SerializeField] GameObject parent;
        //will be used later to tell if single player game is active (bot) 
        [SerializeField] GameObject singlePlayerUI;
        [SerializeField] GameObject MultiPlayerUI;
        [SerializeField] GameObject gameOverUI;

        private InputAction action;

        GameObject spotSelect;
        int cellPosition = 0;
        bool isXPiece = true;

        void Start()
        {
            Debug.Log(emptyCells[0].name);
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

                if(cellPosition >= emptyCells.Count)
                {
                    cellPosition = 0;
                }
            }
            else if(moveInput.x == -1)
            {
                cellPosition -= 1;

                if(cellPosition < 0)
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

            if(isXPiece)
            {
                Instantiate(xPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
            }
            else
            {
                //if in single player mode, choose a random empty cell to place game piece (bot)
                if (singlePlayerUI.gameObject.activeSelf)
                {
                    cellPosition = Random.Range(0, emptyCells.Count);
                    Instantiate(oPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);

                }
                else
                {
                    Instantiate(oPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity, parent.transform);
                }
            }

            isXPiece = !isXPiece;

            filledCells.Add(emptyCells[cellPosition]);
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
            } else
            {//diplay game over once there are no empty spaces left
                gameOverUI.gameObject.SetActive(true);
                singlePlayerUI.gameObject.SetActive(false);
                MultiPlayerUI.gameObject.SetActive(false);
            }
        }

    }
}
