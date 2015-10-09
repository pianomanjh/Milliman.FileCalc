namespace Milliman.FileCalc
{
    using System.Collections.Generic;
    using System.Linq;

    public class SceneProjector : IProjector<Scene>
    {
        private readonly char _delimiter;

        public SceneProjector(char delimiter)
        {
            this._delimiter = delimiter;
        }

        public IEnumerable<Scene> Map(IEnumerable<string> input)
        {
            foreach (var i in input)
            {
                var values = i.Split(this._delimiter);
                yield return new Scene(values[1], values.Skip(2).Select(decimal.Parse));
            }
        }
    }
}