using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotCrystalTitleMover : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime); // rotates the collectible to make it look cool
    }
}
