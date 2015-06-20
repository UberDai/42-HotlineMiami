using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
	public int		maxLife;
	public float	speed;
	[HideInInspector]
	public Vector2	moveDirection;
	[HideInInspector]
	public Vector2	aimDirection;
	public float	rotationSpeed;

	public void	Update()
	{
		Move();
		Rotate();
	}

	public void	Move()
	{
		transform.Translate(moveDirection * speed / (1 / Time.deltaTime));
	}

	void		Rotate()
	{
		float		angle;
		Vector2		mousePosition;
		Quaternion	newRotation;

		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		newRotation = Quaternion.LookRotation(transform.position - (Vector3) mousePosition, Vector3.forward);
	    newRotation.x = 0;
	    newRotation.y = 0;

	    if (rotationSpeed > 0)
		    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
	    else
		    transform.rotation = newRotation;
	}
}
