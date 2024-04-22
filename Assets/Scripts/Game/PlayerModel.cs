using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour {
    public Renderer skin;
    public Color[] colors;

    public SpriteRenderer flag;
    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start() {
        skin.material.color = colors[Random.Range(0, colors.Length)];

        flag.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void Update() {
        flag.transform.eulerAngles = Vector3.right * 60;
    }
}
