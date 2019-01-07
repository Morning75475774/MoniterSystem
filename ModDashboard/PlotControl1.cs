using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ModDashboard
{
    public partial class PlotControl1 : UserControl
    {
        public event EventHandler OnEmptyData;
        public event EventHandler OnNormalData;

        public PlotControl1()
        {
            InitializeComponent();
            Interval = 1000;
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
            get; set;
        }


        private Color m_vColor = Color.Green;
        private Color m_eColor = Color.Blue;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ReInitCurve();
            random = new Random();
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="file"></param>
        public void Start(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                MessageBox.Show("没有设置数据文件！");
                return;
            }
            else if (!File.Exists(file))
            {
                MessageBox.Show(string.Format("数据文件{0}不存在！", file));
                return;

            }
            ReInitCurve();

            reader.DataFile = file;
            reader.InitOffset();

            timerTick = new Timer();
            timerTick.Interval = Interval;
            timerTick.Tick += TimerTick_Tick;
            timerTick.Start();
        }
        public void Stop()
        {
            if (timerTick != null)
                timerTick.Stop();
        }


        /// <summary>
        /// 重置曲线
        /// </summary>
        private void ReInitCurve()
        {
            userCurveDL.RemoveCurve("A");
            /// 电流
            userCurveDL.SetLeftCurve("A", null, ElectricitColor,3);
            /// 电压
            userCurveDY.RemoveCurve("A");
            userCurveDY.SetLeftCurve("A", null, VoltageColor,3);
        }
        private void TimerTick_Tick(object sender, EventArgs e)
        {
            float dl = 0;
            float dy = 0;
            /// 读取数据
            reader.Read();
            List<Tuple<float, float>> datas = reader.Datas;
            if (datas.Count == 0)
            {
                if (OnEmptyData != null)
                {
                    OnEmptyData(++m_emptyCount, null);
                }
                ;
                userCurveDL.AddCurveData("A", userCurveDL.GetLastData("A"));
                userCurveDY.AddCurveData("A", userCurveDL.GetLastData("B"));
            }
            else
            {
                if (OnNormalData != null)
                {
                    OnNormalData(null, null);
                }
                m_emptyCount = 0;
                foreach (Tuple<float, float> data in datas)
                {
                    userCurveDL.AddCurveData("A", data.Item1);
                    userCurveDY.AddCurveData("A", data.Item2);
                    this.listView1.Items.Add(new ListViewItem(new string[] { "", DateTime.Now.ToLongTimeString(), data.Item1.ToString() }));
                    this.listView2.Items.Add(new ListViewItem(new string[] { "", DateTime.Now.ToLongTimeString(), data.Item2.ToString() }));
                    
                    this.listView1.EnsureVisible(this.listView1.Items.Count - 1);
                    this.listView2.EnsureVisible(this.listView2.Items.Count - 1);
                }
            }
        }
        private int m_emptyCount = 0;

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

        private Timer timerTick = null;
        private Random random;
        private bool isVisiable = true;
        private string m_file = "";
        DataReader reader = new DataReader();


    }
}
