using UnityEngine;

public class Daisy : PlantBase
{
    [Header("Daisy")]
    [SerializeField] GameObject coinClicker;

    protected override void Update()
    {
        if (TimePresenter.isDaytime)
        {
            if (Cooltime > 0)
                Cooltime -= Time.deltaTime;
            else
                Ability();
        }
    }

    protected override void Ability()
    {
        var coinInst = Instantiate(coinClicker, transform.position, Quaternion.identity);
        coinInst.transform.parent = transform.parent;
        ICoinClicker coin = coinInst.GetComponent<ICoinClicker>();
        coin.CoinAmount = Power;
        coin.CoinPresenter = coinPresenter;
        coin.SetSpawnBehavior(new JumpSpawn());

        InitCooltime();
    }
}
