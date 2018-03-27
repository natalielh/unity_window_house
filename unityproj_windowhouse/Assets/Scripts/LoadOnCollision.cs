using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnCollision : MonoBehaviour {

	/*
		This script loads a specified scene by its index number upon collision
		with the Player's collider
		
		TO USE: Attach this script to an gameobject with a collider attached,
		set 'Is Trigger' to TRUE
	*/

	// the index number of the scene to be loaded
	public int next_scene_index;

	void OnTriggerEnter(Collider collider)
	{
		// if the collider we are colliding with is the Player's collider
		if(collider.gameObject.tag.Equals("Player") == true){
			// load the scene specified by the inspector-settable variable
			SceneManager.LoadScene(next_scene_index);
		}
	}
}
