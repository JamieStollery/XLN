namespace XLN.Strategies
{
    public class RobotMoveNorthStrategy : IRobotMoveStrategy
    {
        public Direction GetLeftTurnDirection => Direction.W;

        public Direction GetRightTurnDirection => Direction.E;

        public (int x, int y) GetPointAfterMove(int x, int y) => (x, y + 1);
    }
}
