/*public enum Seed
{
    // 성장 관련
    coin = 100,
    coinInc,
    coinBoom,
    
    // 방어 관련
    defense = 200,
    defMelee,
    defRanged,
    defHowitzer,
    defTank,

    // 버프 관련
    buff = 300,
    bufAttack,
    bufHealth,
}*/

public enum Seed
{
    // 성장 관련
    CoinInc,

    // 방어 관련
    DefMelee,
    DefRanged,
    DefHowitzer,
    DefTank,

    // 버프 관련
    BufAttack,
}

public enum Enemy
{
    Beetle,
    DungBeetle,
    HermitCrab,
    Earthworm,
}

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
}
