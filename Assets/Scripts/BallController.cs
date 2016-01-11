using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public float obj_moving_speed;
	public GameObject game_controller;

	public Sprite red_ball_sprite;
	public Sprite blue_ball_sprite;

	public Vector2 init_forward_vec;
	public string ball_team;

	private Vector2 obj_forward_vec;
	private Vector2 boundary_left_bottom;
	private Vector2 boundary_right_top;
	private float boundary_margin;

	private bool collision_mutex = true;

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

		game_controller = GameObject.Find("GameManager");
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
			// 球掉出紅色區域
			if (ball_team == "BLUE")
				GameObject.FindWithTag("MainCamera").GetComponent<CameraEffect>().CameraShake();
			Destroy(this.gameObject);
		}
		if(ball_position.y < boundary_left_bottom.y + boundary_margin) {
			if (ball_team == "RED")
				GameObject.FindWithTag("MainCamera").GetComponent<CameraEffect>().CameraShake();
			Destroy(this.gameObject);
		}

		collision_mutex = true;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if(collision.gameObject.tag == "Paddle"){
			if (collision_mutex){
				// 當球碰撞到拍子時
				collision_mutex = false;
				ContactPoint2D contact = collision.contacts[0];
				SpriteRenderer paddle_renderer = collision.gameObject.GetComponent<SpriteRenderer>();
				float paddle_renderer_left = paddle_renderer.bounds.min.x;
				float paddle_renderer_right = paddle_renderer.bounds.max.x;
				float contact_paddle_pos_bias = (contact.point.x-paddle_renderer_left)/(paddle_renderer_right-paddle_renderer_left);

				obj_forward_vec.x = Mathf.Lerp(-0.9f, 0.9f, contact_paddle_pos_bias);
				obj_forward_vec.y = -obj_forward_vec.y;

				PaddleController paddle_controller = collision.gameObject.GetComponent("PaddleController") as PaddleController;
				if (paddle_controller.team == "RED"){
					this.GetComponent<SpriteRenderer>().sprite = red_ball_sprite;
					ball_team = "RED";
				}
				else if (paddle_controller.team == "BLUE"){
					this.GetComponent<SpriteRenderer>().sprite = blue_ball_sprite;
					ball_team = "BLUE";
				}
			}
		}
		else if(collision.gameObject.tag == "Bricks"){
			if (collision_mutex){
				collision_mutex = false;
				ContactPoint2D contact = collision.contacts[0];
				obj_forward_vec = Vector3.Reflect(obj_forward_vec, contact.normal).normalized;
			}
		}
	}
}
