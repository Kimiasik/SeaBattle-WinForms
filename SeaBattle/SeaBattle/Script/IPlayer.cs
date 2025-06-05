namespace SeaBattle.Script
{
    // Описує гравця в грі
    public interface IPlayer
    {
        // Ім'я гравця
        string Name { get; }

        // Ігрове поле гравця
        IField Field { get; }
    }
}
