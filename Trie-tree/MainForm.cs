using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trie;


namespace Trie_tree
{
    public partial class MainForm : Form
    {
        TreeRoot myRoot = new TreeRoot();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {               
            if (!(myRoot.Add(tbWord.Text)))
            {
                MessageBox.Show("the word was not added", "fail", MessageBoxButtons.OK);
                return;
            }
            tbWord.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!(myRoot.Delete(tbWord.Text)))
            {
                MessageBox.Show("the word was not deleted", "fail", MessageBoxButtons.OK);
                return;
            }
            tbWord.Text = "";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (myRoot.Find(tbWord.Text))
            {
                MessageBox.Show("the word was found", "success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("the word was not found", "fail", MessageBoxButtons.OK);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            tbMain.Text = myRoot.View();
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            myRoot = myRoot.Reverse();
            btnView.PerformClick();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            myRoot.Clear();
            btnView.PerformClick();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            myRoot.Clear();       
            string fileText = System.IO.File.ReadAllText(openFileDialog1.FileName);
            char[] s = { '\n' };
            string[] words = fileText.Split(s, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                myRoot.Add(word.Trim());
            }
            btnView.PerformClick();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            System.IO.File.WriteAllText(saveFileDialog1.FileName, myRoot.View());
        }
    }
}
