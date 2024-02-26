using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace csv
{
    class CsvUtil
    {
        private static char quotechar = ',';
        public static string str;
        public static void WriteCSV(string filePathName, List<string[]> rows, bool append)         
        { 
        StreamWriter fileWriter=new StreamWriter(filePathName,append,Encoding.UTF8);
            foreach (string[] cells in rows)             
            {
                StringBuilder buffer = new StringBuilder();
                for (int i = 0; i < cells.Length; ++i)
                {
                    if (cells[i] != "")
                    {
                        str = cells[i].Replace("\"", "").Trim();
                        if (str == null)
                            str = "";
                        if (str.IndexOf(",") > -1)
                        {
                            str = "\"" + str + "\"";
                        }
                        if (str != "")
                        {
                            buffer.Append(str);
                        }
                        //在每一个cell后面加逗号....但是最后一个cell后面不加逗号
                        if ((i != cells.Length - 1) && (str != ""))
                        {
                            buffer.Append(quotechar);
                        }
                    }
                    
                }
                if (str != "")
                {
                    fileWriter.WriteLine(buffer.ToString());
                    str = "";
                }
                
                
            }
            fileWriter.Flush();             
            fileWriter.Close();
        }

        public static List<string[]> ReadCSV(string filePathName)
        {
            StreamReader fileReader = new StreamReader(filePathName, Encoding.UTF8);
            string rowStr = fileReader.ReadLine();
            // "a,1",b,c     // "\"a,1\",\"b,1,2\",\"c,cc\",ddd"
            List<string[]> rowList = new List<string[]>();
            while (rowStr != null) {
                List<string> cellVals = getStrCellVal(rowStr);
                string[] cells = new string[cellVals.Count];
                for (int i = 0; i < cellVals.Count; i++) {
                    cells[i] = cellVals[i];
                }
                rowList.Add(cells);
                rowStr = fileReader.ReadLine();
            }
            fileReader.Close();
            return rowList;
        }

        private static List<string> getStrCellVal(string rowStr) {
            List<string> cellList = new List<string>();
            while (rowStr != null && rowStr.Length > 0)
            {
                string cellVal = "";
                if (rowStr.StartsWith("\""))
                {
                    rowStr = rowStr.Substring(1);
                    int i = rowStr.IndexOf("\",");
                    int j = rowStr.IndexOf("\" ,");
                    int k = rowStr.IndexOf("\"");
                    if (i < 0) i = j;
                    if (i < 0) i = k;
                    if (i > -1)
                    {
                        cellVal = rowStr.Substring(0, i);
                        if ((i + 2) < rowStr.Length)
                            rowStr = rowStr.Substring(i + 2).Trim();
                        else
                            rowStr = "";
                    }
                    else
                    {
                        cellVal = rowStr;
                        rowStr = "";
                    }
                }
                else
                {
                    int i = rowStr.IndexOf(",");
                    if (i > -1)
                    {
                        cellVal = rowStr.Substring(0, i);
                        if ((i + 1) < rowStr.Length)
                            rowStr = rowStr.Substring(i + 1).Trim();
                        else
                            rowStr = "";
                    }
                    else
                    {
                        cellVal = rowStr;
                        rowStr = "";
                    }
                }
                if (cellVal == "") cellVal = " ";
                cellList.Add(cellVal);
            }
            return cellList;
        }


    }
}
