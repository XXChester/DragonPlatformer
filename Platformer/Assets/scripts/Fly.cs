using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

	Animator animator;
	Rigidbody2D rigidBody;
	public GameController gameController;

	public const float MAX_SPEED = 5f;

	// Use this for initialization
	void Start() {
		animator = LoadingUtils.LoadAndValidate<Animator>(gameObject); 
		rigidBody = LoadingUtils.LoadAndValidate<Rigidbody2D>(gameObject);
		LoadingUtils.Validate<GameController>(gameController);
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (gameController.IsPlaying) {
			float speed = Input.GetAxis("Vertical");
			animator.SetFloat(AnimationValueManager.ANIMATOR_SPEED_Y, Mathf.Abs(speed));
			//bool grounded = rigidBody.velocity.y < 0f;
			//animator.SetBool("grounded", grounded);
			float velocity = rigidBody.velocity.y;
			if (speed < 0) {
				velocity += (speed * (MAX_SPEED / 8f));
			} else if (speed > 0) {
				velocity = speed * MAX_SPEED;
			}
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, velocity);
		}
	}
}
