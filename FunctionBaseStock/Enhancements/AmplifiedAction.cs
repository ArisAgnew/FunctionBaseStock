using System;

using FunctionBaseStock.Enhancements.HighEndAbstraction;

namespace FunctionBaseStock.Enhancements
{
    public class AmplifiedAction<In> : IAmplifiedAction<In>
    {
        private Action<In> _action;

        public AmplifiedAction(Action<In> action) => _action = action
            ?? throw new ArgumentException($"{nameof(Action<In>)} (Consumer) delegate should be defined.");

        /// <example>
        /// if (typeof(IEquatable<int>).IsAssignableFrom(type.GetType()))
        /// {
        ///     Console.WriteLine("int here it is");
        /// }
        /// _action.Invoke(type);
        /// Console.WriteLine("Something");
        /// </example>
        public void Accept(In type)
        {
            // Add some code here
            _action.Invoke(type);
            // Add some code here
        }

        public static implicit operator Action<In>(AmplifiedAction<In> amplified) => amplified._action;
        public static implicit operator AmplifiedAction<In>(Action<In> action) => new AmplifiedAction<In>(action);

        public override bool Equals(object? obj)
        {
            if (obj is AmplifiedAction<In>) return true;
            if (!(obj is AmplifiedAction<In>)) return false;
            return Equals(_action, (obj as AmplifiedAction<In>)?._action);
        }

        public override int GetHashCode() => unchecked(new Random().Next(0, 65535) * 31 + _action.GetHashCode());
    }
}
