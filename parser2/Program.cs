using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace dataparser
{
    class Program
    {
        public class Report
        {

            public List<string> filename = new List<string>();
            public List<string> voc = new List<string>();
            public List<string> jsc = new List<string>();
            public List<string> ff = new List<string>();
            public List<string> pce = new List<string>();

            public string filename_path;
            public int cell_id;

        }

        static void Main(string[] args)
        {

            // Directory to scan for data. .dat files should be in folders corresponding to 
            // the pixel under test. Maximum of 53 pixels available
            string searchdir = @"C:\temp\parse1\";


            for (int i = 0; i < 54; i++)
            {
                // Go through each pixel directory
                string filedir = String.Format(searchdir + i);
                Console.WriteLine(filedir);

                // Load all .dat files paths into an array
                string[] files = Directory.GetFiles(filedir, "*.dat", System.IO.SearchOption.TopDirectoryOnly);

                // Use regex to order data files in numeric order. Without this, file 10.dat comes before 2.dat
                NumericalSort(files);

                Report report1 = new Report();

                foreach (string datafile in files)
                {
                    string filename = datafile;
                    string cell_id = filename.Substring(filename.LastIndexOf("\\") + 1); // Remove the path directory

                    string[] lines = File.ReadAllLines(datafile, Encoding.UTF8);

                    // Pick parameters from given lines in the .dat file
                    string jscLine = lines[15];
                    string vocLine = lines[16];
                    string ffLines = lines[18];
                    string pceLine = lines[17];

                    // Split parameter header from value. For example:
                    // Jsc (mA/cm2)	-1.35359E+1
                    string[] jscSplit = jscLine.Split('\t');
                    string[] vocSplit = vocLine.Split('\t');
                    string[] ffSplit = ffLines.Split('\t');
                    string[] pceSplit = pceLine.Split('\t');

                    report1.jsc.Add(jscSplit[1]);
                    report1.voc.Add(vocSplit[1]);
                    report1.ff.Add(ffSplit[1]);
                    report1.pce.Add(pceSplit[1]);
                    report1.filename.Add(cell_id);


                    string outfilename = filename.Substring(filename.LastIndexOf("_") + 1); // Remove the path directory
                    // Nasty hack to remove the ".dat" from the filename
                    outfilename = outfilename.Remove(outfilename.Length - 1, 1);
                    outfilename = outfilename.Remove(outfilename.Length - 1, 1);
                    outfilename = outfilename.Remove(outfilename.Length - 1, 1);
                    outfilename = outfilename.Remove(outfilename.Length - 1, 1);

                    String dataout = @"C:\temp\parse1\completed\";
                    dataout = dataout + outfilename + "_report.dat";

                    using (StreamWriter file = new StreamWriter(dataout))
                    {

                        file.WriteLine(@"File Name   Jsc(mA / cm2)    Voc(V) Pmax(mW / cm2)   Fill Factor Rect.Rato Rseries(Ohms)  Rshunt(Ohms)");


                        for (int j = 0; j < report1.filename.Count; j++)
                        {
                            file.Write(report1.filename[j] + "\t");
                            file.Write(report1.jsc[j] + "\t");
                            file.Write(report1.voc[j] + "\t");
                            file.Write(report1.pce[j] + "\t");
                            file.WriteLine(report1.ff[j]);

                        }



                    }

                }
            }


        }

        public static void NumericalSort(string[] ar)
        {
            Regex rgx = new Regex("([^0-9]*)([0-9]+)");
            Array.Sort(ar, (a, b) =>
            {
                var ma = rgx.Matches(a);
                var mb = rgx.Matches(b);
                for (int i = 0; i < ma.Count; ++i)
                {
                    int ret = ma[i].Groups[1].Value.CompareTo(mb[i].Groups[1].Value);
                    if (ret != 0)
                        return ret;

                    ret = int.Parse(ma[i].Groups[2].Value) - int.Parse(mb[i].Groups[2].Value);
                    if (ret != 0)
                        return ret;
                }

                return 0;
            });
        }
    }
}
