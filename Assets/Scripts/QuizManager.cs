using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionandAnswers> QnA;
    public GameObject[] options; // the array needs to have questions into it 
    public int currentQuestions; // variable will call a random one to the question
    public TMP_Text qTxt;

    void Start()
    {
        generateQuestion();
    }

    public void correct() // this method will help me create the next question, while removing the previous question
    {
        QnA.RemoveAt(currentQuestions);
        generateQuestion();
    }

    void generateQuestion() // method will just generate a new question everytime it's stepped on the blue plane
    {
        if (qTxt == null)
        {
            Debug.LogError("qTxt is not set. Please assign the Text component in the inspector.");
            return; // Prevent further execution if qTxt is null
        }

        currentQuestions = Random.Range(0, QnA.Count);
        qTxt.text = QnA[currentQuestions].Question;
        setAnswers();

        QnA.RemoveAt(currentQuestions);
    }

    //void setAnswers()
    //{
    //    for (int i = 0; i < options.Length; i++)
    //    {
    //        options[i].GetComponent<AnswerScript>().isCorrect = false;
    //        options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestions].Answer[i];


    //        if (QnA[currentQuestions].CorrectAnswer == i + 1)
    //        {
    //            options[i].GetComponent<AnswerScript>().isCorrect = true;
    //        }
    //    }
    //}


    void setAnswers()
    {
        if (options == null)
        {
            Debug.LogError("Options array is not set.");
            return;
        }
        if (QnA == null || QnA.Count <= currentQuestions)
        {
            Debug.LogError("QnA list is not properly initialized or the current question is out of range.");
            return;
        }
        if (QnA[currentQuestions].Answer == null)
        {
            Debug.LogError("Answer array for the current question is not set.");
            return;
        }

        for (int i = 0; i < options.Length; i++)
        {
            if (options[i] == null)
            {
                Debug.LogError("An option is not set at index " + i);
                continue;
            }

            TMP_Text answerText = options[i].transform.childCount > 0
                ? options[i].transform.GetChild(0).GetComponent<TMP_Text>()
                : null;

            if (answerText != null)
            {
                answerText.text = QnA[currentQuestions].Answer[i];
                // other operations with answerText...
            }
            else
            {
                Debug.LogError("TMP_Text component not found on option " + i);
                // Ensure you don't use answerText if it's null
                continue; // Skip further operations and continue with the next iteration
            }


            options[i].GetComponent<AnswerScript>().isCorrect = (QnA[currentQuestions].CorrectAnswer == i + 1);
        }
    }



}
