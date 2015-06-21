using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Human
{
	public enum MoveState
	{
		Standing,
		Walking,
		FollowingPlayer
	}

	public Enemy.MoveState	currentState;
	public Vector2[]		predefinedPath;
	public bool				hearsGunShots = false;

	int						targetPoint = 0;

	void Start()
	{
		GameObject	go;

		// GameManager.hero.OnFire += OnPlayerShot;

		go = (GameObject) Instantiate(defaultWeapon);
		go.GetComponent<Weapon>().ammo = -1;
		PickWeapon(go);
	}

	protected override void  FixedUpdate()
	{
		if (dead)
			return ;

		base.FixedUpdate();

		if (currentState == Enemy.MoveState.Walking)
			FollowPredefinedPath();
		else if (currentState == Enemy.MoveState.FollowingPlayer)
			FollowPlayer();
	}

	void OnPlayerShot()
	{
		if (hearsGunShots)
			currentState = Enemy.MoveState.FollowingPlayer;
	}

	void FollowPlayer()
	{
		Vector3	heroPosition;
		bool	viewingHero;

		viewingHero = IsViewingHero();
		heroPosition = GameManager.hero.transform.position;

		if (viewingHero)
			Fire();

		if (viewingHero && Vector2.Distance(heroPosition, transform.position) < 2f)
			movingToTarget = false;
		else
			MoveTo(heroPosition);

		SetAimingTarget(heroPosition);
	}

	void FollowPredefinedPath()
	{
		if (predefinedPath.Length == 0)
			return ;

		MoveTo(predefinedPath[targetPoint]);
		SetAimingTarget(predefinedPath[targetPoint]);

		if (IsOnTargetPoint())
			targetPoint = (targetPoint + 1) % predefinedPath.Length;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag != "Hero")
			return ;

		if (IsViewingHero())
			currentState = Enemy.MoveState.FollowingPlayer;
	}

	bool IsOnTargetPoint()
	{
		return (transform.position.x == predefinedPath[targetPoint].x
			&& transform.position.y == predefinedPath[targetPoint].y);
	}

	bool	IsViewingHero()
	{
		Vector3			heroPosition;
		RaycastHit2D[]	hits;
		LayerMask		layerMask;
		GameObject		go;

		layerMask = LayerMask.GetMask("Character", "Wall");
		heroPosition = GameManager.hero.transform.position;
		hits = Physics2D.RaycastAll(transform.position, heroPosition - transform.position, Mathf.Infinity, layerMask.value);

		foreach (RaycastHit2D hit in hits)
		{
			go = hit.collider.gameObject;

			if (go == this)
				continue;
			else if (go.layer == 10)
				return false;
		}

		return (true);
	}

	protected override void	Die(Bullet bullet)
	{
		base.Die(bullet);
		Destroy(gameObject, 2f);
	}
}
