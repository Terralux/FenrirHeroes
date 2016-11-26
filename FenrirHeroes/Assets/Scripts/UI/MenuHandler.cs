using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

	public delegate void StartSession ();
	public StartSession BeginSession;

	public delegate void LevelSelector();
	public LevelSelector ShowLevelSelector;

	// Use this for initialization
	void Awake () {
		Toolbox.RegisterComponent<MenuHandler> (this);
	}

	public void OpenNetworkHostingMenu(){
		//This is used to setup a networking session
		BeginSession();
	}

	public void OpenLevelSelector(){
		//This opens up the level selector
		ShowLevelSelector();
	}

	public void OpenShop(){
		//This will open our shop menu
	}

	public void OpenSettings(){
		//This is used to change settings
	}
}
