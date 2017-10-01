using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour 
{

    //this script is used on the start menu and game over menu. Using buttons and build settings you can seemlessly transition from scene to scene with this type of code.

    //loads the scene containing the gameplay when the applied button is clicked on. This is done by using the SceneManager class (13-16).
    public void OnNewGame_Clicked() //ButtonNG
    {
		SceneManager.LoadScene("MiniGame"); 
	}

	//loads the start menu scene when you press the main menu button in the Win scene. This is also done by using the SceneManager class (19-22).
	public void OnMenu_Clicked()    //ButtonMenu
    {
		SceneManager.LoadScene ("StartMenu");
	}

	//Exits the game by using the Application class (26-28).
	public void OnExit_Clicked() //ButtonExit
    {
		Application.Quit ();
	}
}