using UnityEngine;

public interface IManager
{
    void Init();
}

public class InitManager : MonoBehaviour
{
    SystemLoader loader;

    void OnEnable()
    {
        loader = SystemLoader.Instance;
        loader.Register<IShopManager>(GetComponent<ShopManager>());
    }

    void Start()
    {
        loader.InitAll();
    }
}
