﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCloud2 : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        MeshRenderer mr = GetComponent<MeshRenderer>();//gets the mesh renderer component 

        Material mat = mr.material;//gets the first material in the array of materials

        Vector2 offset = mat.mainTextureOffset;//gets the vector2 element from the main texture

        //offset.y -= Time.deltaTime/30f; //moves along this axis over time

        //mat.mainTextureOffset = offset;//updates the offset by the new offset

        transform.Rotate(new Vector3(0, 0, 20) * Time.deltaTime/5); // rotates  to make it look cool


    }
}
