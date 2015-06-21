using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public Sprite	attachedSprite;
	public Sprite	bulletSprite;
	public float	bulletSpeed;
	public int		ammo;
	public float	range;
	public Object	bulletPrefab;

	public Human	holder;

	public void	Fire()
	{
		Vector2		position;
		Quaternion	rotation;
		GameObject	go;
		Bullet		bullet;

		if (ammo <= 0)
			return ;

		position = holder.transform.position;
		rotation = holder.transform.rotation;
		rotation *= Quaternion.Euler(0, 0, -90);

		go = (GameObject) Instantiate(bulletPrefab, position, rotation);

		bullet = go.GetComponent<Bullet>();
		bullet.direction = holder.aimingDirection * -1;
		bullet.speed = bulletSpeed;
		bullet.range = range;

		bullet.GetComponent<SpriteRenderer>().sprite = bulletSprite;

		if (ammo > 0)
			ammo--;
	}
}
