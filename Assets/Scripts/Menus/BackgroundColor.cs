using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundColor : MonoBehaviour
{
	[HideInInspector]public Image		image;
	[HideInInspector]public Color[]		backgroundColors;

	public float	alpha;
	public float	waitTime;

	int				currentBackgroundColor;
	float			elapsedTime = 0;

	void Start()
	{
		image = GetComponent<Image>();
		InitBackgroundColors();
	}

	void FixedUpdate()
	{
		if (currentBackgroundColor >= backgroundColors.Length)
			currentBackgroundColor = 0;

		image.color = Color.Lerp(image.color, backgroundColors[currentBackgroundColor], Time.time / 2);
		UpdateColorIndex();
	}

	void UpdateColorIndex()
	{
		elapsedTime += Time.deltaTime;

		if (elapsedTime > waitTime)
		{
			elapsedTime = 0;
			currentBackgroundColor += 1;
		}
	}

	void InitBackgroundColors()
	{
		backgroundColors = new Color[4];

		backgroundColors[0] = new Color(0.144f, 0.85f, 0.67f, alpha);
		backgroundColors[1] = new Color(0.255f, 0f, 0.227f, alpha);
		backgroundColors[2] = new Color(0.89f, 0.201f, 0.196f, alpha);
		backgroundColors[3] = new Color(0.214f, 0.204f, 0.68f, alpha);
	}
}
