using UnityEngine;
using System.Collections;

public class Hero : Human
{
	protected override void	HandleInputs()
	{
		base.HandleInputs();

		moveDirection = Vector2.zero;

		moveDirection.x = Input.GetAxis("Horizontal");
		moveDirection.y = Input.GetAxis("Vertical");
		moveDirection = Vector3.Normalize(moveDirection);

		aimDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetButtonDown("Fire1"))
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
