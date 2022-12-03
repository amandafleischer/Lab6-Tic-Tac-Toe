using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Amanda

namespace FleischerFouts.Lab6
{
    public class MultiPlayerClick : MonoBehaviour
    {
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject playMenu;

        // Start is called before the first frame update
        void Start()
        {
            playMenu.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void buttonClick()
        {
            //starts multi-player game, hides main menu, starts game UI
            Debug.Log("Multi-Player Game");
            mainMenu.gameObject.SetActive(false);
            playMenu.gameObject.SetActive(true);
        }
    }
}
