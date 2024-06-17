using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneCRM
{
    public partial class TestFormCombox : Form
    {
        public TestFormCombox()
        {
            InitializeComponent();
            comboBox1.Items.Add(new ComboBoxItem("Value1", "Item 1"));
            comboBox1.Items.Add(new ComboBoxItem("Value2", "Item 2"));
            comboBox1.Items.Add(new ComboBoxItem("Value3", "Item 3"));

            // Select the first item by default
            comboBox1.SelectedIndex = 0;
            comboBox1.MouseWheel += ComboBox_MouseWheel;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox1.SelectedItem;
            // Access value and text properties
            string value = selectedItem.Value;
            string text = selectedItem.Text;

            // Example: Display the selected value and text
          
        }
        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            // Prevent the ComboBox value from changing when scrolling the mouse
            ((HandledMouseEventArgs)e).Handled = true;
        }
    }

    public class ComboBoxItem
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public ComboBoxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        // Override ToString method to display text when item is shown in ComboBox
        public override string ToString()
        {
            return Text;
        }
    }
}
