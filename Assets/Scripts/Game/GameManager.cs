using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;

    //Grounds which are not part of doors
    [SerializeField] GameObject[] grounds;

    [Space(12)]
    public bool isPaused, canMove = true;

    [Header("Prefabs")]
    [Space(12)]
    public PlayerBase player, aiPrefab;
    List<PlayerBase> ais;

    public int levelNum;

    private void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Application.targetFrameRate = 60;


        levelNum = PlayerPrefs.GetInt("CURRENT_LEVEL");
        GameUi._instance.SetUi(levelNum);

        ais = new List<PlayerBase>();

        for (int i = 0; i < Mathf.Min(levelNum * 2 + 5, 26); i++) {
            var ai = Instantiate(aiPrefab);
            ai.transform.position = grounds[Random.Range(0, grounds.Length)].transform.position + Vector3.up;

            ais.Add(ai);
        }
    }

    public void SetPlayerPositions() {
        //player.SetTransform(grounds[Random.Range(0, grounds.Length)].transform);

        foreach (PlayerBase p in ais) {
            p.SetTransform(grounds[Random.Range(0, grounds.Length)].transform);
        }
    }

    public void Victory() {
        player.animator.Play("Victory");

        foreach (PlayerBase p in ais) {
            p.animator.Play("Victory");
        }

        winPanel.SetActive(true);

        PlayerPrefs.SetInt("CURRENT_LEVEL", levelNum + 1);
    }

    [SerializeField] GameObject loosePanel, winPanel;

    public void GameOver() {
        loosePanel.SetActive(true);
    }

    public void RemoveAi(PlayerBase p) {
        ais.Remove(p);

        Destroy(p.gameObject);
    }

    public void AiAnswer() {
        foreach (AiController ai in ais) {
            ai.AnswerQuestion();
        }
    }

    public void ResetScene() {
        SceneManager.LoadScene(0);
    }
}
