namespace UnityDependency.Test
{
    /// <summary>
    /// Class which encapsulates integration tests which take place in a single frame.
    /// </summary>
    public abstract class TestFrame : TestStub
    {
        /// <summary>
        /// Override to define what to do when the test is executed.
        /// This will be called once every frame
        /// until the test is complete.
        /// </summary>
        protected abstract void Execute();

        void Start()
        {
            this.SetUp();
        }

        void Update()
        {
            try
            {
                this.Execute();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.TearDown();
            }
        }
    }
}
