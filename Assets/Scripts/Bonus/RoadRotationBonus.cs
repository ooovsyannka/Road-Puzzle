using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadRotationBonus : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RoadDrager _roadDrager;
    [SerializeField] private List<Quaternion> _allowedRotations;
    [SerializeField] private Wallet _wallet;

    private int _cost = 75;
    private int _freeAttempts = 3;
    private RaycastHit _raycastHit;
    private RoadNode _selectedRoad;
    private bool _isActive;

    public int Cost => _cost;
    public int FreeAttempts => _freeAttempts;
    
    public event Action<bool> SelectionStateChanged;

    private void OnDisable()
    {
        _inputReader.RoadPickupAttempt -= TryRotateRoad;
        _roadDrager.EnableDragRoad();
    }

    public void StartSelectingRoad()
    {
        bool canActiveBonus = false;

        if (_freeAttempts > 0)
        {
            _freeAttempts--;
            canActiveBonus = true;
        }
        else if (_wallet.TryRemoveCoin(_cost))
        {
            canActiveBonus = true;
        }

        if (canActiveBonus)
        {
            _isActive = true;
            SelectionStateChanged?.Invoke(_isActive);
            _roadDrager.DisableDragRoad();
            _inputReader.RoadPickupAttempt += TryRotateRoad;
        }
    }

    public void StopSelectingRoad()
    {
        _isActive = false;
        SelectionStateChanged?.Invoke(false);
        _inputReader.RoadPickupAttempt -= TryRotateRoad;
        _roadDrager.EnableDragRoad();
    }

    private void TryRotateRoad()
    {
        if (!_inputReader.TryHitCellUnderPointer(out _raycastHit) ||
            !_raycastHit.transform.TryGetComponent(out Cell cell) ||
            !cell.TryGetRoad(out _selectedRoad))
        {
            return;
        }

        Quaternion nextRotation = GetNextRotation(_selectedRoad.transform.rotation);
        _selectedRoad.transform.rotation = nextRotation;
        _selectedRoad.RoadRotation.ChangeInitialRotation(nextRotation);
    }

    private Quaternion GetNextRotation(Quaternion currentRotation)
    {
        if (_allowedRotations == null || _allowedRotations.Count == 0)
            return currentRotation;

        float currentAngle = Mathf.Round(
            _selectedRoad.transform.eulerAngles.y / 90f
        ) * 90f;

        int currentIndex = _allowedRotations.FindIndex(rotation =>
            Mathf.Abs(Mathf.DeltaAngle(currentAngle, rotation.eulerAngles.y)) < 1f);

        if (currentIndex < 0)
            return _allowedRotations[0];

        int nextIndex = (currentIndex + 1) % _allowedRotations.Count;
        return _allowedRotations[nextIndex];
    }
}