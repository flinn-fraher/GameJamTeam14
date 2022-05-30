using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Flinn;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        var initialisableObjects = FindObjectsOfType<MonoBehaviour>().OfType<IInitialisable>();
        if (initialisableObjects.Any())
        {
            foreach (var initObj in initialisableObjects)
            {
                initObj.Init();
            }
        }
    }
}
