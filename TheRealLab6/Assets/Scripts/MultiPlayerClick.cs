using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerClick : MonoBehaviour
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

    public void buttonClick()
    {
        //starts multi-player game, hides UI
        Debug.Log("Multi-Player Game");
        canvas.gameObject.SetActive(false);
    }
}
