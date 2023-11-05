using UnityEngine;

public static class GameMain
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Main()
    {
        Application.targetFrameRate = 120;
    }
}
