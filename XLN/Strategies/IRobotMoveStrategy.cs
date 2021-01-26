namespace XLN.Strategies
{
    public interface IRobotMoveStrategy
    {
        public Direction GetLeftTurnDirection { get; }
        public Direction GetRightTurnDirection { get; }
        public (int x, int y) GetPointAfterMove(int x, int y);
    }
}
