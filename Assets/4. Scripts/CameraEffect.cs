using UnityEngine;
using System.Collections;

public class CameraEffect : MonoBehaviour {
	public float shake_duration;
	public float shake_magnitude;

	private bool is_shake;

	private float elapsed;
	private Vector3 originalCamPos;

	void Start(){
		shake_duration = 0.5f;
		shake_magnitude = 0.3f;
		is_shake = false;

		elapsed = 0.0f;
	}

	void FixedUpdate () {
		if (is_shake) {
			elapsed += Time.deltaTime;
			float percentComplete = elapsed / shake_duration;         
			float damper = 1.0f - Mathf.Clamp(percentComplete, 0.0f, 1.0f);
			
			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= shake_magnitude * damper;
			y *= shake_magnitude * damper;

			this.transform.position = new Vector3(x, y, -10);
			if (elapsed > shake_duration){
				is_shake = false;
				elapsed = 0.0f;
				this.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
			}
		}
	}

	public void CameraShake(){
		is_shake = true;
	}
}
