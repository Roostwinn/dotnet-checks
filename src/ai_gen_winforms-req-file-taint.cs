using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    public partial class MainForm : Form
    {
        private RichTextBox richTextBox;

        public MainForm()
        {
            InitializeComponent();
            richTextBox = new RichTextBox();
            this.Controls.Add(richTextBox);
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            string userInput = GetUserInput(); // Получение пользовательского ввода, например, из текстового поля
            // Потенциально небезопасное использование пользовательского ввода для построения пути
            string fullPath = Path.Combine("C:\\Documents", userInput);

            // Использование небезопасного пути без валидации
            richTextBox.LoadFile(fullPath);
        }

        private string GetUserInput()
        {
            // Метод для получения пользовательского ввода
            return "example.rtf"; // Пример значения, в реальном приложении это будет ввод пользователя
        }
    }
}