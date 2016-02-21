using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	private int brick_count = 0;

	// Use this for initialization
	void Awake () {
		foreach (Transform child in transform)
		{
			//child is your child transform
			Brick c_brick = child.gameObject.GetComponent<Brick>();
			if (c_brick != null)
				brick_count++;
		}
	}

	public int getBrickCount()
	{
		return brick_count;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
