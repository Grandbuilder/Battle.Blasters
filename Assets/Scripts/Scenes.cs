using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes : MonoBehaviour {

	
	// Update is called once per frame
	public void ChangeScene (string SceneChangeTo) {
		Application.LoadLevel (SceneChangeTo);
	}
}
