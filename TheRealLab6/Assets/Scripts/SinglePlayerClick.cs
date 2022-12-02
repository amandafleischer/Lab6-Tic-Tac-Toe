using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerClick : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonClicked()
    {
        //starts single player game, hides UI
        Debug.Log("Single Player Game");
        canvas.gameObject.SetActive(false);
    }
}
