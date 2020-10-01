using System;

namespace FunctionBaseStock.Enhancements.HighEndAbstraction
{
    /// <summary>
    /// High-end representation of the <see cref="Func{Out}"/> delegate
    /// </summary>
    /// <typeparam name="Out">An output type</typeparam>
    public interface IAmplifiedSupplier<out Out>
    {
        Out Acquire();
    }
}
