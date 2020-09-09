using UnityEngine;
using System.Collections;

public class ClampedCamera : MonoBehaviour {

	private float previousX;
	private Vector2 targetPosition;
	private Camera camera;

	public GameObject basedOn;
	public float maxX;
	public float minX;

	// Use this for initialization
	void Start() {
		this.previousX = basedOn.transform.position.x;
		this.camera = GetComponent<Camera>();
	}

	void Update() {
		float lerpT = .45f;
		float x = Mathf.Lerp(camera.transform.localPosition.x, targetPosition.x, lerpT);
		//float y = Mathf.Lerp(camera.transform.localPosition.y, targetPosition.y, lerpT);f
		float y = camera.transform.localPosition.y;
		this.camera.transform.localPosition = new Vector3(x, y);
	}

	// Update is called once per frame
	void LateUpdate() {
		float newX = basedOn.transform.position.x;
		float delta = previousX - newX;
		// have we moved?
		if (delta != 0f) {
			float newPosition = this.camera.transform.localPosition.x - delta;
			float x = Mathf.Clamp(newPosition, minX, maxX); ;
			float y = this.camera.transform.localPosition.y;

			this.targetPosition = new Vector2(x, y);
			this.previousX = newX;
		}
	}
}
