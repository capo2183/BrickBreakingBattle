using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public int destory_hit_time = 1;
	public 

	void OnCollisionEnter2D (Collision2D other)
	{
		destory_hit_time--;

		if (destory_hit_time <= 0){
			// Bricks destroy
			GameManager.instance.breakBrick();
			Destroy(gameObject);
		}
	}
}
