using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.DL
{
    public class Situation
    {
        public List<Tuple<int,int>> Options { get; set; }
        public Situation(List<Tuple<int, int>> _options)
        {
            Options = _options;
        }

        public int GetBestDir()
        {
            int dir = 0;
            int max = 0;
            foreach (Tuple<int, int> option in Options)
            {
                if(option.Item2 > max)
                {
                    max = option.Item2;
                    dir = option.Item1;
                }
            }
            return dir;
        }
    }
}
