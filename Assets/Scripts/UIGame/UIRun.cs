using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRun : MonoBehaviour {
	bool entered;
	public bool Enter {
		get { 
			return entered; 
		}
		set {
			entered = value;
		}
	}

	float direction;
	public float Direct {
		get { 
			return direction; 
		}
		set {
			direction = value;
		}
	}

	void Update () {
		if (!entered)
			return;
		else {
			Data.character.Run (direction);
		}
	}
}
