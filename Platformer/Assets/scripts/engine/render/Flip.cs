using UnityEngine;
using System.Collections;

public class Flip : MonoBehaviour {

	public void FlipTransform() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
