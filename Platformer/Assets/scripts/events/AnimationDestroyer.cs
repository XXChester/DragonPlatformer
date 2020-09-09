using UnityEngine;
using System.Collections;

public class AnimationDestroyer : MonoBehaviour {
	public new GameObject gameObject;
	public void Destory() {
		Destroy(gameObject.gameObject);
	}
}
