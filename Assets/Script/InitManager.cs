using UnityEngine;

public interface IManager
{
    void Init();
}

public class InitManager : MonoBehaviour
{
    SystemLoader loader;

    void Awake()
    {
        Application.targetFrameRate = 60;

        loader = SystemLoader.Instance;
        loader.Register<IShopManager>(GetComponent<ShopManager>());
    }

    void Start()
    {
        loader.InitAll();
    }
}
