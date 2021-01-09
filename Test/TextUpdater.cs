using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
	Text textComponent;

	private void Awake()
	{
		textComponent = GetComponent<Text>();
	}

	public void SetText(int number)
	{
		textComponent.text = number.ToString();
	}

	public void SetText(string text)
    {
		textComponent.text = text;
	}

}
