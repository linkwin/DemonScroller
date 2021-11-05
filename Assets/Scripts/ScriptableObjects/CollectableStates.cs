using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollectableStates : ScriptableObject {

    public Transform[] Transforms;
    public bool[] Collected;

}
