using UnityEngine;
using System.Collections;

[System.Serializable]
public class Question {
    private string question;
    public string[] answers = new string[4];

    public int difficulty;

    public Question(string question, string[] a, int dif) {
        this.question = question;

        answers = a;

        difficulty = dif;
    }

    public string GetQuestion() {
        return question;
    }

    public string GetCorrectAnswer() {
        return answers[0];
    }
}