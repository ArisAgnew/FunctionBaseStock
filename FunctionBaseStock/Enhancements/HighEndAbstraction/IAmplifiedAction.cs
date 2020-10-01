using System;

namespace FunctionBaseStock.Enhancements.HighEndAbstraction
{
    /// <summary>
    /// High-end representation of the <see cref="Action{In}"/> delegate
    /// </summary>
    /// <typeparam name="In">An input type</typeparam>
    public interface IAmplifiedAction<in In>
    {
        void Accept(In type);
    }
}
