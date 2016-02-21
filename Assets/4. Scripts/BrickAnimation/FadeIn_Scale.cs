using UnityEngine;
using System.Collections;

public class FadeIn_Scale : MonoBehaviour {
	float time = 0.0f;
	float scale = 0.0f;
	private float yVelocity = 0.0F;

	// Use this for initialization
	void Start () {		
		time = Random.Range (0.2f, 0.5f);
		this.transform.localScale = new Vector3 (scale, scale, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (scale < 1.0f) {
			scale = Mathf.SmoothDamp(scale, 1.0f, ref yVelocity, time);
			this.transform.localScale = new Vector3 (scale, scale, 1.0f);
		}
	}
}
