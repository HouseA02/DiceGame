using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindMap : Effect
{
    public override void Activate()
    {
        MapController mapController = FindAnyObjectByType<MapController>();
        mapController.BlindAll();
    }
}
