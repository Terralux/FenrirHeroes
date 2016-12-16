using UnityEngine;
using System.Collections;

public class SceneMaster : MonoBehaviour {

	private string BuilderSceneID = "LevelBuilder";
	private string SessionSceneID = "GameSession";

	public void LoadBuilderScene(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (BuilderSceneID);
	}

	public void LoadSessionScene(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (SessionSceneID);
	}
}