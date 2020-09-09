using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleText : MonoBehaviour {


	private TextMesh text;
	private TextMesh regularText;
	private TextMesh criticaltext;
	private float scroll;
	private float duration;
	private float alpha = 1f;

	void Update() {
		if (alpha > 0) {
			if (this.text != null) {
				float x = text.transform.position.x;
				float y = text.transform.position.y + ( scroll * Time.deltaTime);
				text.transform.position = new Vector2(x, y);
				alpha -= Time.deltaTime / duration;
				Color color = text.color;
				color.a = alpha;
				text.color = color;
			}
		} else {
			Destroy(gameObject);
		}
	}

	public void Init(string text, bool critical = false) {
		TextMesh[] texts = transform.gameObject.GetComponentsInChildren<TextMesh>();

		this.scroll = Random.Range(1.5f, 2.2f);
		this.duration = 2.5f;
		this.alpha = 1;
		this.regularText = texts[0];
		this.criticaltext = texts[1];
		this.text = this.regularText;
		if (critical) {
			this.text = this.criticaltext;
			this.scroll = 3f;
		}
		this.text.text = text;
	}
}
