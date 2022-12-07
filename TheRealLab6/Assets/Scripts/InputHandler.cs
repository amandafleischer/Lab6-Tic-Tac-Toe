using FleischerFouts;
using FleischerFouts.Lab6;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Hannah and Amanda

namespace FleischerFouts.Lab6
{
    public class InputHandler : MonoBehaviour
    {
        private Controls input;
        [SerializeField] private MoveObject pieceMovement;
        [SerializeField] private RotateBoard rotateBoard;
        [SerializeField] private RestartGame restartGame;


        private void Awake()
        {
            input = new Controls();
            pieceMovement.Initialize(input.GamePiece.MovePiece, input.GamePiece.PlacePiece);
        }

        private void OnEnable()
        {
            var _ = new QuitHandler(input.GamePiece.Quit);
            var rotateHandler = new RotateHandler(input.GamePiece.Rotate, this.rotateBoard);
            var restartHandler = new RestartHandler(input.GamePiece.Restart, this.restartGame);
        }
    }
}
