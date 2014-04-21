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
using System.IO;

namespace WebstieBlocker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string Hpath = "C:\\Windows\\System32\\drivers\\etc\\hosts";

        private void button1_Click(object sender, EventArgs e)
        {
            Process myProcess = new Process();

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter a valid URL");
            }
            else
            {
                string batFile;
                batFile = genBat(textBox1.Text);
                string path = "C:\\Windows\\System32\\drivers\\etc\\blocksite.bat";

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(@batFile);
                }

                Process.Start(path).WaitForExit();

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
         }

        public string genBat(string site)
        {
            string code;
            string Fsite;
                
            Fsite = "www."+ site;
            
            textBox2.Text = "echo off\r\n";
            textBox2.Text += "cd C:\\Windows\\System32\\drivers\\etc\r\n";
            textBox2.Text += "echo. >> \"hosts\"\r\n";
            textBox2.Text += "echo 127.0.0.1 " + Fsite + " >> \"hosts\"\r\n";
            textBox2.Text += "echo 127.0.0.1 " + site + ">> \"hosts\""; 
            
            code = textBox2.Text;

            return code;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(Hpath);     
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter(Hpath);
            //sw.Write(richTextBox1.Lines);
            richTextBox1.SaveFile(Hpath, RichTextBoxStreamType.PlainText);
            //sw.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox2.ReadOnly = true;
            richTextBox2.Text = "Instructions:";
            richTextBox2.Text += "\nBLOCK:\n1. Enter desired URL without http:// and/or www. ";
            richTextBox2.Text += "\n2. Now press the Block button.";
            richTextBox2.Text += "\n3. Check if site is blocked.\n";

            richTextBox2.Text += "\nUNBLOCK:";
            richTextBox2.Text += "\n1. Press ope.n";
            richTextBox2.Text += "\n2. Sroll to the bottom.";
            richTextBox2.Text += "\n3. ONLY ERASE the 127.0.0.1 followed by site you want to unblock.     Ex. 127.0.0.1 google.com \n      127.0.0.1 www.google.com";
            richTextBox2.Text += "\n4. Press Save.";
            richTextBox2.Text += "\n\nNote: If you accidently mess up your file and save it, press the \"Default Code\" button and copy&paste that.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

    }
}

