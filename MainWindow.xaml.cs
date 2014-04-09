using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Windows.Forms;
using System.IO;





namespace WpfApplication4
{
   
    public partial class MainWindow : Window
    {
       
        private string file_path_name;
        private string directory_name;
        private string file_name;
        private string extract_file_name;
        private string targetDir;
        

        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if ((Boolean)DirectoryButton.IsChecked) {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                file_path_name = dialog.SelectedPath;
                source_file_or_directory.Content = file_path_name;
            };
            if ((Boolean)FileButton.IsChecked)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                file_path_name = dlg.SafeFileName;
                source_file_or_directory.Content = file_path_name;
                file_name = dlg.FileName;
                
             
                dlg.Filter = "All documents (.*)|*.*"; 

              
                Nullable<bool> result = dlg.ShowDialog();

              
                if (result == true)
                {
                   

                  file_path_name = dlg.FileName;
                 
                  source_file_or_directory.Content = file_path_name;
                }
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            if (!destination_directory.Content.Equals(""))
            {
                if ((Boolean)DirectoryButton.IsChecked)
                {
                    // Создаём объект FastZip
                    FastZip fz = new FastZip();
                    //Создаём архив           
                    fz.CreateZip(@file_path_name + ".zip", @file_path_name, true, null);
                    // Получаем имя архива
                    FileInfo fInfo = new FileInfo(@file_path_name + ".zip");
                    String name = fInfo.Name;
                    //Перемещаем архив в указанную папку
                    System.IO.File.Move(@file_path_name + ".zip", @directory_name + "\\" + name);
                    //Удаляем пути к папке и папке с архивом
                    source_file_or_directory.Content = "";
                    destination_directory.Content = "";
                   
                }
                if ((Boolean)FileButton.IsChecked)
                {
                    // Создаём объект FastZip
                    FastZip fz = new FastZip();
                    //Создаём архив           
                    //String file_derictory_path = System.IO.Path.GetDirectoryName(file_path_name);
                    FileInfo fInfo1 = new FileInfo(@file_path_name);
                    String name1 = fInfo1.Name;
                    Directory.CreateDirectory("Temp");
                    File.Copy(file_path_name, "Temp\\" + name1, true);
                    //System.IO.File.Move(@file_path_name, "Temp");
                    fz.CreateZip(file_path_name + ".zip", "Temp", true, null);
                    // Получаем имя архива
                    FileInfo fInfo2 = new FileInfo(@file_path_name + ".zip");
                    String name2 = fInfo2.Name;
                    //Перемещаем архив в указанную папку
                    System.IO.File.Move(@file_path_name + ".zip", @directory_name + "\\" + name2);
                    Directory.Delete("Temp", true);
                    //Удаляем пути к папке и папке с архивом
                    // SourceFile.Text = "";
                    //FileDerictory.Text = "";
                    source_file_or_directory.Content = "";
                    destination_directory.Content = "";

                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                directory_name = dialog.SelectedPath;
               
                destination_directory.Content = directory_name;
          
          

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            extract_file_name = dlg.SafeFileName;
            extract_file_name_label.Content = extract_file_name;
            

          
            dlg.Filter = "Zip Archives (.zip)|*.zip";

          
            Nullable<bool> result = dlg.ShowDialog();

           
            if (result == true)
            {
               

                extract_file_name = dlg.FileName;
              
                extract_file_name_label.Content = extract_file_name;
            }

            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            targetDir = dialog.SelectedPath;
           
            targetDir_label.Content = targetDir;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (!targetDir_label.Content.Equals(""))
            {
                FastZip fastZip = new FastZip();
                string fileFilter = null;

              
                fastZip.ExtractZip(extract_file_name, targetDir, fileFilter);
                targetDir_label.Content = "";
                extract_file_name_label.Content = "";
            }

        }

       
       
        }

       
    }

