using FleischerFouts;
using FleischerFouts.Lab6;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Controls input;
    [SerializeField] private MoveObject pieceMovement;
    //[SerializeField] private MoveObject piecePlacement;


    private void Awake()
    {
        input = new Controls();
        pieceMovement.Initialize(input.GamePiece.MovePiece, input.GamePiece.PlacePiece);
        //piecePlacement.Initialize(input.GamePiece.PlacePiece);
    }

    private void OnEnable()
    {
        var _ = new QuitHandler(input.GamePiece.Quit);
    }
}
