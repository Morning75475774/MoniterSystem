using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


/*******************************************************************************
 * 
 *    文件名：UserCurve.cs
 *    程序功能：显示曲线信息，包含了曲线，坐标轴，鼠标移动信息
 *    
 *    创建人：Richard.Hu
 *    时间：2018年1月21日 18:36:08
 * 
 *******************************************************************************/


namespace HslCommunication.Controls
{
    /// <summary>
    /// 曲线控件对象
    /// </summary>
    /// <remarks>
    /// 详细参照如下的博客:
    /// </remarks>
    public partial class UserCurve : UserControl
    {
        /// <summary>
        /// 选中了曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EventSelectedLineHandler(HslCurveItem sender, EventArgs e);

        public event EventSelectedLineHandler OnLineSelected;

        /// <summary>
        /// 选中了点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EventPointSelectedHandler(HslHighlightPointItem sender, EventArgs e);

        public event EventPointSelectedHandler OnPointSelected;

        #region Constructor

        /// <summary>
        /// 实例化一个曲线显示的控件
        /// </summary>
        public UserCurve()
        {
            InitializeComponent();
            DoubleBuffered = true;
            random = new Random();
            data_list = new Dictionary<string, HslCurveItem>();
            auxiliary_lines = new List<AuxiliaryLine>();

            format_left = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };

            format_right = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Far,
            };

