using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass {

	private static Camera m_Camera;

	public static Vector3 GetMouseWorldPosition ()
	{
		if (m_Camera == null) {
			m_Camera = Camera.main;
		}
		//Gets the mouse world position from the camera screen point
		Vector3 mouseWorldPosition = m_Camera.ScreenToWorldPoint (Input.mousePosition);
		//Reset Y axis to 0 for mouse position
		mouseWorldPosition.z = 0f;
		return mouseWorldPosition;
	}

	public static Vector3 GetRandomDir ()
	{
		return new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0f);
	}

	public static float GetAngleFromVector (Vector3 vector)
	{
		float radians = Mathf.Atan2 (vector.y, vector.x);
		float degrees = radians * Mathf.Rad2Deg;
		return degrees;
	}
}
