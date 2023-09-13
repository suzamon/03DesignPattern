using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _03DesignPattern
{
    public class Journal
    {
        //Jounrnal 객체에 대한 Keeping과 관련된 클래스
        private readonly List<String> entries = new List<String>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index) {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistence
    {
        //파일 입출력 및 형상 관리 관련 기능은 Persistence 클래스에서 관리

        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, j.ToString());
            }
        }
    }

    public class _01SOLID_01SingleResponsibility
    {
        static void Main(string[] args)
        {
            var j= new Journal();

            j.AddEntry("I cried today");
            j.AddEntry("I ate a burger");
            WriteLine(j);

            var p = new Persistence();
            var filename = @"C:\Users\psm53\Desktop\ChsarpStudy\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
        }
    }
}
