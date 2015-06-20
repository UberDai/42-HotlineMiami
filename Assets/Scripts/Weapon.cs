using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public Sprite	attachedSprite;
	public Sprite	bulletSprite;
	public uint		ammo;
	public Object	bulletPrefab;

	public void	Fire()
	{
		Vector2		position;
		Quaternion	rotation;
		Object		prefab;

		position = GameManager.hero.transform.position;
		rotation = GameManager.hero.transform.rotation;
		Instantiate(bulletPrefab, position, rotation);

		ammo--;
	}
}
