using UnityEngine;
using System.Collections;

public class AlertLight : MonoBehaviour {
	public float rotation_speed = 360.0f;
	public bool is_counter_clockwise = true;

	public float fade_in_duration = 10.0f;

	private float fade_out_value;

	void Start() {
		fade_out_value = fade_in_duration;
	}

	// Update is called once per frame
	void Update () {
		if (is_counter_clockwise)
			this.transform.Rotate (Vector3.forward * Time.deltaTime * rotation_speed);
		else
			this.transform.Rotate (Vector3.forward * Time.deltaTime * -rotation_speed);

		fade_out_value -= Time.deltaTime;
		float lerp_value = fade_out_value /fade_in_duration;
		float alpha = Mathf.Lerp (0.0f, 1.0f, lerp_value);

		Renderer rend = GetComponent<Renderer> ();
		Color obj_material = rend.material.color;
		obj_material.a = alpha;
		rend.material.color = obj_material;

		if (alpha <= 0.0f)
			Destroy (this.gameObject.transform.parent.gameObject);
	}
}
