using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public enum SpawnStyle {FunctionCall, OnInterval, OneShot};

    public float spawnInterval;
    [Tooltip("Optionally define a period of time before first spawn.")]
    public float delayTime;
    public GameObject prefabToSpawn;
    public SpawnStyle spawnStyle;
    public Transform parent;

    private bool isRepeating;

    private void Start()
    {
        isRepeating = false;
    }

    private void OnEnable()
    {
        if (!parent)
            parent = this.transform;
        switch (spawnStyle)
        {
            case SpawnStyle.OnInterval:
                if (!isRepeating)
                {
                    InvokeRepeating("SpawnPrefab", delayTime, spawnInterval);
                    isRepeating = true;
                }
                break;
            case SpawnStyle.OneShot:
                Invoke("SpawnPrefab", delayTime);
                break;
            default:
                break;
        }
    }
	
    public void SpawnPrefab()
    {
        if (isActiveAndEnabled)
            Instantiate(prefabToSpawn, transform.position, transform.rotation, parent);
    }
}
