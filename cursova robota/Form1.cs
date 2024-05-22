using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Globalization;
using System.Xml;

namespace cursova_robota
{

    public partial class Form1 : Form
    {
        List<Poster> poster = new List<Poster>();

        private DataSet dataSet;
        public Form1()
        {
            InitializeComponent();
            dataSet = new DataSet();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void LoadDataFromXml(string xmlFilePath) //МЕТОД ПОСИЛАННЯ ХМЛ
        {
            /*  try
              {
                  dataSet.Clear(); //очищення
                  dataSet.ReadXml(xmlFilePath);

                  DataTable dt = new DataTable(); //правильний поряддок для стовпців табл
                  dt.Columns.Add("Name", typeof(string));
                  dt.Columns.Add("Date", typeof(string));
                  dt.Columns.Add("Genre", typeof(string));
                  dt.Columns.Add("Start", typeof(string));
                  dt.Columns.Add("Duration", typeof(string));
                  dt.Columns.Add("End", typeof(string));

                  foreach (DataRow cinemaRow in dataSet.Tables["Poster"].Rows)
                  {
                      foreach (DataRow posterRow in cinemaRow.GetChildRows("Cinema_Poster"))
                      {
                          // ЗАПОВНЕННЯ ТАБЛИЧКИ
                          DataRow newRow = dt.NewRow();
                          newRow["Name"] = (string)posterRow["Name"];
                          newRow["Date"] = (string)posterRow["Date"];
                          newRow["Genre"] = (string)posterRow["Genre"];
                          newRow["Start"] = (string)posterRow["Start"];
                          newRow["Duration"] = (string)posterRow["Duration"];
                          newRow["End"] = (string)posterRow["End"];

                          dt.Rows.Add(newRow); //додавання рядка
                      }
                  }
                  dataGridView1.DataSource = dt;
              }
              catch (Exception ex)
              {
                  MessageBox.Show("помилка при завантажені даних: " + ex.Message); //помилка
              }*/
             
                XmlDocument xmlDocument = new XmlDocument();
                string path = "C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile1.xml";
                xmlDocument.Load(path);

                DataTable dt = new DataTable();
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Genre", typeof(string));
                dt.Columns.Add("Start", typeof(string));
                dt.Columns.Add("Duration", typeof(int));
                dt.Columns.Add("End", typeof(string));

                XmlNodeList posterNodes = xmlDocument.SelectNodes("//Poster");

                foreach (XmlNode node in posterNodes)
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = node["Name"].InnerText;
                    dr["Date"] = node["Date"].InnerText;
                    dr["Genre"] = node["Genre"].InnerText;
                    dr["Start"] = node["Start"].InnerText;
                    dr["Duration"] = int.Parse(node["Duration"].InnerText);
                    dr["End"] = node["End"].InnerText;
                    dt.Rows.Add(dr);
                }

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;
             
        }


