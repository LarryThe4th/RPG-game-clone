using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameScene {
	Nothing,
	Loading,
	EstateManagement,
	Raid,
	CampaignSelection
}

public class SceneLoader : MonoBehaviour {

	public delegate void LoadingProgressUpdatedHandler(float percentage);

    public LoadingProgressUpdatedHandler onLoadingProgressUpdated;

    private SceneLoadingInfo loadingInfo;

	private AsyncOperation async = null;

	private static SceneLoader instance = null;

    public static SceneLoader Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake() {
		EnforceSingletonPattern();
		inti();
	}

	private void EnforceSingletonPattern()
    {
        if (Instance== null) {
            // Preserve the reference of the manager singleton instance.
            instance = this;
			// Make sure this gameobject will not be destroy when loaded into new scene.
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			// No duplcate singleton gameObject allow.
			Destroy(this.gameObject);
			return;
		}
    }

	private void inti() {
		SceneManager.sceneLoaded += onSceneLoaded;
	}
	
    public void StartSceneLoading(SceneLoadingInfo info) {
		if (info.NextScene != GameScene.Loading) {
			loadingInfo = info;
			SceneManager.LoadScene(GameScene.Loading.ToString(), LoadSceneMode.Single);
		}
	}

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GameScene.Loading.ToString()) {
			DarkestDungeonManager.Instance.GetScreenFadeContorl.InstantBackOut();
			StartCoroutine(SceneLoading(loadingInfo.NextScene.ToString()));
		}
    }

	private IEnumerator SceneLoading(String nextScene) {
		// Clear up whatever leftover form last scene.
		ClearUp();

		// Appear loading scene.
		yield return new WaitForEndOfFrame();
		ScreenFadeControl screenFadeControl = DarkestDungeonManager.Instance.GetScreenFadeContorl;
		screenFadeControl.Appear();

		// Wait for the appear animation to finished.
		yield return new WaitForSeconds(1f);
		
		// Asynchrony load the next scene in the backgroun.
        async = SceneManager.LoadSceneAsync(nextScene);

		// Pervent the next scene active itself after loading finished.
		async.allowSceneActivation = false;

		// Keep loading until loading complete
        while (!async.isDone)
        {
			// Since we set scene activation to FALSE, loading process will only progress up to 0.9
            if (async.progress >= 0.9f) {
				break;
			}
			else if (onLoadingProgressUpdated != null) {
				// keep updating the loading progress event.
				Debug.Log("check A");
				onLoadingProgressUpdated(async.progress);
			}
				
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        screenFadeControl.FadeOut();
        yield return new WaitForSeconds(1f);

		// Active scene after loading complete.
        async.allowSceneActivation = true;

		Debug.Log("check");

		if (onLoadingProgressUpdated != null) {
			onLoadingProgressUpdated(async.progress);
		}

		screenFadeControl.Appear();
	}

	private void ClearUp() {
		Resources.UnloadUnusedAssets();
        GC.Collect();
	}

	private void OnDestroy()
    {	
		// Don't forget removing the event subscription. 
        SceneManager.sceneLoaded -= onSceneLoaded;
    }
}