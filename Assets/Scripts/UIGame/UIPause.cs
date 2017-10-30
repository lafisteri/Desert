using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : MonoBehaviour {
	
	public void Pause(bool b){
		if (b)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
}
