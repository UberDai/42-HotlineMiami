using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
	[HideInInspector]
	public Vector2			moveDirection;
	[HideInInspector]
	public Vector2			aimDirection;
	public int				maxLife;
	public float			speed;
	public float			rotationSpeed;
	public Sprite			headSprite;
	public Sprite			bodySprite;
	public float			throwForce;

	protected GameObject	_weapon;
	protected GameObject	_availableWeapon;


	void		Awake()
	{
		_availableWeapon = null;
	}

	void		FixedUpdate()
	{
		HandleInputs();
		Move();
		Rotate();
		UpdateSprites();
	}

	protected virtual void		HandleInputs()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			if (_weapon)
				DropWeapon();
			else
				TryToPickWeapon();
		}
	}

	void		Move()
	{
		Quaternion	rotation;

		rotation = transform.rotation;
		transform.rotation = Quaternion.identity;
		transform.Translate(moveDirection * speed / (1 / Time.deltaTime));
		transform.rotation = rotation;
	}

	void		UpdateSprites()
	{
		SpriteRenderer[]	sprites;

		sprites = GetComponentsInChildren<SpriteRenderer>();

		sprites[0].sprite = headSprite;

		if (_weapon != null)
			sprites[1].sprite = _weapon.GetComponent<Weapon>().attachedSprite;
		else
			sprites[1].sprite = null;

		sprites[2].sprite = bodySprite;
	}

	void		Rotate()
	{
		Quaternion	newRotation;

		newRotation = Quaternion.LookRotation(transform.position - (Vector3) aimDirection, Vector3.forward);
	    newRotation.x = 0;
	    newRotation.y = 0;

	    newRotation = newRotation * new Quaternion(0, 0, -1, 0);

	    if (rotationSpeed > 0)
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
	    else
			transform.rotation = newRotation;
	}

	void		TryToPickWeapon()
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

	void		PickWeapon(GameObject target)
	{
		_weapon = target;
		target.GetComponent<SpriteRenderer>().enabled = false;
	}

	void		DropWeapon()
	{
		Rigidbody2D	rigidbody;

		_weapon.transform.position = transform.position;
		_weapon.GetComponent<SpriteRenderer>().enabled = true;
		rigidbody = _weapon.GetComponent<Rigidbody2D>();
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = 0;
		rigidbody.AddForce(Vector3.Normalize(aimDirection) * throwForce, ForceMode2D.Impulse);
		_weapon = null;
	}

	void		OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Weapon")
			_availableWeapon = collider.gameObject;
	}

	void		OnTriggerExit2D(Collider2D collider)
	{
		_availableWeapon = null;
	}
}
