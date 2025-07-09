using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public LayerMask clickableLayer;
    public Zone currentZone;

    #region Player Attributes
    private uint actionsPoints;
    #endregion

    public uint ActionsPoints
    {
        get => actionsPoints;
        set { if (value > 0) actionsPoints = value; else actionsPoints = 0; }
    }

    public void Init()
    {
        actionsPoints = 3;
    }

    public bool CanOpenDoor()
    {
        // Check that player have weapon with door opening ability
        // Add noize if it necessery
        return true;
    }
}
