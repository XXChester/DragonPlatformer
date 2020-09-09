using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	private Rigidbody2D rigidBody;

	public float speed;
	public Vector2 direction;

	public float Damage { get; set; }
	public bool CriticalStrike { get; set; }

	private void SetVelocity() {
		Vector2 force = speed * direction;
		rigidBody.velocity = force;
	}
	// Use this for initialization
	void Start() {
		rigidBody = LoadingUtils.LoadAndValidate<Rigidbody2D>(gameObject);
		SetVelocity();
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Mob"), gameObject.layer);
	}
}
