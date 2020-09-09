using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D otherObject) {
		if (otherObject.gameObject.layer.Equals(LayerMask.NameToLayer("Projectiles"))) {
			//Debug.Log("Triggered\t" + gameObject.name + "\t" + otherObject.name);
			//Destroy(otherObject.gameObject);
			Projectile projectile = otherObject.gameObject.GetComponent<Projectile>();
			Health health = gameObject.transform.parent.GetComponentInChildren<Health>();
			if (health != null) {
				health.TakeDamage(projectile);
			}
		}
	}
}
