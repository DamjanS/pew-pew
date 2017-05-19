using UnityEngine;
using System.Collections;

public class StartFromFirstLevel : MonoBehaviour {

	// respond on collisions
	void OnCollisionEnter(Collision newCollision)
	{
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			// call the Start from First level function in the game manager
			GameManager.gm.StartGameFromFirstLevel();
		}
	}
}
