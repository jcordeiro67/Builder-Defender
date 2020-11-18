using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager {

	public enum Scene {
		MainMenuScene,
		GameScene,
	}

	public static void LoadScene (Scene scene)
	{
		SceneManager.LoadScene (scene.ToString ());
	}
}
