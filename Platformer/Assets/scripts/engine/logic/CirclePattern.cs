using UnityEngine;
using System.Collections;

namespace engine.logic {
	public class CirclePattern : MonoBehaviour {


		private float ticks = 1f;

		public Vector2 Max = new Vector2(1f, 1f);
		public Vector2 DivisioinFactor = new Vector2(100f, 100f);

		// Update is called once per frame
		void LateUpdate() {
			float x = Max.x * Mathf.Sin(ticks * .5f * Mathf.PI / DivisioinFactor.x);
			float y = Max.y * Mathf.Cos(ticks * .5f * Mathf.PI / DivisioinFactor.y);
			this.transform.position = new Vector3(x, y, this.transform.position.z);
			this.ticks++;
		}
	}
}
