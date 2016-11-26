using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	private string BuilderSceneID = "LevelBuilder";

	public void LoadBuilderScene(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (BuilderSceneID);
	}
}