using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	public int score;
	public Text text;

	void Start() {
		LoadingUtils.Validate<Text>(text);
	}
	// Update is called once per frame
	void Update() {
		if (text != null) {
			text.text = "Score\n" + score.ToString();
		}
	}
}
