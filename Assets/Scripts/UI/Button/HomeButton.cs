using IJunior.TypedScenes;
using UnityEngine;

public class HomeButton : ActionButton
{
    [SerializeField] private SceneLoader _sceneLoader;

    protected override void OnButtonAction()
    {
        _sceneLoader.TryLoadMainMenuScene();
    }
}