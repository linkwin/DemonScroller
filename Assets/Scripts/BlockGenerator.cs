using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

	public int xBlocks = 1;
	public int yBlocks = 1;
	public GameObject topLeft;
	public GameObject top;
	public GameObject topRight;
	public GameObject left;
	public GameObject center;
	public GameObject right;
	public GameObject bottomLeft;
	public GameObject bottom;
	public GameObject bottomRight;
	// Use this for initialization
	void Start () {
		for (int x = 0; x < xBlocks; x++)
		{
			for (int y = 0; y < yBlocks; y++)
			{
				GameObject obj;

				if (x == 0 && y == 0) obj = bottomLeft;
				else if (x == xBlocks - 1 && y == 0) obj = bottomRight;
				else if (y == 0) obj = bottom;
				else if (x == 0 && y == yBlocks - 1) obj = topLeft;
				else if (x == xBlocks - 1 && y == yBlocks - 1) obj = topRight;
				else if (x == 0) obj = left;
				else if (x == xBlocks - 1) obj = right;
				else if (y == yBlocks - 1) obj = top;
				else obj = center;

				Instantiate(obj, new Vector2(gameObject.transform.position.x + x, gameObject.transform.position.y + y), Quaternion.identity, transform);
			}
		}
		// var boxCollider = gameObject.GetComponent<BoxCollider2D>();
		// boxCollider.size = new Vector2(xBlocks, yBlocks);

		// float xValue = xBlocks != 1f ? ((float)xBlocks / 2f - 0.5f) : 0f;
		// float yValue = yBlocks != 1f ? ((float)yBlocks / 2f - 0.5f) : 0f;

		// boxCollider.offset = new Vector2(xValue, yValue);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
