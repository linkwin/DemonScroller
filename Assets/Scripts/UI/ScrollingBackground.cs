using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public Transform backgroundPrefab;
    public Transform background;
    public Transform canvas;

    public float scrollSpeed;
    public float spawnRate;
    private float lastSpawnTime;
    private Transform lastPanel;
    private Transform nextPanel;

    private void Start()
    {
        lastSpawnTime = 0f;
        lastPanel = background;
    }
    // Update is called once per frame
    void Update () {
        lastPanel.GetComponent<RectTransform>().position += Vector3.left * scrollSpeed * Time.deltaTime;
        if (Time.time >= lastSpawnTime + spawnRate)
        {
            nextPanel = GameObject.Instantiate(background, canvas);
            nextPanel.position = lastPanel.position + Vector3.right * 164;
            lastSpawnTime = Time.time;
        }
        if (nextPanel != null)
            nextPanel.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        
	}
}
