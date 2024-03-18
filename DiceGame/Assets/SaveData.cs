using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int souls;

    public SaveData (Player player)
    {
        souls = player.souls;
    }
}
