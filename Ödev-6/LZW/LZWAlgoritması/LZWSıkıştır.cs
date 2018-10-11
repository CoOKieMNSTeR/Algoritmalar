using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LZWAlgoritması
{
    class LZWSıkıştır
    {
        public Dictionary<int, string> Sıkıştır(string text, ref List<int> indices)
        {
            Dictionary<int, string> sözlük = new Dictionary<int, string>();

            for (int i = 0; i < 256; i++)
                sözlük.Add(i, new string((char)i, 1));

            char c = '\0';
            int index = 1, n = text.Length, nextKey = 256;
            string s = new string(text[0], 1), sc = string.Empty;

            while (index < n)
            {
                c = text[index++];
                sc = s + c;

                if (sözlük.ContainsValue(sc))
                    s = sc;

                else
                {
                    foreach (KeyValuePair<int, string> kvp in sözlük)
                    {
                        if (kvp.Value == s)
                        {
                            indices.Add(kvp.Key);
                            break;
                        }
                    }

                    sözlük.Add(nextKey++, sc);
                    s = new string(c, 1);
                }
            }

            foreach (KeyValuePair<int, string> kvp in sözlük)
            {
                if (kvp.Value == s)
                {
                    indices.Add(kvp.Key);
                    break;
                }
            }

            return sözlük;
        }
    }
}
