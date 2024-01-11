using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace EVM_3
{
    public partial class Form1 : Form
    {
        string tableName;
        MySqlConnection connection;
        string connectionString = "server = localhost; user = root; password = TyX-3ae-sdd-qLT; database = evm_26";

        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*            DBConnection dBConnection = new DBConnection();
                        // MySqlCommand mysql_query = dBConnection.getConnection().CreateCommand();
                        MySqlCommand mysql_query = DBConnection.msCommand;
                        DBConnection.msCommand = mysql_query;
                        mysql_query.CommandText = "SELECT * FROM evm_26.писатель;";
                        // Запрос
                        MySqlDataReader mysql_result = mysql_query.ExecuteReader();
                        // Метод перехода строк 
                        while (mysql_result.Read())
                        {

                        }*/
            tableName = "писатель";
            VisibleButton();

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                string query = "SELECT * FROM писатель";
                LoadData(query);
            }
            else
            {
                // 11 012375 Боряков Боряк Борякович Дачная,4 79895674561
                string count = textBox1.Text.Trim();
                string[] value = count.Split(' ');
                string query = "INSERT INTO писатель (id_Писатель, Номер_паспорта, Фамилия, Имя, Отчество, Домашний_адрес, Телефон) VALUES ('" + value[0] + "', '" + value[1] + "', '" + value[2] + "', '" + value[3] + "', '" + value[4] + "', '" + value[5] + "', '" + value[6] + "');";
                LoadData(query);
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableName = "книга";
            VisibleButton();

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                string query = "SELECT * FROM книга";
                LoadData(query);
            }
            else
            {   // 2099 L-12 Котик 2210 20.09.2023 391 800 1234
                // 1999 X-47 Мышка 2200 2023-09-19 332 700 1129
                string count = textBox1.Text.Trim();
                string[] value = count.Split(' ');
                string query = "INSERT INTO Книга (id_Книга, Шифр, Название, Тираж, Дата_выхода, Себестоимость, Цена_продажи, Гонорар) values (" + value[0] + ", '" + value[1] + "', '" + value[2] + "', " + value[3] + ", '" + value[4] + "', " + value[5] + ", " + value[6] + ", " + value[7] + ");";
                LoadData(query);
                textBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tableName = "заказ";
            VisibleButton();
            string query = "SELECT * FROM заказ";
            LoadData(query);
        }

        private void InitializeDatabaseConnection()
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MessageBox.Show("Connection successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadData(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteRow()
        {
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    string idName = dataGridView1.Columns[0].Name;
                    // Получение значения ID из выбранной строки
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);

                    // Удаление строки из базы данных
                    var deletequery = $"DELETE FROM {tableName} WHERE {idName} = {id}";

                    try
                    {
                        // Создаем и выполняем запрос
                        MySqlCommand cmd = new MySqlCommand(deletequery, connection);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        // Удаление строки из источника данных и обновление DataGridView
                        ((DataTable)dataGridView1.DataSource).Rows[index].Delete();
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = ((DataTable)dataGridView1.DataSource);

                        MessageBox.Show("Строка успешно удалена из базы данных.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении строки: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления.");
                }
            }
        }

        private void UpdateRow()
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var newValue1 = textBox1.Text;

                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int columnIndex = dataGridView1.SelectedCells[0].ColumnIndex;
                string idName = dataGridView1.Columns[0].Name;

                // Получаем ID строки
                var id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[idName].Value);

                
                // Устанавливаем новое значение в ячейку
                dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = newValue1;
                var updateQuery = "";
                // Обновление значения в базе данных
                string columnName = dataGridView1.Columns[columnIndex].Name;
                if (int.TryParse(newValue1, out int newValue))
                {
                    updateQuery = $"UPDATE {tableName} SET {columnName} = {newValue} WHERE {idName} = {id}";
                }
                else
                {
                    updateQuery = $"UPDATE {tableName} SET {columnName} = '{newValue}' WHERE {idName} = {id}";
                }
                textBox1.Text = "";
                try
                {
                    // Создаем и выполняем запрос
                    MySqlCommand cmd = new MySqlCommand(updateQuery, connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show($"Значение в выбранной ячейке и в базе данных успешно изменено на {newValue}.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Выберите ячейку для изменения значения.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            connection.Close();
            UpdateRow();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            connection.Close();
            DeleteRow();
        }
        // Отношение
        private void button4_Click(object sender, EventArgs e)
        {
            VisibleButton();
            tableName = "писатель_has_книга";

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                string query = "SELECT * FROM Писатель_has_Книга";
                LoadData(query);
            }
            else
            {
                string count = textBox1.Text.Trim();
                string[] value = count.Split(' ');
                string query = "INSERT INTO писатель_has_книга (Писатель_ID_Писатель, Книга_ID_Книга) VALUES ('" + value[0] + "', '" + value[1] + "');";
                LoadData(query);
                textBox1.Text = "";
            }
        }
        // Заказчик
        private void button5_Click(object sender, EventArgs e)
        {
            VisibleButton();
            tableName = "заказчик";

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                string query = "SELECT * FROM заказчик";
                LoadData(query);
            }
            else
            {
                // 11 012375 Боряков Боряк Борякович Дачная,4 79895674561
                string count = textBox1.Text.Trim();
                string[] value = count.Split(' ');
                string query = "INSERT INTO заказчик (id_Заказчик, Название, Адрес, Телефон, Обращаться) values (" + value[0] + ", '" + value[1] + "', '" + value[2] + "', " + value[3] + ", " + value[4] + ");";
                LoadData(query);
                textBox1.Text = "";
            }
        }
        // Контракт
        private void button6_Click(object sender, EventArgs e)
        {
            VisibleButton();
            tableName = "контракт";

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                string query = "SELECT * FROM контракт";
                LoadData(query);
            }
            else
            {
                // 11 012375 Боряков Боряк Борякович Дачная,4 79895674561
                string count = textBox1.Text.Trim();
                string[] value = count.Split(' ');
                string query = "INSERT INTO контракт (id_Контракт, Номер_контракта, Дата_заключения, Срок, Расторгнут, Дата_расторжения, Писатель_id_Писатель) values (" + value[0] + ", '" + value[1] + "', '" + value[2] + "', " + value[3] + ", '" + value[4] + "', " + value[5] + ", " + value[6] + ");";
                textBox1.Text = "";
            }
        }
        // Задание 5
        private void LoadData1()
        {
            string query = "SELECT Заказ.Номер, Книга.Название, Писатель.Фамилия, Писатель.Имя FROM Заказ JOIN Книга ON Заказ.Книга_id_Книга = Книга.id_Книга JOIN писатель_has_книга ON книга.id_Книга = писатель_has_книга.Книга_id_Книга JOIN писатель ON писатель_has_книга.Писатель_id_Писатель = писатель.id_Писатель;";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void LoadData2()
        {
            string query = "SELECT DISTINCT Обращаться FROM Заказчик; ";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void LoadData3()
        {
            string query = "SELECT Фамилия, Имя FROM Писатель ORDER BY Фамилия; ";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            InVisibleButton();
            LoadData1();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            InVisibleButton();
            LoadData2();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            InVisibleButton();
            LoadData3();
        }
        //

        void VisibleButton()
        {
            button7.Visible = true;
            button8.Visible = true;
        }

        void InVisibleButton()
        {
            button7.Visible = false;
            button8.Visible = false;
        }

        //
        private void Result1()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            string query = "WITH ПерваяСтрокаКаждогоЗаказчика AS (SELECT ЗАКАЗЧИК.id_заказчик, ЗАКАЗЧИК.название AS \"Заказчик\", MIN(ЗАКАЗ.id_заказ) AS \"Первый_Заказ\" FROM ЗАКАЗЧИК JOIN ЗАКАЗ ON ЗАКАЗЧИК.id_заказчик = ЗАКАЗ.заказчик_id_заказчик GROUP BY ЗАКАЗЧИК.id_заказчик, ЗАКАЗЧИК.название) SELECT CASE WHEN ЗАКАЗ.id_заказ = ПерваяСтрокаКаждогоЗаказчика.Первый_Заказ THEN ЗАКАЗЧИК.название ELSE '' END AS \"Заказчик\", КНИГА.название AS \"Название книги\", КНИГА.себестоимость AS \"Себестоимость, руб.\", КНИГА.цена_продажи AS \"Цена продажи, руб.\", ЗАКАЗ.колво_экземпляров AS \"Количество экземпляров\", (КНИГА.цена_продажи - КНИГА.себестоимость) * ЗАКАЗ.колво_экземпляров AS \"Прибыль от продажи книги, руб.\" FROM ЗАКАЗ JOIN ПерваяСтрокаКаждогоЗаказчика ON ЗАКАЗ.заказчик_id_заказчик = ПерваяСтрокаКаждогоЗаказчика.id_заказчик JOIN ЗАКАЗЧИК ON ЗАКАЗ.заказчик_id_заказчик = ЗАКАЗЧИК.id_заказчик JOIN КНИГА ON ЗАКАЗ.книга_id_книга = КНИГА.id_книга;";

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Добавляем логику для проверки и добавления строки "Итог"
                string текущийЗаказчик = string.Empty;
                string ResName = "";
                decimal суммаПрибыли = 0;
                decimal общаяСуммаПрибыли = 0;

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string текущийСтолбец = dataTable.Rows[i][0].ToString();

                    if (текущийСтолбец != текущийЗаказчик && текущийСтолбец != "")
                    {
                        ResName = текущийЗаказчик;
                        if (!string.IsNullOrEmpty(текущийЗаказчик))
                        {
                            // Добавляем строку "Итог" после каждого заказчика
                            DataRow итогСтрока = dataTable.NewRow();
                            итогСтрока[0] = $"Итого от {ResName}:";
                            итогСтрока[5] = суммаПрибыли; // Замените 5 на индекс столбца, в котором находится "Прибыль от продажи книги, руб."
                            dataTable.Rows.InsertAt(итогСтрока, i);
                            i++;
                        }

                        текущийЗаказчик = текущийСтолбец;
                        общаяСуммаПрибыли += суммаПрибыли;
                        суммаПрибыли = 0;
                    }

                    // Вычисляем сумму прибыли для каждой строки
                    суммаПрибыли += Convert.ToDecimal(dataTable.Rows[i]["Прибыль от продажи книги, руб."]);
                }

                // Добавляем "Итог" после последнего заказчика
                if (!string.IsNullOrEmpty(текущийЗаказчик))
                {
                    DataRow итогСтрока = dataTable.NewRow();
                    итогСтрока[0] = $"Итого от {текущийЗаказчик}:";
                    итогСтрока[5] = суммаПрибыли;
                    dataTable.Rows.Add(итогСтрока);
                    общаяСуммаПрибыли += суммаПрибыли;
                }

                DataRow общийИтогСтрока = dataTable.NewRow();
                общийИтогСтрока[0] = "Общий итог:";
                общийИтогСтрока[5] = общаяСуммаПрибыли;
                dataTable.Rows.Add(общийИтогСтрока);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        // 
        private void SaveToExcel(DataGridView dataGridView)
        {
            try
            {
                // Создаем новый объект Excel
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;

                // Добавляем новую книгу
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                // Заполняем ячейки данными из DataGridView
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView[j, i].Value != null)
                            worksheet.Cells[i + 1, j + 1] = dataGridView[j, i].Value.ToString();
                    }
                }

                // Сохраняем книгу
                workbook.SaveAs("Profit_from_book_sales.xlsx");

                // Очищаем ресурсы
                workbook.Close();
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Result1();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SaveToExcel(dataGridView1);
        }
    }

    class DBConnection
    {
        // Настройки соединения с базой данных:
        static string DBConnect = "server = localhost; user = root; password = TyX-3ae-sdd-qLT; database = evm_26";
        static public MySqlDataAdapter msDataAdapter;
        // Соединение с базой данных 
        static MySqlConnection myConnect;
        // Выполнение команд
        static public MySqlCommand msCommand;

        public static bool ConnectionDB()
        {
            try
            {
                // Соединение и открытие 
                myConnect = new MySqlConnection(DBConnect);
                myConnect.Open();
                // 
                msCommand = new MySqlCommand();
                //
                msCommand.Connection = myConnect;
                msDataAdapter = new MySqlDataAdapter(msCommand);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void CloseDB()
        {   
            // Закрытие
            myConnect.Close();
        }

        public MySqlConnection getConnection()
        {
            return myConnect;
        }
    }
}
