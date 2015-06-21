using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
	void HidePauseMenu()
	{
		transform.parent.gameObject.SetActive(false);
	}

	public void OnPlayClick()
	{
		Application.LoadLevel("Level01");
	}

	public void OnQuitClick()
	{
		Application.Quit();
	}

	public void OnResumeClick()
	{
		gameObject.SetActive(false);
	}
}
