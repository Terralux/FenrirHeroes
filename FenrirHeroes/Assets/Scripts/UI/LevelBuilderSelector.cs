using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelBuilderSelector : MonoBehaviour {

	private List<Level> levels = new List<Level>();
	public GameObject LevelButtonPrefab;

	private LevelDataManager LM;

	public InputField nameField;

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
		if (nameField == null) {
			return;
		}

		if (nameField.text != "") {
			foreach (Level l in levels) {
				if (l.name == nameField.text) {
					return;
				}
			}

			levels.Add (new Level (nameField.text));
			CreateNewLevelButton (levels [levels.Count - 1]);
			LM.Save (levels [levels.Count - 1]);
			nameField.transform.parent.gameObject.SetActive (false);
		}
	}

	private void CreateNewLevelButton(Level l){
		GameObject tempGO = Instantiate (LevelButtonPrefab, transform) as GameObject;
		tempGO.GetComponent<LevelContainer> ().myLevel = l;
		tempGO.GetComponent<LevelContainer> ().isLevelBuilderLoader = true;
		tempGO.GetComponentInChildren<UnityEngine.UI.Text> ().text = l.name;
	}
}
