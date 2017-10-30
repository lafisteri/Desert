using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;

using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameHelper : MonoBehaviour {
	public List<SavebleObject> objects;
	private string pathToSave;
	private bool loadScene;
	private bool createScene;
	private string pathToSaveXML;
	XElement loadedXML = null;

	private void Awake(){
		pathToSave = Application.dataPath + "/Saves/";
		objects = new List<SavebleObject> ();
		loadScene = false;
		createScene = false;
		pathToSaveXML = pathToSave + SceneManager.GetActiveScene ().name + ".xml";
		SceneManager.sceneLoaded += SceneLoaded;
	}

	public void Save(){
		XElement root = new XElement ("root");

		XElement score = new XElement ("score", Data.character.Score);
		root.Add (score);

		foreach (SavebleObject obj in objects) {
			root.Add(obj.GetElement());
		}
		XDocument saveDoc = new XDocument (root);
		File.WriteAllText (pathToSaveXML, saveDoc.ToString ());
	}

	//delete all saves
	public void DeleteSaves() {
		System.IO.DirectoryInfo di = new DirectoryInfo(pathToSave);
		foreach (FileInfo file in di.GetFiles()){
			file.Delete ();
		}
	}

	//create new csene
	public void Create(string name) {
		/*
		Debug.Log ("dataPath " + Application.dataPath + "\n" +
		"persistentDataPath " + Application.persistentDataPath + "\n" +
		"temporaryCachePath " + Application.temporaryCachePath);
		
		dataPath /Users/Anton/Unity3d/projects/Desert/Assets
		persistentDataPath /Users/Anton/Library/Application Support/nasulichHome/Desert
		temporaryCachePath /var/folders/__/h93m1qfj47sbhvnx2bb9fsgc0000gn/T/nasulichHome/Desert
		*/
		DeleteSaves ();
		createScene = true;
		SceneManager.LoadSceneAsync (name, LoadSceneMode.Single);
	}

	//restart level
	public void ReCreate() {
		DeleteSaves ();
		createScene = true;
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	public void Load(string name) {
		if (!File.Exists (pathToSaveXML)) {
			Debug.Log ("Yob does not have save, creating new scene " + name);
			Create (name);
		} else {
			Debug.Log ("You have save, loading scene " + name);
			loadedXML = XDocument.Parse (File.ReadAllText (pathToSaveXML)).Element ("root");
			if (loadedXML == null) {
				Debug.Log ("Oops... Can't load save, will load new scene " + name);
				return;
			} else {
				loadScene = true;
				SceneManager.LoadSceneAsync (name, LoadSceneMode.Single);
			}
		}
	}

	void SceneLoaded(Scene scene, LoadSceneMode m) {
		if (loadScene) {
			loadScene = false;
			GenerateScene ();
			Debug.Log ("loadScene");
		}
		if (createScene) {
			createScene = false;
			Debug.Log ("createScene");
		}
	}

	void GenerateScene() {
		Debug.Log ("GenerateScene");
		foreach (SavebleObject item in FindObjectsOfType(typeof(SavebleObject)) as SavebleObject[]) {
			item.DestroySelf ();
		}
		foreach (XElement instance in loadedXML.Elements("instance")) {
			Vector3 position = Vector3.zero;
			position.x = float.Parse (instance.Attribute ("x").Value);
			position.y = float.Parse (instance.Attribute ("y").Value);
			position.z = float.Parse (instance.Attribute ("z").Value);
			Instantiate (Resources.Load<GameObject> (instance.Value), position, Quaternion.identity);
		}
	}
}
