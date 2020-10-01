using System;
using System.Collections.Immutable;

using FunctionBaseStock.Decorations;
using FunctionBaseStock.Enhancements;

namespace FunctionBaseStock
{
    public enum Question : long
    {
        Role = 2,
        ProjectFunding = 3,
        NumberOfServers = 5,
        TopBusinessConcern = 6
    }

    class Program
    {
        static void Main(string[] args)
        {
            var supplier = Essenсe<Func<Random>>.Of(() =>
            {
                var random = new Random();
                random.NextBytes(new byte[5]);
                return random;
            }).AdjacentTo(typeof(AmplifiedSupplier<Random>)).ToAssemble();

            Console.WriteLine("Three random integer values:");
            for (int ctr = 0; ctr <= 2; ++ctr)
                Console.Write("{0,1:N3}", supplier?.Next());
            Console.WriteLine();

            var funcRes = Essenсe<Func<int, int>>.Of(a => a + 3)
                .AdjacentTo(typeof(AmplifiedFunction<int, int>))
                .ToAssemble(20);

            Console.WriteLine($"Func Result: {funcRes}");

            Action<(int value, int shift, bool isToLeft)> action = tuple =>
            {
                int result = default;

                if (tuple.isToLeft)
                    result = tuple.value << tuple.shift;
                else
                    result = tuple.value >> tuple.shift;

                Console.WriteLine($"Binary number: 0x{Convert.ToString(result, toBase: 2)};\t" +
                    $"Decimal: {Convert.ToString(result, toBase: 10)}");
            };

            Essenсe<Action<(int value, int shift, bool isToLeft)>>.Of(action)
                .AdjacentTo(typeof(AmplifiedAction<(int, int, bool)>))
                .ToAssemble((119, 2, false));

            Essenсe<Action<Question>>.Of(e => Enum.GetNames(e.GetType()).ToImmutableList()
                    .ForEach(v => Console.WriteLine("{0}\t => \t{1:D}", v, Enum.Parse(e.GetType(), v))))
                .AdjacentTo(typeof(AmplifiedAction<Question>))
                .ToAssemble(default(Question));

            Essenсe<Action>.Of(() => Console.WriteLine("Just as simple as it is"))
                .AdjacentTo(typeof(AmplifiedRunnable))
                .ToAssemble(default);

            Console.WriteLine("======");

            Console.WriteLine(Math.Floor(7 * Math.Pow(2, 5)));

            Console.WriteLine(Math.Floor(119 * 1 / Math.Pow(2, 2)));
        }
    }
}
