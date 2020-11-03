public static class ScenesParametrs
{
    public static int selectedLevelMap;
    public static int currentSceneLevel = 1;
    private static int maxSceneLevel = 1;
    public static void NextSceneLevel()
    {
        currentSceneLevel++;
        if (currentSceneLevel > maxSceneLevel)
        {
            maxSceneLevel++;
        }
    }
}