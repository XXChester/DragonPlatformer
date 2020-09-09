using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSortingLayer : MonoBehaviour {

	public GameObject[] objectsToSort;
	public string sortingLayer;
	public int sortingLayerOrder;

	void Start() {
		int sortLayer = SortingLayer.NameToID(sortingLayer);
		foreach (var temp in objectsToSort) {
			Renderer renderer = temp.gameObject.GetComponent<Renderer>();
			if (renderer != null) {
				renderer.sortingOrder = sortingLayerOrder;
				renderer.sortingLayerID = sortLayer;
			}
		}
	}
}
