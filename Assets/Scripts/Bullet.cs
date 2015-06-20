using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public Vector2	direction;
	public float	speed;

	void	FixedUpdate()
	{
		Move();
	}

	void	Move()
	{
		transform.Translate(direction * speed / (1 / Time.deltaTime));
	}
}
