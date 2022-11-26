using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectVariable", menuName = "Variables/Game Object Variable")]
public class GameObjectVariable : ScriptableObject
{
    [SerializeField] private GameObject gameObject;

    public GameObject ThisGameObject
    {
        get { return gameObject; }
        set { gameObject = value; }
    }

    public Transform GameObjectTransform
    {
        get { return gameObject.transform; }
    }
}
