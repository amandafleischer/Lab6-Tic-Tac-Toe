using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace FleischerFouts.Lab6
{
    public class RotateBoard : MonoBehaviour
    {
        private InputAction inputAction;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Rotation()
        {
            transform.Rotate(0, 10, 0);
        }
    }
}