        private void addBut_Click(object sender, EventArgs e) //add button
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("заповніть всі поля.", "помилка.");
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = dateTimePicker1.Value;
                dataGridView1.Rows[n].Cells[2].Value = textBox2.Text;
                dataGridView1.Rows[n].Cells[3].Value = textBox3.Text;
                dataGridView1.Rows[n].Cells[4].Value = textBox4.Text;
                dataGridView1.Rows[n].Cells[5].Value = textBox5.Text;

            }
        }

        private void saveBut_Click(object sender, EventArgs e)// кнопка збереження в хмл
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable(); //правильний поряддок для стовпців табл
                dt.TableName = "Poster";
                dt.Columns.Add("Name");
                dt.Columns.Add("Date");
                dt.Columns.Add("Genre");
                dt.Columns.Add("Start");
                dt.Columns.Add("Duration");
                dt.Columns.Add("End");
                ds.Tables.Add(dt);


                foreach (DataGridViewRow r in dataGridView1.Rows)

                {
                    DataRow row = ds.Tables["Poster"].NewRow();
                    row["Name"] = r.Cells[0].Value;
                    row["Date"] = r.Cells[1].Value;
                    row["Genre"] = r.Cells[2].Value;
                    row["Start"] = r.Cells[3].Value;
                    row["Duration"] = r.Cells[4].Value;
                    row["End"] = r.Cells[5].Value;
                    ds.Tables["Poster"].Rows.Add(row);

                }
                ds.WriteXml("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile1.xml");
                MessageBox.Show("XML файл збережено", "готово");

            }
            catch
            {
                MessageBox.Show("Помилка при збережені"); //помилка
            }

        }

        private void deleteBut_Click(object sender, EventArgs e) // delete button
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Оберіть строку для видалення.", "Сталася помилка.");//помилка
            }
        }

        private void downloadBut_Click(object sender, EventArgs e) //download from xml
        {
            /* if (File.Exists("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile1.xml"))
             {
                 DataSet ds = new DataSet();
                 ds.ReadXml("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile1.xml");


                 foreach (DataRow item in ds.Tables["Poster"].Rows)
                 {
                     int n = dataGridView1.Rows.Add();
                     dataGridView1.Rows[n].Cells[0].Value = item["Name"];
                     dataGridView1.Rows[n].Cells[1].Value = item["Date"];
                     dataGridView1.Rows[n].Cells[2].Value = item["Genre"];
                     dataGridView1.Rows[n].Cells[3].Value = item["Start"];
                     dataGridView1.Rows[n].Cells[4].Value = item["Duration"];
                     dataGridView1.Rows[n].Cells[5].Value = item["End"];
                     dataGridView1.Sort(dataGridView1.Columns["Name"], System.ComponentModel.ListSortDirection.Ascending);
                 }
             }
             else
             {
                 MessageBox.Show("сталася помилка."); //помилка
             } */
            LoadDataFromXml("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile2.xml");
           // dataGridView1.Sort(dataGridView1.Columns["Name"], System.ComponentModel.ListSortDirection.Ascending);

        }

        private void clearButt_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); //очистка
        }



        public struct Poster                   //структура
        {
            public string Film { get; set; }
            public string Date { get; set; }
            public string Genre { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public int Duration { get; set; }
            public string Day { get; set; }
            public string Cinema { get; set; }

        }

        private void DataFromXml(string filePathXmL) //path 1 xml file
        {
            XmlDocument xmlDocAllMovies = new XmlDocument();
            xmlDocAllMovies.Load("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile1.xml");

            XmlNodeList itemNodes = xmlDocAllMovies.SelectNodes("//Poster");

            foreach (XmlNode elem in xmlDocAllMovies.DocumentElement)
            {
                string nameFilm = string.Format(elem["Name"].InnerText);
                string date = string.Format(elem["Date"].InnerText);
                string genre = string.Format(elem["Genre"].InnerText);
                string timestart = string.Format(elem["Start"].InnerText);
                int duration = Convert.ToInt32(elem["Duration"].InnerText);
                string timeend = string.Format(elem["End"].InnerText);


                poster.Add(new Poster
                {
                    Film = nameFilm,
                    Genre = genre,
                    Date = date,
                    Start = timestart,
                    Duration = duration,
                    End = timeend,
                });
            }
        }
        private void DataFromXml2(string filePathXmL) //path file xml 2
        {
            XmlDocument xmlDocAllMovies = new XmlDocument();
            xmlDocAllMovies.Load("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile2.xml");

            XmlNodeList itemNodes = xmlDocAllMovies.SelectNodes("//Poster");

            foreach (XmlNode elem in xmlDocAllMovies.DocumentElement)
            {
                string nameFilm = string.Format(elem["Name"].InnerText);
                string date = string.Format(elem["Date"].InnerText);
                string genre = string.Format(elem["Genre"].InnerText);
                string timestart = string.Format(elem["Start"].InnerText);
                int duration = Convert.ToInt32(elem["Duration"].InnerText);
                string timeend = string.Format(elem["End"].InnerText);
                string day = string.Format(elem["Day"].InnerText);
                string cinema = string.Format(elem["Cinema"].InnerText);

                poster.Add(new Poster
                {
                    Film = nameFilm,
                    Genre = genre,
                    Date = date,
                    Start = timestart,
                    Duration = duration,
                    End = timeend,
                    Day = day,
                    Cinema = cinema,
                });
            }
        }

        private void EndOfFilm() // 1 TASK, закінчення сеансу
        {
            DataFromXml2("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile2.xml");

            dataGridView2.ColumnCount = 4;
            dataGridView2.Columns[0].Name = "Name";
            dataGridView2.Columns[1].Name = "Date";
            dataGridView2.Columns[2].Name = "Start";
            dataGridView2.Columns[3].Name = "End";

            dataGridView2.Rows.Clear(); //очистка
            foreach (var poster in poster)
            {
                dataGridView2.Rows.Add(poster.Film, poster.Date, poster.Start, poster.End);
            }
        }

        private void EndradioBut_CheckedChanged_1(object sender, EventArgs e) //кнопка перемикач
        {
            if (EndradioBut.Checked == true)
            {
                searchBut.Enabled = true;
            }
        }

        private void searchBut_Click_1(object sender, EventArgs e) //кнопка пошуку
        {
            if (EndradioBut.Checked)
            {
                EndOfFilm();
            }

        }

        private void weekendRadioBut_CheckedChanged_1(object sender, EventArgs e) //перемикач кнопки
        {
            if (weekendRadioBut.Checked == true)
            {
                searchBut2.Enabled = true;
            }

        }

        private void searchBut2_Click_1(object sender, EventArgs e) //кнопка пошуку
        {
            if (weekendRadioBut.Checked)
            {
                WeekendSession();
            }
        }

        private void searchBut3_Click_1(object sender, EventArgs e) //TASK 3d, AMOUNT SESSION + SESSION PER DAY

        {

            string date = dateTimePicker2.Text;
            List<Poster> filmsOnDAte = poster.Where(f => f.Date == date).ToList();

            if (filmsOnDAte.Any())
            {
                int totalDuration = filmsOnDAte.Sum(f => f.Duration);
                int count = filmsOnDAte.Count;
                double averageDuration = count > 0 ? (double)totalDuration / count : 0;

                SessionPerDayLbl.Text = $"{count}";
                AmountSessionLbl.Text = $"{averageDuration:F2}";
            }
            else
            {
                SessionPerDayLbl.Text = "-";
                AmountSessionLbl.Text = "-";
            }
        }

        private void searchBut4_Click_1(object sender, EventArgs e) // 4th Task, Choosing kinoshka
        {
            string selectedCinema = ChoosingCinemaCBox.Text;
            string date = dateTimePicker3.Text;
            dataGridView2.Rows.Clear();  //clear

            InsertionSortByStartTime(poster);

            var filteredMovies = poster.Where(movie => movie.Cinema == selectedCinema && movie.Date == date).ToList();
            foreach (var movie in filteredMovies)
            {
                dataGridView2.Rows.Add(movie.Film, movie.Day, movie.Date, movie.Start, movie.Duration, movie.End);
            }

        }

























        private void WeekendSession() //TASK 2, WEEKEND
        {
            DataFromXml2("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFile2.xml");

            dataGridView2.ColumnCount = 6;
            dataGridView2.Columns[0].Name = "Name";
            dataGridView2.Columns[1].Name = "Day";
            dataGridView2.Columns[2].Name = "Date";
            dataGridView2.Columns[3].Name = "Start";
            dataGridView2.Columns[4].Name = "Duration";
            dataGridView2.Columns[5].Name = "End";

            dataGridView2.Rows.Clear(); // CLEAR

            var weekendPosters = poster.Where(p => p.Day == "Saturday" || p.Day == "Sunday");

            foreach (var poster in weekendPosters)
            {
                dataGridView2.Rows.Add(poster.Film, poster.Day, poster.Date, poster.Start, poster.Duration, poster.End);
            }

        }
       
        private void InsertionSortByStartTime(List<Poster> posters) //insertion sort
        {
            int n = posters.Count;

            for (int i = 1; i < n; ++i)
            {
                Poster key = posters[i];
                int j = i - 1;

                DateTime zxcsa = DateTime.ParseExact(key.Start, "HH:mm", CultureInfo.InvariantCulture);

                while (j >= 0 && DateTime.ParseExact(posters[j].Start, "HH:mm", CultureInfo.InvariantCulture) > zxcsa)
                {
                    posters[j + 1] = posters[j];
                    j = j - 1;
                }
                posters[j + 1] = key;
            }
        }

        private void AsiaBut_Click(object sender, EventArgs e)
        {

            if (File.Exists("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFileAsia.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLFileAsia.xml");

                foreach (DataRow item in ds.Tables["Movie"].Rows)
                {
                    int n = dataGridView3.Rows.Add();
                    dataGridView3.Rows[n].Cells[0].Value = item["Name"];
                    dataGridView3.Rows[n].Cells[1].Value = item["Date"];
                    dataGridView3.Rows[n].Cells[2].Value = item["Genre"];
                    dataGridView3.Rows[n].Cells[3].Value = item["Start"];
                    dataGridView3.Rows[n].Cells[4].Value = item["Duration"];
                    dataGridView3.Rows[n].Cells[5].Value = item["End"];

                    dataGridView3.Sort(dataGridView3.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            else
            {
                MessageBox.Show("сталася помилка."); //error
            }
        }

        private void EuropeBut_Click(object sender, EventArgs e)
        {
            if (File.Exists("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLEurope.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLEurope.xml");
                foreach (DataRow item in ds.Tables["Movie"].Rows)
                {
                    int n = dataGridView3.Rows.Add();
                    dataGridView3.Rows[n].Cells[0].Value = item["Name"];
                    dataGridView3.Rows[n].Cells[1].Value = item["Date"];
                    dataGridView3.Rows[n].Cells[2].Value = item["Genre"];
                    dataGridView3.Rows[n].Cells[3].Value = item["Start"];
                    dataGridView3.Rows[n].Cells[4].Value = item["Duration"];
                    dataGridView3.Rows[n].Cells[5].Value = item["End"];

                    dataGridView3.Sort(dataGridView3.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            else
            {
                MessageBox.Show("сталася помилка."); //error
            }
        }

        private void AnimBut_Click(object sender, EventArgs e)
        {
            if (File.Exists("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLAnimation.xml"))
            {
                DataSet ds = new DataSet();
                ds.ReadXml("C:\\Users\\HP\\source\\repos\\cursova robota\\cursova robota\\XMLAnimation.xml");

                foreach (DataRow item in ds.Tables["Movie"].Rows)
                {
                    int n = dataGridView3.Rows.Add();
                    dataGridView3.Rows[n].Cells[0].Value = item["Name"];
                    dataGridView3.Rows[n].Cells[1].Value = item["Date"];
                    dataGridView3.Rows[n].Cells[2].Value = item["Genre"];
                    dataGridView3.Rows[n].Cells[3].Value = item["Start"];
                    dataGridView3.Rows[n].Cells[4].Value = item["Duration"];
                    dataGridView3.Rows[n].Cells[5].Value = item["End"];

                    dataGridView3.Sort(dataGridView3.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            else
            {
                MessageBox.Show("сталася помилка."); //error
            }

        }

        private void ClearBut_Click(object sender, EventArgs e) //clear but 
        {
            dataGridView3.Rows.Clear();
        }

       
    }
}
