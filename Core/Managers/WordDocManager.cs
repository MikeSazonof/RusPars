using System;
using System.Data;
using System.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace RusParse.Core.Managers
{
    internal class WordDocManager
    {
        public void Run(Dictionary<string, string> dict)
        {
            Document doc = new Document();
            Section s = doc.AddSection();
            Table table = s.AddTable(true);
            string[] Header = { "№", "url", "data"};

            List<string[]> data = new List<string[]>();
            int j = 1;
            foreach (var ele in dict)
                data.Add(new string[] { j++.ToString(), ele.Key, ele.Value });

            table.ResetCells(data.Count + 1, Header.Length);
            TableRow FRow = table.Rows[0];
            FRow.IsHeader = true;
            FRow.Height = 8;
            FRow.RowFormat.BackColor = Color.AliceBlue;
            for (int i = 0; i < Header.Length; i++)
            {
                Paragraph p = FRow.Cells[i].AddParagraph();
                FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                TextRange TR = p.AppendText(Header[i]);
                TR.CharacterFormat.FontName = "Calibri";
                TR.CharacterFormat.FontSize = 8;
                TR.CharacterFormat.TextColor = Color.Teal;
                TR.CharacterFormat.Bold = true;
            }

            for (int row = 0; row < data.Count(); row++)
            {
                TableRow DataRow = table.Rows[row + 1];
                //DataRow.Height = ;
                for (int col = 0; col < data[row].Length; col++)
                {
                    DataRow.Cells[col].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    Paragraph p2 = DataRow.Cells[col].AddParagraph();
                    TextRange TR2 = p2.AppendText(data[row][col]);
                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    TR2.CharacterFormat.FontName = "Calibri";
                    TR2.CharacterFormat.FontSize = 8;
                    //TR2.CharacterFormat.TextColor = Color.Brown;
                }
            }

            //Save and Launch
            doc.SaveToFile(@"C:\prog\mocks\WordTable.docx", FileFormat.Docx2013);
            //System.Diagnostics.Process.Start(@"C:\prog\mocks\WordTable.docx");
        }
    }
}
