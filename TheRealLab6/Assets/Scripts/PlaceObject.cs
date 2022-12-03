using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FleischerFouts.Lab6;

//written by Hannah

namespace FleischerFouts.Lab6
{
    public class PlaceObject : MonoBehaviour
    {
        [SerializeField] GameObject xPlacedPrefab;
        [SerializeField] GameObject oPlacedPrefab;

        private InputAction action;
    }
}
