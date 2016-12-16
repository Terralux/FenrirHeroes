using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSessionSelector : MonoBehaviour {

	private List<Level> levels = new List<Level>();
	public GameObject LevelButtonPrefab;

	private LevelDataManager LM;

	public void Start(){
		Toolbox.FindComponent<MenuHandler> ().BeginSession += Show;
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

	private void CreateNewLevelButton(Level l){
		GameObject tempGO = Instantiate (LevelButtonPrefab, transform) as GameObject;
		tempGO.GetComponent<LevelContainer> ().myLevel = l;
		tempGO.GetComponent<LevelContainer> ().isLevelBuilderLoader = false;
		tempGO.GetComponentInChildren<UnityEngine.UI.Text> ().text = l.name;
	}
}
