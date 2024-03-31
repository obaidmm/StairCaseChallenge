using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestionDialog : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Button yesBtn;
    private Button noBtn;

    private void Awake()
    {
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();

        ShowQuestion("Answer this question to go back", () =>
        {
            Debug.Log("Yes");
        }, () =>
        {
            Debug.Log("No");
        });
    }

    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        textMeshPro.text = questionText;
        yesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(yesAction));
        noBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(noAction));
    }


}
