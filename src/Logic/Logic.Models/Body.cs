namespace Logic.Models
{
    using System.Numerics;

    public class Body
    {
        #region properties

        public double Mass { get; set; }

        public Vector Position { get; set; }

        public Vector Acceleration { get; set; }

        public Vector Velocity { get; set; }

        #endregion
    }
}