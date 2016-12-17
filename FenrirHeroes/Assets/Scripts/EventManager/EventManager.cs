using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	public delegate void voidEvent();
	public voidEvent OnLevelWasLoaded;
	public voidEvent OnAllPlayersReady;
	public voidEvent OnAllActionsExecuted;
	public voidEvent OnAllPlayersDead;
	public voidEvent OnPlayerDisconnected;
	public voidEvent OnPlayerConnected;
}