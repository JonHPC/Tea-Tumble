using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCrystalTitleMover : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); // rotates the collectible to make it look cool
    }
}
