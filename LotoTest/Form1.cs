namespace LotoTest
{
    public partial class Form1 : Form
    {
        private const int maxSelection = 7;
        private List<Button> selectedButtons = new List<Button>();

        //List<int> parsedValues = new List<int>();

        public Form1()
        {
            InitializeComponent();
            InitializeButtons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeButtons()
        {
            foreach (Button button in selectedButtons)
            {
                button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedBtn = (Button)sender;

            if (selectedButtons.Contains(clickedBtn))
            {
                selectedButtons.Remove(clickedBtn);
                clickedBtn.BackColor = SystemColors.Control;
                clickedBtn.ForeColor = SystemColors.ControlText;
            }
            else
            {
                if (selectedButtons.Count < maxSelection)
                {
                    selectedButtons.Add(clickedBtn);
                    clickedBtn.BackColor = Color.Red;
                    clickedBtn.ForeColor = Color.White;
                    if (selectedButtons.Count == maxSelection)
                    {
                        DialogResult result = MessageBox.Show
                            ("Odabrali ste 7/7 brojeva.\n\nDa li želite sortiranje brojeva?", "LOTO 7/39",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            SortNumbersInLabels();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Izabrali ste 7 brojeva. Proverite svoju kombinaciju!", "Loto 7/39", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //SortNumbers();
            //SortNumbersInLabels();

        }
        private void SortNumbers()
        {
            List<int> parsedValues = new List<int>();

            // Vrednost teksta sa dugmadi pretvotiri u broj
            foreach (Button button in selectedButtons)
            {
                int parsedValue;
                if (int.TryParse(button.Text, out parsedValue))
                {
                    parsedValues.Add(parsedValue);
                }
            }

            // Sortiranje brojeva uzlazno
            parsedValues.Sort();

            // Rasporediti brojeve hronoloski u odgovarajuće RTB (Lbl, Txt...), od najmanjeg ka najvecem (1-7)

            for (int i = 0; i < Math.Min(parsedValues.Count, 7); i++)
            {
                string richTextBoxName = "richTextBox" + (i + 1);
                RichTextBox richTextBox = Controls.Find(richTextBoxName, true).FirstOrDefault() as RichTextBox;
                richTextBox.SelectionAlignment = HorizontalAlignment.Center;

                richTextBox.ForeColor = Color.Blue;

                if (richTextBox != null)
                {
                    richTextBox.Text = parsedValues[i].ToString();
                }
            }
        }
        private void SortNumbersInLabels()
        {
            List<int> parsedValues = new List<int>();

            foreach (Button button in selectedButtons)
            {
                int parsedValue;
                if (int.TryParse(button.Text, out parsedValue))
                {
                    parsedValues.Add(parsedValue);
                }
            }
            parsedValues.Sort();
            for (int i = 0; i < parsedValues.Count; i++)
            {
                string labelName = "label" + (i + 1);
                Label label = Controls.Find(labelName, true).FirstOrDefault() as Label;
                if (label != null)
                {
                    label.Text = parsedValues[i].ToString();
                }
            }
            //for (int i = 0; i < Math.Min(parsedValues.Count, 7); i++)
            //{
            //    string labelName = "label" + (i + 1);
            //    Label label = Controls.Find(labelName, true).FirstOrDefault() as Label;

            //    if (label != null)
            //    {
            //        label.Text = parsedValues[i].ToString();
            //    }
            //}
        }

        private void RandomNumbers()
        {
            Random random = new Random();
            List<int> randomNumbers = new List<int>();

            while (randomNumbers.Count < 7)
            {
                int randomNumber = random.Next(1, 40);
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers.Add(randomNumber);
                }
            }
            randomNumbers.Sort();

            for (int i = 0; i < randomNumbers.Count; i++)
            {
                string labelName = "label" + (i + 1);
                Label label = Controls.Find(labelName, true).FirstOrDefault() as Label;
                if (label != null)
                {
                    label.Text = randomNumbers[i].ToString();
                }
            }
        }
        private void btnAuto_Click(object sender, EventArgs e)
        {
            foreach (Button button in selectedButtons)
            {
                button.Enabled = false;
                button.BackColor = SystemColors.Control;
                button.ForeColor = SystemColors.ControlText;
            }
            selectedButtons.Clear();

            RandomNumbers();

            foreach (Control control in Controls)
            {
                if (control is Button && control != btnAuto && control != btnSave && control != btnClear)
                {
                    control.Enabled = false;
                    control.BackColor = SystemColors.Control;
                    control.ForeColor = SystemColors.ControlText;
                }
            }
            btnClear.Enabled = true;
            btnSave.Enabled = true;
            btnAuto.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            selectedButtons.Clear();

            for (int i = 1; i <= 7; i++)
            {
                string labelName = "label" + i;
                Label label = Controls.Find(labelName, true).FirstOrDefault() as Label;

                if (label != null)
                {
                    label.Text = string.Empty;
                }
            }
            foreach (Button button in Controls.OfType<Button>())
            {
                button.Enabled = true;
                button.BackColor = SystemColors.Control;
                button.ForeColor = SystemColors.ControlText;
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (selectedButtons.Count != 7)
            {
                MessageBox.Show("Odaberite 7 brojeva!", "LOTO 7/39", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else SortNumbersInLabels();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<int> selectedNumbers;
            
            //if (selectedButtons.Count != 7)
            if (selectedButtons.Count == 7)
            {
                //MessageBox.Show("Odaberite 7 brojeva.", "LOTO 7/39", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                selectedNumbers = selectedButtons.Select(button => int.Parse(button.Text)).ToList();
            }
            else
            {
                selectedNumbers = RandomNumbersFromLabels();
            }

            //List<int> selectedNumbers = selectedButtons.Select(button => int.Parse(button.Text)).ToList();

            LotteryCombination lotteryCombination = new LotteryCombination
            {
                Numbers = selectedNumbers,
                DrawDate = DateTime.Now
            };

            using (var dbContext = new LottoDbContext())
            {
                dbContext.LotteryCombinations.Add(lotteryCombination);
                dbContext.SaveChanges();
            }

            MessageBox.Show("Kombinacija je sačuvana.", "LOTO 7/39", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // uraditi listu onih vrednosti/brojeva koji su vec u lejblima
        private List<int> RandomNumbersFromLabels()
        {
            List<int> randomNumbersFromLabels = new List<int>();

            for (int i = 1; i <= 7; i++)
            {
                string labelName = "label" + i;
                Label label = Controls.Find(labelName, true).FirstOrDefault() as Label;

                if (label != null && int.TryParse(label.Text, out int parsedValue))
                {
                    randomNumbersFromLabels.Add(parsedValue);
                }
                
                else
                {
                    MessageBox.Show("Neuspešno učitavanje brojeva", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<int>(); 
                }
            }

            return randomNumbersFromLabels;
        }
    }

}