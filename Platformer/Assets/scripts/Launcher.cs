using UnityEngine;
using System.Collections.Generic;

public class Launcher : MonoBehaviour {

	private PlayerState playerState;

	public Component attackSource;
	public Rigidbody2D projectilePrefab;
	public GameController gameController;

	void Start() {
		playerState = LoadingUtils.LoadAndValidate<PlayerState>(gameObject);
		LoadingUtils.Validate<Component>(attackSource);
		LoadingUtils.Validate<Rigidbody2D>(projectilePrefab);
		LoadingUtils.Validate<GameController>(gameController);
	}

	// Update is called once per frame
	void Update() {
		if (gameController.IsPlaying) {
			float input = Input.GetAxis("Fire1");
			if (InputUtils.isFire1Down()) {
				Launch();
				if (!playerState.Attacking) {
					playerState.Attacking = true;
				}
			} else {
				playerState.Attacking = false;
			}
		}
	}

	private void Launch() {
		Rigidbody2D clone = (Rigidbody2D)Instantiate(this.projectilePrefab, attackSource.transform.position, attackSource.transform.rotation);
		float x = -1, y = 0;
		if (playerState.Flying && playerState.Attacking) {
			x = -.5f;
			y = -.5f;
		}
		if (playerState.FacingRight) {
			x = -x;
		}

		float damage = 1f;
		bool criticalStrike = Random.Range(0, 25) == 15;
		if (criticalStrike) {
			damage *= 2;
		}
		Projectile projectile = clone.GetComponent<Projectile>();
		projectile.direction = new Vector2(x, y);
		projectile.CriticalStrike = criticalStrike;
		projectile.Damage = damage;
	}
}
