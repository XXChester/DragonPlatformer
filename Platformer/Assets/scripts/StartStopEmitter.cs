using UnityEngine;
using System.Collections;

public class StartStopEmitter : MonoBehaviour {
	private ParticleSystem emitter;

	public float timeDelayBetweenEmissions;
	
	void Start () {
		emitter = LoadingUtils.LoadAndValidate<ParticleSystem>(gameObject);
		StartCoroutine(StartProcess());
	}

	IEnumerator StartProcess() {
		bool emitting = true;
		while (true) {
			emitter.enableEmission = emitting;
			emitting = !emitting;
			Debug.Log("next emitting: " + emitting + "\tenableEmission: " + emitter.enableEmission);
			/*if (emitter.isPlaying) {
				emitter.Stop(false);
			} else {
				emitter.Play();
			}
			Debug.Log(emitter.isPlaying);*/
			yield return new WaitForSeconds(timeDelayBetweenEmissions);
		}
	}
}
