namespace Milliman.FileCalc
{
    using System.Collections.Generic;

    public class Scene
    {
        public Scene(string varName, IEnumerable<decimal> values)
        {
            this.VarName = varName;
            this.Values = values;
        }

        public string VarName;
        public IEnumerable<decimal> Values;
    }
}