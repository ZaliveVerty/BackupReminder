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
using System.Diagnostics;

namespace BackupReminder
{
    public partial class Form1 : Form
    {
        public static String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Backup Reminder";
        public static List<List<String>> listOfFiles = new List<List<String>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CHECK BASE
            checkBase();

            //REFRESH DATAGRIDVIEW
            refresh_dataGridView();

            //START TIMER
            timer1.Start();
        }

        private static void setList() {
            FileInfo[] files = new DirectoryInfo(path + @"\Storage").GetFiles();

            listOfFiles.Clear();

            for (int i = 0; i < files.Length; i++)
            {
                String[] array = readFile(files[i].FullName.ToString()).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                listOfFiles.Add(new List<String> { files[i].FullName, array[0], array[1], array[2], array[3] });
            }
        }

        public void refresh_dataGridView() {
            //GET ALL FILES IN A LIST
            setList();

            dataGridView1.Rows.Clear();

            for (int i = 0; i < listOfFiles.Count; i++) {
                dataGridView1.Rows.Add(listOfFiles[i][0], listOfFiles[i][1], listOfFiles[i][2], listOfFiles[i][4]);
            }
        }

        private static void checkBase() {
            createDirectory(path + @"\Storage");
        }

        public static void createDirectory(String path) {
            Directory.CreateDirectory(path);
        }

        public static void createFile(String path, String text)
        {
            using (var tw = new StreamWriter(path, false))
            {
                tw.WriteLine(text);
            }
        }

        public static void deleteFile(String path) {
            File.Delete(path);
        }

        public static String readFile(String path) {
            return File.ReadAllText(path);
        }

        public String getCol(int index) {
            return dataGridView1.SelectedRows[0].Cells[index].Value.ToString();
        }

        private void addButotn_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Form2.form = this;
            form2.ShowDialog();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this?\t\t", "Remove", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                deleteFile(getCol(0));
                refresh_dataGridView();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            String[] array = readFile(getCol(0)).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            createFile(getCol(0), array[0] + "\n" + array[1] + "\n" + array[2] + "\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            refresh_dataGridView();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String path1 = listOfFiles[dataGridView1.SelectedRows[0].Index][2];
            String path2 = listOfFiles[dataGridView1.SelectedRows[0].Index][3];

            Process.Start("explorer.exe", "/select, \"" + path1 + "\"");
            Process.Start("explorer.exe", "/select, \"" + path2 + "\"");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                removeButton.Enabled = true;
                updateButton.Enabled = true;
                linkLabel1.Enabled = true;
            }
            else {
                removeButton.Enabled = false;
                updateButton.Enabled = false;
                linkLabel1.Enabled = false;
            }
        }
    }
}
