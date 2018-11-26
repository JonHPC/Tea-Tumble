using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public IEnumerator Shake (float duration, float magnitude) //we are using a coroutine so we need an Ienumerator with two inputs
    {
        Vector3 originalPos = transform.localPosition; // creates a new vector3 and stores the original camera position

        float elapsed = 0.0f; //initialized the elapsed time variable to 0

        while (elapsed < duration) //this code will run while the elapsed time is less than the duration
        {
            float x = Random.Range(-1f, 1f) * magnitude;//picks a random float between -1 and 1 and multiplies it by ther magnitude
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z); //sets the camera to this new local position

            elapsed += Time.deltaTime; //adds to the elapsed time per frame

            yield return null;//waits until the next frame is drawn before running the while loop again
        }

        transform.localPosition = originalPos; // after the elapsed time exceed the duration, the while loop completes and runs this code to set the camera back to the original position
    }
}
