using UnityEngine;
using System.Collections.Generic;

public class WaypointPathing : MonoBehaviour {
	private class Target {
		public Vector2 Position { get; set; }
		public int NodeIndex { get; set; }
	}

	private Rigidbody2D rigidBody;
	private Flip flip;
	private Animator animator;

	private Target target;
	private float speed;
	private float min;
	private float max;
	private bool waiting;
	private float waitTime;

	internal List<GameObject> Nodes { get; set; }

	public const float MAX_SPEED = .05f;
	public const float WAIT_FOR = 2f;

	void Start () {
		rigidBody = LoadingUtils.LoadAndValidate<Rigidbody2D>(gameObject);
		flip = LoadingUtils.LoadAndValidate<Flip>(gameObject);
		animator = LoadingUtils.LoadAndValidate<Animator>(gameObject);
		Nodes.Sort(RangeComparer.instance);
		ChangeTarget();
	}

	private bool determineDirectionChange(float? previousTarget, Target target) {
		float previousDirection = -1f;
		float newDirection = 0f;
		if (previousTarget != null) {
			previousDirection = transform.position.x.CompareTo(this.target.Position.x);
			newDirection = transform.position.x.CompareTo(target.Position.x);
		} else {
			// we may have spawned on the right side of a node, we need to update our direction
			previousDirection = -1f;
			newDirection = transform.position.x.CompareTo(target.Position.x);
		}
		return newDirection != previousDirection;
	}

	private void determinePositionalDirections(Target target) {
		// where are we, in relation to our target
		if (transform.position.x > target.Position.x) {
			this.speed = -MAX_SPEED;
			this.min = target.Position.x;
			this.max = transform.position.x;
		} else if (transform.position.x < target.Position.x) {
			this.speed = MAX_SPEED;
			this.min = transform.position.x;
			this.max = target.Position.x;
		}
	}

	private void ChangeTarget() {
		int index = 0;
		if (this.target == null) {
			index = Random.Range(0, Nodes.Count - 1);
		} else {
			int temp = this.target.NodeIndex + 1;
			// are we at the end of the list
			if (temp == Nodes.Count) {
				Nodes.Reverse();
				temp = 0;
			}
			index = temp;
		}

		float? previousTarget = null;
		if (this.target != null) {
			previousTarget = this.target.Position.x;
		}
		GameObject node = Nodes[index];
		Target target = new Target() {
			NodeIndex = index,
			Position = node.transform.position
		};

		determinePositionalDirections(target);
		if (determineDirectionChange(previousTarget, target)) {
			flip.FlipTransform();
		}

		this.target = target;
		waitTime = 0f;
		waiting = false;
	}

	void FixedUpdate() {
		if (waiting) {
			waitTime += Time.fixedDeltaTime;
		}

		int x1 = (int)(target.Position.x * 10);
		int x2 = (int)(transform.position.x * 10);
		if (x1 != x2) {
			float x = Mathf.Clamp(rigidBody.position.x + speed, min, max);
			rigidBody.MovePosition(new Vector2(x, rigidBody.position.y));
			animator.SetFloat(AnimationValueManager.ANIMATOR_SPEED_X, Mathf.Abs(x));
		} else {
			// have we paused for a few seconds? If so, change our target
			if (waiting && waitTime > WAIT_FOR) {
				ChangeTarget();
			} else {
				waiting = true;
			}
		}
	}
}
