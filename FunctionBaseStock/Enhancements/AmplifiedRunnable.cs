using System;

using FunctionBaseStock.Enhancements.HighEndAbstraction;

namespace FunctionBaseStock.Enhancements
{
    public class AmplifiedRunnable : IAmplifiedRunnable
    {
        private Action _action;

        public AmplifiedRunnable(Action action) => _action = action
            ?? throw new ArgumentException($"{nameof(Action)} (Runnable) delegate should be defined.");

        /// <example>
        /// if (typeof(IEquatable<int>).IsAssignableFrom(type.GetType()))
        /// {
        ///     Console.WriteLine("int here it is");
        /// }
        /// _action.Invoke();
        /// Console.WriteLine("Something");
        /// </example>
        public void Run()
        {
            // Add some code here
            _action.Invoke();
            // Add some code here
        }
        public static implicit operator Action(AmplifiedRunnable amplified) => amplified._action;

        public static implicit operator AmplifiedRunnable(Action action) => new AmplifiedRunnable(action);

        public override bool Equals(object? obj)
        {
            if (obj is AmplifiedRunnable) return true;
            if (!(obj is AmplifiedRunnable)) return false;
            return Equals(_action, (obj as AmplifiedRunnable)?._action);
        }

        public override int GetHashCode() => unchecked(new Random().Next(0, 65535) * 31 + _action.GetHashCode());
    }
}
