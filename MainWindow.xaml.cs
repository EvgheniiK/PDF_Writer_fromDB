﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;

namespace WpfApp_PDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

  

        /*        //Определяем объект DataSet
                DataSet MyDataSet = new DataSet();

                //Имя каталога открываемого файла БД
                string catName = "";
                //Непосредственное имя самого файла БД
                string fileName = "";*/

        //Определяем объект DataSet
      //  DataSet MyDataSet = new DataSet();



        public MainWindow()
        {
            InitializeComponent();
        }


        List<Phone> phonesList;



        private void Button_Click_1222(object sender, RoutedEventArgs e)
        {
        
            phonesList = new List<Phone>
{
    new Phone { Title="iPhone 6S", Company="Apple", Price=54990 },
    new Phone {Title="Lumia 950", Company="Microsoft", Price=39990 },
    new Phone {Title="Nexus 5X", Company="Google", Price=29990 }
};
            phonesGrid.ItemsSource = phonesList;

        

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //Объект документа пдф
            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            //Создаем объект записи пдф-документа в файл
            PdfWriter.GetInstance(doc, new FileStream("pdfTables.pdf", FileMode.Create));

            //Открываем документ
            doc.Open();

            //Определение шрифта необходимо для сохранения кириллического текста
            //Иначе мы не увидим кириллический текст
            //Если мы работаем только с англоязычными текстами, то шрифт можно не указывать
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);//"C:\\Windows\\Fonts\\Arial", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            //Обход по всем таблицам датасета (хотя в данном случае мы можем опустить
            //Так как в нашей бд только одна таблица)
            for (int i = 0; i < MyDataSet.Tables.Count; i++)
            {
                //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
                PdfPTable table = new PdfPTable(MyDataSet.Tables[i].Columns.Count);

                //Добавим в таблицу общий заголовок
                PdfPCell cell = new PdfPCell(new Phrase("БД " + "fileName" + ", таблица №" + (i + 1), font));

                cell.Colspan = MyDataSet.Tables[i].Columns.Count;
                cell.HorizontalAlignment = 1;
                //Убираем границу первой ячейки, чтобы балы как заголовок
                cell.Border = 0;
                table.AddCell(cell);

                //Сначала добавляем заголовки таблицы
                for (int j = 0; j < MyDataSet.Tables[i].Columns.Count; j++)
                {
                    cell = new PdfPCell(new Phrase(new Phrase(MyDataSet.Tables[i].Columns[j].ColumnName, font)));
                    //Фоновый цвет (необязательно, просто сделаем по красивее)
                    cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }

                //Добавляем все остальные ячейки
                for (int j = 0; j < MyDataSet.Tables[i].Rows.Count; j++)
                {
                    for (int k = 0; k < MyDataSet.Tables[i].Columns.Count; k++)
                    {
                        table.AddCell(new Phrase(MyDataSet.Tables[i].Rows[j][k].ToString(), font));
                    }
                }
                //Добавляем таблицу в документ
                     
                doc.Add(table);
            }
            //Закрываем документ
      doc.Close();

            MessageBox.Show("Pdf-документ сохранен");




        }


        /*        //Стандартный код открытия базы данных в программе и вывод таблицы в DatagridView
                private void button1_Click(object sender, EventArgs e)
                {
                    //Подключение к БД
                    //Определяем подключение
                    OleDbConnection StrCon;
                    //Строка для выборки данных
                    string Select1;
                    //Создание объекта Command
                    OleDbCommand comand1;
                    //Определяем объект Adapter для взаимодействия с источником данных
                    OleDbDataAdapter adapter1;

                    try
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        if (ofd.ShowDialog() != DialogResult.Cancel)
                        {
                            if (ofd.FileName != null)
                            {
                                catName = ofd.FileName.Remove(ofd.FileName.LastIndexOf("\"));

                                fileName = ofd.FileName.Remove(0, ofd.FileName.LastIndexOf("\") + 1);

                                Select1 = "SELECT * FROM [" + fileName + "]";
                                //Создаем подключение
                                StrCon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + catName + ";Extended Properties=text");
                                comand1 = new OleDbCommand(Select1, StrCon);
                                adapter1 = new OleDbDataAdapter(comand1);
                                //Открываем подключение
                                StrCon.Open();

                                adapter1.Fill(MyDataSet);
                                //Заполняем обект datagridview для отображения данных на форме
                                dataGridView1.DataSource = MyDataSet.Tables[0];
                                StrCon.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Возникла ошибка : " + ex.Message);
                    }
                }*/


        DataSet MyDataSet = new DataSet("BookStore");
        DataTable booksTable = new DataTable("Books");


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

           
            // добавляем таблицу в dataset
            MyDataSet.Tables.Add(booksTable);

            // создаем столбцы для таблицы Books
            DataColumn idColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            idColumn.Unique = true; // столбец будет иметь уникальное значение
            idColumn.AllowDBNull = false; // не может принимать null
            idColumn.AutoIncrement = true; // будет автоинкрементироваться
            idColumn.AutoIncrementSeed = 1; // начальное значение
            idColumn.AutoIncrementStep = 1; // приращении при добавлении новой строки

            DataColumn nameColumn = new DataColumn("Name", Type.GetType("System.String"));
            DataColumn priceColumn = new DataColumn("Price", Type.GetType("System.Decimal"));
            priceColumn.DefaultValue = 100; // значение по умолчанию
            DataColumn discountColumn = new DataColumn("Discount", Type.GetType("System.Decimal"));
            discountColumn.Expression = "Price * 0.2";

            booksTable.Columns.Add(idColumn);
            booksTable.Columns.Add(nameColumn);
            booksTable.Columns.Add(priceColumn);
            booksTable.Columns.Add(discountColumn);
            // определяем первичный ключ таблицы books
            booksTable.PrimaryKey = new DataColumn[] { booksTable.Columns["Id"] };

            DataRow row = booksTable.NewRow();
            row.ItemArray = new object[] { null, "Война и мир", 200 };
            booksTable.Rows.Add(row); // добавляем первую строку
            booksTable.Rows.Add(new object[] { null, "Отцы и дети", 170 }); // добавляем вторую строку

            Console.Write("\tИд \tНазвание \tЦена \tСкидка");
            Console.WriteLine();
            foreach (DataRow r in booksTable.Rows)
            {
                foreach (var cell in r.ItemArray)
                    Console.Write("\t{0}", cell);
                Console.WriteLine();
            }
            Console.Read();


        }






        }
}