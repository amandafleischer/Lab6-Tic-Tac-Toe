using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private InputAction action;

        GameObject spotSelect;
        int cellPosition = 0;
        bool isXPiece = true;

        void Start()
        {
            Debug.Log(emptyCells[0].name);
            spotSelect = Instantiate(xChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity);
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
                spotSelect = Instantiate(xChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity);
            }
            else
            {
                spotSelect = Instantiate(oChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity);
            }
            
        }

        private void PlacePiece(InputAction.CallbackContext context)
        {
            Destroy(spotSelect);

            if(isXPiece)
            {
                Instantiate(xPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(oPlacedPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity);
            }

            isXPiece = !isXPiece;

            filledCells.Add(emptyCells[cellPosition]);
            emptyCells.Remove(emptyCells[cellPosition]);

            cellPosition = 0;

            if (isXPiece)
            {
                spotSelect = Instantiate(xChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity);
            }
            else
            {
                spotSelect = Instantiate(oChoosingPrefab, new Vector3(emptyCells[cellPosition].transform.position.x, emptyCells[cellPosition].transform.position.y + 1.6f, emptyCells[cellPosition].transform.position.z), Quaternion.identity);
            }
        }

    }
}
