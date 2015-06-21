using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
	public void OnPlayClick()
	{
		print("play");
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnQuitClick()
	{
		print("quit");
		Application.Quit();
	}

	public void OnResumeClick()
	{
		print("resume");
		GameManager.Resume();
	}
}
