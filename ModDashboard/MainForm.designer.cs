namespace ModDashboard
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.RibbonPanel ribbonPanelFigure;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.m_ribbonMain = new System.Windows.Forms.Ribbon();
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.btnConnect = new System.Windows.Forms.RibbonButton();
            this.btnDisConnect = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.btnStart = new System.Windows.Forms.RibbonButton();
            this.btnStop = new System.Windows.Forms.RibbonButton();
            this.btnClearData = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.btnMonitorSetting = new System.Windows.Forms.RibbonButton();
            this.btnViewSetting = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator2 = new System.Windows.Forms.RibbonSeparator();
            this.btnExportToFile = new System.Windows.Forms.RibbonButton();
            this.btnEndCreateFile = new System.Windows.Forms.RibbonButton();
            this.tbFileName = new System.Windows.Forms.RibbonTextBox();
            this.ribbonCheckBox1 = new System.Windows.Forms.RibbonCheckBox();
            this.ribbonTab3 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton4 = new System.Windows.Forms.RibbonButton();
            this.btnrole = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel7 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton5 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton8 = new System.Windows.Forms.RibbonButton();
            this.ribbonTab4 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel6 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton9 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton10 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton11 = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel8 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton12 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton13 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton14 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton15 = new System.Windows.Forms.RibbonButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.monitorCtrl1 = new ModDashboard.MonitorCtrl();
            ribbonPanelFigure = new System.Windows.Forms.RibbonPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonPanelFigure
            // 
            ribbonPanelFigure.ButtonMoreEnabled = false;
            ribbonPanelFigure.ButtonMoreVisible = false;
            ribbonPanelFigure.ID = null;
            ribbonPanelFigure.Name = "ribbonPanelFigure";
            ribbonPanelFigure.Text = "图形";
            ribbonPanelFigure.Visible = false;
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "ribbonTab1";
            this.ribbonTab1.Visible = false;
            // 
            // m_ribbonMain
            // 
            this.m_ribbonMain.CaptionBarVisible = false;
            this.m_ribbonMain.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.m_ribbonMain.Location = new System.Drawing.Point(0, 0);
            this.m_ribbonMain.Minimized = false;
            this.m_ribbonMain.Name = "m_ribbonMain";
            // 
            // 
            // 
            this.m_ribbonMain.OrbDropDown.BorderRoundness = 8;
            this.m_ribbonMain.OrbDropDown.ContentRecentItemsMinWidth = 0;
            this.m_ribbonMain.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.m_ribbonMain.OrbDropDown.Name = "";
            this.m_ribbonMain.OrbDropDown.RecentItemsCaption = "";
            this.m_ribbonMain.OrbDropDown.Size = new System.Drawing.Size(158, 207);
            this.m_ribbonMain.OrbDropDown.TabIndex = 0;
            this.m_ribbonMain.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010;
            this.m_ribbonMain.OrbText = "系统";
            this.m_ribbonMain.PanelCaptionHeight = 19;
            // 
            // 
            // 
            this.m_ribbonMain.QuickAccessToolbar.Visible = false;
            this.m_ribbonMain.RibbonTabFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.m_ribbonMain.Size = new System.Drawing.Size(856, 105);
            this.m_ribbonMain.TabIndex = 2;
            this.m_ribbonMain.Tabs.Add(this.ribbonTab2);
            this.m_ribbonMain.Tabs.Add(this.ribbonTab3);
            this.m_ribbonMain.Tabs.Add(this.ribbonTab4);
            this.m_ribbonMain.TabsMargin = new System.Windows.Forms.Padding(12, 2, 20, 0);
            this.m_ribbonMain.TabSpacing = 3;
            this.m_ribbonMain.Text = "`";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Panels.Add(this.ribbonPanel1);
            this.ribbonTab2.Panels.Add(this.ribbonPanel2);
            this.ribbonTab2.Text = "实时监控";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ID = null;
            this.ribbonPanel1.Items.Add(this.btnConnect);
            this.ribbonPanel1.Items.Add(this.btnDisConnect);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "设置";
            // 
            // btnConnect
            // 
            this.btnConnect.Image = global::ModDashboard.Properties.Resources.ChartYAxisSettings_32x32;
            this.btnConnect.LargeImage = global::ModDashboard.Properties.Resources.ChartYAxisSettings_32x32;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnConnect.SmallImage")));
            this.btnConnect.Text = "连接";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Enabled = false;
            this.btnDisConnect.Image = global::ModDashboard.Properties.Resources.BreakingChange_32x32;
            this.btnDisConnect.LargeImage = global::ModDashboard.Properties.Resources.BreakingChange_32x32;
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnDisConnect.SmallImage")));
            this.btnDisConnect.Text = "断开连接";
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ID = null;
            this.ribbonPanel2.Items.Add(this.btnStart);
            this.ribbonPanel2.Items.Add(this.btnStop);
            this.ribbonPanel2.Items.Add(this.btnClearData);
            this.ribbonPanel2.Items.Add(this.ribbonSeparator1);
            this.ribbonPanel2.Items.Add(this.btnMonitorSetting);
            this.ribbonPanel2.Items.Add(this.btnViewSetting);
            this.ribbonPanel2.Items.Add(this.ribbonSeparator2);
            this.ribbonPanel2.Items.Add(this.btnExportToFile);
            this.ribbonPanel2.Items.Add(this.btnEndCreateFile);
            this.ribbonPanel2.Items.Add(this.tbFileName);
            this.ribbonPanel2.Items.Add(this.ribbonCheckBox1);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "操作";
            // 
            // btnStart
            // 
            this.btnStart.Image = global::ModDashboard.Properties.Resources.Action_Debug_Start_32x32;
            this.btnStart.LargeImage = global::ModDashboard.Properties.Resources.Action_Debug_Start_32x32;
            this.btnStart.Name = "btnStart";
            this.btnStart.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnStart.SmallImage")));
            this.btnStart.Text = "开始监控";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::ModDashboard.Properties.Resources.Action_Debug_Breakpoint_Toggle_32x32;
            this.btnStop.LargeImage = global::ModDashboard.Properties.Resources.Action_Debug_Breakpoint_Toggle_32x32;
            this.btnStop.Name = "btnStop";
            this.btnStop.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnStop.SmallImage")));
            this.btnStop.Text = "结束监控";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnClearData
            // 
            this.btnClearData.Image = global::ModDashboard.Properties.Resources.Action_ClearFormatting_32x32;
            this.btnClearData.LargeImage = global::ModDashboard.Properties.Resources.Action_ClearFormatting_32x32;
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnClearData.SmallImage")));
            this.btnClearData.Text = "清除数据";
            this.btnClearData.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // btnMonitorSetting
            // 
            this.btnMonitorSetting.Image = global::ModDashboard.Properties.Resources.HistoryItem_32x32;
            this.btnMonitorSetting.LargeImage = global::ModDashboard.Properties.Resources.HistoryItem_32x32;
            this.btnMonitorSetting.Name = "btnMonitorSetting";
            this.btnMonitorSetting.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnMonitorSetting.SmallImage")));
            this.btnMonitorSetting.Text = "监控设置";
            this.btnMonitorSetting.Click += new System.EventHandler(this.btnMonitorSetting_Click);
            // 
            // btnViewSetting
            // 
            this.btnViewSetting.Image = global::ModDashboard.Properties.Resources.Action_Inline_Edit_32x32;
            this.btnViewSetting.LargeImage = global::ModDashboard.Properties.Resources.Action_Inline_Edit_32x32;
            this.btnViewSetting.Name = "btnViewSetting";
            this.btnViewSetting.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnViewSetting.SmallImage")));
            this.btnViewSetting.Text = "显示定义";
            this.btnViewSetting.Click += new System.EventHandler(this.btnViewSetting_Click);
            // 
            // ribbonSeparator2
            // 
            this.ribbonSeparator2.Name = "ribbonSeparator2";
            // 
            // btnExportToFile
            // 
            this.btnExportToFile.Image = global::ModDashboard.Properties.Resources.Action_Export_Chart_32x32;
            this.btnExportToFile.LargeImage = global::ModDashboard.Properties.Resources.Action_Export_Chart_32x32;
            this.btnExportToFile.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnExportToFile.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnExportToFile.Name = "btnExportToFile";
            this.btnExportToFile.SmallImage = global::ModDashboard.Properties.Resources.ExportToTXT_16x16;
            this.btnExportToFile.Text = "导出文件";
            this.btnExportToFile.Click += new System.EventHandler(this.btnExportToFile_Click);
            // 
            // btnEndCreateFile
            // 
            this.btnEndCreateFile.Enabled = false;
            this.btnEndCreateFile.Image = global::ModDashboard.Properties.Resources.Action_Apply_32x32;
            this.btnEndCreateFile.LargeImage = global::ModDashboard.Properties.Resources.Action_Apply_32x32;
            this.btnEndCreateFile.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnEndCreateFile.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnEndCreateFile.Name = "btnEndCreateFile";
            this.btnEndCreateFile.SmallImage = global::ModDashboard.Properties.Resources.Action_Delete_16x16;
            this.btnEndCreateFile.Text = "结束导出";
            this.btnEndCreateFile.Click += new System.EventHandler(this.btnEndCreateFile_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.TextBoxText = "";
            // 
            // ribbonCheckBox1
            // 
            this.ribbonCheckBox1.Checked = true;
            this.ribbonCheckBox1.Enabled = false;
            this.ribbonCheckBox1.Name = "ribbonCheckBox1";
            this.ribbonCheckBox1.Text = "保存所有扫描";
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Panels.Add(this.ribbonPanel3);
            this.ribbonTab3.Panels.Add(this.ribbonPanel7);
            this.ribbonTab3.Text = "基本管理";
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ID = null;
            this.ribbonPanel3.Items.Add(this.ribbonButton1);
            this.ribbonPanel3.Items.Add(this.ribbonButton4);
            this.ribbonPanel3.Items.Add(this.btnrole);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "基本管理";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = global::ModDashboard.Properties.Resources.BO_Department_32x32;
            this.ribbonButton1.LargeImage = global::ModDashboard.Properties.Resources.BO_Department_32x32;
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            this.ribbonButton1.Text = "组织管理";
            // 
            // ribbonButton4
            // 
            this.ribbonButton4.Image = global::ModDashboard.Properties.Resources.BO_Role_32x32;
            this.ribbonButton4.LargeImage = global::ModDashboard.Properties.Resources.BO_Role_32x32;
            this.ribbonButton4.Name = "ribbonButton4";
            this.ribbonButton4.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.SmallImage")));
            this.ribbonButton4.Text = "人员管理";
            // 
            // btnrole
            // 
            this.btnrole.Image = global::ModDashboard.Properties.Resources.Action_ChartDataVertical_32x32;
            this.btnrole.LargeImage = global::ModDashboard.Properties.Resources.Action_ChartDataVertical_32x32;
            this.btnrole.Name = "btnrole";
            this.btnrole.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnrole.SmallImage")));
            this.btnrole.Text = "权限管理";
            // 
            // ribbonPanel7
            // 
            this.ribbonPanel7.ID = null;
            this.ribbonPanel7.Items.Add(this.ribbonButton5);
            this.ribbonPanel7.Items.Add(this.ribbonButton8);
            this.ribbonPanel7.Name = "ribbonPanel7";
            this.ribbonPanel7.Text = "设备管理";
            // 
            // ribbonButton5
            // 
            this.ribbonButton5.Image = global::ModDashboard.Properties.Resources.product;
            this.ribbonButton5.LargeImage = global::ModDashboard.Properties.Resources.product;
            this.ribbonButton5.Name = "ribbonButton5";
            this.ribbonButton5.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton5.SmallImage")));
            this.ribbonButton5.Text = "焊机管理";
            // 
            // ribbonButton8
            // 
            this.ribbonButton8.Image = global::ModDashboard.Properties.Resources.notify;
            this.ribbonButton8.LargeImage = global::ModDashboard.Properties.Resources.notify;
            this.ribbonButton8.Name = "ribbonButton8";
            this.ribbonButton8.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton8.SmallImage")));
            this.ribbonButton8.Text = "报警管理";
            // 
            // ribbonTab4
            // 
            this.ribbonTab4.Name = "ribbonTab4";
            this.ribbonTab4.Panels.Add(this.ribbonPanel6);
            this.ribbonTab4.Panels.Add(this.ribbonPanel8);
            this.ribbonTab4.Text = "数据管理";
            // 
            // ribbonPanel6
            // 
            this.ribbonPanel6.ID = null;
            this.ribbonPanel6.Items.Add(this.ribbonButton9);
            this.ribbonPanel6.Items.Add(this.ribbonButton10);
            this.ribbonPanel6.Items.Add(this.ribbonButton11);
            this.ribbonPanel6.Name = "ribbonPanel6";
            this.ribbonPanel6.Text = "查询";
            // 
            // ribbonButton9
            // 
            this.ribbonButton9.Image = global::ModDashboard.Properties.Resources.BO_Person_32x32;
            this.ribbonButton9.LargeImage = global::ModDashboard.Properties.Resources.BO_Person_32x32;
            this.ribbonButton9.Name = "ribbonButton9";
            this.ribbonButton9.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton9.SmallImage")));
            this.ribbonButton9.Text = "按人员查询";
            // 
            // ribbonButton10
            // 
            this.ribbonButton10.Image = global::ModDashboard.Properties.Resources.product;
            this.ribbonButton10.LargeImage = global::ModDashboard.Properties.Resources.product;
            this.ribbonButton10.Name = "ribbonButton10";
            this.ribbonButton10.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton10.SmallImage")));
            this.ribbonButton10.Text = "按设备查询";
            // 
            // ribbonButton11
            // 
            this.ribbonButton11.Image = global::ModDashboard.Properties.Resources.BO_Scheduler_32x32;
            this.ribbonButton11.LargeImage = global::ModDashboard.Properties.Resources.BO_Scheduler_32x32;
            this.ribbonButton11.Name = "ribbonButton11";
            this.ribbonButton11.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton11.SmallImage")));
            this.ribbonButton11.Text = "按时间查询";
            // 
            // ribbonPanel8
            // 
            this.ribbonPanel8.ID = null;
            this.ribbonPanel8.Items.Add(this.ribbonButton12);
            this.ribbonPanel8.Items.Add(this.ribbonButton13);
            this.ribbonPanel8.Items.Add(this.ribbonButton14);
            this.ribbonPanel8.Items.Add(this.ribbonButton15);
            this.ribbonPanel8.Name = "ribbonPanel8";
            this.ribbonPanel8.Text = "数据统计";
            // 
            // ribbonButton12
            // 
            this.ribbonButton12.Image = global::ModDashboard.Properties.Resources.DrillDownOnSeries_Chart_32x32;
            this.ribbonButton12.LargeImage = global::ModDashboard.Properties.Resources.DrillDownOnSeries_Chart_32x32;
            this.ribbonButton12.Name = "ribbonButton12";
            this.ribbonButton12.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton12.SmallImage")));
            this.ribbonButton12.Text = "设备空载率";
            // 
            // ribbonButton13
            // 
            this.ribbonButton13.Image = global::ModDashboard.Properties.Resources.RangeBar_32x32;
            this.ribbonButton13.LargeImage = global::ModDashboard.Properties.Resources.RangeBar_32x32;
            this.ribbonButton13.Name = "ribbonButton13";
            this.ribbonButton13.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton13.SmallImage")));
            this.ribbonButton13.Text = "设备闲置率";
            // 
            // ribbonButton14
            // 
            this.ribbonButton14.Image = global::ModDashboard.Properties.Resources.RangeArea_32x32;
            this.ribbonButton14.LargeImage = global::ModDashboard.Properties.Resources.RangeArea_32x32;
            this.ribbonButton14.Name = "ribbonButton14";
            this.ribbonButton14.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton14.SmallImage")));
            this.ribbonButton14.Text = "运行数据统计";
            // 
            // ribbonButton15
            // 
            this.ribbonButton15.Image = global::ModDashboard.Properties.Resources.TimeLineView_32x32;
            this.ribbonButton15.LargeImage = global::ModDashboard.Properties.Resources.TimeLineView_32x32;
            this.ribbonButton15.Name = "ribbonButton15";
            this.ribbonButton15.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton15.SmallImage")));
            this.ribbonButton15.Text = "参数超标分析";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ChartDemoEmptyPoints.Icon.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 446);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(856, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 105);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(856, 341);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.monitorCtrl1);
            this.tabPage2.ImageIndex = 0;
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(848, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "实时监控";
            // 
            // monitorCtrl1
            // 
            this.monitorCtrl1.Address = "0000";
            this.monitorCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorCtrl1.ElectricitColor = System.Drawing.Color.Blue;
            this.monitorCtrl1.ElectricityEnd = 300F;
            this.monitorCtrl1.ElectricityExpress = null;
            this.monitorCtrl1.ElectricityLower = 0F;
            this.monitorCtrl1.ElectricityStart = 200F;
            this.monitorCtrl1.ElectricityUpper = 0F;
            this.monitorCtrl1.Interval = 1000;
            this.monitorCtrl1.Length = ((ushort)(2));
            this.monitorCtrl1.Location = new System.Drawing.Point(3, 3);
            this.monitorCtrl1.Name = "monitorCtrl1";
            this.monitorCtrl1.Size = new System.Drawing.Size(842, 300);
            this.monitorCtrl1.TabIndex = 5;
            this.monitorCtrl1.VoltageColor = System.Drawing.Color.Green;
            this.monitorCtrl1.VoltageEnd = 0F;
            this.monitorCtrl1.VoltageExpress = null;
            this.monitorCtrl1.VoltageLower = 0F;
            this.monitorCtrl1.VoltageStart = 10F;
            this.monitorCtrl1.VoltageUpper = 30F;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 468);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.m_ribbonMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据监控平台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.Ribbon m_ribbonMain;
        private System.Windows.Forms.RibbonTab ribbonTab2;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton btnConnect;
        private System.Windows.Forms.RibbonTab ribbonTab3;
        private System.Windows.Forms.RibbonButton btnViewSetting;
        private System.Windows.Forms.RibbonButton btnStart;
        private System.Windows.Forms.RibbonButton btnStop;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonButton ribbonButton4;
        private System.Windows.Forms.RibbonPanel ribbonPanel7;
        private System.Windows.Forms.RibbonButton ribbonButton5;
        private System.Windows.Forms.RibbonButton ribbonButton8;
        private System.Windows.Forms.RibbonTab ribbonTab4;
        private System.Windows.Forms.RibbonPanel ribbonPanel6;
        private System.Windows.Forms.RibbonButton ribbonButton9;
        private System.Windows.Forms.RibbonButton ribbonButton10;
        private System.Windows.Forms.RibbonButton ribbonButton11;
        private System.Windows.Forms.RibbonPanel ribbonPanel8;
        private System.Windows.Forms.RibbonButton ribbonButton12;
        private System.Windows.Forms.RibbonButton ribbonButton13;
        private System.Windows.Forms.RibbonButton ribbonButton14;
        private System.Windows.Forms.RibbonButton ribbonButton15;
        private System.Windows.Forms.RibbonButton btnMonitorSetting;
        private System.Windows.Forms.RibbonButton btnrole;
        private System.Windows.Forms.RibbonButton btnDisConnect;
        private MonitorCtrl monitorCtrl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator2;
        private System.Windows.Forms.RibbonButton btnExportToFile;
        private System.Windows.Forms.RibbonButton btnEndCreateFile;
        private System.Windows.Forms.RibbonTextBox tbFileName;
        private System.Windows.Forms.RibbonCheckBox ribbonCheckBox1;
        private System.Windows.Forms.RibbonButton btnClearData;
    }
}