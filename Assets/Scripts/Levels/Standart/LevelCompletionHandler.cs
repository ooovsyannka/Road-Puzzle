using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LevelWinHandler _levelWinHandler;
    [SerializeField] private GameOverLeveHandler _gameOverLeveHandler;
    [SerializeField] private HintPurchaseHandler _hintPurchaseHandler;
    [SerializeField] private Timer _timer;
    [SerializeField] private Chains _chains;

    private int _completeRoadCount;
    private List<RoadNode> _roads;
    private List<StartRoad> _startRoads;
    private List<FinishRoad> _finishRoads;

    private void OnDisable()
    {
        _timer.TimeIsOvered -= _gameOverLeveHandler.Faild;
    }

    public void Initialize(LevelData levelData, List<StartRoad> startRoads, List<FinishRoad> finishRoads,
        List<RoadNode> roads)
    {
        _levelWinHandler.Initialize(levelData);
        _gameOverLeveHandler.Initialize(levelData);
        _roads = roads;
        _startRoads = startRoads;
        _finishRoads = finishRoads;
        _timer.SetMaxMinute(levelData.TimeForLevelInMinute);
        _timer.SetMaxSecond(levelData.TimeForLevelInSeconds);
        _timer.TimeIsOvered += _gameOverLeveHandler.Faild;
        SubscribeToFinishRoads();
        _timer.LaunchCountdown();
    }

    private void SubscribeToFinishRoads()
    {
        foreach (FinishRoad finishRoad in _finishRoads)
        {
            if (finishRoad is IFinishRoad finish)
            {
                finish.OnConnected += TryCompleteLevel;
            }
        }
    }

    private void TryCompleteLevel(IFinishRoad iFinishRoad)
    {
        Car car = null;
        bool allRoadIsConnet = true;

        if (iFinishRoad is FinishRoad finishRoad)
        {
            foreach (RoadNode road in _roads)
            {
                if (road.IsConnect)
                    continue;

                allRoadIsConnet = false;

                break;
            }

            foreach (StartRoad startRoad in _startRoads)
            {
                if (finishRoad.Index == startRoad.Index)
                {
                    if (startRoad is IStartRoad iStartRoad)
                    {
                        car = startRoad.Car;
                        car.Move(_chains.CreateRoute(iStartRoad, finishRoad.SplineComputer).SplineComputer,
                            finishRoad.SplineComputer);
                        _completeRoadCount++;

                        break;
                    }
                }
            }
        }

        if (_completeRoadCount == _startRoads.Count)
        {
            if (allRoadIsConnet)
            {
                _levelWinHandler.Win();
            }
            else
            {
                _gameOverLeveHandler.Faild("НЕ ВСЕ ДОРОГИ СОЕДЕНЕННЫ!");
            }

            StopLevelProgression();
        }
    }

    private void StopLevelProgression()
    {
        _inputReader.StopReadInput();
        _timer.StopCountdown();
        _hintPurchaseHandler.DisableHintButton();
    }
}