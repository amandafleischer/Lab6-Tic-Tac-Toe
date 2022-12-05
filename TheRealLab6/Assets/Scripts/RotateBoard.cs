using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//written by Amanda

namespace FleischerFouts.Lab6
{
    public class RotateBoard : MonoBehaviour
    {
        public void Rotation()
        {
            transform.Rotate(0, 10, 0);
        }
    }
}
