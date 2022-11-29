using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FleischerFouts.Input;

namespace FleischerFouts.Lab6
{
    public class PlaceObject : MonoBehaviour
    {

        private GameControls gameControls;

        private Camera mainCamera;

        private Ray ray;
        private RaycastHit hit;

        private bool player1;

        // Start is called before the first frame update
        void Start()
        {
            gameControls = new GameControls();
            mainCamera = Camera.main;
            player1 = true;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void onClick()
        {
            Debug.Log("Mouse Click!");

            ray = mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Free")
            {//changes the tag of the space from "Free" to "Taken"; represents that an object is already in space
                Debug.Log("Hit free space!");
                hit.collider.tag = "Taken";
                //TODO: spawn X or O in the space clicked (another method from a different class??)
                spawnPiece(hit.collider.gameObject);
            } else
            { //just for purpose of testing; delete later. 
                Debug.Log("NOT a free space, try again");
            }
        }

        public void spawnPiece(GameObject space)
        {
            if (player1)
            {
                //spawn an X in space
            } else
            {
                //spawn an O in space
            }
            player1 = !player1;
        }
    }
}
