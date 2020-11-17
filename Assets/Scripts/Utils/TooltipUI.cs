using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour {

	public static TooltipUI Instance { get; private set; }

	[SerializeField] RectTransform canvasRectTransform;
	private RectTransform rectTransform;
	private TextMeshProUGUI textMeshPro;
	private RectTransform backgroundTransform;
	private TooltipTimer tooltipTimer;

	private void Awake ()
	{
		Instance = this;

		rectTransform = GetComponent<RectTransform> ();
		textMeshPro = transform.Find ("text").GetComponent<TextMeshProUGUI> ();
		backgroundTransform = transform.Find ("background").GetComponent<RectTransform> ();

		Hide ();
	}

	private void Update ()
	{
		HandleFollowMouse ();

		if (tooltipTimer != null) {
			tooltipTimer.timer -= Time.deltaTime;
			if (tooltipTimer.timer <= 0) {
				Hide ();
			}
		}
	}

	private void HandleFollowMouse ()
	{
		Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

		if (anchoredPosition.x + backgroundTransform.rect.width > canvasRectTransform.rect.width) {
			anchoredPosition.x = canvasRectTransform.rect.width - backgroundTransform.rect.width;
		}
		if (anchoredPosition.y + backgroundTransform.rect.height > canvasRectTransform.rect.height) {
			anchoredPosition.y = canvasRectTransform.rect.height - backgroundTransform.rect.height;
		}

		rectTransform.anchoredPosition = anchoredPosition;
	}

	private void SetText (string tooltipText)
	{
		//Set the text and resize the bankground
		textMeshPro.SetText (tooltipText);
		textMeshPro.ForceMeshUpdate ();
		Vector2 padding = new Vector2 (8, 4);
		Vector2 textSize = textMeshPro.GetRenderedValues (false);
		backgroundTransform.sizeDelta = textSize + padding;
	}

	public void Show (string toolTipText, TooltipTimer tooltipTimer = null)
	{
		//Enables the tooltip text
		this.tooltipTimer = tooltipTimer;
		gameObject.SetActive (true);
		SetText (toolTipText);
		HandleFollowMouse ();
	}

	public void Hide ()
	{
		//hide the tooltip
		gameObject.SetActive (false);
	}

	public class TooltipTimer {
		public float timer;
	}

}
