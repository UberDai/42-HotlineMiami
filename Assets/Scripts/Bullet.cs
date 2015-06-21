using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	[HideInInspector]
	public Vector2			direction;
	[HideInInspector]
	public float			speed;
	[HideInInspector]
	public float			range;
	protected Rigidbody2D	_rigidbody;
	protected Vector2		_basePosition;
	public Human			shooter;

	void	Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_basePosition = _rigidbody.position;
	}

	void	FixedUpdate()
	{
		Move();
	}

	void	Move()
	{
		_rigidbody.position = _rigidbody.position + direction * speed * Time.deltaTime;

		if (range != -1 && Vector2.Distance(_basePosition, _rigidbody.position) > range)
			Destroy(gameObject);
	}

	void	OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject != shooter.gameObject && collision.gameObject.tag != "Bullet")
		{
			if (collision.gameObject.tag == "Door")
				collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 2f, ForceMode2D.Impulse);

			Destroy(gameObject);
		}
	}
}
