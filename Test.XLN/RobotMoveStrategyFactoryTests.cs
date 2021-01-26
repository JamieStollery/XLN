using Autofac;
using System;
using XLN;
using XLN.Strategies;
using Xunit;

namespace Test.XLN
{
    public class RobotMoveStrategyFactoryTests : IDisposable
    {
        private readonly ILifetimeScope _scope;
        private readonly Func<Direction, IRobotMoveStrategy> _robotMoveStrategyFactory;
        public RobotMoveStrategyFactoryTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<IoC>();
            var container = builder.Build();
            _scope = container.BeginLifetimeScope();
            _robotMoveStrategyFactory = _scope.Resolve<Func<Direction, IRobotMoveStrategy>>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        [Theory]
        [InlineData(Direction.N, typeof(RobotMoveNorthStrategy))]
        [InlineData(Direction.E, typeof(RobotMoveEastStrategy))]
        [InlineData(Direction.S, typeof(RobotMoveSouthStrategy))]
        [InlineData(Direction.W, typeof(RobotMoveWestStrategy))]
        public void WhenRobotMoveStrategyFactoryIsCalled_ThenTheExpectedStrategyIsReturned(Direction direction, Type strategyType)
        {
            var strategy = _robotMoveStrategyFactory(direction);
            Assert.IsType(strategyType, strategy);
        }
    }
}
