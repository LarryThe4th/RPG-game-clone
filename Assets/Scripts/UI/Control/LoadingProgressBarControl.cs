using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBarControl : MonoBehaviour {
	
	[SerializeField]
	private Image progressBarImage;

	private void Awake() {
		Init();
	}

	private void Init() {
		if (progressBarImage == null)
			progressBarImage = GetComponent<Image>();

		progressBarImage.fillAmount = 0;
		SceneLoader.Instance.onLoadingProgressUpdated += UpdateProgessBar;
	}

    private void UpdateProgessBar(float percentage)
    {
        progressBarImage.fillAmount = percentage;
		Debug.Log(percentage);
    }

	private void OnDestroy() {
		// Don't forget removing the event subscription. 
		SceneLoader.Instance.onLoadingProgressUpdated -= UpdateProgessBar;
	}
}
