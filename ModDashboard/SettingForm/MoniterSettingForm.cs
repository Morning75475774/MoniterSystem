using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModDashboard.SettingForm
{
    public partial class MoniterSettingForm : Form
    {
        public MoniterSettingForm()
        {
            InitializeComponent();
            this.comboBox1.SelectedValue = "1000";
        }
        [DefaultValue(1000)]
        public int Interval
        {
            get;
            set;
        }

        [DefaultValue(0000)]
        public string Address
        {
            get; set;
        }
        [DefaultValue(2)]
        public ushort Length
        {
            get;
            set;
        }
        /// <summary>
        /// 电流的数据转换公式
        /// </summary>
        [DefaultValue("$0000$*0.01")]
        public string ElectricityExpress
        {
            get;
            set;
        }
        /// <summary>
        /// 电压的数据转换公式
        /// </summary>
        [DefaultValue("$0001$*0.01")]
        public string VoltageExpress
        {
            get;
            set;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.comboBox1.SelectedValue = "1000";
            this.comboBox1.Text = "1000";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(this.comboBox1.Text.ToString(), out int i))
            {
                MessageBox.Show("监控频率输入错误！");
                return;
            }
            Interval = i;
            if (!int.TryParse(this.tbAddress.Text, out int add))
            {
                MessageBox.Show("地址输入错误！输入格式为4位数字，例如0000");
                return;
            }
            Address = this.tbAddress.Text;
            if (!ushort.TryParse(this.tbLength.Text, out ushort length))
            {
                MessageBox.Show("长度输入错误！输入格式为整数，例如2");
                return;
            }
            Length = length;

            ElectricityExpress = this.textBox1.Text;
            VoltageExpress = this.textBox2.Text;


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
