using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {
	public string team;
	public float speed;
	public float ball_reborn_time;

	private float centre_line_pos_y;
	private bool is_ready_to_lunch_ball;
	private float ball_reborn_interval;

	void Start () {
		speed = 2.0f;
		centre_line_pos_y = 0.0f;
		ball_reborn_interval = 5.0f;
		is_ready_to_lunch_ball = true;

		ball_reborn_time = ball_reborn_interval;
	}

	void FixedUpdate () {
		Vector2 paddle_origin_pos = gameObject.transform.position;
		Vector2 paddle_update_pos = paddle_origin_pos;

		// 讀取玩家觸控位置
		int touchCount = Input.touchCount;
		if (touchCount > 0){
			for (int i = 0; i < Input.touchCount; ++i) {
				Touch touch = Input.GetTouch(i);
				Vector3 touch_target = Camera.main.ScreenToWorldPoint(touch.position);

				if(team == "BLUE" && touch_target.y > centre_line_pos_y)
					continue;
				if(team == "RED" && touch_target.y < centre_line_pos_y)
					continue;

				// 移動 Paddle
				if(Mathf.Abs(touch_target.x - paddle_update_pos.x) <= speed)
					paddle_update_pos.x = touch_target.x;
				else{
					if(touch_target.x < paddle_update_pos.x)
						paddle_update_pos.x -= speed;
					else
						paddle_update_pos.x += speed;
				}

				// 觸控狀態
				if (is_ready_to_lunch_ball && Input.GetTouch(i).phase == TouchPhase.Ended) {
					GameManager.instance.release_ball(team);
					is_ready_to_lunch_ball = false;
				}
			}
		}

		// 倒數重生球時間
		if (is_ready_to_lunch_ball == false && ball_reborn_time > 0.0f)
			ball_reborn_time -= Time.deltaTime;

		if (ball_reborn_time <= 0.0f){
			GameManager.instance.reborn_ball(team);
			is_ready_to_lunch_ball = true;
			ball_reborn_time = ball_reborn_interval;
		}


		gameObject.transform.position = paddle_update_pos;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if(collision.gameObject.tag == "Ball"){
			ball_reborn_time = ball_reborn_interval;
		}
	}
}