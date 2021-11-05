using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public Transform cam;
	private Vector3 prevCamPosition;
	public Transform[] backgrounds;
	private float[] scales;
	public float transitionTime = 1f;
    public float scale = 1;



	// Use this for initialization
	void Start () {
		prevCamPosition = cam.position;

		scales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++)
		{
			scales[i] = backgrounds[i].position.z * -scale;
		}

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++)
		{
			//float parallax = (prevCamPosition.x - cam.position.x) * scales[i];
			float parallaxX = (prevCamPosition.x - cam.position.x) / scales[i];
			float parallaxY = (prevCamPosition.y - cam.position.y) / scales[i];

			float x = backgrounds[i].position.x - parallaxX;
			float y = backgrounds[i].position.y - parallaxY;

			//Vector3 newVector = new Vector3(x, backgrounds[i].position.y, backgrounds[i].position.z);
			Vector3 newVector = new Vector3(x, y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, newVector, transitionTime * Time.deltaTime);
		}
		prevCamPosition = cam.position;
	}
}
