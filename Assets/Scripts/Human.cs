using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
	public Vector2		moveDirection;
	[HideInInspector]
	public Vector2		aimDirection;
	public int			maxLife;
	public float		speed;
	public float		rotationSpeed;
	public Sprite		headSprite;
	public Sprite		bodySprite;
	public GameObject	weapon;

	public void	FixedUpdate()
	{
		Move();
		Rotate();
		UpdateSprites();
	}

	public void	Move()
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
		sprites[1].sprite = weapon.GetComponent<Weapon>().attachedSprite;
		sprites[2].sprite = bodySprite;
	}

	void		Rotate()
	{
		float		angle;
		Vector2		mousePosition;
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
}
