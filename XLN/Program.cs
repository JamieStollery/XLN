using Autofac;
using System;
using System.Drawing;
using XLN.Exceptions;

namespace XLN
{
    class Program
    {
        private static ILifetimeScope Scope
        {
            get
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<IoC>();
                return builder.Build();
            }
        }

        static void Main(string[] args)
        {
            var presenter = Scope.Resolve<Presenter>();

            Console.WriteLine("Enter the size of the warehouse in the format: X Y");

            Size warehouseSize = Size.Empty;
            while (warehouseSize == Size.Empty)
            {
                var warehouseString = Console.ReadLine();
                try
                {
                    warehouseSize = presenter.GetWarehouseSize(warehouseString);
                }
                catch(Exception ex)
                {
                    if (!(ex is ArgumentNullException) && !(ex is InvalidSizeStringException)) throw ex;

                    Console.WriteLine(ex.Message);
                }
            }

            while (true)
            {
                Console.WriteLine("Enter the starting position for a robot in the format: X Y D\n(D can be N, E, S, W)");
                Robot robot = null;
                while (robot is null)
                {
                    var robotString = Console.ReadLine();
                    try
                    {
                        robot = presenter.CreateRobot(robotString, warehouseSize);
                    }
                    catch(Exception ex)
                    {
                        if (!(ex is ArgumentNullException) && !(ex is InvalidPositionStringException)) throw ex;

                        Console.WriteLine(ex.Message);
                    }
                }

                Console.WriteLine("Enter movements for the robot using only the following keys: < > ^");
                var movementStringIsValid = false;
                while (!movementStringIsValid)
                {
                    try
                    {
                        var movementString = Console.ReadLine();
                        presenter.MoveRobot(robot, movementString);
                        movementStringIsValid = true;

                        Console.WriteLine(robot.Position);
                    }
                    catch(Exception ex)
                    {
                        if (!(ex is ArgumentNullException) && !(ex is InvalidMovementStringException)) throw ex;

                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
