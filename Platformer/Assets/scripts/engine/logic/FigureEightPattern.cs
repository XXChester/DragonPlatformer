using UnityEngine;
using System.Collections;

namespace engine.logic {
	public class FigureEightPattern : MonoBehaviour {

		private Vector3 pivot;
		private Vector3 pivotOffset;
		private float phase;
		private bool invert = false;
		private float twoPi = Mathf.PI * 2;


		public float speed = 1f;
		public Vector2 scale = new Vector2(1f, 1f);
		public bool layingDown = false;


		// Use this for initialization
		void Start() {
			pivot = transform.position;
		}

		// Update is called once per frame
		void LateUpdate() {
			pivotOffset = Vector3.up * 2 * scale.y;

			phase += speed * Time.deltaTime;
			if (phase > twoPi) {
				invert = !invert;
				phase -= twoPi;
			}
			if (phase < 0) {
				phase += twoPi;
			}

			transform.position = pivot + (invert ? pivotOffset : Vector3.zero);
			float x, y;
			if (!layingDown) {
				x = transform.position.x + Mathf.Sin(phase) * scale.x;
				y = transform.position.y + Mathf.Cos(phase) * (invert ? -1 : 1) * scale.y;
			} else {
				y = transform.position.x + Mathf.Sin(phase) * scale.x;
				x = transform.position.y + Mathf.Cos(phase) * (invert ? -1 : 1) * scale.y;
			}
			transform.position = new Vector3(x, y, 0f);
		}
	}
}