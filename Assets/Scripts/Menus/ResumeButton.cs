using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour
{
	public delegate				void ResumeEvent();
	public event ResumeEvent	OnResume;

	void OnMouseDown()
	{
		gameObject.SetActive(false);
		OnResume();
	}
}
