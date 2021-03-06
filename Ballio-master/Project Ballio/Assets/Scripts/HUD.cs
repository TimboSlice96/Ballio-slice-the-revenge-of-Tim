using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    //these are in reference to the text fields on the canvas that will show the strings.
    public Text TxtRed;
	public Text TxtYellow;
	public Text TxtGreen;
	public Text TimeShow;

	//public bool TimeFreeze = false;

	void Start ()
	{
		PlayerManager.Get ().stats.TimeFreeze = false; //making sure the bool is always set to false when starting
		PlayerManager.Get ().stats.Timer = 0;
	}

	void Update ()
    {

		//this shows the code used to show the timer, it gets the current time and counts upwards.
		if (!PlayerManager.Get().stats.TimeFreeze) 
		{
			PlayerManager.Get().stats.Timer += Time.deltaTime; //time will increase in value by 1 every second.
			TimeShow.text = "Time: " + PlayerManager.Get().stats.Timer;

		}
			
		//Red text updates to show amount of red pickups collected
		TxtRed.text = "Red: " + PlayerManager.Get().stats.Red.ToString();

		//Yellow text updates to show amount of yellow pickups collected
		TxtYellow.text = "Yellow: " + PlayerManager.Get().stats.Yellow.ToString();

		//Green text updates to show amount of green pickups collected
		TxtGreen.text = "Green: " + PlayerManager.Get().stats.Green.ToString();
    }
}
