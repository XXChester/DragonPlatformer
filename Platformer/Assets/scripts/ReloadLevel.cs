using UnityEngine;
using System.Collections;

public class ReloadLevel : MonoBehaviour {

	public void RestartLevel() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
