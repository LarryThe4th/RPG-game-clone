using System;
using UnityEngine;
using UnityEngine.UI;

/*
Controls the fade in and fade out effects during scene transaction.
 */
public class ScreenFadeControl : MonoBehaviour {
    private const int animationPlayBackSpeed_normal = 1;
	
	private Animator screenFadeAnimator;

	private event Action onFadeOutEnded;

	private event Action onAppearEnded;

    void Awake()
	{
		init();
	}

	// Setup all the components related to this controller. 
	private void init() 
	{
		screenFadeAnimator = GetComponent<Animator>();
	}

	public void FadeOut(int speed = animationPlayBackSpeed_normal, Action fadeOutEnded = null) {
		onFadeOutEnded = fadeOutEnded;
		screenFadeAnimator.speed = speed;
		screenFadeAnimator.SetBool("FadeOut", true);
		
	}

	public void Appear(int speed = animationPlayBackSpeed_normal, Action appearEnded = null) {
		onAppearEnded = appearEnded;
		screenFadeAnimator.speed = speed;
		screenFadeAnimator.SetBool("Appear", true);
		
	}

	public void InstantBackOut() {
		if (screenFadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dark")) return;
		screenFadeAnimator.SetBool("Appear", false);
        screenFadeAnimator.SetBool("FadeOut", false);
		screenFadeAnimator.SetTrigger("InstantBackout");
	}

	public void Reset()
    {
        screenFadeAnimator.speed = animationPlayBackSpeed_normal;
        screenFadeAnimator.SetBool("Appear", false);
        screenFadeAnimator.SetBool("FadeOut", false);
        screenFadeAnimator.SetTrigger("Reset");
    }
	
	public void OnFadeOutEnded() {
		screenFadeAnimator.SetBool("FadeOut", false);
		if (onFadeOutEnded != null) {
			onFadeOutEnded.Invoke();
		}
	}

	public void OnAppearEnded() {
		screenFadeAnimator.SetBool("Appear", false);
		if (onAppearEnded != null) {
			onAppearEnded.Invoke();
		}
	}
}
