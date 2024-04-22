using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public static CameraFollow _instance;

    public Transform player;
    Vector3 offset;

    private void Awake() {
        _instance = this;
    }

    private void Start() {
        offset = new Vector3(-1.2f, -11.2f, -20.7f);
    }

    private void LateUpdate() {
        if (player == null) return;

        transform.position = player.position - offset;
    }

    public void SetPlayer(Transform t) {
        player = t;
    }
}
