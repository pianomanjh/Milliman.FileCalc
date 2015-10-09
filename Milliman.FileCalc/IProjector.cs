namespace Milliman.FileCalc
{
    using System.Collections.Generic;

    public interface IProjector<out T>
    {
        IEnumerable<T> Map(IEnumerable<string> input);
    }
}