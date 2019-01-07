using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication.ModBus;
using System.Threading;
using HslCommunication;
using System.IO;

namespace ModDashboard
{
    public partial class MonitorCtrl : UserControl
    {
        public MonitorCtrl()
        {
            InitializeComponent();
            //this.userControl11.OnEmptyData += UserControl11_OnEmptyData;
            //this.userControl11.OnNormalData += UserControl11_OnNormalData;
            //this.cbTime.Text = this.userControl11.Interval.ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 是否正在监控中
        /// </summary>
        public bool IsExcuting
        {
            get
            {
                return this.m_executing;
            }
        }

        [DefaultValue(0000)]
        public string Address
        {
            get
            {
                return this.m_add;
            }
            set
            {
                this.m_add = value;
                this.tbAddress.Text = this.m_add;
            }
        }

        [DefaultValue(2)]
        public ushort Length
        {
            get
            {
                return this.m_length;
            }
            set
            {
                this.m_length = value;
                this.tbLength.Text = Length.ToString();

            }
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
        public float VoltageStart
        {
            get; set;
        }
        public float VoltageEnd
        {
            get; set;
        }
        public float ElectricityStart
        {
            get; set;
        }
        public float ElectricityEnd
        {
            get; set;
        }

        public float VoltageUpper
        {
            get; set;
        }
        public float VoltageLower
        {
            get; set;
        }
        public float ElectricityUpper
        {
            get; set;
        }
        public float ElectricityLower
        {
            get; set;
        }

        public Color VoltageColor
        {
            get
            {
                return this.m_vColor;
            }
            set
            {
                this.m_vColor = value;
            }
        }

        public Color ElectricitColor
        {
            get
            {
                return this.m_eColor;
            }
            set
            {
                this.m_eColor = value;
            }
        }
        [DefaultValue(100)]
        public int Interval
        {
            get
            {
                if (!int.TryParse(cbTime.Text, out timeSleep))
                {
                    return 1000;
                }
                return timeSleep;
            }
            set
            {
                this.timeSleep = value;
                this.cbTime.Text = value.ToString();
            }
        }


        public ModbusRtu ModbusRtuClient
        {
            get
            {
                return this.busRtuClient;
            }
        }
        private ModbusRtu busRtuClient = null;


        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="client"></param>
        public void Connect(ModbusRtu client)
        {
            m_isConnect = true;
            busRtuClient = client;
            SetConnectState(m_isConnect);
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (m_executing)
            {
                Stop();
            }
            m_isConnect = false;
            // busRtuClient = null;
            SetConnectState(m_isConnect);
        }

        /// <summary>
        /// 停止监控
        /// </summary>
        public void Stop()
        {
            if (m_executing)
            {
                Run();
                SetExecuting(false);
            }
        }
        /// <summary>
        /// 开始监控
        /// </summary>
        public bool Start()
        {
            if (!m_isConnect)
            {
                MessageBox.Show("没有连接！不能启动监控扫描！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (m_executing)
            {
                MessageBox.Show("正在监控中！不能再次启动！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (!byte.TryParse(tbStation.Text, out byte station))
            {
                MessageBox.Show("站号输入不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(cbTime.Text, out timeSleep))
            {
                MessageBox.Show("频率输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(tbAddress.Text, out int add))
            {
                MessageBox.Show("地址输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Address = tbAddress.Text;

            if (!ushort.TryParse(tbLength.Text, out ushort length))
            {
                MessageBox.Show("长度输入错误！请输入整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (length < 2)
            {
                MessageBox.Show("长度必须大于1！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Length = length;
            this.busRtuClient.Station = station;

            if (!ContainCurve())
            {
                ReInitCurve();
            }
            if (Run())
            {
                SetExecuting(true);
                return true;
            }
            else
            {
                SetExecuting(false);
                return false;
            }
        }
        /// <summary>
        /// 监控到的数据，全部写入到文件
        /// </summary>
        /// <param name="file">文件路径</param>
        public void ImportToFile(string file)
        {
            m_isWriting = true;
            this.m_file = file;
        }

        /// <summary>
        /// 停止写入文件
        /// </summary>
        public void StopFile()
        {
            m_isWriting = false;
        }
        /// <summary>
        ///清空数据
        /// </summary>
        public void ReInitCurve()
        {
            /// 电流
            userCurveDL.RemoveCurve("A");
            userCurveDL.SetLeftCurve("A", null, ElectricitColor, 3);
            /// 电压
            userCurveDY.RemoveCurve("A");
            userCurveDY.SetLeftCurve("A", null, VoltageColor, 3);
            /// 数据列表
            tbData.Clear();
        }

        /// <summary>
        /// 是否已经包含曲线
        /// </summary>
        /// <returns></returns>
        private bool ContainCurve()
        {
            return userCurveDL.ContainCurve("A");
        }

      
        /// <summary>
        /// 设置数据的范围以及上下限
        /// </summary>
        /// <param name="vLower"></param>
        /// <param name="vUpper"></param>
        /// <param name="eLower"></param>
        /// <param name="eUpper"></param>
        /// <param name="vStart"></param>
        /// <param name="vEnd"></param>
        /// <param name="eStart"></param>
        /// <param name="eEnd"></param>
        public void SetDataRange(float vLower = -1, float vUpper = -1, float eLower = -1, float eUpper = -1, float vStart = -1, float vEnd = -1, float eStart = -1, float eEnd = -1)
        {
            // 纵轴范围
            VoltageStart = vStart;
            if (vStart > -1)
            {
                userCurveDY.ValueMinLeft = userCurveDY.ValueMinRight = vStart;
            }
            VoltageEnd = vEnd;
            if (vStart > -1)
            {
                userCurveDY.ValueMaxLeft = userCurveDY.ValueMaxRight = vEnd;
            }

            ElectricityStart = eStart;
            if (vStart > -1)
            {
                userCurveDL.ValueMinLeft = userCurveDL.ValueMinRight = eStart;
            }
            ElectricityEnd = eEnd;
            if (vStart > -1)
            {
                userCurveDL.ValueMaxLeft = userCurveDL.ValueMaxRight = eEnd;
            }
            /// 上下限
            userCurveDY.RemoveAllAuxiliary();
            userCurveDL.RemoveAllAuxiliary();
            VoltageLower = vLower;
            if (vLower > -1)
            {
                userCurveDY.AddLeftAuxiliary(vLower, Color.Yellow);
            }
            VoltageUpper = vUpper;
            if (vUpper > -1)
            {
                userCurveDY.AddLeftAuxiliary(vUpper, Color.Yellow);
            }
            ElectricityLower = eLower;
            if (eLower > -1)
            {
                userCurveDL.AddLeftAuxiliary(eLower, Color.Yellow);
            }
            ElectricityUpper = eUpper; ;
            if (eUpper > -1)
            {
                userCurveDL.AddLeftAuxiliary(eUpper, Color.Yellow);
            }

        }

        /// <summary>
        /// 设置曲线颜色
        /// </summary>
        /// <param name="vColor"></param>
        /// <param name="eColor"></param>
        public void SetCurveColor(Color vColor, Color eColor)
        {
            VoltageColor = vColor;
            ElectricitColor = eColor;
        }

        #region 定时器读取测试

        // 外加曲线显示
        // 后台读取的线程
        private Thread thread = null;
        // 用来标记线程的运行状态
        private bool isThreadRun = false;

        /// <summary>
        /// 开始监控
        /// </summary>
        private bool Run()
        {
            // 启动后台线程，定时读取PLC中的数据，然后在曲线控件中显示

            if (!isThreadRun)
            {
                isThreadRun = true;
                thread = new Thread(ThreadReadServer);
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                isThreadRun = false;
            }
            return true;
        }

        private void ThreadReadServer()
        {
            if (!m_isConnect)
                return;
            while (isThreadRun)
            {
                Thread.Sleep(timeSleep);
                try
                {
                    if (ushort.TryParse(tbLength.Text, out ushort length))
                    {

                        OperateResult<short[]> read = busRtuClient.ReadInt16(tbAddress.Text, length);
                        if (read.IsSuccess && read.Content.Count() > 0)
                        {
                            // 显示曲线
                            if (isThreadRun) Invoke(new Action<OperateResult<short[]>>(AddDataCurve), read);
                            m_lastData = read;
                            /// 进行状态提示
                            if (isThreadRun) Invoke(new Action<bool, int>(SetReciveDataState), true, 0);

                            /// 进行状态提示
                            m_emptyData = 0;
                        }
                        else
                        {
                            if (m_lastData != null)
                            {
                                //获取数据失败，显示上一次的数据
                                if (isThreadRun) Invoke(new Action<OperateResult<short[]>>(AddDataCurve), m_lastData);
                            }
                            else
                            {
                                OperateResult<short[]> d = new OperateResult<short[]>();
                                d.Content = new short[2] { 0, 0 };
                                //获取数据失败，显示上一次的数据
                                if (isThreadRun) Invoke(new Action<OperateResult<short[]>>(AddDataCurve), d);

                            }
                            /// 进行状态提示
                            if (isThreadRun) Invoke(new Action<bool, int>(SetReciveDataState), false, ++m_emptyData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取失败：" + ex.Message);
                }
            }
        }
        private OperateResult<short[]> m_lastData = null;


        private void AddDataCurve(OperateResult<short[]> data)
        {
            if (data.Content == null)
                return;
            if (data.Content.Length < 2)
                return;
            string content = string.Format("{0} {1}:{2} {3}\n", DateTime.Now.ToString("[HH:mm:ss] "), Address, data.Content[0].ToString(), data.Content[1].ToString());
            tbData.AppendText(content);

            ///数据写入文件
            if (m_isWriting)
            {
                if (!string.IsNullOrEmpty(m_file))
                {
                    File.AppendAllText(m_file, content);
                }
            }

            float dl = 0, dy = 0;
            if (int.TryParse(tbAddress.Text, out int add))
            {
                do
                {
                    if (Length == add)
                        break;
                    if (ElectricityExpress.Contains(string.Format("${0}$", add.ToString("d04"))))
                    {
                        dl = Calculation.CalProvider.Cal(ElectricityExpress, string.Format("${0}$", add.ToString("d04")), data.Content[add].ToString());

                    }
                    else if (ElectricityExpress.Contains(string.Format("${0}$", add)))
                    {
                        dl = Calculation.CalProvider.Cal(ElectricityExpress, string.Format("${0}$", add.ToString()), data.Content[add].ToString());

                    }
                    if (VoltageExpress.Contains(string.Format("${0}$", add.ToString("d04"))))
                    {
                        dy = Calculation.CalProvider.Cal(VoltageExpress, string.Format("${0}$", add.ToString("d04")), data.Content[add].ToString());

                    }
                    else if (VoltageExpress.Contains(string.Format("${0}$", add)))
                    {
                        dy = Calculation.CalProvider.Cal(VoltageExpress, string.Format("${0}$", add.ToString()), data.Content[add].ToString());

                    }
                    add++;
                } while (true);
            }

            if (dl == 0)
            {
                dl = data.Content[0];
            }

            if (dy == 0)
            {
                dy = data.Content[1];
            }
            //如果电流值超过Y轴上限，则调整上限
            if (dl > userCurveDL.ValueMaxLeft)
            {
                userCurveDL.ValueMaxLeft = userCurveDL.ValueMaxRight = dl;
            }
            if (dl < userCurveDL.ValueMinLeft)
            {
                userCurveDL.ValueMinLeft = userCurveDL.ValueMinLeft = dl;
            }
            //如果电压值超过Y轴上限，则调整上限
            if (dy > userCurveDY.ValueMaxLeft)
            {
                userCurveDY.ValueMaxLeft = userCurveDY.ValueMaxLeft = dy;
            }
            if (dy < userCurveDY.ValueMinLeft)
            {
                userCurveDY.ValueMinLeft = userCurveDY.ValueMinLeft = dy;
            }
            userCurveDL.AddCurveData("A", dl);
            userCurveDY.AddCurveData("A", dy);
        }


        #endregion
        /// <summary>
        /// 显示连接状态
        /// </summary>
        /// <param name="succ">是否连接</param>
        private void SetConnectState(bool succ)
        {
            this.userLanternConn.LanternBackground = succ ? Color.Green : Color.Red;
            this.lbConnect.Text = succ ? "连接成功！" : "未连接!";
        }

        /// <summary>
        /// 设置执行的状态
        /// </summary>
        /// <param name="executing">是否正在执行中</param>
        private void SetExecuting(bool executing)
        {
            if (executing)
            {
                m_executing = true;
                this.userLantern1.LanternBackground = System.Drawing.Color.Lime;
                this.label2.Text = "正在监控中!";
            }
            else
            {
                m_executing = false;
                this.userLantern1.LanternBackground = System.Drawing.Color.Red;
                this.label2.Text = "监控未开始!";

                this.lbConWaring.Text = "未接收数据！";
                this.userLantern2.LanternBackground = System.Drawing.Color.Red;
            }
        }

        private void SetReciveDataState(bool succ, int count = 0)
        {
            if (succ)
            {
                this.lbConWaring.Text = "正常接收数据";
                this.userLantern2.LanternBackground = System.Drawing.Color.Green;
            }
            else
            {
                if (count == 0)
                {
                    this.lbConWaring.Text = "未接收数据";
                    this.userLantern2.LanternBackground = System.Drawing.Color.Red;

                }
                else
                {
                    this.lbConWaring.Text = "第" + count.ToString() + "次未接收到数据！";
                    this.userLantern2.LanternBackground = System.Drawing.Color.Red;
                }
            }
        }


        /// <summary>
        ///  当前是否正在监控中
        ///  “开始”按钮按下后，开始监控
        /// </summary>
        protected bool m_executing = false;
        /// <summary>
        /// 是否已经连接成功
        /// </summary>
        protected bool m_isConnect = false;

        /// <summary>
        /// 连续接收空数据的次数
        /// </summary>
        protected int m_emptyData = 0;
        private ushort m_length = 2;
        private string m_add = "0000";
        private int timeSleep = 1000;

        private Color m_vColor = Color.Green;
        private Color m_eColor = Color.Blue;
        /// <summary>
        /// 数据是否需要写入文件中
        /// </summary>
        private bool m_isWriting = false;
        //监控的数据持续的写入文件中
        private string m_file = "";
    }
}
