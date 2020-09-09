using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {


	Animator animator;
	Rigidbody2D rigidBody;
	PlayerState playerState;
	Flip flip;

	public GameController gameController;

	public const float MAX_SPEED = 10f;

	private void Flip() {
		playerState.FacingRight = !playerState.FacingRight;
		flip.FlipTransform();

	}

	// Use this for initialization
	void Start() {
		animator = LoadingUtils.LoadAndValidate<Animator>(gameObject);
		rigidBody = LoadingUtils.LoadAndValidate<Rigidbody2D>(gameObject);
		playerState = LoadingUtils.LoadAndValidate<PlayerState>(gameObject);
		flip = LoadingUtils.LoadAndValidate<Flip>(gameObject);
		LoadingUtils.Validate<GameController>(gameController);
	}

	// Physics update
	void FixedUpdate() {
		if (gameController.IsPlaying) {
			float speed = Input.GetAxis("Horizontal");
			animator.SetFloat(AnimationValueManager.ANIMATOR_SPEED_X, Mathf.Abs(speed));

			rigidBody.velocity = new Vector2(speed * MAX_SPEED, rigidBody.velocity.y);

			if (speed > 0 && !playerState.FacingRight) {
				Flip();
			} else if (speed < 0 && playerState.FacingRight) {
				Flip();
			}
		}
	}
}
