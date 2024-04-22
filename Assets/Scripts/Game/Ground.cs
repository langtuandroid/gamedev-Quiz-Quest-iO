using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    public bool isAnswer;

    private void OnMouseUpAsButton() {
        if (!GameManager._instance.canMove) return;

        if (isAnswer) {
            GameManager._instance.player.SetTransform(transform);
        }
    }
}
