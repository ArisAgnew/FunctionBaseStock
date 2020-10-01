using System;

namespace FunctionBaseStock.Enhancements.HighEndAbstraction
{
    /// <summary>
    /// High-end representation of the <see cref="Func{In, Out}"/> delegate
    /// </summary>
    /// <typeparam name="In">An input type</typeparam>
    /// <typeparam name="Out">An output type</typeparam>
    public interface IAmplifiedFunction<in In, out Out>
    {
        Out Apply(In type);
    }
}
