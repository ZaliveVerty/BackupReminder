using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BackupReminder
{
    public partial class Form2 : Form
    {
        public static Form1 form;

        public Form2()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void browseButton1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox2.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox3.Text = folderBrowserDialog1.SelectedPath;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text)) {
                doneButton.Enabled = true;
            }
            else {
                doneButton.Enabled = false;
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            String text = textBox1.Text + "\n" + textBox2.Text + "\n" + textBox3.Text + "\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            Form1.createFile(Form1.path + @"\Storage\" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-FFF"), text);
            form.refresh_dataGridView();
            Close();
        }
    }
}
