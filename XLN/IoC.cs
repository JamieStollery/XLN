using Autofac;
using System;
using System.Drawing;
using XLN.Strategies;

namespace XLN
{
    public class IoC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Presenter>().InstancePerLifetimeScope();
            builder.RegisterType<Robot>();

            builder.RegisterType<RobotMoveNorthStrategy>().As<IRobotMoveStrategy>().Keyed<IRobotMoveStrategy>(Direction.N).InstancePerLifetimeScope();
            builder.RegisterType<RobotMoveEastStrategy>().As<IRobotMoveStrategy>().Keyed<IRobotMoveStrategy>(Direction.E).InstancePerLifetimeScope();
            builder.RegisterType<RobotMoveSouthStrategy>().As<IRobotMoveStrategy>().Keyed<IRobotMoveStrategy>(Direction.S).InstancePerLifetimeScope();
            builder.RegisterType<RobotMoveWestStrategy>().As<IRobotMoveStrategy>().Keyed<IRobotMoveStrategy>(Direction.W).InstancePerLifetimeScope();
            builder.Register<Func<Direction, IRobotMoveStrategy>>(ctx =>
            {
                var context = ctx.Resolve<IComponentContext>();
                return direction => context.ResolveKeyed<IRobotMoveStrategy>(direction);
            });

            builder.Register<Func<int, int, Direction, Size, Robot>>(ctx =>
            {
                var context = ctx.Resolve<IComponentContext>();
                return (x, y, direction, warehouseSize) => context.Resolve<Robot>(
                    new NamedParameter("x", x), 
                    new NamedParameter("y", y), 
                    TypedParameter.From(direction), 
                    TypedParameter.From(warehouseSize));
            });
        }
    }
}
