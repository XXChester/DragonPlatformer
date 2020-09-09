using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {

	private bool facingRight;
	private bool grounded;
	private bool rotated;
	private bool attacking;
	private Animator animator;

	public Transform groundCheck;
	public Transform chinCheck;
	public LayerMask whatIsGround;

	private const float GROUND_RADIUS = .2f;
	private const float ROTATE_BY = 25f;

	public bool Flying { get { return !grounded; } }
	public bool Grounded {
		get { return grounded; }
		set {
			this.grounded = value;
			animator.SetBool("grounded", value);
		}
	}
	public bool Attacking {
		get { return this.attacking; }
		set { this.attacking = value;
			animator.SetBool("attacking", value);
		}
	}
	public bool FacingRight {
		get { return this.facingRight; }
		set {
			transform.rotation = Quaternion.identity;
			this.facingRight = value;
		}
	}


	// Use this for initialization
	void Start() {
		animator = LoadingUtils.LoadAndValidate<Animator>(gameObject);
		LoadingUtils.Validate<Transform>(groundCheck);
		LoadingUtils.Validate<Transform>(chinCheck);
	}

	void Update() {
		this.Grounded = Physics2D.OverlapCircle(groundCheck.position, GROUND_RADIUS, whatIsGround);
		this.rotated = transform.rotation.z != 0f;
		//Debug.Log("rotated(value): " + rotated + "(" + transform.rotation.z + ")" + "\tgrounded: " + grounded + "\tflying: " + Flying + "\tattacking: " + Attacking);
		if (!rotated && Flying && Attacking) {
			float z = ROTATE_BY;
			if (facingRight) {
				z = -z;
			}
			transform.Rotate(new Vector3(0f, 0f, z));
		} else if (rotated && !Attacking) {
			transform.rotation = Quaternion.identity;
		} else {
			// if we are angled while falling, our chin can get stuck
			if (this.grounded || Physics2D.OverlapCircle(chinCheck.position, GROUND_RADIUS, whatIsGround)) {
				transform.rotation = Quaternion.identity;
			}
		}
			//Debug.Log(transform.rotation);
	}

	// Update is called once per frame
	void FixedUpdate() {
	}
}
