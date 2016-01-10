using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public float obj_moving_speed;
	public GameObject game_controller;

	private Vector2 obj_forward_vec;
	private Vector2 boundary_left_bottom;
	private Vector2 boundary_right_top;
	private float boundary_margin;

	// Use this for initialization
	void Start () {
		obj_forward_vec = new Vector2(0.0f, -1.0f);
		obj_moving_speed = 0.2f;
		boundary_margin = 0.15f;

		obj_forward_vec.Normalize();

		// 計算螢幕邊界位置
		Vector2 screen_boundary_left_bottom = new Vector2(0.0f, -15.0f);
		Vector2 screen_boundary_right_top = new Vector2(Screen.width, Screen.height);
		boundary_left_bottom = Camera.main.ScreenToWorldPoint(screen_boundary_left_bottom);
		boundary_right_top = Camera.main.ScreenToWorldPoint(screen_boundary_right_top);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 origin_pos = gameObject.transform.position;
		gameObject.transform.position = origin_pos + (obj_forward_vec * obj_moving_speed);

		// 若球超出螢幕邊界，則改變球的方向
		Vector2 ball_position = gameObject.transform.position;
		if(ball_position.x < boundary_left_bottom.x + boundary_margin || ball_position.x > boundary_right_top.x - boundary_margin){
			obj_forward_vec.x = -obj_forward_vec.x;
		}
		if(ball_position.y > boundary_right_top.y - boundary_margin){
			obj_forward_vec.y = -obj_forward_vec.y;
		}
		if(ball_position.y < boundary_left_bottom.y + boundary_margin) {
			GameObject.FindWithTag("MainCamera").GetComponent<CameraEffect>().CameraShake();
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if(collision.gameObject.tag == "Paddle"){
			// 當球碰撞到拍子時
			ContactPoint2D contact = collision.contacts[0];
			SpriteRenderer paddle_renderer = collision.gameObject.GetComponent<SpriteRenderer>();
			float paddle_renderer_left = paddle_renderer.bounds.min.x;
			float paddle_renderer_right = paddle_renderer.bounds.max.x;
			float contact_paddle_pos_bias = (contact.point.x-paddle_renderer_left)/(paddle_renderer_right-paddle_renderer_left);

			obj_forward_vec.x = Mathf.Lerp(-0.9f, 0.9f, contact_paddle_pos_bias);
			obj_forward_vec.y = -obj_forward_vec.y;

		}
		else if(collision.gameObject.tag == "Bricks"){
			ContactPoint2D contact = collision.contacts[0];
			obj_forward_vec = Vector3.Reflect(obj_forward_vec, contact.normal);
			Destroy(collision.gameObject);
		}
	}
}
