using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : PlayerBase {
    [SerializeField] Renderer colorRenderer;


    protected override void Start() {
        base.Start();

    }

    protected override void FixedUpdate() {
        base.FixedUpdate();

    }

    public void AnswerQuestion() {
        StartCoroutine(Delay());
    }

    bool GetGoodAnswer() {
        int dif = QuestionManager._instance.currentQuestion.difficulty;

        int n = Random.Range(0, 100);
        if (dif == 1) {
            if (n < 77) return true;
            return false;
        }

        if (dif == 2) {
            if (n < 50) return true;
            return false;
        }

        if (dif == 3) {
            if (n < 27) return true;
            return false;
        }

        return false;
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(Random.Range(3.5f, 8f));

        bool isGood = GetGoodAnswer();

        if (isGood) {
            DoorObject d = QuestionManager._instance.GetGoodDoor();
            SetTransform(d.grounds[Random.Range(0, d.grounds.Length)]);
        } else {
            DoorObject d = QuestionManager._instance.GetBadDoor();
            SetTransform(d.grounds[Random.Range(0, d.grounds.Length)]);
        }
    }
}
