using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntervalCounting : MonoBehaviour {
	private Animator _animator;
	private int target_blue_score;
	private int target_red_score;
	private int blue_score;
	private int red_score;

	public Text blue_score_text;
	public Text red_score_text;
	public Text blue_comment_text;
	public Text red_comment_text;

	private static string LEADING_TEXT = "You are leading!";
	private static string BEHIND_TEXT = "You are fall behind!";
	private static string TIE_TEXT = "Tei!";

	private bool is_load_next_stage = false;

	public int add_score_update_frame;

	public bool IsCounting
	{
		get {return _animator.GetBool("IsCounting");}
		set {_animator.SetBool("IsCounting", value);}
	}

	public void Awake()
	{
		_animator = GetComponent<Animator> ();

		RectTransform rect = GetComponent<RectTransform> ();
		rect.position = new Vector3 (0.0f, 0.0f, 0.0f);

	}

	public void SetStartCounting()
	{
		_animator.SetBool ("IsStartCount", true);
	}

	public void Update()
	{
		if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("IntervalCountingEmpty")) 
		{			
			target_blue_score = GameManager.instance.blueScore;
			target_red_score = GameManager.instance.redScore;
			blue_score = 0;
			red_score = 0;
			blue_score_text.text = blue_score.ToString();
			red_score_text.text = red_score.ToString();
			_animator.SetBool ("IsFinishCount", false);

			// update comment
			if (target_blue_score > target_red_score){
				blue_comment_text.text = LEADING_TEXT;
				red_comment_text.text = BEHIND_TEXT;
			}
			else if (target_red_score > target_blue_score){
				red_comment_text.text = LEADING_TEXT;
				blue_comment_text.text = BEHIND_TEXT;
			}
			else{
				red_comment_text.text = TIE_TEXT;
				blue_comment_text.text = TIE_TEXT;
			}
		}
		else if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("IntervalCountingScore")) 
		{
			// Add score animation
			int add_score = 0;
			if (target_blue_score > target_red_score)
				add_score = (int)(target_blue_score / add_score_update_frame);
			else
				add_score = (int)(target_red_score / add_score_update_frame);

			if (blue_score >= target_blue_score){
				blue_score = target_blue_score;
			}
			else{
				blue_score += add_score;
			}

			if (red_score >= target_red_score){
				red_score = target_red_score;
			}
			else{
				red_score += add_score;
			}

			blue_score_text.text = blue_score.ToString();
			red_score_text.text = red_score.ToString();

			// Finish counting scoure
			if (blue_score >= target_blue_score && red_score >= target_red_score){
				_animator.SetBool ("IsFinishCount", true);
				_animator.SetBool ("IsStartCount", false);
			}
		}
		else if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("IntervalCountingFinish")) 
		{
			if (!is_load_next_stage){
				GameManager.instance.enter_next_level();
				is_load_next_stage = true;
			}
		}
	}
}
