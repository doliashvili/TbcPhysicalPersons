namespace CodeInterview
{
    internal class Examples
    {
        public Task Example1()//რა დაიბეჭდება ამდროს?
        {
            try
            {
                Console.WriteLine("A");
                DoSomething(); //აქ ექსეფშენს ვერ ვიჭერთ ვინაიდან აქ ბრუნდება ტასკი შესაბმისად რათქმაუნდა catch-ბლოკი
                               //არ აღიძვრება (პასუხი A B Finish)
            }
            catch (Exception)
            {
                Console.WriteLine("D");
            }

            Console.WriteLine("finish");

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
                DoSomething(); //ამ შემთხვევაში შესრულდება delay-მდე კოდი Console.WriteLine("B");
                               // რათქმაუნდა აქ არგვიწერია await შესაბამისად წავა და შეასრულებს შემდგომ ოპერაციას Console.WriteLine("Finish");
                               //პასუხი(A B Finish C)
            }
            catch (Exception)
            {
                Console.WriteLine("D");
            }

            Console.WriteLine("Finish");

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