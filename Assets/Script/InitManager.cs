using UnityEngine;

public interface IManager
{
    void Init();
}

public class InitManager : MonoBehaviour
{
    SystemLoader loader;
    IShopManager _shopManager;

    void OnEnable()
    {
        loader = SystemLoader.Instance;
        loader.Register<IShopManager>(GetComponent<ShopManager>());
        loader.Register<ICoinManager>(GetComponent<CoinManager>());
    }

    void Start()
    {
        loader.InitAll();
        _shopManager = loader.Get<IShopManager>();
    }
}
