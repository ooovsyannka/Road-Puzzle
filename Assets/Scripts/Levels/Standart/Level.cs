using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Chains _chains;
    [SerializeField] private CarSpawner _carSpawner;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LevelCompletionHandler _levelCompletionHandler;
    [SerializeField] private LevelInfoBar _levelInfoBar;
    [SerializeField] private float _carPositionY = 5;
    [SerializeField] private HintPurchaseHandler _hintPurchaseHandler;

    private List<StartRoad> _startRoads;
    private List<FinishRoad> _finishRoads;
    private List<RoadNode> _roads;
    private Grid _grid;
    private LevelData _currentLevelData;

    public void SetLevelInfo(Grid grid, List<StartRoad> startRoads, List<FinishRoad> finishRoads, List<RoadNode> roads,
        LevelData levelData)
    {
        _grid = grid;
        _startRoads = startRoads;
        _finishRoads = finishRoads;
        _roads = roads;
        _levelInfoBar.UpdateLevelNumber(levelData.Index);

        _currentLevelData = levelData;
        InitializeLevel();
        _levelCompletionHandler.Initialize(levelData, startRoads, finishRoads, roads);
    }

    private void InitializeLevel()
    {
        InitializeStartRoads();
        InitializeFinishRoads();
        InitializeRoadNodes();
        _hintPurchaseHandler.Initialize(_wallet, _currentLevelData) ;
    }

    private void InitializeStartRoads()
    {
        Cell cell;
        Vector3 carSpawnPosition;
        Vector3 cellPosition;

        foreach (StartRoad startRoad in _startRoads)
        {
            cellPosition = startRoad.transform.position;

            if (!_grid.TryGetCell(cellPosition, out cell))
                continue;

            cell.SetRoadBoundary(startRoad);
            carSpawnPosition = startRoad.transform.position;
            _carSpawner.InstalSelectedCar();
            Car car = _carSpawner.GetRandomCar(carSpawnPosition + Vector3.up, startRoad.transform.rotation,
                _currentLevelData.TimeOfDay);
            _chains.CreateChain(startRoad);
            startRoad.SetCar(car);
        }
    }

    private void InitializeFinishRoads()
    {
        Cell cell;
        Vector3 cellPosition;

        foreach (FinishRoad finishRoad in _finishRoads)
        {
            cellPosition = finishRoad.transform.position;

            if (_grid.TryGetCell(cellPosition, out cell))
            {
                cell.SetRoadBoundary(finishRoad);
            }
        }
    }

    private void InitializeRoadNodes()
    {
        Cell cell;
        Vector3 cellPosition;

        foreach (RoadNode road in _roads)
        {
            foreach (SingleRoad singleRoad in road.SingleRoadHolder.SingleRoads)
            {
                cellPosition = singleRoad.transform.position;

                if (_grid.TryGetCell(cellPosition, out cell))
                {
                    cell.SetRoad(road);
                }
            }
        }
    }
}