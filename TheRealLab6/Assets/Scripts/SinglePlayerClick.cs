using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Amanda

namespace FleischerFouts.Lab6
{
    public class SinglePlayerClick : MonoBehaviour
    {
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject playMenu;
        [SerializeField] GameObject gameOver;

        void Start()
        {
            playMenu.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(false);
        }

        public void buttonClicked()
        {
            //starts single player game, hides main menu, starts game UI
            Debug.Log("Single Player Game");
            mainMenu.gameObject.SetActive(false);
            playMenu.gameObject.SetActive(true);
        }
    }
}
