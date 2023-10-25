using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TouristCalculator
{
    public partial class MainForm : Form
    {
        Dictionary<string, double> cities = new Dictionary<string, double>
        {
            {"Берлин", 399},
            {"Прага", 300},
            {"Париж", 350},
            {"Рига", 250},
            {"Лондон", 390},
            {"Ватикан", 500},
            {"Палермо", 230},
            {"Варшава", 300},
            {"Кишинёв", 215},
            {"Мадрид", 260},
            {"Будапешт", 269}
        };

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var city in cities.Keys)
            {
                departureCityComboBox.Items.Add(city);
            }
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            string departureCity = departureCityComboBox.Text;
            int citiesToVisitCount = (int)citiesToVisitCountNumericUpDown.Value;
            List<string> citiesToVisit = new List<string>();

            for (int i = 0; i < citiesToVisitCount; i++)
            {
                citiesToVisit.Add(citiesToVisitComboBoxes[i].Text);
            }

            double totalCost = 0;

            if (departureCity == "Мадрид")
            {
                if (!citiesToVisit.Contains("Париж"))
                {
                    MessageBox.Show("Поездка из Мадрида обязательно включает в себя Париж.");
                    return;
                }
            }

            if (departureCity == "Кишинёв")
            {
                if (!citiesToVisit.Contains("Будапешт"))
                {
                    MessageBox.Show("Поездка из Кишинёва обязательно включает в себя Будапешт.");
                    return;
                }
            }

            foreach (var city in citiesToVisit)
            {
                totalCost += cities[city];
            }

            if (citiesToVisit.Contains("Ватикан"))
            {
                totalCost += 0.5 * totalCost; // Дополнительный налог на пребывание в Ватикане.
            }

            if (departureCity == "Палермо" || citiesToVisit.Contains("Палермо"))
            {
                if (departureCity == "Лондон")
                {
                    totalCost += 0.07 * totalCost; // Дополнительный налог для туристов из Лондона.
                }
                else if (departureCity == "Кишинёв")
                {
                    totalCost += 0.11 * totalCost; // Дополнительный налог для туристов из Кишинёва.
                }
            }

            if (departureCity == "Рига" && citiesToVisit.Contains("Париж"))
            {
                totalCost += 0.09 * totalCost; // Дополнительный налог для туристов из Парижа.
            }

            if (departureCity == "Палермо" && citiesToVisit.Contains("Рига"))
            {
                totalCost += 0.13 * totalCost; // Дополнительный налог для туристов из Риги.
            }

            if (IsNonEUCitizen(departureCity))
            {
                totalCost += 0.04 * totalCost; // Налог 4% для туристов не из ЕС.
            }

            if (departureCity == "Ватикан" || citiesToVisit.Contains("Ватикан"))
            {
                totalCost += 0.5 * totalCost; // Дополнительный налог на пребывание в Ватикане.
            }

            if (departureCity == "Берлин" || citiesToVisit.Contains("Берлин"))
            {
                totalCost += 0.13 * totalCost; // Дополнительный налог в Берлине 13%.
            }

            resultLabel.Text = $"Стоимость поездки: {totalCost:F2}";
        }

        // Функция для определения, является ли город отправления не из ЕС.
        private bool IsNonEUCitizen(string city)
        {
            // Здесь можно добавить условия для определения, является ли город отправления туриста из-за пределов Европейского Союза.
            // Например, если город не в ЕС, то вернуть true, иначе false.
            // В этом примере предполагается, что города не в ЕС.
            return city == "Мадрид" || city == "Палермо";
        }
    }
}