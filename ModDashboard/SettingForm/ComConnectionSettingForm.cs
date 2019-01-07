using HslCommunication.ModBus;
using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace ModDashboard.SettingForm
{
    public partial class ComConnectionSettingForm : Form
    {
        public ComConnectionSettingForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cbCom.DataSource = SerialPort.GetPortNames();
            try
            {
                cbCom.SelectedIndex = 0;
            }
            catch
            {
                cbCom.Text = "COM3";
            }
        }

        public bool IsConnect
        {
            get; set;
        }

        public ModbusRtu ModbusRtuClient
        {
            get
            {
                return this.busRtuClient;
            }
        }
        public void CloseConnection()
        {
            if (busRtuClient == null)
                return;
            if (busRtuClient.IsOpen())
                busRtuClient.Close();
        }
        private ModbusRtu busRtuClient = null;

        private void button1_Click(object sender, EventArgs e)
        {
            IsConnect = false;
            if (!int.TryParse(textBox2.Text, out int baudRate))
            {
                MessageBox.Show("波特率输入错误！");
                return;
            }

            if (!int.TryParse(textBox16.Text, out int dataBits))
            {
                MessageBox.Show("数据位输入错误！");
                return;
            }

            if (!int.TryParse(textBox17.Text, out int stopBits))
            {
                MessageBox.Show("停止位输入错误！");
                return;
            }


            //if (!byte.TryParse(textBox15.Text, out byte station))
            //{
            //    MessageBox.Show("站号输入不正确！");
            //    return;
            //}

            busRtuClient?.Close();
            busRtuClient = new ModbusRtu();
            busRtuClient.AddressStartWithZero = checkBox1.Checked;


            ComboBox2_SelectedIndexChanged(null, new EventArgs());
            // 字符串颠倒
            busRtuClient.IsStringReverse = false;


            try
            {
                busRtuClient.SerialPortInni(sp =>
                {
                    sp.PortName = cbCom.Text;
                    sp.BaudRate = baudRate;
                    sp.DataBits = dataBits;
                    sp.StopBits = stopBits == 0 ? System.IO.Ports.StopBits.None : (stopBits == 1 ? System.IO.Ports.StopBits.One : System.IO.Ports.StopBits.Two);
                    sp.Parity = comboBox1.SelectedIndex == 0 ? System.IO.Ports.Parity.None : (comboBox1.SelectedIndex == 1 ? System.IO.Ports.Parity.Odd : System.IO.Ports.Parity.Even);
                });
                busRtuClient.Open();

                button2.Enabled = true;
                button1.Enabled = false;
                IsConnect = true;


                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (busRtuClient != null)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.ABCD; break;
                    case 1: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.BADC; break;
                    case 2: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.CDAB; break;
                    case 3: busRtuClient.DataFormat = HslCommunication.Core.DataFormat.DCBA; break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            IsConnect = false;
            busRtuClient = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       
    }
}
