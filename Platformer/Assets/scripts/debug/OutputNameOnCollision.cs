using UnityEngine;
using System.Collections;

public class OutputNameOnCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D otherObject) {
		Debug.Log("me: " + gameObject.name + "\tCollidedWith: " + otherObject.gameObject.name);
	}
}