using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjMAUIDEMO.Models
{
    class CLotto
    {
        public string pull()
        {
            string result = "";
            Random r = new Random();
            int num;
            List<int> alllist = new List<int>();
            List<int> selectnum = new List<int>();
            for (int i = 1; i <= 49; i++)
            {
                alllist.Add(i);
            }
            for (int ii = 0; ii < 6; ii++)
            {
                int s = r.Next(0, 49 - ii);
                num = alllist[s];
                selectnum.Add(num);
                alllist.RemoveAt(s);
            }
            selectnum.Sort();
            foreach (int i in selectnum)
            {
                result += i.ToString() + " ";
            }
            return result;
        }
    }
}
