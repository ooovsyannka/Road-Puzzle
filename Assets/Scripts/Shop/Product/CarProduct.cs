using UnityEngine;

public abstract class CarProduct : ScriptableObject
{
    [SerializeField] private Car _carPrefab;
    [SerializeField] private int _index;

    protected bool _isBought;
    private bool _isSelected;

   // private string _name;
    
    public string Name => name;
    public int Speed => _carPrefab.Speed;
    public bool IsBought => _isBought;
    public int Index => _index;
    public Car CarPrefab => _carPrefab;
}