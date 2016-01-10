﻿using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {
	public float speed;

	private float centre_line_pos_y;

	void Start () {
		speed = 2.0f;

		centre_line_pos_y = 0.0f;
	}

	void FixedUpdate () {
		Vector2 paddle_origin_pos = gameObject.transform.position;
		Vector2 paddle_update_pos = paddle_origin_pos;

		// 讀取玩家觸控位置
		int touchCount = Input.touchCount;
		if (touchCount > 0){
			Touch touch = Input.GetTouch(0);
			Vector3 touch_target = Camera.main.ScreenToWorldPoint(touch.position);
			
			if(touch_target.y > centre_line_pos_y)
				return;

			// 移動 Paddle
			if(Mathf.Abs(touch_target.x - paddle_update_pos.x) <= speed)
				paddle_update_pos.x = touch_target.x;
			else{
				if(touch_target.x < paddle_update_pos.x)
					paddle_update_pos.x -= speed;
				else
					paddle_update_pos.x += speed;
			}


			if(touch.phase == TouchPhase.Began){
			}
			else if(touch.phase == TouchPhase.Moved){
			}
			else if(touch.phase == TouchPhase.Ended){
			}
		}

		gameObject.transform.position = paddle_update_pos;
	}
}