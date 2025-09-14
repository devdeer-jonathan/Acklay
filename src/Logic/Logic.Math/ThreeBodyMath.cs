namespace Logic.Math
{
    using Models;

    using Math = System.Math;
    using Vector = Models.Vector;

    public class ThreeBodyMath
    {
        #region methods

        public Vector GetAcceleration(Body firstBody, Body secondBody, Body thirdBody)
        {
            var firstSummandVectorTerm = DivideVectorByScalar(
                SubtractVectorFromVector(firstBody.Position, secondBody.Position),
                Math.Pow(GetEuclidianNorm(SubtractVectorFromVector(firstBody.Position, secondBody.Position)), 3));
            var firstSummandScalarTerm = -PhysicsConstants.GravitationalConstant * secondBody.Mass;
            var firstSummand = MultiplyVectorByScalar(firstSummandVectorTerm, firstSummandScalarTerm);
            var secondSummandVectorTerm = DivideVectorByScalar(
                SubtractVectorFromVector(firstBody.Position, thirdBody.Position),
                Math.Pow(GetEuclidianNorm(SubtractVectorFromVector(firstBody.Position, thirdBody.Position)), 3));
            var secondSummandScalarTerm = -PhysicsConstants.GravitationalConstant * thirdBody.Mass;
            var secondSummand = MultiplyVectorByScalar(secondSummandVectorTerm, secondSummandScalarTerm);
            var result = AddVectorToVector(firstSummand, secondSummand);
            return result;
        }

        public Vector GetPositionInT(Body body, double timedelta)
        {
            var x = body.Position;
            var y = MultiplyVectorByScalar(body.Velocity, timedelta);
            var z = MultiplyVectorByScalar(body.Acceleration, Math.Pow(timedelta, 2)/2);
            return AddVectorToVector(AddVectorToVector(x, y), z);
        }

        public Vector GetVelocityInT(Body body, Vector previousAcceleration, double timedelta)
        {
            var v = body.Velocity;
            var w = AddVectorToVector(body.Acceleration, previousAcceleration);
            var x = MultiplyVectorByScalar(w, 0.5 * timedelta);
            return AddVectorToVector(v, x);
        }

        private static Vector AddVectorToVector(Vector firstVector, Vector secondVector)
        {
            return new Vector(
                firstVector.X + secondVector.X,
                firstVector.Y + secondVector.Y,
                firstVector.Z + secondVector.Z);
        }

        private static Vector DivideVectorByScalar(Vector vector, double scalar)
        {
            return new Vector(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
        }

        private static double GetEuclidianNorm(Vector vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
        }

        private static Vector MultiplyVectorByScalar(Vector vector, double scalar)
        {
            return new Vector(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        private static Vector SubtractVectorFromVector(Vector firstVector, Vector secondVector)
        {
            return new Vector(
                firstVector.X - secondVector.X,
                firstVector.Y - secondVector.Y,
                firstVector.Z - secondVector.Z);
        }

        #endregion
    }
}