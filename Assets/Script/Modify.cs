using UnityEngine;

public static class Modify
{
    public static int GetDepth(float y)
    {
        return -(int)(y + 0.5f) * 2;
    }
}
