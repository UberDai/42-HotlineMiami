using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public Sprite	attachedSprite;
	public uint		ammo;

	void	Update()
	{
		HandleInputs();
	}

	void	HandleInputs()
	{
		if (Input.GetButton("Fire1"))
			Fire();
	}

	void	Fire()
	{

	}
}
