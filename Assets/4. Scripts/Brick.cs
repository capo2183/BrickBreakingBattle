using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public int destory_hit_time = 1;
	public int break_score = 100;

	void OnCollisionEnter2D (Collision2D collision)
	{
		destory_hit_time--;

		if (destory_hit_time <= 0){
			// Bricks destroy
			Team team = collision.gameObject.GetComponent<BallController>().ball_team;
			GameManager.instance.breakBrick(break_score, team);
			Destroy(gameObject);
		}
	}
}
