namespace SeaBattle.Script
{
    // Реалізація інтерфейсу IPlayer
    public class Player : IPlayer
    {
        // Ім'я гравця
        public string Name { get; }

        // Поле гравця (конкретний тип Field)
        public Field Field { get; }

        public Player(string name, Field field)
        {
            Field = field;
            Name = name;
        }

        // Реалізація властивості інтерфейсу IPlayer
        IField IPlayer.Field => Field;

        public override string ToString() => $"Player {Name}";
    }
}
