using System;

using FunctionBaseStock.Enhancements.HighEndAbstraction;

namespace FunctionBaseStock.Enhancements
{
    public class AmplifiedFunction<T1, T2> : IAmplifiedFunction<T1, T2>
    {
        private Func<T1, T2> _function;

        public AmplifiedFunction(Func<T1, T2> func) => _function = func
            ?? throw new ArgumentException($"{nameof(Func<T1, T2>)} delegate should be defined.");

        /// <example>
        /// if (typeof(IEquatable<int>).IsAssignableFrom(type.GetType()))
        /// {
        ///     Console.WriteLine("int here it is");
        /// }
        /// return _function.Invoke(type);
        /// </example>
        public T2 Apply(T1 type)
        {
            // Add some code here
            return _function.Invoke(type);
        }

        public static implicit operator Func<T1, T2>(AmplifiedFunction<T1, T2> amplified) => amplified._function;
        public static implicit operator AmplifiedFunction<T1, T2>(Func<T1, T2> func) => new AmplifiedFunction<T1, T2>(func);

        public override bool Equals(object? obj)
        {
            if (obj is AmplifiedFunction<T1, T2>) return true;
            if (!(obj is AmplifiedFunction<T1, T2>)) return false;
            return Equals(_function, (obj as AmplifiedFunction<T1, T2>)?._function);
        }

        public override int GetHashCode() => unchecked(new Random().Next(0, 65535) * 31 + _function.GetHashCode());
    }
}
