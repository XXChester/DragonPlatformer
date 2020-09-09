using System.Reflection;
using UnityEngine;

public static class LoadingUtils {

	public static T LoadAndValidate<T>(GameObject source, bool enabledOnly = true, bool inChildren=false) where T : Object {
		T result = null;
		if (!inChildren) {
			result = source.GetComponent<T>();
		} else {
			result = source.GetComponentInChildren<T>();
		}

		Validate<T>(result);
		return result;
	}

	public static void Validate<T>(T result, bool enabledOnly = true) where T : Object {
		string type = typeof(T).GetType().Name;
		string error = null;
		if (result == null) {
			error = "Failed to locate a " + type;
		} else {
			bool isEnabled = true;
			PropertyInfo enabled = result.GetType().GetProperty("enabled");
			if (enabled != null) {
				isEnabled = (bool)enabled.GetValue(result, null);
			}

			if (enabledOnly && !isEnabled) {
				error ="Failed to load an active " + type;
			}
		}
		if (error != null) {
			Debug.LogError(error);
			throw new System.ArgumentNullException(error + System.Environment.StackTrace);
		}
	}
}

