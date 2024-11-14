namespace CodeInterview
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var example = new Examples();

            await example.Example1();

            Console.ReadLine();
        }
    }
}