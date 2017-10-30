using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
	public static Character character;
	public static GameHelper helper;

	void Awake(){
		character = FindObjectOfType <Character> ();
		helper = FindObjectOfType <GameHelper> ();
	}

	void Update(){
		if (!character)
			character = FindObjectOfType <Character> ();

		if (!helper)
			helper = FindObjectOfType <GameHelper> ();
	}
}
