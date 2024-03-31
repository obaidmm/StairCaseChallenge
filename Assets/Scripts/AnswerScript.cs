using UnityEngine;
using System.Collections;

public class AnswerScript : MonoBehaviour
{
	public bool isCorrect = false;
	public QuizManager quizManager;

	public void Answer()
	{
		Debug.Log("Button Clicked");

		if (isCorrect)
		{
			Debug.Log("Correct Answer");
			quizManager.correct();
		}
		else
		{
            Debug.Log("Wrong Answer");
			quizManager.correct();
        }
	}
}

