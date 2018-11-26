using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour {


    public TextMeshPro levelText;
    public TextMeshPro scoreText;
    public TextMeshPro upgradeText;

    // Use this for initialization
    void Start () 

    {
        levelText = GetComponent<TextMeshPro>();
        scoreText = GetComponent<TextMeshPro>();
        upgradeText = GetComponent<TextMeshPro>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        levelText.text = "1";
        scoreText.text = "0";
        upgradeText.text = "None";
	}
}
