using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour {
    public static GameUi _instance;

    public TMP_Text levelText;


    private void Awake() {
        _instance = this;
    }

    public void SetUi(int lvl) {
        levelText.text = "Level " + (lvl + 1) + "";
    }


    public void RestartScene() {
        SceneManager.LoadScene(0);
    }
}
