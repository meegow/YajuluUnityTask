using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float floatValue;

    public float FloatValue
    {
        get { return floatValue; }
        set { floatValue = value; }
    }
}
