namespace CodeInterview
{
    internal class Examples
    {
        public Task Example1()//რა დაიბეჭდება ამდროს?
        {
            try
            {
                Console.WriteLine("A");
                DoSomething();
            }
            catch (Exception)
            {
                Console.WriteLine("D");
            }

            async Task DoSomething()
            {
                Console.WriteLine("B");
                await Task.Delay(500);
                throw new Exception("Test");
                Console.WriteLine("C");
            }

            return Task.CompletedTask;
        }

        public Task Example2()//რა დაიბეჭდება ამდროს?
        {
            try
            {
                Console.WriteLine("A");
                DoSomething();
            }
            catch (Exception)
            {
                Console.WriteLine("D");
            }

            async Task DoSomething()
            {
                Console.WriteLine("B");
                await Task.Delay(500);
                Console.WriteLine("C");
            }

            return Task.CompletedTask;
        }
    }
}