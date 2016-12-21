using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats {

	public List<BaseStat> stats = new List<BaseStat> ();

	public Stats(){
		stats.Add (new AdjustableStat());		//health
		stats.Add (new AdjustableStat());		//mana

		stats.Add (new ModifiableStat());		//damage
		stats.Add (new ModifiableStat());		//defense
		stats.Add (new ModifiableStat());		//move
	}

}