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
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ids = Directory.GetLogicalDrives();
            foreach (string id in ids) { 
                tvwDirs.Nodes.Add(id);
                lvwExplorer.Items.Add(id, 8);
                    
            }
        }
        private void assignIcons(string[] file)
        {
            foreach (string fl in file)
            {
                if (fl.EndsWith(".JPG") || fl.EndsWith(".jpg") || fl.EndsWith(".png") || fl.EndsWith(".PNG") || fl.EndsWith(".GIF") || fl.EndsWith(".gif"))
                    lvwExplorer.Items.Add(fl, 2);
                else if (fl.EndsWith(".MP4") || fl.EndsWith(".mp4") || fl.EndsWith(".avi") || fl.EndsWith(".AVI") || fl.EndsWith(".MPEG") || fl.EndsWith(".mpeg"))
                    lvwExplorer.Items.Add(fl, 3);
                else if (fl.EndsWith(".txt") || fl.EndsWith(".TXT"))
                    lvwExplorer.Items.Add(fl, 4);
                else if (fl.EndsWith(".doc") || fl.EndsWith(".DOC") || fl.EndsWith(".docx") || fl.EndsWith(".DOCX"))
                    lvwExplorer.Items.Add(fl, 5);
                else if (fl.EndsWith(".exe") || fl.EndsWith(".EXE"))
                    lvwExplorer.Items.Add(fl, 6);
                else if (fl.EndsWith(".msi") || fl.EndsWith(".MSI"))
                    lvwExplorer.Items.Add(fl, 7);
                else if (fl.EndsWith(".dll") || fl.EndsWith(".DLL"))
                    lvwExplorer.Items.Add(fl, 9);
                else if (fl.EndsWith(".lnk") || fl.EndsWith(".LNK"))
                    lvwExplorer.Items.Add(fl, 10);
                else
                    lvwExplorer.Items.Add(fl, 1);
            }
        }
        private void showDriveDirects()
        {
            try
            {
                lvwExplorer.Clear();
                string[] dirs = Directory.GetDirectories(tvwDirs.SelectedNode.Text);
                string[] files = Directory.GetFiles(tvwDirs.SelectedNode.Text);
                foreach ( string dir in dirs )
                {
                    lvwExplorer.Items.Add(dir, 0);
                    tvwDirs.SelectedNode.Nodes.Add(dir);
                }
                assignIcons(files);
                cbxAddress.Text=tvwDirs.SelectedNode.Text;
                
            }
            catch (Exception ex){
                MessageBox.Show("Error - " + ex.Message);
            
            }
        }
        private void GotoDirectory()
        {
            try
            {
                lvwExplorer.Clear();
                string[] dirs = Directory.GetDirectories(cbxAddress.Text);
                string[] files = Directory.GetFiles(cbxAddress.Text);
                foreach (string dir in dirs)
                {
                    lvwExplorer.Items.Add(dir, 0);
                }
                assignIcons(files);
                if (cbxAddress.Items.Contains(cbxAddress.Text) == false)
                    cbxAddress.Items.Add(cbxAddress.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler -" + ex.Message);
            }
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwExplorer.View = View.LargeIcon;
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwExplorer.View = View.SmallIcon;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwExplorer.View= View.Details;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwExplorer.View = View.List;
        }


        private void lvwExplorer_DoubleClick(object sender, EventArgs e)
        {
            string direc = lvwExplorer.SelectedItems[0].Text;
            try
            {
                lvwExplorer.Clear();
                string[] dirs = Directory.GetDirectories(direc);
                string[] files = Directory.GetFiles(direc);
                foreach (string dir in dirs)
                {
                    lvwExplorer.Items.Add(dir, 0);
                }
                assignIcons(files);
                cbxAddress.Text = direc;
                if (cbxAddress.Items.Contains(cbxAddress.Text) == false)
                    cbxAddress.Items.Add(direc);
            }
            catch (Exception ex)
            {
                try
                {
                    cbxAddress.Text = direc;
                    System.Diagnostics.Process.Start(direc);
                }
                catch(Exception ex2)
                {
                    MessageBox.Show("Fehler -" + ex.Message);
                }
            }
        }

        private void cbxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                GotoDirectory();
            }
        }

        private void tvwDirs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            showDriveDirects();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvwExplorer.SelectedItems[0].Text != "" && lvwExplorer.SelectedItems.Count == 1)
                Clipboard.SetText(lvwExplorer.SelectedItems[0].Text);
            else
                MessageBox.Show("You can only copy one element at a time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Clipboard.GetText();
            char seperator = '\\';
            string originalFilename = path.Split(seperator)[path.Split(seperator).Length - 1];
            string taget = cbxAddress.Text + '\\' + originalFilename;
            try
            {
                if (File.Exists(taget))
                    if (MessageBox.Show("Exits, do u want to replace it?", "File exits", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        File.Delete(taget);
                File.Copy(path, taget, false);
                GotoDirectory();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error - " + ex.Message);
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwExplorer.SelectedItems.Count == 1)
                {
                    if (MessageBox.Show("Do u want to delete this file?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        File.Delete(lvwExplorer.SelectedItems[0].Text);
                }
                else
                    MessageBox.Show("You can only delete one item at a time.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GotoDirectory();
        }
    }
}
