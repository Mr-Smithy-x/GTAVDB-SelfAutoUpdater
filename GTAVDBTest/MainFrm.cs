using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTAVDBTest
{
    public partial class MainFrm : MaterialSkin.Controls.MaterialForm, GTAVDB.Listeners.OnNativeListener
    {
        public MainFrm()
        {
            InitializeComponent();
            
            //GTAVDB.Latest.LoadNatives(this, this, "http://pastebin.com/raw.php?i=ZHzcPLUC");
        }
        string latest = "http://pastebin.com/raw.php?i=ZHzcPLUC";
    

        public void OnNativesLoaded(string message)
        {
            MessageBox.Show(message);
            comboBox1.Focus();
            comboBox1.DroppedDown = true;
        }

        public void OnNativesFailed(string message)
        {
            MessageBox.Show(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Keep RPC & Natives Updated By Just Grabbing Hashes");
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

            uint i = GTAVDB.Latest.GetNative(comboBox1.Text);
            if (i == 0x0)
            {
                i = GTAVDB.Latest.GetRPCHash(comboBox1.Text);
                if (i == 0x0)
                {
                    MessageBox.Show("Could Not Find Hash");
                }
                else
                {
                    MessageBox.Show(i.ToString("X4"));
                }
            }
            else
            {
                MessageBox.Show(i.ToString("X4"));   
            }
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            comboBox1.DroppedDown = true;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                materialRaisedButton1_Click(null, null);
            }
        }

        private void fromLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GTAVDB.Latest.LoadNatives(this, this, (toolStripTextBox1.Text.Length > 0) ? (toolStripTextBox1.Text) : (latest));
            string[] text = Enum.GetNames(typeof(GTAVDB.Natives));
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(text);
           
        }

        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GTAVDB.Latest.LoadNativesFromFile(this, this);
            string[] text = Enum.GetNames(typeof(GTAVDB.Natives));
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(text);
           
        }

        private void fromFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GTAVDB.Latest.LoadRPC(this, this);
            string[] text = Enum.GetNames(typeof(GTAVDB.RPC));
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(text);

        }

    
    }
}
