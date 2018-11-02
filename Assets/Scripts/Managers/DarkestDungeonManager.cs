using UnityEngine;
/*
This is the core manager class of the game.
 */
public class DarkestDungeonManager : MonoBehaviour {

    // The singleton partten for the manager class.
    private static DarkestDungeonManager instance = null;

    private ScreenFadeControl screenFadeContorl;

    public ScreenFadeControl GetScreenFadeContorl
    {
        get
        {
            return screenFadeContorl;
        }
    }

    public static DarkestDungeonManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Used to initialize any variables or game state before the game starts.
    void Awake() {
		EnforceSingletonPattern();
        init();
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
    
    private void init() {
        screenFadeContorl = GetComponentInChildren<ScreenFadeControl>();
    }
}