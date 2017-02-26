namespace gitWeb.Tests
{
    public abstract class TestFixtureBase
    {
        /// <summary>
        /// Represents any object for mock parameters witch are not test in current fact or theory
        /// </summary>
        public T Any<T>()
        {
            return default(T);
        }
    }
}