using UnityEngine;
using System.Collections;

public class Hero : Human
{
	protected override void	HandleInputs()
	{
		base.HandleInputs();

		movingDirection = Vector2.zero;

		movingDirection.x = Input.GetAxis("Horizontal");
		movingDirection.y = Input.GetAxis("Vertical");
		movingDirection.Normalize();

		SetAimingTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		if (Input.GetButton("Fire1"))
			Fire();

		if (Input.GetButtonDown("Fire2"))
		{
			if (_weapon != null)
				DropWeapon();
			else
				TryToPickWeapon();
		}
	}
}
