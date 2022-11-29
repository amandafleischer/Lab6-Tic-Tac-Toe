using FleischerFouts.Lab6;
using FleischerFouts.Input;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlaceObject placeObject;
    private GameControls inputScheme;

    private void Awake()
    {
        inputScheme = new GameControls();
    }

    private void OnEnable()
    {
        var placeHandler = new PlaceHandler(inputScheme.Player.Click, this.placeObject);
    }
}