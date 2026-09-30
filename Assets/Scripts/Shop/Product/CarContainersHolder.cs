using System;
using System.Collections.Generic;
using UnityEngine;

public class CarContainersHolder : MonoBehaviour
{
    [SerializeField] private List<CarContainer> _carContainers;
    [SerializeField] private CarProductSaver _carProductSaver;
    
    private CarContainer _currentCarContainer;
    
    public event Action<CarContainer> CarContainerSelected;
    
    public CarContainer CurrentCarContainer => _currentCarContainer;
    
    private void Awake()
    {
        var carProducts = new List<CarProduct>(_carContainers.Count);
        foreach (CarContainer carContainer in _carContainers)
        {
            if (carContainer != null && carContainer.CarProduct != null)
                carProducts.Add(carContainer.CarProduct);
        }

        _carProductSaver.RegisterGiftProducts(carProducts);

        int index = 0;

        foreach (CarContainer carContainer in _carContainers)
        {
            carContainer.CarInfo.UpdateLockImage(_carProductSaver.IsCarBought(carContainer.CarProduct));
            carContainer.CarInfo.UpdateSelectImage(_carProductSaver.IsCarSelected(carContainer.CarProduct));

            if (carContainer.CarProduct is CarGift carGift && carContainer.CarInfo is CarGifrInfo carGiftInfo)
                carGiftInfo.UpdateSlider(_carProductSaver.GetGiftCarProgress(carGift));

            carContainer.CarGoodsInfoShowed += SetCurrentCarContainer;
            carContainer.SetIndex(index);
            index++;
        }
    }

    private void SetCurrentCarContainer(CarContainer carContainer)
    {
        _currentCarContainer = carContainer;
        
        CarContainerSelected?.Invoke(_currentCarContainer);
    }
}
