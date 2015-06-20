using UnityEngine;
using System.Collections;

public class Hero : Human
{
	void	FixedUpdate()
	{
		base.FixedUpdate();
		HandleInputs();
	}

	void	HandleInputs()
	{
		moveDirection = Vector2.zero;

		moveDirection.x = Input.GetAxis("Horizontal");
		moveDirection.y = Input.GetAxis("Vertical");
		moveDirection = Vector3.Normalize(moveDirection);

		aimDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
