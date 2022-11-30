using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

namespace FleischerFouts.Lab6
{
    public class RotateHandler
    {
        private RotateBoard rotate;

        public RotateHandler(InputAction rotateAction, RotateBoard rotate)
        {
            //subscribe to action
            rotateAction.performed += RotateAction_performed;
            rotateAction.Enable();
            this.rotate = rotate;
        }

        private void RotateAction_performed(InputAction.CallbackContext obj)
        {
            rotate.Rotation();
        }
    }
}
