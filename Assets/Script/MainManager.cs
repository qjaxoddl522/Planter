using UnityEngine;

public interface IManager
{
    void Init();
}

public class MainManager : MonoBehaviour
{
    SystemLoader loader;
    IShopManager shopManager;

    void Start()
    {
        loader = SystemLoader.Instance;
        loader.Register<IShopManager>(GetComponent<ShopManager>());
        shopManager = loader.Get<IShopManager>();

        shopManager.Init();
        shopManager.CreateShop();
    }
}
