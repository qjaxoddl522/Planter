using UnityEngine;

public class Daisy : PlantBase
{
    [Header("Daisy")]
    [SerializeField] GameObject coinClicker;

    void Update()
    {
        if (TimePresenter.isDaytime)
        {
            if (Cooltime > 0)
                Cooltime -= Time.deltaTime;
            else
            {
                Ability();
                Cooltime = MaxCooltime;
            }
        }
    }

    protected override void Ability()
    {
        var coinInst = Instantiate(coinClicker, transform.position, Quaternion.identity);
        coinInst.transform.parent = transform.parent;
        ICoinClicker coin = coinInst.GetComponent<ICoinClicker>();
        coin.coinPresenter = coinPresenter;
        coin.SetSpawnBehavior(new JumpSpawn());
    }
}
