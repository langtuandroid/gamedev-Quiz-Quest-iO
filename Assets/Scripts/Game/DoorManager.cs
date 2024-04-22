using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DoorObject {
    public Animator animator;
    public TMP_Text answer;
    public Transform[] grounds;
}

public class DoorManager : MonoBehaviour {
    public static DoorManager _instance;

    public DoorObject[] doors;


    private void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    public void OpenDoor(int id) {
        int animId = Random.Range(0, 2);

        doors[id].animator.Play("Rotate" + animId);
    }

}
