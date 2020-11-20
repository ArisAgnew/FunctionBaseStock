using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FunctionBaseStock.Enhancements;
using FunctionBaseStock.Enhancements.HighEndAbstraction;

namespace FunctionBaseStock.Decorations
{
    public class Essenсe<Delegate> where Delegate : notnull
    {
        private static readonly Essenсe<Delegate> EmptyDelegate = new Essenсe<Delegate>(default);

        private readonly Delegate _delegate;

        [NotNull]
        private Type? _amplifiedType;

        private Essenсe([AllowNull] Delegate ddelegate) => _delegate = ddelegate
            ?? throw new ArgumentException($"delegate should be defined.");

        public static Essenсe<Delegate> Of(Delegate ddelegate) =>
            ddelegate != null ? new Essenсe<Delegate>(ddelegate) : EmptyDelegate;

        public Essenсe<Delegate> AdjacentTo([NotNull] Type amplifiedType)
        {
            _amplifiedType = amplifiedType ?? throw new ArgumentException($"Amplified type should be defined.");
            return this;
        }

        public dynamic? ToAssemble(dynamic? input = default)
        {
            Type delegetaType = _delegate.GetType();

            dynamic? amplifiedDelegate = default;
            dynamic? result = default;

            try
            {
                IEnumerable<Type> amplifiedTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => _amplifiedType.Name.Equals(p.Name));

                foreach (Type aType in amplifiedTypes)
                {
                    amplifiedDelegate = Activator.CreateInstance(aType, _delegate);
                }

                result = _delegate.GetType().Name switch
                {
                    string fullFunc when fullFunc == typeof(Func<,>).Name => amplifiedDelegate?.Apply(input),

                    string action when action == typeof(Action<>).Name => new Func<dynamic>(() =>
                    {
                        amplifiedDelegate?.Accept(input);
                        return default;
                    })(),

                    string runnable when runnable == typeof(Action).Name => new Func<dynamic>(() =>
                    {
                        amplifiedDelegate?.Run();
                        return default;
                    })(),

                    string supplier when supplier == typeof(Func<>).Name => amplifiedDelegate?.Acquire(),

                    _ => throw new ArgumentException("Invalid delegate type")
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n");
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        //Compiled expression is best way! (for performance to repeatedly create instance in runtime)
        //Func<> YCreator = Expression.Lambda<Func<X>>(Expression.New(typeof(Y).GetConstructor(Type.EmptyTypes))).Compile();

        // Get the instance of the desired constructor (here it takes a string as a parameter).
        /*ConstructorInfo c = typeof(T).GetConstructor(new[] { typeof(string) });
        // Don't forget to check if such constructor exists
        if (c == null)
            throw new InvalidOperationException(string.Format("A constructor for type '{0}' was not found.", typeof(T)));
        T instance = (T)c.Invoke(new object[] { "test" });*/
    }
}
