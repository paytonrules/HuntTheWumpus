namespace HuntTheWumpusDotNet
{
    public class GamePresenter
    {
        private Game game;
        public GamePresenter(Game game)
        {
            this.game = game;
        }

        public void AddPath(int start, int end, char direction)
        {
            game.AddPath(start, end, ConvertCharToDirection(direction));
        }

        public void Move(char direction)
        {
            game.Move(ConvertCharToDirection(direction));
        }

        private static Direction ConvertCharToDirection(char direction)
        {
            Direction directionEnum;
            switch (direction)
            {
                case 'E':
                    directionEnum = Direction.East;
                    break;
                case 'W':
                    directionEnum = Direction.West;
                    break;
                case 'N':
                    directionEnum = Direction.North;
                    break;
                case 'S':
                    directionEnum = Direction.South;
                    break;
                default:
                    directionEnum = Direction.West;
                    break;
            }
            return directionEnum;
        }
    }
}