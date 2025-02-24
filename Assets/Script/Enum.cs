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
    DefRanged,
    DefHowitzer,
    DefMelee,
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
    Bee,
}

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    HideDown,
    HideUp,
}

public enum XPos
{
    Left,
    Right,
    Both,
}

public enum Layer
{
    Plant = 6,
    Enemy = 7,
}