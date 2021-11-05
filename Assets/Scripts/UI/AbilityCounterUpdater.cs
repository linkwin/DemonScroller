using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCounterUpdater : MonoBehaviour {

    public CountdownTimer timer;

    private Image img;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time < timer.Timestamp + timer.AmmountOfTime)
            img.color = new Color(img.color.r, img.color.b, img.color.b, 0.4f);
        else
            img.color = new Color(img.color.r, img.color.b, img.color.b, 1);
		
	}
}
