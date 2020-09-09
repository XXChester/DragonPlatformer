using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public float time;
	public Text text;
	public GameController gameController;

	void Start() {
		LoadingUtils.Validate<Text>(text);
		LoadingUtils.Validate<GameController>(gameController);
	}
	void FixedUpdate() {
		if (gameController.IsPlaying) {
			time += Time.deltaTime;
			text.text = "Time:\n" + Mathf.RoundToInt(time).ToString();
		}
	}
}
