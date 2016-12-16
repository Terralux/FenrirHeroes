using UnityEngine;
using System.Collections;

public class LevelContainer : MonoBehaviour {

	[HideInInspector]
	public Level myLevel;
	[HideInInspector]
	public bool isLevelBuilderLoader = true;

	public void OnSelected(){
		transform.SetParent (null);
		DontDestroyOnLoad (gameObject);
		if (isLevelBuilderLoader) {
			Toolbox.FindComponent<SceneManager> ().LoadBuilderScene ();
		} else {
			Toolbox.FindComponent<SceneManager> ().LoadSessionScene ();
		}
	}

	void OnLevelWasLoaded(){
		if (isLevelBuilderLoader) {
			Toolbox.FindComponent<BuilderManager> ().SetLevel (myLevel);
		} else {
			Toolbox.FindComponent<LevelLoader> ().SetLevel (myLevel);
		}
	}
}
