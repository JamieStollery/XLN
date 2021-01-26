using System;
using System.Drawing;
using XLN.Strategies;

namespace XLN
{
    public class Robot
    {
        private int _x;
        private int _y;
        private Direction _direction;
        private readonly Size _warehouseSize;
        private readonly Func<Direction, IRobotMoveStrategy> _robotMoveStrategyFactory;
        
        public Robot(int x, int y, Direction direction, Size warehouseSize, Func<Direction, IRobotMoveStrategy> robotMoveStrategyFactory)
        {
            if (x > warehouseSize.Width || y > warehouseSize.Height) throw new NotImplementedException("Invalid Robot Position, position must be within the bounds of the warehouse");
            _x = x;
            _y = y;
            _direction = direction;
            _warehouseSize = warehouseSize;
            _robotMoveStrategyFactory = robotMoveStrategyFactory;
        }

        public string Position => $"{_x} {_y} {_direction}";

        private IRobotMoveStrategy RobotMoveStrategy => _robotMoveStrategyFactory(_direction);

        public void TurnLeft()
        {
            _direction = RobotMoveStrategy.GetLeftTurnDirection;
        }

        public void TurnRight()
        {
            _direction = RobotMoveStrategy.GetRightTurnDirection;
        }

        public void Move()
        {
            var (x, y) = RobotMoveStrategy.GetPointAfterMove(_x, _y);
            if (x <= _warehouseSize.Width && x >= 0 && y <= _warehouseSize.Height && y >= 0)
            {
                _x = x;
                _y = y;
            }
        }
    }
}
