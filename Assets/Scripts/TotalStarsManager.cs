using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalStarsManager : MonoBehaviour {

    public TextMeshProUGUI totalStarsText;//reference sthe total stars text
    public TextMeshProUGUI noAdsText;//reference sthe no ads text

    public GameObject sparkle0;
    public GameObject sparkle1;
    public GameObject sparkle2;
    public GameObject sparkle3;
    public GameObject sparkle4;

    private float totalStars;//will store the total stars ever collected

	// Use this for initialization
	void Start () {
        totalStars = PlayerPrefs.GetFloat("totalStars", 0f); //gets the totalStars player pref and stores it in this variable, default value is 0

        totalStarsText.text ="TOTAL STARS: " + totalStars.ToString();//sets the totalStars TMP to the current total stars 
    }
	
    void Update()
    {
        if(totalStars > 49999)
        {
            noAdsText.gameObject.SetActive(true);//if the total stars is over the specificed amount, this text shows
        }

        if(totalStars > 0)
        {
            sparkle0.gameObject.SetActive(true);//sets this sparkle active if player has more than one star
        }

        if (totalStars > 9999)
        {
            sparkle1.gameObject.SetActive(true);//sets this sparkle active if player has more than these stars
        }

        if (totalStars > 19999)
        {
            sparkle2.gameObject.SetActive(true);//sets this sparkle active if player has more than these stars
        }

        if (totalStars > 29999)
        {
            sparkle3.gameObject.SetActive(true);//sets this sparkle active if player has more than these stars
        }

        if (totalStars > 39999)
        {
            sparkle4.gameObject.SetActive(true);//sets this sparkle active if player has more than these stars
        }
    }
	
}
