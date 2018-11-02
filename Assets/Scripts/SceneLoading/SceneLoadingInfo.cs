public struct SceneLoadingInfo {
	private GameScene nextScene;

    public GameScene NextScene
    {
        get
        {
            return nextScene;
        }

        set
        {
            nextScene = value;
        }
    }
}