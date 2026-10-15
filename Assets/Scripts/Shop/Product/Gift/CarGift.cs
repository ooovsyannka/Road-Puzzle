using UnityEngine;

[CreateAssetMenu(fileName = "NewGiftCar", menuName = "Goods/CarGift", order = 1)]

public class CarGift : CarProduct
{
    [SerializeField] private Sprite _carGiftImage;

    public Sprite CarGiftImage => _carGiftImage;
}
