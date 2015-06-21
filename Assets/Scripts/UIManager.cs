using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
	Text	ammoValue;

	void Start()
	{
		ammoValue = GetComponentsInChildren<Text>()[1];
	}

	void Update()
	{
		if (GameManager.hero.weapon == null)
			ammoValue.text = "None";
		else if (GameManager.hero.weapon.ammo == -1)
			ammoValue.text = "Too Much";
		else
			ammoValue.text = GameManager.hero.weapon.ammo.ToString();
	}
}
