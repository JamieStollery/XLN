using System;
using System.Drawing;
using XLN;
using XLN.Exceptions;
using Xunit;

namespace Test.XLN
{
    public class PresenterTests
    {
        private readonly Presenter _presenter;

        public PresenterTests()
        {
            _presenter = new Presenter((x, y, direction, size) => null);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentNullException))]
        [InlineData(" ", typeof(ArgumentNullException))]
        [InlineData("x", typeof(InvalidSizeStringException))]
        [InlineData("1", typeof(InvalidSizeStringException))]
        [InlineData("1 x", typeof(InvalidSizeStringException))]
        [InlineData("1 1 1", typeof(InvalidSizeStringException))]
        public void WhenCreateWarehouseIsCalled_AndWarehouseSizeStringIsNotValid_ThenAnExceptionIsThrown(string warehouseSizeString, Type exceptionType)
        {
            Assert.Throws(exceptionType, () => _presenter.GetWarehouseSize(warehouseSizeString));
        }

        [Theory]
        [InlineData("1 1")]
        [InlineData("10 10")]
        public void WhenCreateWarehouseIsCalled_AndWarehouseStringIsValid_ThenAnExceptionIsNotThrown(string warehouseString)
        {
            _ = _presenter.GetWarehouseSize(warehouseString);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentNullException))]
        [InlineData(" ", typeof(ArgumentNullException))]
        [InlineData("x", typeof(InvalidPositionStringException))]
        [InlineData("1", typeof(InvalidPositionStringException))]
        [InlineData("1 x", typeof(InvalidPositionStringException))]
        [InlineData("1 1 x", typeof(InvalidPositionStringException))]
        [InlineData("1 1 N x", typeof(InvalidPositionStringException))]
        public void WhenCreateRobotIsCalled_AndRobotPositionStringIsNotValid_ThenAnExceptionIsThrown(string robotPositionString, Type exceptionType)
        {
            Assert.Throws(exceptionType, () => _presenter.CreateRobot(robotPositionString, new Size(1, 1)));
        }

        [Fact]
        public void WhenCreateRobotIsCalled_AndWarehouseSizeIsEmpty_ThenArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => _presenter.CreateRobot("1 1 N", Size.Empty));
        }

        [Theory]
        [InlineData("1 1 N")]
        [InlineData("1 1 E")]
        [InlineData("1 1 S")]
        [InlineData("1 1 W")]
        [InlineData("10 10 N")]
        public void WhenCreateRobotIsCalled_AndRobotStringIsValid_AndWarehouseSizeIsNotEmpty_ThenAnExceptionIsNotThrown(string robotString)
        {
            _ = _presenter.CreateRobot(robotString, new Size(10, 10));
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentNullException))]
        [InlineData(" ", typeof(ArgumentNullException))]
        [InlineData("x", typeof(InvalidMovementStringException))]
        [InlineData("^x", typeof(InvalidMovementStringException))]
        public void WhenMoveRobotIsCalled_AndRobotMovementStringIsNotValid_ThenAnExceptionIsThrown(string robotMovementString, Type exceptionType)
        {
            var robot = new Robot(0, 0, Direction.N, new Size(1, 1), (direction) => null);
            Assert.Throws(exceptionType, () => _presenter.MoveRobot(robot, robotMovementString));
        }

        [Fact]
        public void WhenMoveRobotIsCalled_AndRobotIsNull_ThenArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => _presenter.MoveRobot(null, ">^^"));
        }

    }
}
