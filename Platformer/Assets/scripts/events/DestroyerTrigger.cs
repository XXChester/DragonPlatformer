using UnityEngine;
using System.Collections;

public class DestroyerTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D otherObject) {
		//Debug.Log("me: " + gameObject.name + "\tother: " + otherObject.gameObject.name);
		Destroy(otherObject.gameObject);
	}
}
