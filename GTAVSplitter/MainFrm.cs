using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.IO;

namespace GTAVSplitter
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (String s in richTextBox1.Lines)
            {
                string[] x = s.Split(new string[] { "=" }, StringSplitOptions.None);
                x[0] = x[0].Trim();
                richTextBox2.AppendText(x[0] + "," + Environment.NewLine);
            }
        }
    }
}
