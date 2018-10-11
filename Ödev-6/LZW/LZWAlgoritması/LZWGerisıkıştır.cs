using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LZWAlgoritması
{
    class LZWGerisıkıştır
    {
        public string Gerisıkıştır(
            Dictionary<int, string> sözlük, List<int> indix)
        {
            string s = string.Empty;

            foreach (int index in indix)
            {
                foreach (KeyValuePair<int, string> kvp in sözlük)
                {
                    if (kvp.Key == index)
                    {
                        s += kvp.Value;
                        break;
                    }
                }
            }

            return s;
        }
    }
}
