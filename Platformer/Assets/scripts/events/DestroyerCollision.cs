using UnityEngine;
using System.Collections;

public class DestroyerCollision : MonoBehaviour {

	void OnCollisionEnter2D( Collision2D otherObject) {
		if (!otherObject.gameObject.name.Equals(gameObject.name)) {
			if (LayerMask.NameToLayer("Collisions").Equals(otherObject.gameObject.layer)) {
				Destroy(gameObject);
			}
		}
	}
}
