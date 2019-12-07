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

namespace MineCraftChatExtraction
{
    public partial class MainForm : Form
    {
        string filePath;
        int LastLine;//更新の目印

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {

            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //デフォルトで選択されるファイルを指定
            ofd.FileName = "latest.log";
            //はじめに表示されるフォルダを指定する Environment.UserNameでユーザ名を取得
            ofd.InitialDirectory = @"C:\Users\"+ Environment.UserName + @"\AppData\Roaming\.minecraft\logs";
            //[ファイルの種類]に表示される選択肢を指定する
            ofd.Filter = "ログファイル(*.log)|*.log;|すべてのファイル(*.*)|*.*";

            ofd.FilterIndex = 1;
            //タイトル
            ofd.Title = "開くファイルを選択してください";
            
            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                FirstRead();
            }
        }

        private void Reflesh(object sender, EventArgs e)
        {
            //ファイルが選択されるまではスキップ
            if (filePath == null) return;

            LogsRead();

        }

        private void LogsRead()
        {
            try
            {
                using (FileStream fs = new FileStream(filePath,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (TextReader sr = new StreamReader(fs, Encoding.GetEncoding("shift-jis")))
                    {
                        int LineCount = 0;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            LineCount++;                            
                            if (LineCount <= LastLine) continue;
                            
                            Log log = new Log(line);

                            chatTextBox.AppendText(log.message);

                        }

                        LastLine = LineCount;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("ファイルがひらけません");
                Console.WriteLine(err.Message);
                filePath = null;
            }
        }

        //初回読み込みで行数をとる
        private void FirstRead()
        {
            try
            {
                using (FileStream fs = new FileStream(filePath,
                    FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (TextReader sr = new StreamReader(fs, Encoding.GetEncoding("shift-jis")))
                    {
                        int LineCount = 0;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            LineCount++;
                        }
                        LastLine = LineCount;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("ファイルがひらけません");
                Console.WriteLine(err.Message);
                filePath = null;
            }
        }

        private void LoadConfig()
        {

        }
    }
}
