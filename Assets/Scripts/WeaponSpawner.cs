using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSpawner : MonoBehaviour
{
	public List<Object>	weapons;

	void	Awake()
	{
		Object		prefab;
		int			index;

		index = (int) Random.Range(0, (float) (weapons.Count));
		prefab = weapons[index];

		Instantiate(prefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
