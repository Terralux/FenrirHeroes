using UnityEngine;
using System.Collections;

public class LevelContainer : MonoBehaviour {

	[HideInInspector]
	public Level myLevel;

	public void OnSelected(){
		transform.SetParent (null);
		DontDestroyOnLoad (gameObject);
		Toolbox.FindComponent<SceneManager> ().LoadBuilderScene ();
	}

	void OnLevelWasLoaded(){
		Toolbox.FindComponent<BuilderManager> ().SetLevel (myLevel);
	}
}
