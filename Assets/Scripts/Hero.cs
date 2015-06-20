using UnityEngine;
using System.Collections;

public class Hero : Human
{
	void	Update()
	{
		base.Update();
		HandleInputs();
	}

	void	HandleInputs()
	{
		moveDirection = Vector2.zero;

		if (Input.GetButton("Right"))
			moveDirection.x += 1f;
		if (Input.GetButton("Left"))
			moveDirection.x -= 1f;

		if (Input.GetButton("Up"))
			moveDirection.y += 1f;
		if (Input.GetButton("Down"))
			moveDirection.y -= 1f;

		moveDirection = Vector3.Normalize(moveDirection);
	}
}
