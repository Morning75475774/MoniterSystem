using ModDashboard.SettingForm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ModDashboard
{
    public partial class MainForm : Form
    {

        public MainForm()
        {

            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.monitorCtrl1.SetDataRange(-1, -1, -1, -1, 10, 30, 200, 300);
            this.monitorCtrl1.ElectricityExpress = "$0000$*0.01";
            this.monitorCtrl1.VoltageExpress = "$0001$*0.001"; ;
        }

        /// <summary>
        /// 程序关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {

                }
                catch
                {

                }
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonButtonExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void btnViewSetting_Click(object sender, EventArgs e)
        {
            SetDataRangeForm form = new SetDataRangeForm();
            form.Init(this.monitorCtrl1.VoltageLower, this.monitorCtrl1.VoltageUpper, this.monitorCtrl1.ElectricityLower, this.monitorCtrl1.ElectricityUpper,
                  this.monitorCtrl1.VoltageStart, this.monitorCtrl1.VoltageEnd, this.monitorCtrl1.ElectricityStart, this.monitorCtrl1.ElectricityEnd);

            form.SetColor(this.monitorCtrl1.VoltageColor, this.monitorCtrl1.ElectricitColor);
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.monitorCtrl1.SetDataRange(form.VoltageLower, form.VoltageUpper, form.ElectricityLower, form.ElectricityUpper, form.VoltageStart, form.VoltageEnd, form.ElectricityStart, form.ElectricityEnd);
                this.monitorCtrl1.SetCurveColor(form.VoltageColor, form.ElectricitColor);
            }
        }

        /// <summary>
        ///  连接设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ComConnectionSettingForm form = new ComConnectionSettingForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.IsConnect)
                {
                    this.monitorCtrl1.Connect(form.ModbusRtuClient);
                    this.btnConnect.Enabled = false;
                    this.btnDisConnect.Enabled = true;
                }
            }

        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            ///重置显示状态
            this.monitorCtrl1.DisConnect();
            ///关闭连接
            ComConnectionSettingForm form = new ComConnectionSettingForm();
            form.CloseConnection();

            ///重置按钮状态
            this.btnConnect.Enabled = true;
            this.btnDisConnect.Enabled = false;
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            this.monitorCtrl1.Stop();
            this.btnStart.Enabled = true;
            this.btnStop.Enabled = false;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.monitorCtrl1.Start())
            {
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
            }
        }

        /// <summary>
        /// 监控设置，包括地址、长度、频率、数据转换公式等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMonitorSetting_Click(object sender, EventArgs e)
        {
            MoniterSettingForm form = new MoniterSettingForm();
            form.Interval = this.monitorCtrl1.Interval;
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (this.monitorCtrl1.IsExcuting)
                {
                    if (MessageBox.Show(string.Format("正在监控中，修改设置将重新启动监控，是否继续?"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                this.monitorCtrl1.Interval = form.Interval;
                this.monitorCtrl1.Address = form.Address;
                this.monitorCtrl1.Length = form.Length;
                this.monitorCtrl1.ElectricityExpress = form.ElectricityExpress;
                this.monitorCtrl1.VoltageExpress = form.VoltageExpress;
            }
        }

        /// <summary>
        /// 监控的数据写入文件中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "*.txt";
            dlg.Filter = "*.txt|文件文件";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.monitorCtrl1.ImportToFile(dlg.FileName);
                this.tbFileName.Text = Path.GetFileName(dlg.FileName);
                this.btnExportToFile.Enabled = false;
                this.btnEndCreateFile.Enabled = true;
            }
        }
        /// <summary>
        /// 监控的数据，不在写入文件中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEndCreateFile_Click(object sender, EventArgs e)
        {
            this.monitorCtrl1.StopFile();
            this.btnExportToFile.Enabled = true;
            this.btnEndCreateFile.Enabled = false;
        }

        /// <summary>
        /// 清空数据，包括曲线以及数据列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearData_Click(object sender, EventArgs e)
        {
            this.monitorCtrl1.ReInitCurve();
        }
    }
}