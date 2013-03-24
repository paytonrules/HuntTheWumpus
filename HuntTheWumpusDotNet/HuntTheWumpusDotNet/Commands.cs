namespace HuntTheWumpusDotNet
{
    public class Command
    {
        public Commands Order { get; set; }
        public Directions Direction { get; set; }

        public enum Commands
        {
            Invalid,
            Go,
            Rest,
            Shoot
        }
        public enum Directions
        {
            Invalid,
            East,
            West,
            North,
            South
        }

        public static bool IsRest(Commands command)
        {
            return command == Commands.Rest;
        }

        public static bool IsMove(Commands command)
        {
            return command == Commands.Go;
        }
    };
}