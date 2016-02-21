using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {	
	public int destory_hit_time;

	public int break_score = 100;
	public Color[] stage_color;

	void Start()
	{
		destory_hit_time = 0;
		if (stage_color.Length > 0)
			this.GetComponent<SpriteRenderer>().color = stage_color[destory_hit_time];
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		destory_hit_time++;

		if (destory_hit_time >= stage_color.Length){
			// Bricks destroy
			Team team = collision.gameObject.GetComponent<BallController>().ball_team;
			GameManager.instance.breakBrick(break_score, team);
			Destroy(gameObject);
		}
		else{
			this.GetComponent<SpriteRenderer>().color = stage_color[destory_hit_time];
		}
	}
}
