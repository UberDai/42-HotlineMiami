using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	public GameObject	hero;

	float				originalZ;

	void Start()
	{
		originalZ = transform.position.z;

		hero = GameObject.FindGameObjectsWithTag("Hero")[0];
	}

	void Update()
	{
		if (hero)
			transform.position = new Vector3(HeroPos().x, HeroPos().y, originalZ);
	}

	Vector3	HeroPos() { return hero.transform.position; }
}
