using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace FleischerFouts.Lab6
{
    public class RotateBoard : MonoBehaviour
    {
        private InputAction inputAction;
        private GameObject[] gamePieces;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Rotation()
        {
            //gamePieces = GameObject.FindGameObjectsWithTag("BoardPiece");
            //foreach (var piece in gamePieces)
            //{
                //piece.transform.Rotate(0, 10, 0);
            //}
            transform.Rotate(0, 10, 0);
        }
    }
}
