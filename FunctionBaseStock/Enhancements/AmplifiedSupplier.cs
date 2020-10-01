using System;

using FunctionBaseStock.Enhancements.HighEndAbstraction;

namespace FunctionBaseStock.Enhancements
{
    public class AmplifiedSupplier<Out> : IAmplifiedSupplier<Out>
    {
        private Func<Out> _supplier;

        public AmplifiedSupplier(Func<Out> supplier) => _supplier = supplier
            ?? throw new ArgumentException($"{nameof(Func<Out>)} (Supplier) delegate should be defined.");

        /// <example>
        /// if (typeof(IEquatable<int>).IsAssignableFrom(type.GetType()))
        /// {
        ///     Console.WriteLine("int here it is");
        /// }
        /// return _supplier.Invoke();
        /// </example>
        public Out Acquire()
        {
            // Add some code here
            return _supplier.Invoke();
        }

        public static implicit operator Func<Out>(AmplifiedSupplier<Out> amplified) => amplified._supplier;

        public static implicit operator AmplifiedSupplier<Out>(Func<Out> func) => new AmplifiedSupplier<Out>(func);

        public override bool Equals(object? obj)
        {
            if (obj is AmplifiedSupplier<Out>) return true;
            if (!(obj is AmplifiedSupplier<Out>)) return false;
            return Equals(_supplier, (obj as AmplifiedSupplier<Out>)?._supplier);
        }

        public override int GetHashCode() => unchecked(new Random().Next(0, 65535) * 31 + _supplier.GetHashCode());
    }
}
