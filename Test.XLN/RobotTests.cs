using Moq;
using System.Drawing;
using XLN;
using XLN.Strategies;
using Xunit;

namespace Test.XLN
{
    public class RobotTests
    {
        private readonly Mock<IRobotMoveStrategy> _robotMoveStrategyMock;
        private readonly Robot _robot;

        public RobotTests()
        {
            _robotMoveStrategyMock = new Mock<IRobotMoveStrategy>();
            _robotMoveStrategyMock.SetupAllProperties();
            _robot = new Robot(0, 0, Direction.N, new Size(0, 0), (direction) => _robotMoveStrategyMock.Object);
        }

        [Fact]
        public void WhenTurnLeftIsCalled_ThenGetLeftTurnDirectionIsCalled()
        {
            _robot.TurnLeft();
            _robotMoveStrategyMock.Verify(strategy => strategy.GetLeftTurnDirection, Times.Once);
        }

        [Fact]
        public void WhenTurnRightIsCalled_ThenGetRightTurnDirectionIsCalled()
        {
            _robot.TurnRight();
            _robotMoveStrategyMock.Verify(strategy => strategy.GetRightTurnDirection, Times.Once);
        }

        [Fact]
        public void WhenMoveIsCalled_ThenGetPointAfterMoveIsCalled()
        {
            _robot.Move();
            _robotMoveStrategyMock.Verify(strategy => strategy.GetPointAfterMove(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}
