using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	[HideInInspector]
	public Vector2	direction;
	[HideInInspector]
	public float	speed;

	void	FixedUpdate()
	{
		Move();
	}

	void	Move()
	{
		transform.Translate(direction * speed * Time.deltaTime);
	}

	void	OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject != GameManager.hero.gameObject)
			Destroy(gameObject);
	}
}
