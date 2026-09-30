using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private List<CarProduct> _carProducts;
    [SerializeField] private CarProductSaver _carProductSaver;

    private Spawner<Car> _spawner;

    private void Awake()
    {
        _carProductSaver.RegisterGiftProducts(_carProducts);
    }

    public void InstalSelectedCar()
    {
        List<Car> _carPrefabs = new List<Car>();

        foreach (CarProduct carProduct in _carProducts)
        {
            if (_carProductSaver.IsCarSelected(carProduct))
            {
                _carPrefabs.Add(carProduct.CarPrefab);
            }
        }

        _spawner = new Spawner<Car>(_carPrefabs);

        Debug.Log($"{_carPrefabs.Count} objects spawned in CarSpawner");
    }

    public Car GetRandomCar(Vector3 carPosition, Quaternion rotation, TimeOfDay timeOfDay)
    {
        Car car = _spawner.SpawnObjectFromList(carPosition);

        car.transform.rotation = rotation;
        car.UpdateHeadlightsBasedOnTime(timeOfDay);
        car.Died += ReturnCarInPool;

        return car;
    }

    private void ReturnCarInPool(Car car)
    {
        _spawner.ReturnObjectInPool(car);
        car.Died -= ReturnCarInPool;
    }
}