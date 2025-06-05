namespace SeaBattle.Script
{
    // Результати пострілу
    public enum ShotResult
    {
        Miss = -1,    // Промах
        Cancel = 0,   // Скасування (вже стріляли в цю точку)
        Hit = 1       // Влучання
    }
}
