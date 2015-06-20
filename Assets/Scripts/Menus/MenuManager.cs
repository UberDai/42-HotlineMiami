using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
	public List<GameObject>		elements = new List<GameObject>();
	public GameObject			resumeButton;

	public int rotateRange;

	void Start()
	{
		if (resumeButton != null)
		{
			ResumeButton rb = resumeButton.GetComponent<ResumeButton>();
			rb.OnResume += HidePauseMenu;
		}
	}

	void Update()
	{
		foreach (GameObject e in elements)
		{
			e.transform.Rotate(Vector3.right * Time.deltaTime);
		}
	}

	void HidePauseMenu()
	{
		transform.parent.gameObject.SetActive(false);
	}
}
