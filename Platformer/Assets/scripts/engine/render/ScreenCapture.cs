using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ScreenCapture : MonoBehaviour {

	private enum CaptureState { Init, Capture, TearDown }

	private CaptureState captureState;

	public GameObject[] toHide;
	public KeyCode invokeOn;
	public string screenShotName;

	private const string DATE_FORMAT = "MM-dd-yyyy hh-mm-ss";


	private void invertVisibility() {
		foreach (var temp in toHide) {
			if (temp != null) {
				temp.SetActive(!temp.activeSelf);
			}
		}
	}

	void Update() {
		if (toHide == null || toHide.Length == 0) {
			StartCoroutine(TakeScreenShot());
		} else {
			// We need to potentially hide items so the screen shot process takes 3 frames.\
			if (CaptureState.TearDown.Equals(this.captureState)) {
				invertVisibility();
				this.captureState = CaptureState.Init;
			} else if (CaptureState.Capture.Equals(this.captureState)) {
				StartCoroutine(TakeScreenShot());
				this.captureState = CaptureState.TearDown;
			}
			if (Input.GetKeyUp(invokeOn)) {
				this.captureState = CaptureState.Capture;
				invertVisibility();
			}
		}
	}
	IEnumerator TakeScreenShot() {
		yield return new WaitForEndOfFrame();
		Texture2D sshot = new Texture2D(Screen.width, Screen.height);
		sshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		sshot.Apply();
		byte[] pngShot = sshot.EncodeToPNG();
		Destroy(sshot);
		File.WriteAllBytes(Application.dataPath + "/../screenshot_"  + DateTime.Now.ToString(DATE_FORMAT) + ".png", pngShot);
	}
}
