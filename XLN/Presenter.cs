using System;
using System.Drawing;
using System.Linq;
using XLN.Exceptions;

namespace XLN
{
    public class Presenter
    {
        private readonly Func<int, int, Direction, Size, Robot> _robotFactory;

        public Presenter(Func<int, int, Direction, Size, Robot> robotFactory)
        {
            _robotFactory = robotFactory;
        }

        public Size GetWarehouseSize(string warehouseSizeString)
        {
            if (string.IsNullOrWhiteSpace(warehouseSizeString)) throw new ArgumentNullException(nameof(warehouseSizeString));

            var warehouseSizes = warehouseSizeString.Split();
            if (warehouseSizes.Length == 2 && int.TryParse(warehouseSizes[0], out var x) && int.TryParse(warehouseSizes[1], out var y))
            {
                return new Size(x, y);
            }
            throw new InvalidSizeStringException(warehouseSizeString);
        }

        public Robot CreateRobot(string robotPositionString, Size warehouseSize)
        {
            if (string.IsNullOrWhiteSpace(robotPositionString)) throw new ArgumentNullException(nameof(robotPositionString));
            if (warehouseSize == Size.Empty) throw new ArgumentNullException(nameof(warehouseSize));

            var robotPositions = robotPositionString.Split();
            if (robotPositions.Length == 3 && int.TryParse(robotPositions[0], out var x) && 
                int.TryParse(robotPositions[1], out var y) && Enum.TryParse<Direction>(robotPositions[2], out var direction))
            {
                return _robotFactory(x, y, direction, warehouseSize);
            }
            throw new InvalidPositionStringException(robotPositionString);
        }

        public void MoveRobot(Robot robot, string robotMovementString)
        {
            if (string.IsNullOrWhiteSpace(robotMovementString)) throw new ArgumentNullException(nameof(robotMovementString));
            if (robot is null) throw new ArgumentNullException(nameof(robot));

            var robotMovements = robotMovementString.ToCharArray();
            var validMoveChars = new char[] { '<', '>', '^' };
            if (robotMovements.Any(move => !validMoveChars.Contains(move))) throw new InvalidMovementStringException(robotMovementString);

            foreach (var move in robotMovements)
            {
                switch (move)
                {
                    case '^':
                        robot.Move();
                        break;
                    case '<':
                        robot.TurnLeft();
                        break;
                    case '>':
                        robot.TurnRight();
                        break;
                }
            }
        }
    }
}
