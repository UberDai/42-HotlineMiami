using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager	instance { get; private set; }
	public static Hero			hero { get; private set; }

	void		Awake()
	{
		if (instance == null)
			instance = this;

		if (hero == null)
			hero = GameObject.FindWithTag("Hero").GetComponent<Hero>();
	}
}
