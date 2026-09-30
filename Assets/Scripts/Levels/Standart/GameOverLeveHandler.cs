using UnityEngine;

public class GameOverLeveHandler : MonoBehaviour
{
    [SerializeField] private EndGameScreen _endGameScreen;
    
    [SerializeField] private HintPurchaseHandler _hintPurchaseHandler;
    
    private InputReader _inputReader;

    public void Initialize(LevelData levelData, InputReader inputReader)
    {
        _inputReader = inputReader;
        _endGameScreen.SetLevelMode(LevelMode.Classic, levelData);
    }

    public void Faild(string textFinishGame)
    {
        _inputReader.StopReadInput();
        _endGameScreen.Open();
        _endGameScreen.ShowLoosInfo(textFinishGame);
        _hintPurchaseHandler.DisableHintButton();
    }
}