            format_center = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center,
            };



            font_size9 = new Font("宋体", 9);
            font_size12 = new Font("宋体", 12);
            InitializationColor();
            pen_dash = new Pen(color_deep);
            pen_dash.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen_dash.DashPattern = new float[] { 5, 5 };
        }

        #endregion

        #region Const Data

        private const int value_count_max = 2048;           // 缓存的数据的最大量

        #endregion

        #region Private Member

        private float value_max_left = 100;                 // 左坐标的最大值
        private float value_min_left = 0;                   // 左坐标的最小值
        private float value_max_right = 100;                // 右坐标的最大值
        private float value_min_right = 0;                  // 右坐标的最小值

        private int value_Segment = 5;                      // 纵轴的片段分割
        private bool value_IsAbscissaStrech = false;        // 指示横坐标是否填充满整个坐标系
        private int value_StrechDataCountMax = 300;         // 拉伸模式下的最大数据量
        private bool value_IsRenderDashLine = true;         // 是否显示虚线的信息
        // private bool value_IsRenderAbscissaText = false;    // 指示是否显示横轴的文本信息
        private string textFormat = "HH:mm";                // 时间文本的信息
        private int value_IntervalAbscissaText = 100;       // 指示显示横轴文本的间隔数据
        private Random random = null;                       // 获取随机颜色使用
        private string value_title = "";                    // 图表的标题


        private int leftRight = 50;
        private int upDowm = 25;


        #endregion

        #region Data Member

        private HslHighlightPointItem highlightPoint = null;        //高亮的点
        private Dictionary<string, HslCurveItem> data_list = null;  // 等待显示的实际数据
        private string[] data_text = null;                          // 等待显示的横轴信息

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置图形的纵坐标的最大值，该值必须大于最小值
        /// </summary>
        [Category("外观")]
        [Description("是否显示曲线名称")]
        [Browsable(true)]
        [DefaultValue(false)]
        public bool ShowLegend
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置图形的纵坐标的最大值，该值必须大于最小值
        /// </summary>
        [Category("外观")]
        [Description("获取或设置图形的左纵坐标的最大值，该值必须大于最小值")]
        [Browsable(true)]
        [DefaultValue(100f)]
        public float ValueMaxLeft
        {
            get { return value_max_left; }
            set
            {
                if (value > value_min_left)
                {
                    value_max_left = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的纵坐标的最小值，该值必须小于最大值
        /// </summary>
        [Category("外观")]
        [Description("获取或设置图形的左纵坐标的最小值，该值必须小于最大值")]
        [Browsable(true)]
        [DefaultValue(0f)]
        public float ValueMinLeft
        {
            get { return value_min_left; }
            set
            {
                if (value < value_max_left)
                {
                    value_min_left = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的纵坐标的最大值，该值必须大于最小值
        /// </summary>
        [Category("外观")]
        [Description("获取或设置图形的右纵坐标的最大值，该值必须大于最小值")]
        [Browsable(true)]
        [DefaultValue(100f)]
        public float ValueMaxRight
        {
            get { return value_max_right; }
            set
            {
                if (value > value_min_right)
                {
                    value_max_right = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的纵坐标的最小值，该值必须小于最大值
        /// </summary>
        [Category("外观")]
        [Description("获取或设置图形的右纵坐标的最小值，该值必须小于最大值")]
        [Browsable(true)]
        [DefaultValue(0f)]
        public float ValueMinRight
        {
            get { return value_min_right; }
            set
            {
                if (value < value_max_right)
                {
                    value_min_right = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置图形的纵轴分段数
        /// </summary>
        [Category("外观")]
        [Description("获取或设置图形的纵轴分段数")]
        [Browsable(true)]
        [DefaultValue(5)]
        public int ValueSegment
        {
            get { return value_Segment; }
            set
            {
                value_Segment = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置所有的数据是否强制在一个界面里显示
        /// </summary>
        [Category("外观")]
        [Description("获取或设置所有的数据是否强制在一个界面里显示")]
        [Browsable(true)]
        [DefaultValue(false)]
        public bool IsAbscissaStrech
        {
            get { return value_IsAbscissaStrech; }
            set
            {
                value_IsAbscissaStrech = value;
                Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置拉伸模式下的最大数据量
        /// </summary>
        [Category("外观")]
        [Description("获取或设置拉伸模式下的最大数据量")]
        [Browsable(true)]
        [DefaultValue(300)]
        public int StrechDataCountMax
        {
            get { return value_StrechDataCountMax; }
            set
            {
                value_StrechDataCountMax = value;
                Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置虚线是否进行显示
        /// </summary>
        [Category("外观")]
        [Description("获取或设置虚线是否进行显示")]
        [Browsable(true)]
        [DefaultValue(true)]
        public bool IsRenderDashLine
        {
            get { return value_IsRenderDashLine; }
            set
            {
                value_IsRenderDashLine = value;
                Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置坐标轴及相关信息文本的颜色
        /// </summary>
        [Category("外观")]
        [Description("获取或设置坐标轴及相关信息文本的颜色")]
        [Browsable(true)]
        [DefaultValue(typeof(Color), "DimGray")]
        public Color ColorLinesAndText
        {
            get { return color_deep; }
            set
            {
                color_deep = value;
                InitializationColor();
                Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置虚线的颜色
        /// </summary>
        [Category("外观")]
        [Description("获取或设置虚线的颜色")]
        [Browsable(true)]
        [DefaultValue(typeof(Color), "Gray")]
        public Color ColorDashLines
        {
            get { return color_dash; }
            set
            {
                color_dash = value;
                pen_dash?.Dispose();
                pen_dash = new Pen(color_dash);
                pen_dash.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                pen_dash.DashPattern = new float[] { 5, 5 };
                Invalidate();
            }
        }


        /// <summary>
        /// 获取或设置纵向虚线的分隔情况，单位为多少个数据
        /// </summary>
        [Category("外观")]
        [Description("获取或设置纵向虚线的分隔情况，单位为多少个数据")]
        [Browsable(true)]
        [DefaultValue(100)]
        public int IntervalAbscissaText
        {
            get { return value_IntervalAbscissaText; }
            set
            {
                value_IntervalAbscissaText = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置实时数据新增时文本相对应于时间的格式化字符串，默认HH:mm
        /// </summary>
        [Category("外观")]
        [Description("获取或设置实时数据新增时文本相对应于时间的格式化字符串，默认HH:mm")]
        [Browsable(true)]
        [DefaultValue("HH:mm")]
        public string TextAddFormat
        {
            get { return textFormat; }
            set { textFormat = value; Invalidate(); }
        }


        /// <summary>
        /// 获取或设置图标的标题信息
        /// </summary>
        [Category("外观")]
        [Description("获取或设置图标的标题信息")]
        [Browsable(true)]
        [DefaultValue("")]
        public string Title
        {
            get { return value_title; }
            set { value_title = value; Invalidate(); }
        }


        private void InitializationColor()
        {
            pen_normal?.Dispose();
            title_brush_deep?.Dispose();
            pen_normal = new Pen(color_deep);
            title_brush_deep = new SolidBrush(color_deep);
            lengendbrush_deep = new SolidBrush(Color.Black);
        }

        #endregion

        #region Public Method

        /// <summary>
        /// 设置曲线的横坐标文本，适用于显示一些固定的曲线信息
        /// </summary>
        /// <param name="descriptions">应该和曲线的点数一致</param>
        public void SetCurveText(string[] descriptions)
        {
            data_text = descriptions;

            // 重绘图形
            Invalidate();
        }


        /// <summary>
        /// 新增或修改一条指定关键字的左参考系曲线数据，需要指定数据，颜色随机，没有数据上限，线条宽度为1
        /// </summary>
        /// <param name="key">曲线关键字</param>
        /// <param name="data">曲线的具体数据</param>
        public void SetLeftCurve(string key, float[] data)
        {
            SetLeftCurve(key, data, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), 1f);
        }
        /// <summary>
        /// 新增或修改一条指定关键字的左参考系曲线数据，需要指定数据，颜色，没有数据上限，线条宽度为1
        /// </summary>
        /// <param name="key">曲线关键字</param>
        /// <param name="data"></param>
        /// <param name="lineColor"></param>
        /// <param name="thickness">宽度</param>
        public void SetLeftCurve(string key, float[] data, Color lineColor)
        {
            SetCurve(key, true, data, lineColor, 1F);
        }

        /// <summary>
        /// 新增或修改一条指定关键字的左参考系曲线数据，需要指定数据，颜色，没有数据上限，线条宽度为1
        /// </summary>
        /// <param name="key">曲线关键字</param>
        /// <param name="data"></param>
        /// <param name="lineColor"></param>
        /// <param name="thickness">宽度</param>
        public void SetLeftCurve(string key, float[] data, Color lineColor, float thickness)
        {
            SetCurve(key, true, data, lineColor, thickness);
        }

        /// <summary>
        /// 新增或修改一条指定关键字的右参考系曲线数据，需要指定数据，颜色随机，没有数据上限，线条宽度为1
        /// </summary>
        /// <param name="key">曲线关键字</param>
        /// <param name="data"></param>
        public void SetRightCurve(string key, float[] data)
        {
            SetRightCurve(key, data, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));
        }

        /// <summary>
        /// 新增或修改一条指定关键字的右参考系曲线数据，需要指定数据，颜色，没有数据上限，线条宽度为1
        /// </summary>
        /// <param name="key">曲线关键字</param>
        /// <param name="data"></param>
        /// <param name="lineColor"></param>
        public void SetRightCurve(string key, float[] data, Color lineColor)
        {
            SetCurve(key, false, data, lineColor, 1f);
        }


        /// <summary>
        /// 新增或修改一条指定关键字的曲线数据，需要指定参考系及数据，颜色，线条宽度
        /// </summary>
        /// <param name="key">曲线关键字</param>
        /// <param name="isLeft">是否以左侧坐标轴为参照系</param>
        /// <param name="data">数据</param>
        /// <param name="lineColor">线条颜色</param>
        /// <param name="thickness">线条宽度</param>
        public void SetCurve(string key, bool isLeft, float[] data, Color lineColor, float thickness)
        {
            if (data_list.ContainsKey(key))
            {
                if (data == null) data = new float[] { };
                data_list[key].YData = data;
            }
            else
            {
                if (data == null) data = new float[] { };
                data_list.Add(key, new HslCurveItem()
                {
                    YData = data,
                    LineThickness = thickness,
                    LineColor = lineColor,
                    IsLeftFrame = isLeft,
                });

                if (data_text == null) data_text = new string[data.Length];
            }

            // 重绘图形
            Invalidate();
        }
        /// <summary>
        /// 是否包含指定关键字的曲线
        /// </summary>
        /// <param name="key">曲线关键字</param>
        /// <returns></returns>
        public bool ContainCurve(string key)
        {
            return data_list.ContainsKey(key);
        }

        /// <summary>
        /// 移除指定关键字的曲线
        /// </summary>
        /// <param name="key">曲线关键字</param>
        public void RemoveCurve(string key)
        {
            if (data_list.ContainsKey(key))
            {
                data_list.Remove(key);
            }
            if (data_list.Count == 0) data_text = new string[0];
            // 重绘图形
            Invalidate();
        }


        /// <summary>
        /// 移除指定关键字的曲线
        /// </summary>
        public void RemoveAllCurve()
        {
            int count = data_list.Count;
            data_list.Clear();
            if (data_list.Count == 0) data_text = new string[0];
            // 重绘图形
            if (count > 0) Invalidate();
        }

        /// <summary>
        /// 获取曲线的最后一个数值
        /// </summary>
        /// <param name="key">曲线的关键字</param>
        /// <returns></returns>
        public float GetLastData(string key)
        {
            if (data_list.Count == 0)
                return 0;
            if (data_list.ContainsKey(key))
            {
                HslCurveItem curve = data_list[key];
                if (curve.YData != null && curve.YData.Count() > 0)
                {
                    return curve.YData.Last();
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取曲线的数据数量
        /// </summary>
        /// <param name="key">曲线的关键字</param>
        /// <returns></returns>
        public int GetDataCount(string key)
        {
            if (data_list.Count == 0)
                return -1;
            HslCurveItem curve = data_list[key];
            if (curve.YData != null)
            {
                return curve.YData.Count();
            }
            return -1;
        }
        // ======================================================================================================



        /// <summary>
        /// 新增指定关键字曲线的一个数据，注意该关键字的曲线必须存在，否则无效
        /// </summary>
        /// <param name="key">新增曲线的关键字</param>
        /// <param name="values"></param>
        /// <param name="isUpdateUI">是否刷新界面</param>
        private void AddCurveData(string key, float[] values, bool isUpdateUI)
        {
            if (values?.Length < 1) return;                              // 没有传入数据

            if (data_list.ContainsKey(key))
            {
                HslCurveItem curve = data_list[key];
                if (curve.YData != null)
                {
                    if (value_IsAbscissaStrech)
                    {
                        // 填充玩整个图形的情况
                        BasicFramework.SoftBasic.AddArrayData(ref curve.YData, values, value_StrechDataCountMax);
                    }
                    else
                    {
                        // 指定点的情况，必然存在最大值情况
                        BasicFramework.SoftBasic.AddArrayData(ref curve.YData, values, value_count_max);
                    }

                    if (isUpdateUI) Invalidate();
                }
            }
        }

        // 新增曲线的时间节点
        private void AddCurveTime(int count)
        {
            if (data_text == null) return;
            string[] values = new string[count];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = DateTime.Now.ToString(textFormat);
            }


            if (value_IsAbscissaStrech)
            {
                BasicFramework.SoftBasic.AddArrayData(ref data_text, values, value_StrechDataCountMax);
            }
            else
            {
                BasicFramework.SoftBasic.AddArrayData(ref data_text, values, value_count_max);
            }
        }

        /// <summary>
        /// 新增指定关键字曲线的一个数据，注意该关键字的曲线必须存在，否则无效
        /// </summary>
        /// <param name="key">曲线的关键字</param>
        /// <param name="value">数据值</param>
        public void AddCurveData(string key, float value)
        {
            AddCurveData(key, new float[] { value });
        }

        /// <summary>
        /// 新增指定关键字曲线的一组数据，注意该关键字的曲线必须存在，否则无效
        /// </summary>
        /// <param name="key">曲线的关键字</param>
        /// <param name="values">数组值</param>
        public void AddCurveData(string key, float[] values)
        {
            AddCurveData(key, values, false);
            if (values?.Length > 0)
            {
                AddCurveTime(values.Length);
            }
            Invalidate();
        }

        /// <summary>
        /// 新增指定关键字数组曲线的一组数据，注意该关键字的曲线必须存在，否则无效，一个数据对应一个数组
        /// </summary>
        /// <param name="keys">曲线的关键字数组</param>
        /// <param name="values">数组值</param>
        public void AddCurveData(string[] keys, float[] values)
        {
            if (keys == null) throw new ArgumentNullException("keys");
            if (values == null) throw new ArgumentNullException("values");
            if (keys.Length != values.Length) throw new Exception("两个参数的数组长度不一致。");


            for (int i = 0; i < keys.Length; i++)
            {
                AddCurveData(keys[i], new float[] { values[i] }, false);
            }

            AddCurveTime(1);
            // 统一的更新显示
            Invalidate();
        }


        /// <summary>
        /// 设置一条曲线是否是可见的，如果该曲线不存在，则无效。
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="visible">是否可见</param>
        public void SetCurveVisible(string key, bool visible)
        {
            if (data_list.ContainsKey(key))
            {
                HslCurveItem curve = data_list[key];
                curve.Visible = visible;
                Invalidate();
            }
        }

        /// <summary>
        /// 设置多条曲线是否是可见的，如果该曲线不存在，则无效。
        /// </summary>
        /// <param name="keys">关键字</param>
        /// <param name="visible">是否可见</param>
        public void SetCurveVisible(string[] keys, bool visible)
        {
            foreach (var key in keys)
            {
                if (data_list.ContainsKey(key))
                {
                    HslCurveItem curve = data_list[key];
                    curve.Visible = visible;
                }
            }
            Invalidate();
        }


        #endregion

        #region Auxiliary Line

        // 辅助线的信息，允许自定义辅助线信息，来标注特殊的线条

        private List<AuxiliaryLine> auxiliary_lines;               // 所有辅助线的列表

        /// <summary>
        /// 新增一条左侧的辅助线，使用默认的文本颜色
        /// </summary>
        /// <param name="value">数据值</param>
        public void AddLeftAuxiliary(float value)
        {
            AddLeftAuxiliary(value, ColorLinesAndText);
        }

        /// <summary>
        /// 新增一条左侧的辅助线，使用指定的颜色
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="lineColor">线条颜色</param>
        public void AddLeftAuxiliary(float value, Color lineColor)
        {
            AddLeftAuxiliary(value, lineColor, 1f);
        }

        /// <summary>
        /// 新增一条左侧的辅助线
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="lineColor">线条颜色</param>
        /// <param name="lineThickness">线条宽度</param>
        public void AddLeftAuxiliary(float value, Color lineColor, float lineThickness)
        {
            AddAuxiliary(value, lineColor, lineThickness, true);
        }


        /// <summary>
        /// 新增一条右侧的辅助线，使用默认的文本颜色
        /// </summary>
        /// <param name="value">数据值</param>
        public void AddRightAuxiliary(float value)
        {
            AddRightAuxiliary(value, ColorLinesAndText);
        }

        /// <summary>
        /// 新增一条右侧的辅助线，使用指定的颜色
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="lineColor">线条颜色</param>
        public void AddRightAuxiliary(float value, Color lineColor)
        {
            AddRightAuxiliary(value, lineColor, 1f);
        }


        /// <summary>
        /// 新增一条右侧的辅助线
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="lineColor">线条颜色</param>
        /// <param name="lineThickness">线条宽度</param>
        public void AddRightAuxiliary(float value, Color lineColor, float lineThickness)
        {
            AddAuxiliary(value, lineColor, lineThickness, false);
        }


        private void AddAuxiliary(float value, Color lineColor, float lineThickness, bool isLeft)
        {
            auxiliary_lines.Add(new AuxiliaryLine()
            {
                Value = value,
                LineColor = lineColor,
                PenDash = new Pen(lineColor)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Custom,
                    DashPattern = new float[] { 5, 5 }
                },
                IsLeftFrame = isLeft,
                LineThickness = lineThickness,
                LineTextBrush = new SolidBrush(lineColor),
            });
            Invalidate();
        }

        /// <summary>
        /// 移除所有的指定值的辅助曲线，包括左边的和右边的
        /// </summary>
        /// <param name="value"></param>
        public void RemoveAuxiliary(float value)
        {
            int removeCount = 0;
            for (int i = auxiliary_lines.Count - 1; i >= 0; i--)
            {
                if (auxiliary_lines[i].Value == value)
                {
                    auxiliary_lines[i].Dispose();
                    auxiliary_lines.RemoveAt(i);
                    removeCount++;
                }
            }
            if (removeCount > 0) Invalidate();
        }


        /// <summary>
        /// 移除所有的辅助线
        /// </summary>
        public void RemoveAllAuxiliary()
        {
            int removeCount = auxiliary_lines.Count;
            auxiliary_lines.Clear();

            if (removeCount > 0) Invalidate();
        }


        #endregion

        #region Private Method




        #endregion

        #region Core Paint



        private Font font_size9 = null;
        private Font font_size12 = null;

        private Brush title_brush_deep = null;                  // 文本的颜色
        private Brush lengendbrush_deep = null;                  // 文本的颜色

        private Pen pen_normal = null;                    // 绘制极轴和分段符的坐标线
        private Pen pen_dash = null;                      // 绘制图形的虚线

        private Color color_normal = Color.DeepPink;      // 文本的颜色
        private Color color_deep = Color.DimGray;         // 坐标轴及数字文本的信息
        private Color color_dash = Color.Gray;            // 虚线的颜色

        private StringFormat format_left = null;          // 靠左对齐的文本
        private StringFormat format_right = null;         // 靠右对齐的文本
        private StringFormat format_center = null;        // 中间对齐的文本


        /// <summary>
        /// 鼠标单击时，如果选中了点，触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserCurve_MouseClick(object sender, MouseEventArgs e)
        {
            HslHighlightPointItem p = GetNearbyPoint(e.X, e.Y);

            // 如果鼠标点中其中一条曲线上的点，该曲线高亮/取消高亮，并触发相应时间
            if (p != null)
            {
                if (OnPointSelected != null)
                {
                    OnPointSelected(p, e);
                }

                foreach (var key in data_list.Keys)
                {
                    if (p.Key != key)
                        data_list[key].Blod = false;
                    else
                    {
                        data_list[key].Blod = !data_list[key].Blod;
                    }
                }
                Invalidate();
            }
            /// 没有选中曲线上的节点，判断是否选中的曲线
            else
            {
                string curveKey = GetNerabyCurve(e.X, e.Y, 10);
                if (!string.IsNullOrEmpty(curveKey))
                {
                    foreach (var key in data_list.Keys)
                    {
                        if (curveKey != key)
                            data_list[key].Blod = false;
                        else
                        {
                            data_list[key].Blod = !data_list[key].Blod;
                            Invalidate();
                            break;
                        }
                    }
                }
                ///没有选中任何曲线，取消所有曲线的高亮
                else
                {
                    bool change = false;
                    foreach (var key in data_list.Keys)
                    {
                        if (data_list[key].Blod)
                            change = true;
                        data_list[key].Blod = false;
                    }
                    if (change)
                        Invalidate();

                }
            }
        }
        /// <summary>
        /// 鼠标移动时，判断鼠标附件的点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserCurve_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            HslHighlightPointItem p = GetNearbyPoint(e.X, e.Y);

            if (highlightPoint == p)
            {
                return;
            }
            highlightPoint = p;
            Invalidate();
        }


        /// <summary>
        /// 判断当前曲线中，指定坐标附件的点，用于高亮
        /// </summary>
        /// <param name="x">指定坐标的x</param>
        /// <param name="y">指定坐标的x</param>
        /// <param name="dalta">余量范围，默认为20</param>
        /// <returns>用于高亮显示的点信息</returns>
        private HslHighlightPointItem GetNearbyPoint(int x, int y, int dalta = 20)
        {
            if (highlightPoint != null)
            {
                if (highlightPoint.Point.X >= x - dalta && highlightPoint.Point.X <= x + dalta &&
                   highlightPoint.Point.Y >= y - dalta && highlightPoint.Point.Y <= y + dalta)
                    return highlightPoint;
            }

            if (curvePoints != null && curvePoints.Keys.Count > 0)
            {
                foreach (var key in curvePoints.Keys)
                {
                    int index = FindPointNumber(curvePoints[key], x, y, dalta);
                    if (index > -1)
                    {
                        float yData = data_list[key].YData[index];
                        float xData = 0;
                        if (data_list[key].XData != null && data_list[key].XData.Length == data_list[key].YData.Length)
                        {
                            xData = data_list[key].XData[index];
                        }
                        else
                        {
                            xData = index;
                        }
                        HslHighlightPointItem p = new HslHighlightPointItem() { Key = key, YData = yData, XData = xData, Point = new Point((int)curvePoints[key][index].X, (int)curvePoints[key][index].Y) };

                        return p;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 判断当前曲线中，指定坐标附近的曲线，用于高亮
        /// </summary>
        /// <param name="x">指定坐标的x</param>
        /// <param name="y">指定坐标的x</param>
        /// <param name="dalta">余量范围，默认为20</param>
        /// <returns>曲线关键字,为空表示没有找到任何曲线</returns>
        private string GetNerabyCurve(int x, int y, int dalta = 20)
        {
            if (curvePoints != null && curvePoints.Keys.Count > 0)
            {
                foreach (var key in curvePoints.Keys)
                {
                    PointF[] points = curvePoints[key];
                    /// 判断指定的X是否在当前曲线的某条线段之间
                    ///一般都各条曲线都会满足
                    int index = FindPointNumber(curvePoints[key], float.NaN, y, dalta);
                    if (index == -1)
                    {
                        continue;
                    }

                    PointF currentPoint = points[index];
                    /// 坐标点 在当前点附近
                    if (Math.Abs(currentPoint.Y - y) < 20)
                    {
                        return key;
                    }
                    /// 坐标点在当前点前边
                    if (currentPoint.X >= x)
                    {
                        ///  当前点为第一个点，如果坐标点的y也在当前点附近，则返回该曲线关键字

                        if (index > 0)
                        {
                            PointF prePoint = points[index - 1];
                            double dis = GetMinDistance(prePoint, currentPoint, new PointF(x, y));
                            if (dis < dalta)
                                return key;

                        }

                    }
                    /// 坐标点在当前点前边
                    else
                    {
                        if (index < points.Length)
                        {
                            PointF nextPoint = points[index + 1];
                            double dis = GetMinDistance(currentPoint, nextPoint, new PointF(x, y));
                            if (dis < dalta)
                                return key;

                        }
                    }

                }
            }

            return "";
        }

        /****点到直线的距离***
         * 过点（x1,y1）和点（x2,y2）的直线方程为：KX -Y + (x2y1 - x1y2)/(x2-x1) = 0
         * 设直线斜率为K = (y2-y1)/(x2-x1),C=(x2y1 - x1y2)/(x2-x1)
         * 点P(x0,y0)到直线AX + BY +C =0DE 距离为：d=|Ax0 + By0 + C|/sqrt(A*A + B*B)
         * 点（x3,y3）到经过点（x1,y1）和点（x2,y2）的直线的最短距离为：
         * distance = |K*x3 - y3 + C|/sqrt(K*K + 1)
         */
        private double GetMinDistance(PointF pt1, PointF pt2, PointF pt3)
        {
            double dis = 0;
            if (pt1.X == pt2.X)
            {
                dis = Math.Abs(pt3.X - pt1.X);
                return dis;
            }
            double lineK = (pt2.Y - pt1.Y) / (pt2.X - pt1.X);
            double lineC = (pt2.X * pt1.Y - pt1.X * pt2.Y) / (pt2.X - pt1.X);
            dis = Math.Abs(lineK * pt3.X - pt3.Y + lineC) / (Math.Sqrt(lineK * lineK + 1));
            return dis;

        }
        /// <summary>
        /// 二分查找While循环实现
        /// </summary>
        /// <param name="points">数据点集合</param>
        /// <param name="xTarget">要查找的对象</param>
        /// <param name="yTarget">要查找的对象</param>
        /// <param name="dalta">差值</param>
        /// <returns>返回索引</returns>
        private int FindPointNumber(PointF[] points, float xTarget, float yTarget, float dalta)
        {
            int low = 0;
            int high = points.Length;
            /// 由于点几何是根据X排序，故判断时，优先考虑X是否满足指定区间[xTarget-dalta,xTarget+dalta]
            if (!float.IsNaN(xTarget))
            {
                while (low <= high)
                {
                    if (low == high && high == points.Length)
                        break;
                    int middle = (low + high) / 2;
                    if (xTarget >= points[middle].X - dalta && xTarget <= points[middle].X + dalta)
                    {
                        /// 如果y是非数字，表示不判断Y
                        if (float.IsNaN(yTarget))
                        {
                            return middle;
                        }
                        if (yTarget > points[middle].Y - dalta && yTarget < points[middle].Y + dalta)
                            return middle;
                        return -1;
                    }
                    else if (xTarget > points[middle].X + dalta)
                    {
                        low = middle + 1;
                    }
                    else if (xTarget < points[middle].X - dalta)
                    {
                        high = middle - 1;
                    }
                }
            }
            // 如果xTarget为NaN，表示只考虑Y是否在[yTarget-dalta,yTarget+dalta]
            else
            {
                int index = 0;
                foreach (var p in points)
                {
                    if (p.Y <= yTarget + dalta && p.Y >= yTarget - dalta)
                    {
                        return index;
                    }
                    index++;
                }
            }
            return -1;
        }

        private void UserCurve_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            if (BackColor != Color.Transparent)
            {
                g.Clear(BackColor);
            }


            int width_totle = Width;
            int heigh_totle = Height;

            if (width_totle < 120 || heigh_totle < 60) return;


            // 绘制极轴
            g.DrawLines(pen_normal, new Point[] {
                new Point(leftRight-1, upDowm - 8),
                new Point(leftRight-1, heigh_totle - upDowm),
                new Point(width_totle - leftRight, heigh_totle - upDowm),
                new Point(width_totle - leftRight, upDowm - 8)
            });

            // 绘制图表的标题
            if (!string.IsNullOrEmpty(value_title))
            {
                g.DrawString(value_title, font_size9,title_brush_deep, new Rectangle(0, 0, width_totle - 1, 20), format_center);
            }
            // 绘制线段名称
            // if (ShowLegend)
            {
                int wid = 100;
                int hei = 10;
                foreach (var key in data_list.Keys)
                {
                    g.DrawString(key, font_size9, lengendbrush_deep, new Rectangle(width_totle - (wid + key.Length * 15), 2, key.Length * 15, 10), format_center);

                    wid += 8;
                    using (Pen pentemp = new Pen(data_list[key].LineColor, data_list[key].LineThickness+1))
                    {
                        g.DrawLine(pentemp, width_totle - (wid + 20), 6, width_totle - (wid + 5),   6);
                    }
                    wid += 20;
                }
            }
            // 绘制倒三角
            BasicFramework.SoftPainting.PaintTriangle(g,title_brush_deep, new Point(leftRight - 1, upDowm - 8), 4, BasicFramework.GraphDirection.Upward);
            BasicFramework.SoftPainting.PaintTriangle(g,title_brush_deep, new Point(width_totle - leftRight, upDowm - 8), 4, BasicFramework.GraphDirection.Upward);

            // 计算所有辅助线的实际值
            for (int i = 0; i < auxiliary_lines.Count; i++)
            {
                if (auxiliary_lines[i].IsLeftFrame)
                {
                    auxiliary_lines[i].PaintValue = BasicFramework.SoftPainting.ComputePaintLocationY(value_max_left, value_min_left, (heigh_totle - upDowm - upDowm), auxiliary_lines[i].Value) + upDowm;
                }
                else
                {
                    auxiliary_lines[i].PaintValue = BasicFramework.SoftPainting.ComputePaintLocationY(value_max_right, value_min_right, (heigh_totle - upDowm - upDowm), auxiliary_lines[i].Value) + upDowm;
                }
            }

            // 绘制刻度线，以及刻度文本
            for (int i = 0; i <= value_Segment; i++)
            {
                float valueTmpLeft = i * (value_max_left - value_min_left) / value_Segment + value_min_left;
                float paintTmp = BasicFramework.SoftPainting.ComputePaintLocationY(value_max_left, value_min_left, (heigh_totle - upDowm - upDowm), valueTmpLeft) + upDowm;
                if (IsNeedPaintDash(paintTmp))
                {
                    // 左坐标轴
                    g.DrawLine(pen_normal, leftRight - 4, paintTmp, leftRight - 1, paintTmp);
                    RectangleF rectTmp = new RectangleF(0, paintTmp - 9, leftRight - 4, 20);
                    g.DrawString(valueTmpLeft.ToString(), font_size9,title_brush_deep, rectTmp, format_right);

                    // 右坐标轴
                    float valueTmpRight = i * (value_max_right - value_min_right) / value_Segment + value_min_right;
                    g.DrawLine(pen_normal, width_totle - leftRight + 1, paintTmp, width_totle - leftRight + 4, paintTmp);
                    rectTmp.Location = new PointF(width_totle - leftRight + 4, paintTmp - 9);
                    g.DrawString(valueTmpRight.ToString(), font_size9,title_brush_deep, rectTmp, format_left);

                    if (i > 0 && value_IsRenderDashLine) g.DrawLine(pen_dash, leftRight, paintTmp, width_totle - leftRight, paintTmp);
                }
            }

            // 绘制纵向虚线信息
            if (value_IsRenderDashLine)
            {
                if (value_IsAbscissaStrech)
                {
                    // 拉伸模式下，因为错位是均匀的，所以根据数据来显示
                    float offect = (width_totle - leftRight * 2) * 1.0f / (value_StrechDataCountMax - 1);
                    int dataCount = CalculateDataCountByOffect(offect);
                    for (int i = 0; i < value_StrechDataCountMax; i += dataCount)
                    {
                        if (i > 0 && i < value_StrechDataCountMax - 1)
                        {
                            g.DrawLine(pen_dash, i * offect + leftRight, upDowm, i * offect + leftRight, heigh_totle - upDowm - 1);
                        }

                        if (data_text != null)
                        {
                            if (i < data_text.Length && ((i * offect + leftRight) < (data_text.Length - 1) * offect + leftRight - 40))
                            {
                                Rectangle rec = new Rectangle((int)(i * offect), heigh_totle - upDowm + 1, 100, upDowm);
                                g.DrawString(data_text[i], font_size9,title_brush_deep, rec, format_center);
                            }
                        }
                    }

                    if (data_text?.Length > 1)
                    {
                        if (data_text.Length < value_StrechDataCountMax)
                        {
                            // 绘制最前端的虚线
                            g.DrawLine(pen_dash, (data_text.Length - 1) * offect + leftRight, upDowm, (data_text.Length - 1) * offect + leftRight, heigh_totle - upDowm - 1);
                        }

                        Rectangle rec = new Rectangle((int)((data_text.Length - 1) * offect + leftRight) - leftRight, heigh_totle - upDowm + 1, 100, upDowm);
                        g.DrawString(data_text[data_text.Length - 1], font_size9,title_brush_deep, rec, format_center);
                    }
                }
                else
                {
                    int countTmp = width_totle - 2 * leftRight + 1;
                    // 普通模式下绘制图形
                    for (int i = leftRight; i < width_totle - leftRight; i += value_IntervalAbscissaText)
                    {
                        if (i != leftRight)
                        {
                            g.DrawLine(pen_dash, i, upDowm, i, heigh_totle - upDowm - 1);
                        }

                        if (data_text != null)
                        {
                            int right_limit = countTmp > data_text.Length ? data_text.Length : countTmp;

                            if ((i - leftRight) < data_text.Length)
                            {
                                if ((right_limit - (i - leftRight)) > 40)
                                {
                                    // 点数大于1的时候才绘制
                                    if (data_text.Length <= countTmp)
                                    {
                                        Rectangle rec = new Rectangle(i - leftRight, heigh_totle - upDowm + 1, 100, upDowm);
                                        g.DrawString(data_text[i - leftRight], font_size9,title_brush_deep, rec, format_center);
                                    }
                                    else
                                    {
                                        Rectangle rec = new Rectangle(i - leftRight, heigh_totle - upDowm + 1, 100, upDowm);
                                        g.DrawString(data_text[i - leftRight + data_text.Length - countTmp], font_size9,title_brush_deep, rec, format_center);
                                    }
                                }
                            }
                        }
                    }

                    if (data_text?.Length > 1)
                    {
                        if (data_text.Length < countTmp)
                        {
                            // 绘制最前端的虚线
                            g.DrawLine(pen_dash, (data_text.Length + leftRight - 1), upDowm, (data_text.Length + leftRight - 1), heigh_totle - upDowm - 1);
                            Rectangle rec = new Rectangle((data_text.Length + leftRight - 1) - leftRight, heigh_totle - upDowm + 1, 100, upDowm);
                            g.DrawString(data_text[data_text.Length - 1], font_size9,title_brush_deep, rec, format_center);
                        }
                        else
                        {
                            Rectangle rec = new Rectangle(width_totle - leftRight - leftRight, heigh_totle - upDowm + 1, 100, upDowm);
                            g.DrawString(data_text[data_text.Length - 1], font_size9,title_brush_deep, rec, format_center);
                        }
                    }
                }
            }

            // 绘制辅助线信息
            for (int i = 0; i < auxiliary_lines.Count; i++)
            {
                if (auxiliary_lines[i].IsLeftFrame)
                {
                    // 左坐标轴
                    g.DrawLine(auxiliary_lines[i].PenDash, leftRight - 4, auxiliary_lines[i].PaintValue, leftRight - 1, auxiliary_lines[i].PaintValue);
                    RectangleF rectTmp = new RectangleF(0, auxiliary_lines[i].PaintValue - 9, leftRight - 4, 20);
                    g.DrawString(auxiliary_lines[i].Value.ToString(), font_size9, auxiliary_lines[i].LineTextBrush, rectTmp, format_right);
                }
                else
                {
                    g.DrawLine(auxiliary_lines[i].PenDash, width_totle - leftRight + 1, auxiliary_lines[i].PaintValue, width_totle - leftRight + 4, auxiliary_lines[i].PaintValue);
                    RectangleF rectTmp = new RectangleF(width_totle - leftRight + 4, auxiliary_lines[i].PaintValue - 9, leftRight - 4, 20);
                    g.DrawString(auxiliary_lines[i].Value.ToString(), font_size9, auxiliary_lines[i].LineTextBrush, rectTmp, format_left);
                }
                g.DrawLine(auxiliary_lines[i].PenDash, leftRight, auxiliary_lines[i].PaintValue, width_totle - leftRight, auxiliary_lines[i].PaintValue);
            }

            // 绘制线条
            if (value_IsAbscissaStrech)
            {
                // 横坐标充满图形
                /// 先将线排序，加粗在放在最后
                //   List<string> sortCurve = new List<string>();
                data_list = data_list.OrderBy(t => t.Value.Blod).ToDictionary(t => t.Key, p => p.Value);
                foreach (var key in data_list.Keys)
                {
                    var line = data_list[key];
                    if (!line.Visible) continue;

                    if (line.YData?.Length > 1)
                    {
                        float offect = (width_totle - leftRight * 2) * 1.0f / (value_StrechDataCountMax - 1);

                        // 点数大于1的时候才绘制
                        PointF[] points = new PointF[line.YData.Length];
                        for (int i = 0; i < line.YData.Length; i++)
                        {
                            points[i].X = leftRight + i * offect;
                            points[i].Y = BasicFramework.SoftPainting.ComputePaintLocationY(
                                line.IsLeftFrame ? value_max_left : value_max_right,
                                line.IsLeftFrame ? value_min_left : value_min_right,
                                (heigh_totle - upDowm - upDowm), line.YData[i]) + upDowm;
                        }
                        curvePoints[key] = points;
                        //如果加粗显示，则在原线宽上+2
                        using (Pen penTmp = new Pen(line.LineColor, line.Blod ? line.LineThickness + 2 : line.LineThickness))
                        {
                            g.DrawLines(penTmp, points);
                        }
                    }
                }
            }
            else
            {
                // 横坐标对应图形
                foreach (var key in data_list.Keys)
                {
                    var line = data_list[key];
                    if (!line.Visible) continue;

                    if (line.YData?.Length > 1)
                    {
                        int countTmp = width_totle - 2 * leftRight + 1;
                        PointF[] points;
                        // 点数大于1的时候才绘制
                        if (line.YData.Length <= countTmp)
                        {
                            points = new PointF[line.YData.Length];
                            for (int i = 0; i < line.YData.Length; i++)
                            {
                                points[i].X = leftRight + i;
                                points[i].Y = BasicFramework.SoftPainting.ComputePaintLocationY(
                                    line.IsLeftFrame ? value_max_left : value_max_right,
                                    line.IsLeftFrame ? value_min_left : value_min_right,
                                    (heigh_totle - upDowm - upDowm), line.YData[i]) + upDowm;
                            }
                            curvePoints[key] = points;
                        }
                        else
                        {
                            points = new PointF[countTmp];
                            for (int i = 0; i < points.Length; i++)
                            {
                                points[i].X = leftRight + i;
                                points[i].Y = BasicFramework.SoftPainting.ComputePaintLocationY(
                                    line.IsLeftFrame ? value_max_left : value_max_right,
                                    line.IsLeftFrame ? value_min_left : value_min_right,
                                    (heigh_totle - upDowm - upDowm), line.YData[i + line.YData.Length - countTmp]) + upDowm;
                            }
                            curvePoints[key] = points;
                        }

                        using (Pen penTmp = new Pen(line.LineColor, line.Blod ? line.LineThickness + 2 : line.LineThickness))
                        {
                            g.DrawLines(penTmp, points);
                        }
                    }
                }
            }
            /// 高亮的点
            if (highlightPoint != null)
            {
                using (Pen penTmp = new Pen(highlightPoint.PointColor, highlightPoint.Thickness))
                {
                    g.DrawEllipse(penTmp, highlightPoint.Point.X - 3, highlightPoint.Point.Y - 3, 6, 6);
                    string txt = string.Format("[{0},{1}]", highlightPoint.XData.ToString(), highlightPoint.YData.ToString());
                    Rectangle rec = new Rectangle((int)highlightPoint.Point.X + 3, (int)highlightPoint.Point.Y + 3, txt.Length * 10, 30);
                    g.DrawString(txt, font_size9,title_brush_deep, rec, format_left);
                }
            }
        }



        /// <summary>
        /// 每条曲线的所有真实坐标点
        /// </summary>
        private Dictionary<string, PointF[]> curvePoints = new Dictionary<string, PointF[]>();


        private bool IsNeedPaintDash(float paintValue)
        {
            // 遍历所有的数据组
            for (int i = 0; i < auxiliary_lines.Count; i++)
            {
                if (Math.Abs(auxiliary_lines[i].PaintValue - paintValue) < font_size9.Height)
                {
                    // 与辅助线冲突，不需要绘制
                    return false;
                }
            }

            // 需要绘制虚线
            return true;
        }

        private int CalculateDataCountByOffect(float offect)
        {
            if (offect > 40) return 1;
            offect = 40f / offect;
            return (int)Math.Ceiling(offect);
        }

        #endregion

        #region Size Changed

        private void UserCurve_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion


    }

    /// <summary>
    /// 高亮的点
    /// </summary>
    public class HslHighlightPointItem
    {
        public HslHighlightPointItem()
        {
            PointColor = Color.DarkRed;
            Thickness = 3f;
            IsLeftFrame = true;

        }

        /// <summary>
        /// 所属权限的关键字
        /// </summary>
        public string Key { get; set; }

        public PointF Point { get; set; }
        /// <summary>
        /// Y轴数据
        /// </summary>
        public float? YData = null;
        /// <summary>
        /// X轴数据
        /// </summary>
        public float? XData = null;

        /// <summary>
        /// 点的宽度
        /// </summary>
        public float Thickness { get; set; }

        /// <summary>
        /// 点颜色
        /// </summary>
        public Color PointColor { get; set; }

        /// <summary>
        /// 是否左侧参考系，True为左侧，False为右侧
        /// </summary>
        public bool IsLeftFrame { get; set; }
    }

    /// <summary>
    /// 曲线数据对象
    /// </summary>
    public class HslCurveItem
    {
        /// <summary>
        /// 实例化一个对象
        /// </summary>
        public HslCurveItem()
        {
            LineThickness = 2.0f;
            IsLeftFrame = true;
            Visible = true;
        }


        /// <summary>
        /// Y轴数据
        /// </summary>
        public float[] YData = null;
        /// <summary>
        /// X轴数据
        /// </summary>
        public float[] XData = null;

        /// <summary>
        /// 线条的宽度
        /// </summary>
        public float LineThickness { get; set; }

        /// <summary>
        /// 曲线颜色
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// 是否左侧参考系，True为左侧，False为右侧
        /// </summary>
        public bool IsLeftFrame { get; set; }

        /// <summary>
        /// 本曲线是否显示出来，默认为显示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 是否加粗显示
        /// </summary>
        public bool Blod { get; set; }
    }

    /// <summary>
    /// 辅助线对象
    /// </summary>
    internal class AuxiliaryLine : IDisposable
    {
        /// <summary>
        /// 实际的数据值
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// 实际的数据绘制
        /// </summary>
        public float PaintValue { get; set; }

        /// <summary>
        /// 辅助线的颜色
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// 辅助线的画笔资源
        /// </summary>
        public Pen PenDash { get; set; }

        /// <summary>
        /// 辅助线的宽度
        /// </summary>
        public float LineThickness { get; set; }

        /// <summary>
        /// 辅助线文本的画刷
        /// </summary>
        public Brush LineTextBrush { get; set; }

        /// <summary>
        /// 是否左侧参考系，True为左侧，False为右侧
        /// </summary>
        public bool IsLeftFrame { get; set; }

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    PenDash?.Dispose();
                    LineTextBrush.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AuxiliaryLine() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。

        /// <summary>
        /// 释放内存信息
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
