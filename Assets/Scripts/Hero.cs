using UnityEngine;
using System.Collections;

public class Hero : Human
{
	// public delegate			void ShotEvent();
	// public event ShotEvent	OnFire;

	protected override void	HandleInputs()
	{
		base.HandleInputs();

		movingDirection = Vector2.zero;

		movingDirection.x = Input.GetAxis("Horizontal");
		movingDirection.y = Input.GetAxis("Vertical");
		movingDirection.Normalize();

		SetAimingTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		if (Input.GetButton("Fire1"))
		{
			// OnFire();
			Fire();
		}

		if (Input.GetButtonDown("Fire2"))
		{
			if (weapon != null)
				DropWeapon();
			else
				TryToPickWeapon();
		}
	}

	protected override void	Die(Bullet bullet)
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}