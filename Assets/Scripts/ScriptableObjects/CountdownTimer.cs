using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CountdownTimer : ScriptableObject, ISerializationCallbackReceiver
{
    public float AmmountOfTime;

    [NonSerialized]
    public float Timestamp;

    public void OnAfterDeserialize()
    {
        Timestamp = -AmmountOfTime;
    }

    public void OnBeforeSerialize()
    {
    }
}
