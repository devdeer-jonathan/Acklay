namespace Logic.Simulation
{
    using System.Collections.ObjectModel;

    using Interfaces;

    using Math;

    using Models;

    public class ThreeBodySimulator : IThreeBodySimulator
    {
        public void SimulatePositions()
        {
            // Set starting position
            const double TimeDelta = 0.01;
            var body1 = new Body()
            {
                Mass = 1,
                Position = new Vector(1, 2, 1),
                Velocity = new Vector(0,0,0)
            };
            var body2 = new Body()
            {
                Mass = 1,
                Position = new Vector(8, 10, 11),
                Velocity = new Vector(0,0,0)
            };
            var body3 = new Body()
            {
                Mass = 1,
                Position = new Vector(3, 4, 5),
                Velocity = new Vector(0, 0, 0)
            };
            var math = new ThreeBodyMath();
            var accelerationBody1 = math.GetAcceleration(body1, body2, body3);
            var accelerationBody2 = math.GetAcceleration(body2, body3, body1);
            var accelerationBody3 = math.GetAcceleration(body3, body1, body2);
            body1.Acceleration = accelerationBody1;
            body2.Acceleration = accelerationBody2;
            body3.Acceleration = accelerationBody3;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Body 1:");
                Console.WriteLine($"X: {body1.Position.X}");
                Console.WriteLine($"Y: {body1.Position.Y}");
                Console.WriteLine($"Z: {body1.Position.Z}");
                Console.WriteLine("Body 2:");
                Console.WriteLine($"X: {body2.Position.X}");
                Console.WriteLine($"Y: {body2.Position.Y}");
                Console.WriteLine($"Z: {body2.Position.Z}");
                Console.WriteLine("Body 3:");
                Console.WriteLine($"X: {body3.Position.X}");
                Console.WriteLine($"Y: {body3.Position.Y}");
                Console.WriteLine($"Z: {body3.Position.Z}");
                // Calculate next position
                body1.Position = math.GetPositionInT(body1, TimeDelta);
                body2.Position = math.GetPositionInT(body2, TimeDelta);
                body3.Position = math.GetPositionInT(body3, TimeDelta);
                // Store current acceleration
                var currentAccelerationBody1 = body1.Acceleration;
                var currentAccelerationBody2 = body2.Acceleration;
                var currentAccelerationBody3 = body3.Acceleration;
                // Calculate next acceleration
                body1.Acceleration = math.GetAcceleration(body1, body2, body3);
                body2.Acceleration = math.GetAcceleration(body2, body3, body1);
                body3.Acceleration = math.GetAcceleration(body3, body1, body2);
                // Calculate next velocity
                body1.Velocity = math.GetVelocityInT(body1, currentAccelerationBody1, TimeDelta);
                body2.Velocity = math.GetVelocityInT(body2, currentAccelerationBody2, TimeDelta);
                body3.Velocity = math.GetVelocityInT(body3, currentAccelerationBody3, TimeDelta);
                Thread.Sleep(100);
            }
        }

        /// <inheritdoc />
        public BodiesPositions SimulateNextPosition()
        {
            const double TimeDelta = 100;
            var math = new ThreeBodyMath();
            var body1 = BodiesState.Bodies[1];
            var body2 = BodiesState.Bodies[2];
            var body3 = BodiesState.Bodies[3];
            // Calculate next position
            body1.Position = math.GetPositionInT(body1, TimeDelta);
            body2.Position = math.GetPositionInT(body2, TimeDelta);
            body3.Position = math.GetPositionInT(body3, TimeDelta);
            // Store current acceleration
            var currentAccelerationBody1 = body1.Acceleration;
            var currentAccelerationBody2 = body2.Acceleration;
            var currentAccelerationBody3 = body3.Acceleration;
            // Calculate next acceleration
            body1.Acceleration = math.GetAcceleration(body1, body2, body3);
            body2.Acceleration = math.GetAcceleration(body2, body3, body1);
            body3.Acceleration = math.GetAcceleration(body3, body1, body2);
            // Calculate next velocity
            body1.Velocity = math.GetVelocityInT(body1, currentAccelerationBody1, TimeDelta);
            body2.Velocity = math.GetVelocityInT(body2, currentAccelerationBody2, TimeDelta);
            body3.Velocity = math.GetVelocityInT(body3, currentAccelerationBody3, TimeDelta);
            BodiesState.Bodies[1] = body1;
            BodiesState.Bodies[2] = body2;
            BodiesState.Bodies[3] = body3;
            return new BodiesPositions()
            {
                Positions = new Dictionary<int, Vector>()
                {
                    { 1, body1.Position },
                    { 2, body2.Position },
                    { 3, body3.Position }
                }
            };

        }

        private static BodiesState GetInitialBodiesState()
        {
            var body1 = new Body()
            {
                Mass = 1,
                Position = new Vector(1, 2, 1),
                Velocity = new Vector(0, 0, 0)
            };
            var body2 = new Body()
            {
                Mass = 1,
                Position = new Vector(8, 10, 11),
                Velocity = new Vector(0, 0, 0)
            };
            var body3 = new Body()
            {
                Mass = 1,
                Position = new Vector(3, 4, 5),
                Velocity = new Vector(0, 0, 0)
            };
            var math = new ThreeBodyMath();
            var accelerationBody1 = math.GetAcceleration(body1, body2, body3);
            var accelerationBody2 = math.GetAcceleration(body2, body3, body1);
            var accelerationBody3 = math.GetAcceleration(body3, body1, body2);
            body1.Acceleration = accelerationBody1;
            body2.Acceleration = accelerationBody2;
            body3.Acceleration = accelerationBody3;
            var state = new BodiesState()
            {
                Bodies = new Dictionary<int, Body>()
                {
                    { 1, body1 },
                    { 2, body2 },
                    { 3, body3 }
                }
            };
            return state;
        }

        private static BodiesState BodiesState { get; set; } = GetInitialBodiesState();
    }
}
