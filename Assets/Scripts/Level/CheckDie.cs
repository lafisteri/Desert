using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckDie : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.GetComponent<Character>()) {
			Data.helper.Load(gameObject.scene.name);
		}
	}
}
