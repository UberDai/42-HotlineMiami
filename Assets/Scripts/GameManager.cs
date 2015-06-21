using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static Hero			hero { get; private set; }
	public static bool			paused;
	public static GameObject	pauseMenu;

	void		Awake()
	{
		if (hero == null)
			hero = GameObject.FindWithTag("Hero").GetComponent<Hero>();

		if (pauseMenu == null)
			pauseMenu = GameObject.FindWithTag("PauseMenu");

		Resume();
	}

	void		Update()
	{
		if (Input.GetButtonDown("Cancel"))
			TogglePause();
	}

	void		TogglePause()
	{
		if (paused)
			Resume();
		else
			Pause();
	}

	public static void	Pause()
	{
		pauseMenu.SetActive(true);
		paused = true;
		Time.timeScale = 0;
	}

	public static void	Resume()
	{
		pauseMenu.SetActive(false);
		paused = false;
		Time.timeScale = 1f;
	}
}
