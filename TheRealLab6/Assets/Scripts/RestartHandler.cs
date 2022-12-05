using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

//written by Amanda

namespace FleischerFouts.Lab6
{
    public class RestartHandler
    {
        private RestartGame restart;

        public RestartHandler(InputAction restartAction, RestartGame restart)
        {
            //subscribe to action
            restartAction.performed += RestartAction_performed;
            restartAction.Enable();
            this.restart = restart;
        }

        private void RestartAction_performed(InputAction.CallbackContext obj)
        {
            restart.Restart();
        }
    }
}
