using UnityEngine;

public class Daisy : PlantBase
{
    [SerializeField] GameObject coinClicker;

    protected override void Ability()
    {
        var coinInst = Instantiate(coinClicker, transform.position, Quaternion.identity);
        coinInst.transform.parent = transform.parent;
        ICoinClicker coin = coinInst.GetComponent<ICoinClicker>();
        coin.coinPresenter = coinPresenter;
        coin.SetSpawnBehavior(new JumpSpawn());
    }
}
