using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Team{
	BLUE, RED
}

public class GameManager : MonoBehaviour {
	public int total_bricks_count = 0;
	public int blueScore = 0;
	public int redScore = 0;
	public int level_id = 1;

	// Game object
	public GameObject bluePaddlePref;
	public GameObject redPaddlePref;
	public GameObject BallPref;
	public GameObject alertLightBluePref;
	public GameObject alertLightRedPref;

	public static GameManager instance = null;

	public int loss_ball_point = 300;

	private GameObject playerBluePaddle;
	private GameObject playerRedPaddle;

	List<GameObject> balls = new List<GameObject>();

	private GameObject ballHoldedByBluePaddle;
	private GameObject ballHoldedByRedPaddle;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		Setup();
	}

	// Setup paddle and load level bricks 
	public void Setup ()
	{
		playerBluePaddle = Instantiate(bluePaddlePref, new Vector3(0.0f, -4.0f, 0.0f), Quaternion.identity) as GameObject;
		playerRedPaddle = Instantiate(redPaddlePref, new Vector3(0.0f,  4.0f, 0.0f), Quaternion.identity) as GameObject;
		playerBluePaddle.name = "BluePaddle";
		playerRedPaddle.name = "RedPaddle";
		reborn_ball(Team.BLUE);
		reborn_ball(Team.RED);
	}

	public void Start()
	{		
		load_level (level_id);
	}

	public void enter_next_level()
	{
		level_id += 1;
		load_level (level_id);
	}

	public void load_level(int _id)
	{
		// Level 
		string load_level_name = "Levels/Level_" + _id;
		GameObject level = Instantiate (Resources.Load (load_level_name), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;

		total_bricks_count = level.GetComponent<Level>().getBrickCount();
	}

	public void release_ball(Team team){
		if (team == Team.BLUE && ballHoldedByBluePaddle != null){  
			BallController bc = ballHoldedByBluePaddle.GetComponent<BallController>();
			bc.be_released();

		}
		else if (team == Team.RED && ballHoldedByRedPaddle != null){
			BallController bc = ballHoldedByRedPaddle.GetComponent<BallController>();
			bc.be_released();
		}
	}

	public void reborn_ball(Team team){
		if (team == Team.BLUE){
			float paddle_x = playerBluePaddle.transform.position.x;
			ballHoldedByBluePaddle = Instantiate(BallPref, new Vector3(paddle_x,  -3.0f, 0.0f), Quaternion.identity) as GameObject;
			ballHoldedByBluePaddle.GetComponent<BallController>().set_team_color(Team.BLUE);
			balls.Add(ballHoldedByBluePaddle);
		}
		else if (team == Team.RED){
			float paddle_x = playerRedPaddle.transform.position.x;
			ballHoldedByRedPaddle = Instantiate(BallPref, new Vector3(paddle_x,  3.0f, 0.0f), Quaternion.identity) as GameObject;
			ballHoldedByRedPaddle.GetComponent<BallController>().set_team_color(Team.RED);
			balls.Add(ballHoldedByRedPaddle);
		}
	}

	public void loss_ball(Team team){		
		GameObject.FindWithTag("MainCamera").GetComponent<CameraEffect>().CameraShake();
		if (team == Team.BLUE){
			Instantiate(alertLightRedPref, new Vector3(0.0f, 5.7f, 0.0f), Quaternion.identity);
			redScore -= loss_ball_point;
		}
		else if (team == Team.RED){
			Instantiate(alertLightBluePref, new Vector3(0.0f, -5.7f, 0.0f), Quaternion.identity);
			blueScore -= loss_ball_point;
		}
	}

	public void destory_ball(GameObject ball){
		balls.Remove(ball);
	}

	public void destory_all_balls(){
		foreach (GameObject ball in balls)
		{
			Destroy(ball);
		}

		balls.Clear();
	}


	public void breakBrick(int break_score, Team team)
	{
		if (team == Team.BLUE)
			blueScore += break_score;
		else if (team == Team.RED)
			redScore += break_score;

		total_bricks_count--;

		if (total_bricks_count <= 0){
			foreach (GameObject ball in balls)
			{
				if(ball != null)
					ball.GetComponent<BallController>().set_slow_down_to_stop_and_delete();
			}
			UIManager.instance.setIntervalCounting();
		}
	}

}
