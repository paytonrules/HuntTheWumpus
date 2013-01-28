namespace HuntTheWumpusDotNet
{
    public class Command
    {
        public enum AllCommands
        {
            East,
            West,
            North,
            South,
            Rest,
            Invalid
        }

        public static bool IsRest(AllCommands command)
        {
            return command == AllCommands.Rest;
        }

        public static bool IsMove(AllCommands command)
        {
            return command >= AllCommands.East && command <= AllCommands.South;
        }
    };
}