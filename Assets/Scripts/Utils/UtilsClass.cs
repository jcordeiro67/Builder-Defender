using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass {

	private static Camera m_Camera;

	/// <summary>
	/// Gets the mouse position on screen in world space
	/// </summary>
	/// <returns>Vector3: mouseWorldPosition</returns>
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

	/// <summary>
	/// Gets a Random Direction
	/// </summary>
	/// <returns>Vector3: Randomizes the X and Y axis from -1 to 1 </returns>
	public static Vector3 GetRandomDir ()
	{
		return new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), 0f);
	}

	/// <summary>
	/// Gets the Angle from a given Vector
	/// </summary>
	/// <param name="vector"> Vector3</param>
	/// <returns>float: degrees</returns>
	public static float GetAngleFromVector (Vector3 vector)
	{
		float radians = Mathf.Atan2 (vector.y, vector.x);
		float degrees = radians * Mathf.Rad2Deg;
		return degrees;
	}
}
