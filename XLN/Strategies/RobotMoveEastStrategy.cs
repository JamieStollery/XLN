namespace XLN.Strategies
{
    public class RobotMoveEastStrategy : IRobotMoveStrategy
    {
        public Direction GetLeftTurnDirection => Direction.N;

        public Direction GetRightTurnDirection => Direction.S;

        public (int x, int y) GetPointAfterMove(int x, int y) => (x + 1, y);
    }
}
