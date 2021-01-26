namespace XLN.Strategies
{
    public class RobotMoveWestStrategy : IRobotMoveStrategy
    {
        public Direction GetLeftTurnDirection => Direction.S;

        public Direction GetRightTurnDirection => Direction.N;

        public (int x, int y) GetPointAfterMove(int x, int y) => (x - 1, y);
    }
}
