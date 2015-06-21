using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Enemy : Human
{
	public enum MoveState {
		Standing,
		Walking,
		FollowPlayer
	}

	[HideInInspector] bool	collidesWithPlayer;

	public Enemy.MoveState	currentState;
	public Vector2[]		predefinedPath;

	public Transform 		target;
	private Seeker 			seeker;
	public Path 			path;
	public float 			nextWaypointDistance = 3;
	private int 			currentWaypoint = 0;

	int						targetPoint = 0;

	void Start()
	{
		seeker = GetComponent<Seeker>();

		seeker.StartPath( transform.position, target.position, OnPathComplete );
	}

	public void OnPathComplete ( Path p )
	{
    	Debug.Log( "Yay, we got a path back. Did it have an error? " + p.error );
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}

	protected override void  FixedUpdate()
	{
		base.FixedUpdate();
		seeker.StartPath( transform.position, target.position, OnPathComplete );

		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count)
		{
			Debug.Log( "End Of Path Reached" );
			return;
		}

		MoveTo(path.vectorPath[ currentWaypoint ]);

		if (Vector3.Distance( transform.position, path.vectorPath[ currentWaypoint ] ) < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}

		// if (currentState == Enemy.MoveState.Walking)
		// 	FollowPredefinedPath();
		// else if (currentState == Enemy.MoveState.FollowPlayer)
		// 	FollowPlayer();
	}

	void FollowPlayer()
	{
		MoveTo(GameManager.hero.transform.position);
	}

	void FollowPredefinedPath()
	{
		// MoveTo(predefinedPath[targetPoint]);

		if (IsOnTargetPoint())
			targetPoint += 1;

		if (targetPoint >= predefinedPath.Length)
			targetPoint = 0;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Hero")
			currentState = Enemy.MoveState.FollowPlayer;
	}

	bool IsOnTargetPoint() { return transform.position.x == predefinedPath[targetPoint].x && transform.position.y == predefinedPath[targetPoint].y; }
}