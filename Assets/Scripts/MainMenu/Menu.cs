using UnityEngine;
using IJunior.TypedScenes;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [SerializeField] private LevelHolder _levelHolder;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Button _infinityButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private MenuRender _menuRender;
    [SerializeField] private Grid _infinityLevelGrid;
    [SerializeField] private SceneLoader _sceneLoader;


    private LevelData _currentLevel;

    private void Start()
    {
        //LaunchLevel();
    }

    private void OnEnable()
    {
        
        _levelButton.onClick.AddListener(LaunchLevel);
        _infinityButton.onClick.AddListener(LaunchInfinityLevel);
        _shopButton.onClick.AddListener(OpenShop);

       //  if(_levelHolder.TryGetLevelByIndex(out LevelData levelData))
       if (_levelHolder.TryGetLevelBySaver(out LevelData levelData))
        {
            _currentLevel = levelData;
            _menuRender.ShowLevelNumber(_currentLevel.Index);
        }
        else
        {
            _menuRender.ShowMessage();
        }
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(LaunchLevel);
        _infinityButton.onClick.RemoveListener(LaunchInfinityLevel);
        _shopButton.onClick.RemoveListener(OpenShop);
    }

    private void LaunchLevel()
    {
        _sceneLoader.TryLoadLevelScene(_currentLevel);
    }

    private void LaunchInfinityLevel()
    {
        _sceneLoader.TryLoadInfinityLevelScene();
        
    }

    private void OpenShop()
    {
        _sceneLoader.TryLoasShopScene();
       
    }
}