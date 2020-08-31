using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Rename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region
        string src_name;
        string des_name;
        string path;
        UInt32 number;
        const UInt16 NUM_DIGIT = 6;
        UInt16 len = NUM_DIGIT;
        #endregion
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        private void textBoxDir_TextChanged(object sender, EventArgs e)
        {
            path = textBoxDir.Text;
            if (path.EndsWith("\\"))
            {
                path = path.Remove(path.Length - 1, 1);
            } 
        }


        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            //des_name = textBoxName.Text;
            //len = Convert.ToUInt16(textBoxLength.Text);
            //if (len <= des_name.Length)
            //{
            //    len = (UInt16)(des_name.Length + 1);
            //    textBoxLength.Text = len.ToString();
            //}
            //else
            //{
            //    len = (UInt16)des_name.Length;
            //    textBoxLength.Text = len.ToString();
            //}
            pBar1.Value = 0;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            des_name = textBoxName.Text;
            if (des_name=="")
            {
                MessageBox.Show("New name of file can't empty!!!");
                return;
            }
            if (!IsNumeric(des_name))
            {
                MessageBox.Show("Rename file error. Require only number in name!!!");
                return;
            }
            number = Convert.ToUInt32(des_name);
            try 
            {

                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] Files = dir.GetFiles("*");
                if (Files.Length != 0)
                {
                    pBar1.Maximum = Files.Length;
                    foreach (FileInfo file in Files)
                    {
                        src_name = file.FullName;
                        des_name = Getnewname(number);
                        number += 1;
                        string newpath = path + "\\" + des_name + file.Extension;
                        if (!File.Exists(newpath)) file.MoveTo(newpath);
                        pBar1.Value += 1;
                    }
                    ResetValue();
                    MessageBox.Show(Convert.ToString(Files.Length)+ " files have been renamed ", "COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("ERROR");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }
        public string Getnewname(UInt32 num)
        {
            string format = "D" + len.ToString();
            return  num.ToString(format);
        }
        public void ResetValue()
        {
            des_name = textBoxName.Text;
            number = Convert.ToUInt32(des_name);
        }
        private void textBoxLength_TextChanged(object sender, EventArgs e)
        {
            string temp = textBoxLength.Text;
            len = Convert.ToUInt16(temp);
        }
        public void BrowseFolder()
        {
            FolderBrowserDialog FolderBrowserDialog = new FolderBrowserDialog();
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDir.Text = FolderBrowserDialog.SelectedPath;
                path = textBoxDir.Text;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabRename;
        }

        public static bool IsNumeric(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            return true;
        }

        private void useToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp f2 = new FormHelp();
            f2.ShowDialog(); // Shows Form2
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabRename;
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabResize;
        }
        Image MyResize(Image image, int with, int hight)
        {
            Bitmap bmp = new Bitmap(with, hight);
            Graphics graphic = Graphics.FromImage(bmp);
            graphic.DrawImage(image, 0, 0, with, hight);
            graphic.Dispose();
            return bmp;
        }
    }
}
