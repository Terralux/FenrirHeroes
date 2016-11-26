using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour {

	private List<Level> levels = new List<Level>();
	public GameObject LevelButtonPrefab;

	private LevelDataManager LM;

	public void Start(){
		Toolbox.FindComponent<MenuHandler> ().ShowLevelSelector += Show;
		Hide ();
	}

	public void Show(){
		LM = Toolbox.FindComponent<LevelDataManager> ();
		levels = LM.LoadLevels ();
		foreach (Level l in levels) {
			CreateNewLevelButton (l);
		}
	}

	public void Hide(){
		gameObject.SetActive (false);
	}

	public void AddNewLevel(){
		levels.Add (new Level ());
		CreateNewLevelButton (levels[levels.Count - 1]);
		LM.Save (levels [levels.Count - 1]);
	}

	private void CreateNewLevelButton(Level l){
		GameObject tempGO = Instantiate (LevelButtonPrefab, transform) as GameObject;
		tempGO.GetComponent<LevelContainer> ().myLevel = l;
		tempGO.GetComponentInChildren<UnityEngine.UI.Text> ().text = l.name;
	}
}
