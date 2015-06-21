using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
	protected Vector2		_movingTarget;
	[HideInInspector]
	public bool				movingToTarget;
	[HideInInspector]
	public Vector2			movingDirection;
	[HideInInspector]
	public Vector2			aimingTarget;
	[HideInInspector]
	public Vector2			aimingDirection;
	public Object			defaultWeapon;
	public int				maxLife;
	public float			speed;
	public float			rotationSpeed;
	public Sprite			headSprite;
	public Sprite			bodySprite;
	public float			throwingForce;
	protected Rigidbody2D	_rigidbody;
	protected Animator		_legsAnimator;
	public bool				dead = false;

	public Weapon			weapon;

	void				Awake()
	{
		movingToTarget = false;
		_rigidbody = GetComponent<Rigidbody2D>();
		_legsAnimator = GetComponentsInChildren<SpriteRenderer>()[3].GetComponent<Animator>();
	}

	protected void		Update()
	{
		HandleInputs();
	}

	protected virtual void		FixedUpdate()
	{
		if (dead)
			return ;

		_rigidbody.velocity = Vector3.zero;
		_rigidbody.angularVelocity = 0;

		Move();
		Rotate();
		UpdateSprites();
	}

	protected virtual void		HandleInputs()
	{
	}

	protected void		Move()
	{
		Quaternion	rotation;

		if (!movingToTarget && movingDirection == Vector2.zero)
		{
			_legsAnimator.SetBool("Walking", false);
			return ;
		}

		rotation = transform.rotation;
		transform.rotation = Quaternion.identity;

		_legsAnimator.SetBool("Walking", true);

		if (movingToTarget)
		{
			_rigidbody.position = Vector3.MoveTowards(_rigidbody.position, _movingTarget, speed * Time.deltaTime);

			if ((Vector2) transform.position == _movingTarget)
				movingToTarget = false;
		}
		else
			_rigidbody.MovePosition(_rigidbody.position + movingDirection * speed * Time.deltaTime);

		transform.rotation = rotation;
	}

	protected void		SetAimingTarget(Vector2 target)
	{
		aimingTarget = target;
		aimingDirection = (Vector2) transform.position - aimingTarget;
		aimingDirection.Normalize();
	}

	protected void		UpdateSprites()
	{
		SpriteRenderer[]	sprites;

		sprites = GetComponentsInChildren<SpriteRenderer>();

		sprites[0].sprite = headSprite;

		if (weapon != null)
			sprites[1].sprite = weapon.attachedSprite;
		else
			sprites[1].sprite = null;

		sprites[2].sprite = bodySprite;
	}

	protected void		Fire()
	{
		if (weapon == null)
			return ;

		weapon.Fire();
	}

	protected void		Rotate()
	{
		Quaternion	newRotation;
		Vector3		to;

		to = transform.position - (Vector3) aimingTarget;

		if (to == Vector3.zero)
			return ;

		newRotation = Quaternion.LookRotation(to, Vector3.forward);
	    newRotation.x = 0;
	    newRotation.y = 0;

	    newRotation = newRotation * new Quaternion(0, 0, -1, 0);

	    if (rotationSpeed > 0)
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
	    else
			transform.rotation = newRotation;
	}

	protected void		TryToPickWeapon()
	{
		RaycastHit2D[]	hits;

		hits = Physics2D.RaycastAll(transform.position, Vector2.zero);

		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider.gameObject.tag == "Weapon")
			{
				PickWeapon(hit.collider.gameObject);
				break;
			}
		}
	}

	protected void		PickWeapon(GameObject target)
	{
		weapon = target.GetComponent<Weapon>();
		weapon.GetComponent<SpriteRenderer>().enabled = false;
		weapon.GetComponent<BoxCollider2D>().enabled = false;
		weapon.holder = this;
	}

	protected void		DropWeapon()
	{
		Rigidbody2D	rigidbody;

		weapon.transform.position = transform.position;
		weapon.transform.rotation = Quaternion.identity;
		weapon.GetComponent<SpriteRenderer>().enabled = true;
		weapon.GetComponent<BoxCollider2D>().enabled = true;
		weapon.holder = null;
		rigidbody = weapon.GetComponent<Rigidbody2D>();
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = 0;
		rigidbody.AddTorque(Random.Range(-2f, 2f), ForceMode2D.Impulse);
		rigidbody.AddForce(aimingDirection * throwingForce * -1, ForceMode2D.Impulse);
		weapon = null;
	}

	protected void		MoveTo(Vector2 target)
	{
		_movingTarget = target;
		_rigidbody.fixedAngle = true;
		movingToTarget = true;
	}

	protected virtual void				Die(Bullet bullet)
	{
		dead = true;
		_rigidbody.drag = 6f;
		_rigidbody.angularDrag = 0;
		_legsAnimator.SetBool("Walking", false);
		_rigidbody.AddForce(bullet.direction * 4, ForceMode2D.Impulse);
	}

	virtual protected void	OnCollisionEnter2D(Collision2D collision)
	{
		Bullet	bullet;

		if (collision.gameObject.tag == "Bullet")
		{
			bullet = collision.gameObject.GetComponent<Bullet>();

			if (bullet.shooter != this)
			{
				Die(bullet);
			}
		}
	}
}
