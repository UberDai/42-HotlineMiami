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
	public bool				hearsGunShots;

	int						targetPoint = 0;

	void Start()
	{
		hearsGunShots = false;
		GameManager.hero.OnFire += OnPlayerShot;
	}

	protected override void  FixedUpdate()
	{
		if (currentState == Enemy.MoveState.Walking)
			FollowPredefinedPath();
		else if (currentState == Enemy.MoveState.FollowingPlayer)
			FollowPlayer();

		base.FixedUpdate();
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

		if (viewingHero && Vector2.Distance(heroPosition, transform.position) < 2f)
			movingToTarget = false;
		else
			MoveTo(heroPosition);

		SetAimingTarget(heroPosition);
	}

	void FollowPredefinedPath()
	{
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
		RaycastHit2D	hit;

		heroPosition = GameManager.hero.transform.position;
		hit = Physics2D.Raycast(transform.position, heroPosition - transform.position);

		return (hit.collider.gameObject.tag == "Hero");
	}
}
