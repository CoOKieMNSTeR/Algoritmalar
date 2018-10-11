using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LZWAlgoritması
{
    public partial class MainForm : Form
    {
        private Dictionary<int, string> sözlük;
        private List<int> indix;

        public MainForm()
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            LZWSıkıştır comp = new LZWSıkıştır();

            indix = new List<int>();
            sözlük = comp.Sıkıştır(text, ref indix);
            textBox2.Text += "Sözlük\r\n\r\n";

            foreach (KeyValuePair<int, string> kvp in sözlük)
            {
                if (kvp.Key >= 256)
                {
                    textBox2.Text += kvp.Key.ToString("D3") + "\t";
                    textBox2.Text += kvp.Value + "\r\n";
                }
            }

            textBox2.Text += "\r\nKelime Oluşturulurken Kullanılan İndexler\r\n\r\n";

            foreach (int index in indix)
                textBox2.Text += index.ToString() + "\r\n";

            textBox2.Text += "\r\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sözlük != null && indix != null)
            {
                LZWGerisıkıştır dec = new LZWGerisıkıştır();

                textBox2.Text += dec.Gerisıkıştır(sözlük, indix) + "\r\n\r\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
        }
    }
}
