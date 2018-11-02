using UnityEngine;

public class CampaignSelectionManager : MonoBehaviour {

	public void OnFinalEmbarkButtonClicked() {
		SceneLoader.Instance.StartSceneLoading(new SceneLoadingInfo() { NextScene = GameScene.Raid });
	}
}
