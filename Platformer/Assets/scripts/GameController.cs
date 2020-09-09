using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GameController : MonoBehaviour {


	private bool playing;
	private MobSpawner spawner;

	public GameObject playButton;
	public Camera cam;
	public GameObject splashScreen;

	public bool IsPlaying { get { return playing; } }

	// Use this for initialization
	void Start() {
		if (cam == null) {
			cam = Camera.main;
		}
		spawner = LoadingUtils.LoadAndValidate<MobSpawner>(gameObject);
		LoadingUtils.Validate<GameObject>(playButton);
		LoadingUtils.Validate<Camera>(cam);
		LoadingUtils.Validate<GameObject>(splashScreen);

		if (!splashScreen.activeSelf) {
			StartGame();
		}
	}

	public void StartGame() {
		splashScreen.SetActive(false);
		playButton.SetActive(false);
		playing = true;
		spawner.StartGame();
	}
}
