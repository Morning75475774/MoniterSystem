using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModDashboard
{
    public partial class SetDataRangeForm : Form
    {
        public SetDataRangeForm()
        {
            InitializeComponent();
        }

        public float VoltageStart { get; set; }
        public float VoltageEnd { get; set; }
        public float ElectricityStart { get; set; }
        public float ElectricityEnd { get; set; }

        public float VoltageUpper { get; set; }
        public float VoltageLower { get; set; }
        public float ElectricityUpper { get; set; }
        public float ElectricityLower { get; set; }

        public Color VoltageColor { get; set; }
        public Color ElectricitColor { get; set; }

        public void Init(float vLower = -1, float vUpper = -1, float eLower = -1, float eUpper = -1, float vStart = -1, float vEnd = -1, float eStart = -1, float eEnd = -1)
        {
            VoltageUpper = vUpper;
            VoltageLower = vLower;
            ElectricityLower = eLower;
            ElectricityUpper = eUpper;

            VoltageStart = vStart;
            VoltageEnd = vEnd;
            ElectricityStart = eStart;
            ElectricityEnd = eEnd;

            this.tb1.Text = vLower == -1 ? "" : vLower.ToString();
            this.tb2.Text = vUpper == -1 ? "" : vUpper.ToString();
            this.tb3.Text = eLower == -1 ? "" : eLower.ToString();
            this.tb4.Text = eUpper == -1 ? "" : eUpper.ToString();

            this.tb5.Text = vStart == -1 ? "" : vStart.ToString();
            this.tb6.Text = vEnd == -1 ? "" : vEnd.ToString();
            this.tb7.Text = eStart == -1 ? "" : eStart.ToString();
            this.tb8.Text = eEnd == -1 ? "" : eEnd.ToString();
        }

        public void SetColor(Color vColor, Color eColor)
        {
            ElectricitColor = eColor;
            VoltageColor = vColor;
            this.DYColorPanel.BackColor = vColor;
            this.DLColorPanel.BackColor = eColor;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            float value = 0;
            // 电压下限
            if (float.TryParse(tb1.Text, out value))
            {
                VoltageLower = value;
            }

            //电压上限
            if (float.TryParse(tb2.Text, out value))
            {
                VoltageUpper = value;
            }
            // 电流下限
            if (float.TryParse(tb3.Text, out value))
            {
                ElectricityLower = value;
            }

            //电流上限
            if (float.TryParse(tb4.Text, out value))
            {
                ElectricityUpper = value;
            }
            // 电压起点
            if (float.TryParse(tb5.Text, out value))
            {
                VoltageStart = value;
            }

            //电压终点
            if (float.TryParse(tb6.Text, out value))
            {
                VoltageEnd = value;
            }
            // 电流起点
            if (float.TryParse(tb7.Text, out value))
            {
                ElectricityStart = value;
            }

            //电流终点
            if (float.TryParse(tb8.Text, out value))
            {
                ElectricityEnd = value;
            }
            VoltageColor = DYColorPanel.BackColor;
            ElectricitColor = DLColorPanel.BackColor;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DLColorPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                DLColorPanel.BackColor = colorDialog1.Color;
            }

        }

        private void DYColorPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                DYColorPanel.BackColor = colorDialog1.Color;
            }

        }
    }
}
