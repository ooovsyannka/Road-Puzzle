using System.Collections.Generic;
using UnityEngine;

public class CarProductSaver : MonoBehaviour
{
    public List<CarContainer> carContainers;
    public CarProduct DefaultCarProduct;

    private const string OpenCar = nameof(OpenCar);
    private const string SelectCarCount = nameof(SelectCarCount);
    private const string SelectCar = nameof(SelectCar);
    private const string ProcentageUnblockingCar = nameof(ProcentageUnblockingCar);
    private const string CurrentRewardCarIndex = nameof(CurrentRewardCarIndex);

    private const int NumberPurchasedCar = 1;
    private int _selectCarCount;
    private readonly List<CarGift> _carGifts = new List<CarGift>();

    private void Awake()
    {
        _selectCarCount = PlayerPrefs.GetInt($"{SelectCarCount}");
    }

    /*private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            foreach (CarContainer carContainer in carContainers)
            {
                PlayerPrefs.DeleteKey($"{OpenCar} {carContainer.CarProduct.Index}");
                PlayerPrefs.DeleteKey($"{SelectCar} {carContainer.CarProduct.Index}");
            }

            _selectCarCount = 1;

            PlayerPrefs.SetInt(SelectCarCount, _selectCarCount);
            PlayerPrefs.SetInt($"{OpenCar} {DefaultCarProduct.Index}", NumberPurchasedCar);
            PlayerPrefs.SetInt($"{SelectCar} {DefaultCarProduct.Index}", NumberPurchasedCar);
            PlayerPrefs.DeleteKey(CurrentRewardCarIndex);
            PlayerPrefs.DeleteKey(ProcentageUnblockingCar);
            print("Купленные Машини почистины");
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            PlayerPrefs.DeleteKey(ProcentageUnblockingCar);
            print("Процент удален");
        }
    }*/

    public void SaveCarProduct(CarProduct carProduct)
    {
        PlayerPrefs.SetInt($"{OpenCar} {carProduct.Index}", NumberPurchasedCar);
        SaveSelectCarProduct(carProduct);
    }

    public void SaveSelectCarProduct(CarProduct carProduct)
    {
        _selectCarCount++;
        PlayerPrefs.SetInt(SelectCarCount, _selectCarCount);
        PlayerPrefs.SetInt($"{SelectCar} {carProduct.Index}", NumberPurchasedCar);
    }

    public void SaveProcentageUnblockingCar(float procentage)
    {
            float procentageToSave = GetProcentageUnblockingCar() + procentage;
            PlayerPrefs.SetFloat(ProcentageUnblockingCar, procentageToSave);
    }

    public float GetProcentageUnblockingCar()
    {
        return PlayerPrefs.GetFloat(ProcentageUnblockingCar);
    }

    public void RegisterGiftProducts(IEnumerable<CarProduct> products)
    {
        if (products == null)
            return;

        foreach (CarProduct product in products)
            RegisterGiftProduct(product);
    }

    public CarGift GetCurrentRewardCar()
    {
        RegisterGiftProductsFromContainers();

        int currentIndex = PlayerPrefs.GetInt(CurrentRewardCarIndex, -1);
        if (currentIndex >= 0)
        {
            CarGift currentGift = FindGift(currentIndex);

            
            if (currentGift == null)
                return null;

            if (IsRewardCar(currentGift) && IsCarBought(currentGift) == false)
                return currentGift;

            PlayerPrefs.SetInt(CurrentRewardCarIndex, -1);
            PlayerPrefs.SetFloat(ProcentageUnblockingCar, 0f);
        }

        List<CarGift> lockedGifts = GetLockedGiftCars();
        if (lockedGifts.Count == 0)
            return null;

        CarGift selectedGift = lockedGifts[Random.Range(0, lockedGifts.Count)];
        PlayerPrefs.SetInt(CurrentRewardCarIndex, selectedGift.Index);
        return selectedGift;
    }

    public float GetGiftCarProgress(CarGift gift)
    {
        if (gift == null)
            return 0f;

        CarGift currentGift = GetCurrentRewardCar();
        if (currentGift == null || currentGift.Index != gift.Index)
            return 0f;

        return Mathf.Clamp01(GetProcentageUnblockingCar());
    }

    public bool AddProgressToCurrentRewardCar(float amount, out float displayedProgress)
    {
        CarGift currentGift = GetCurrentRewardCar();
        if (currentGift == null)
        {
            displayedProgress = GetProcentageUnblockingCar();
            return false;
        }

        displayedProgress = Mathf.Clamp01(GetProcentageUnblockingCar() + Mathf.Max(0f, amount));
        PlayerPrefs.SetFloat(ProcentageUnblockingCar, displayedProgress);

        if (displayedProgress < 1f)
            return false;

        SaveCarProduct(currentGift);
        PlayerPrefs.SetInt(CurrentRewardCarIndex, -1);
        PlayerPrefs.SetFloat(ProcentageUnblockingCar, 0f);
        displayedProgress = 1f;
        return true;
    }

    public void DeletSelectCarProduct(CarProduct carProduct)
    {
        _selectCarCount--;
        PlayerPrefs.DeleteKey($"{SelectCar} {carProduct.Index}");
        PlayerPrefs.SetInt(SelectCarCount, _selectCarCount);
    }

    public bool IsLastSelectCarProduct()
    {
        return _selectCarCount > 1;
    }

    public bool IsCarBought(CarProduct carProduct)
    {
        return PlayerPrefs.GetInt($"{OpenCar} {carProduct.Index}", 0) == NumberPurchasedCar;
    }

    public bool IsCarSelected(CarProduct carProduct)
    {
        return PlayerPrefs.GetInt($"{SelectCar} {carProduct.Index}", 0) == NumberPurchasedCar;
    }

    private void RegisterGiftProductsFromContainers()
    {
        if (carContainers == null)
            return;

        foreach (CarContainer container in carContainers)
        {
            if (container != null && container.CarProduct != null)
                RegisterGiftProduct(container.CarProduct);
        }
    }

    private void RegisterGiftProduct(CarProduct product)
    {
        if (product is not CarGift gift)
            return;

        bool alreadyRegistered = _carGifts.Exists(registered => registered.Index == gift.Index);
        if (alreadyRegistered == false)
            _carGifts.Add(gift);
    }

    private CarGift FindGift(int index)
    {
        return _carGifts.Find(gift => gift != null && gift.Index == index);
    }

    private List<CarGift> GetLockedGiftCars()
    {
        var lockedGifts = new List<CarGift>();

        foreach (CarGift gift in _carGifts)
        {
            if (IsRewardCar(gift) && IsCarBought(gift) == false)
                lockedGifts.Add(gift);
        }

        return lockedGifts;
    }

    private bool IsRewardCar(CarGift gift)
    {
        return gift != null &&
               (DefaultCarProduct == null || gift.Index != DefaultCarProduct.Index);
    }
}
