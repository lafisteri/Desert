using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILivesBar : MonoBehaviour {
	Transform[] hearts;

	void Awake () {
		hearts = new Transform[5];
		for (int i = 0; i < hearts.Length; i++) {
			hearts [i] = transform.GetChild (i);//get hearts from LivesBar
		}
	}

	public void Refresh () {
		for (int i = 0; i < hearts.Length; i++) {
			if (i < Data.character.Lives)
				hearts [i].gameObject.SetActive (true);
			else
				hearts [i].gameObject.SetActive (false);
		}
	}
}
