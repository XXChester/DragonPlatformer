using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {
	private float health;
	private GameObject damageObject;
	private BattleText battleTextPrefab;

	public int maxHealth;
	public Score Score { get; set; }


	void Start() {
		health = maxHealth;
		damageObject = transform.GetChild(0).gameObject;
		LoadingUtils.Validate<GameObject>(damageObject);
		this.battleTextPrefab = LoadingUtils.LoadAndValidate<BattleText>(transform.parent.gameObject, inChildren: true);
	}

	void LateUpdate() {
		if (health <= 0 && Score != null) {
			Score.score++;
			Destroy(transform.parent.gameObject);
		}
	}

	public void TakeDamage(Projectile projectile) {
		health -= projectile.Damage;
		float percentage = (health / maxHealth);
		float x = 1 - percentage;
		Vector2 scale = damageObject.transform.localScale;
		damageObject.transform.localScale = new Vector2(x, scale.y);
		float textX = projectile.transform.position.x + Random.Range(-1f, 1f);
		float textY = projectile.transform.position.y + .5f;
		BattleText battleText = (BattleText)Instantiate(this.battleTextPrefab, new Vector2(textX, textY), Quaternion.identity);
		battleText.Init(projectile.Damage.ToString(), projectile.CriticalStrike);
		//Debug.Log("%: " + percentage);
	}
}
