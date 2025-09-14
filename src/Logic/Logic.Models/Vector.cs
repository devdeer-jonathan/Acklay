namespace Logic.Models
{
    public readonly struct Vector(double vectorX, double vectorY, double vectorZ)
    {
        #region properties

        public double X => vectorX;

        public double Y => vectorY;

        public double Z => vectorZ;

        #endregion
    }
}