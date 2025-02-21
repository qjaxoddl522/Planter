/*public enum Seed
{
    // ���� ����
    coin = 100,
    coinInc,
    coinBoom,
    
    // ��� ����
    defense = 200,
    defMelee,
    defRanged,
    defHowitzer,
    defTank,

    // ���� ����
    buff = 300,
    bufAttack,
    bufHealth,
}*/

public enum Seed
{
    // ���� ����
    CoinInc,

    // ��� ����
    DefRanged,
    DefHowitzer,
    DefMelee,
    DefTank,

    // ���� ����
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
    HideDown,
    HideUp,
}

public enum XPos
{
    Left,
    Right,
    Both,
}
