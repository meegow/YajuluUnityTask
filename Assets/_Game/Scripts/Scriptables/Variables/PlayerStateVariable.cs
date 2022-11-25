using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateVariable", menuName = "Variables/Player State Variable")]
public class PlayerStateVariable : ScriptableObject
{
    private PlayerStates currentSate;

    public PlayerStates CurrentSate
    {
        get{ return currentSate; }
        set{ currentSate = value; }
    }
    
}
