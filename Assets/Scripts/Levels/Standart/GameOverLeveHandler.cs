using System;
using UnityEngine;

public class GameOverLeveHandler : MonoBehaviour
{
    [SerializeField] private EndGameScreen _endGameScreen;

    public void Initialize(LevelData levelData)
    {
        _endGameScreen.SetLevelMode(LevelMode.Classic, levelData);
    }

    public void Faild(string textFinishGame)
    {
        _endGameScreen.Open();
        _endGameScreen.ShowLoosInfo(textFinishGame);
    }
}