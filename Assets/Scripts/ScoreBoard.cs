using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
	[SerializeField]
	private AudioSource goalAudio;

	[SerializeField]
	private TextMeshPro _textMeshPro;

	private int _score = 0;

	public void OnGoalScored()
	{
		_score++;
		if (_textMeshPro != null)
			_textMeshPro.text = _score.ToString();
		if(goalAudio != null)
			goalAudio.Play();
	}
}
