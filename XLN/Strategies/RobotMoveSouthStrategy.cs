namespace XLN.Strategies
{
    public class RobotMoveSouthStrategy : IRobotMoveStrategy
    {
        public Direction GetLeftTurnDirection => Direction.E;

        public Direction GetRightTurnDirection => Direction.W;

        public (int x, int y) GetPointAfterMove(int x, int y) => (x, y - 1);
    }
}
