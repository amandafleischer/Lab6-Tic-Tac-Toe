using UnityEngine;
using UnityEngine.InputSystem;

namespace FleischerFouts.Lab6
{
    public class PlaceHandler
    {

        private PlaceObject placeObject;

        public PlaceHandler(InputAction mouseClick, PlaceObject placeObject)
        {
            //subscribes to action, then stores instance of CameraSwitcher 
            mouseClick.performed += MouseClick_performed;
            mouseClick.Enable();
            this.placeObject = placeObject;
        }

        private void MouseClick_performed(InputAction.CallbackContext obj)
        {
            placeObject.onClick();
        }
    }
}