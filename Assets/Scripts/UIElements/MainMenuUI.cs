using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

	private void Start ()
	{
		transform.Find ("PlayBtn").GetComponent<Button> ().onClick.AddListener (() => {
			Debug.Log ("Start Play");
			GameSceneManager.LoadScene (GameSceneManager.Scene.GameScene);
		});

		transform.Find ("QuitBtn").GetComponent<Button> ().onClick.AddListener (() => {
			Debug.Log ("Quit Game");
			Application.Quit ();
		});
	}
}
