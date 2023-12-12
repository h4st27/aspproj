namespace MyApp.services.CalcService
{
    public class CalcService : ICalcService
    {
        public float Sum(float a, float b) => a + b;

        public float Subtract(float a, float b) => a - b;

        public float Multiply(float a, float b) => a * b;

        public float Divide(float a, float b)
        {
            if (b == 0)
                throw new DivideByZeroException("Division by zero is Forbidden.");

            return (float)a / b;
        }
    }

}
