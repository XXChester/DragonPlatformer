using UnityEngine;
using System.Collections.Generic;

public class RangeComparer : Comparer<GameObject> {
	//singleton
	public static RangeComparer instance = new RangeComparer();

	public override int Compare(GameObject x1, GameObject x2) {
		int result = 0;
		if (x1 != x2) {
			result = x1.transform.position.x.CompareTo(x2.transform.position.x);
		}
		return result;
	}
}