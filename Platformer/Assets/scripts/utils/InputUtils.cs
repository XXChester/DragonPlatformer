using UnityEngine;
using System.Collections.Generic;

public class InputUtils {

	public static bool isFire1Down() {
		bool down = false;
		float input = Input.GetAxis("Fire1");
		if (input != 0) {
			down = true;
		}
		return down;
	}
}
