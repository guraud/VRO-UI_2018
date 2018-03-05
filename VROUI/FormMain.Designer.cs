namespace VROUI
{
    partial class FormMain
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
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barMain = new DevExpress.XtraBars.Bar();
            this.menuFile = new DevExpress.XtraBars.BarSubItem();
            this.menuExit = new DevExpress.XtraBars.BarButtonItem();
            this.menuMasterData = new DevExpress.XtraBars.BarSubItem();
            this.menuParameterSetup = new DevExpress.XtraBars.BarButtonItem();
            this.menuDispatchingGeneration = new DevExpress.XtraBars.BarSubItem();
            this.menuMakeDispatching = new DevExpress.XtraBars.BarButtonItem();
            this.menuDispatchingCar = new DevExpress.XtraBars.BarButtonItem();
            this.menuDispatchingManagement = new DevExpress.XtraBars.BarSubItem();
            this.menuConfirmDispatching = new DevExpress.XtraBars.BarButtonItem();
            this.menuFinalizeDispatching = new DevExpress.XtraBars.BarButtonItem();
            this.menuWindow = new DevExpress.XtraBars.BarSubItem();
            this.menuPaintStyle = new DevExpress.XtraBars.BarSubItem();
            this.menuHelp = new DevExpress.XtraBars.BarSubItem();
            this.buttonTest = new DevExpress.XtraBars.BarButtonItem();
            this.barStatus = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barCenter = new DevExpress.XtraBars.BarStaticItem();
            this.barWhereHouse = new DevExpress.XtraBars.BarStaticItem();
            this.barUser = new DevExpress.XtraBars.BarStaticItem();
            this.barDB = new DevExpress.XtraBars.BarStaticItem();
            this.barOrder = new DevExpress.XtraBars.BarStaticItem();
            this.barResult = new DevExpress.XtraBars.BarStaticItem();
            this.tabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedMdiManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMain,
            this.barStatus});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.menuMasterData,
            this.menuDispatchingGeneration,
            this.menuDispatchingManagement,
            this.menuFile,
            this.menuWindow,
            this.menuHelp,
            this.menuExit,
            this.menuParameterSetup,
            this.menuMakeDispatching,
            this.menuConfirmDispatching,
            this.menuFinalizeDispatching,
            this.barStaticItem1,
            this.barCenter,
            this.barWhereHouse,
            this.menuPaintStyle,
            this.barUser,
            this.barDB,
            this.barOrder,
            this.barResult,
            this.menuDispatchingCar,
            this.buttonTest});
            this.barManager.MainMenu = this.barMain;
            this.barManager.MaxItemId = 35;
            this.barManager.StatusBar = this.barStatus;
            // 
            // barMain
            // 
            this.barMain.BarName = "Main menu";
            this.barMain.DockCol = 0;
            this.barMain.DockRow = 0;
            this.barMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMain.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuMasterData),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuDispatchingGeneration),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuDispatchingManagement),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuWindow),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuHelp)});
            this.barMain.OptionsBar.MultiLine = true;
            this.barMain.OptionsBar.UseWholeRow = true;
            this.barMain.Text = "Main menu";
            // 
            // menuFile
            // 
            this.menuFile.Caption = "파일(&F)";
            this.menuFile.Id = 13;
            this.menuFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuExit)});
            this.menuFile.Name = "menuFile";
            // 
            // menuExit
            // 
            this.menuExit.Caption = "종료(&X)";
            this.menuExit.Id = 17;
            this.menuExit.Name = "menuExit";
            this.menuExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuExit_ItemClick);
            // 
            // menuMasterData
            // 
            this.menuMasterData.Caption = "기준정보관리(&M)";
            this.menuMasterData.Id = 0;
            this.menuMasterData.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuParameterSetup)});
            this.menuMasterData.Name = "menuMasterData";
            // 
            // menuParameterSetup
            // 
            this.menuParameterSetup.Caption = "파라메터설정(&P)";
            this.menuParameterSetup.Id = 18;
            this.menuParameterSetup.Name = "menuParameterSetup";
            this.menuParameterSetup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuParameterSetup_ItemClick);
            // 
            // menuDispatchingGeneration
            // 
            this.menuDispatchingGeneration.Caption = "자동배차관리(&A)";
            this.menuDispatchingGeneration.Id = 10;
            this.menuDispatchingGeneration.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuMakeDispatching),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuDispatchingCar)});
            this.menuDispatchingGeneration.Name = "menuDispatchingGeneration";
            // 
            // menuMakeDispatching
            // 
            this.menuMakeDispatching.Caption = "자동배차 실행관리(&E)";
            this.menuMakeDispatching.Id = 19;
            this.menuMakeDispatching.Name = "menuMakeDispatching";
            this.menuMakeDispatching.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuMakeDispatching_ItemClick);
            // 
            // menuDispatchingCar
            // 
            this.menuDispatchingCar.Caption = "선배차 차량관리(&S)";
            this.menuDispatchingCar.Id = 33;
            this.menuDispatchingCar.Name = "menuDispatchingCar";
            this.menuDispatchingCar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuDispatchingCar_ItemClick);
            // 
            // menuDispatchingManagement
            // 
            this.menuDispatchingManagement.Caption = "배차결과관리(&D)";
            this.menuDispatchingManagement.Id = 12;
            this.menuDispatchingManagement.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuConfirmDispatching),
            new DevExpress.XtraBars.LinkPersistInfo(this.menuFinalizeDispatching)});
            this.menuDispatchingManagement.Name = "menuDispatchingManagement";
            // 
            // menuConfirmDispatching
            // 
            this.menuConfirmDispatching.Caption = "배차지시 가확정(&C)";
            this.menuConfirmDispatching.Id = 20;
            this.menuConfirmDispatching.Name = "menuConfirmDispatching";
            this.menuConfirmDispatching.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuConfirmDispatching_ItemClick);
            // 
            // menuFinalizeDispatching
            // 
            this.menuFinalizeDispatching.Caption = "배차지시 확정 (&F)";
            this.menuFinalizeDispatching.Id = 21;
            this.menuFinalizeDispatching.Name = "menuFinalizeDispatching";
            this.menuFinalizeDispatching.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.menuFinalizeDispatching.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menuFinalizeDispatching_ItemClick);
            // 
            // menuWindow
            // 
            this.menuWindow.Caption = "창(&W)";
            this.menuWindow.Id = 14;
            this.menuWindow.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menuPaintStyle)});
            this.menuWindow.Name = "menuWindow";
            // 
            // menuPaintStyle
            // 
            this.menuPaintStyle.Caption = "화면스타일(&S)";
            this.menuPaintStyle.Id = 28;
            this.menuPaintStyle.Name = "menuPaintStyle";
            // 
            // menuHelp
            // 
            this.menuHelp.Caption = "도움말(&H)";
            this.menuHelp.Id = 15;
            this.menuHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonTest)});
            this.menuHelp.Name = "menuHelp";
            // 
            // buttonTest
            // 
            this.buttonTest.Caption = "Test";
            this.buttonTest.Id = 34;
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonTest_ItemClick);
            // 
            // barStatus
            // 
            this.barStatus.BarName = "Status Bar";
            this.barStatus.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barStatus.DockCol = 0;
            this.barStatus.DockRow = 0;
            this.barStatus.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barStatus.OptionsBar.AllowQuickCustomization = false;
            this.barStatus.OptionsBar.DrawDragBorder = false;
            this.barStatus.OptionsBar.UseWholeRow = true;
            this.barStatus.Text = "Custom 3";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1114, 22);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 465);
            this.barDockControlBottom.Size = new System.Drawing.Size(1114, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 22);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 443);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1114, 22);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 443);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 23;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barCenter
            // 
            this.barCenter.Caption = "THiRA";
            this.barCenter.Id = 25;
            this.barCenter.Name = "barCenter";
            this.barCenter.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // barWhereHouse
            // 
            this.barWhereHouse.Caption = "창고 : 상온 창고   ";
            this.barWhereHouse.Id = 26;
            this.barWhereHouse.Name = "barWhereHouse";
            this.barWhereHouse.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barUser
            // 
            this.barUser.Caption = "사용자 : UserID   ";
            this.barUser.Id = 29;
            this.barUser.Name = "barUser";
            this.barUser.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDB
            // 
            this.barDB.Caption = "접속DB : GLSTCC";
            this.barDB.Id = 30;
            this.barDB.Name = "barDB";
            this.barDB.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barOrder
            // 
            this.barOrder.Caption = "주문정보 : 2013년10월22일 신규6차";
            this.barOrder.Id = 31;
            this.barOrder.Name = "barOrder";
            this.barOrder.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barResult
            // 
            this.barResult.Caption = "배차결과U/L : [CJADS04U02] 배차지시 가확정";
            this.barResult.Id = 32;
            this.barResult.Name = "barResult";
            this.barResult.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // tabbedMdiManager
            // 
            this.tabbedMdiManager.FloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            this.tabbedMdiManager.FloatOnDrag = DevExpress.Utils.DefaultBoolean.True;
            this.tabbedMdiManager.MdiParent = this;
            this.tabbedMdiManager.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.tabbedMdiManager_PageRemoved);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl11);
            this.panelControl1.Controls.Add(this.labelControl12);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 22);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1114, 51);
            this.panelControl1.TabIndex = 5;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Location = new System.Drawing.Point(878, 17);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(196, 16);
            this.labelControl11.TabIndex = 11;
            this.labelControl11.Text = "[CJADS04U02] 배차지시 가확정";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Location = new System.Drawing.Point(790, 17);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(92, 16);
            this.labelControl12.TabIndex = 10;
            this.labelControl12.Text = "배차결과U/L : ";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Location = new System.Drawing.Point(634, 17);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(150, 16);
            this.labelControl9.TabIndex = 9;
            this.labelControl9.Text = "2014년2월26일 신규7차";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Location = new System.Drawing.Point(564, 17);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(73, 16);
            this.labelControl10.TabIndex = 8;
            this.labelControl10.Text = "주문정보 : ";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(512, 17);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(50, 16);
            this.labelControl7.TabIndex = 7;
            this.labelControl7.Text = "GLST CC";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(458, 17);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(58, 16);
            this.labelControl8.TabIndex = 6;
            this.labelControl8.Text = "접속DB : ";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(397, 18);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(35, 16);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "THiRA";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(344, 18);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(58, 16);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "사용자 : ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(278, 18);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 16);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "상온창고";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(237, 18);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(43, 16);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "창고 : ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(53, 18);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 16);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "티아에스앤씨";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(12, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "센터 : ";
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCenter),
            new DevExpress.XtraBars.LinkPersistInfo(this.barWhereHouse),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUser),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDB),
            new DevExpress.XtraBars.LinkPersistInfo(this.barOrder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barResult)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 3";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 488);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Text = "자동 배차 시스템";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedMdiManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barMain;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem menuMasterData;
        private DevExpress.XtraBars.BarSubItem menuDispatchingGeneration;
        private DevExpress.XtraBars.BarSubItem menuFile;
        private DevExpress.XtraBars.BarSubItem menuDispatchingManagement;
        private DevExpress.XtraBars.BarSubItem menuWindow;
        private DevExpress.XtraBars.BarSubItem menuHelp;
        private DevExpress.XtraBars.BarButtonItem menuExit;
        private DevExpress.XtraBars.Bar barStatus;
        private DevExpress.XtraBars.BarButtonItem menuParameterSetup;
        private DevExpress.XtraBars.BarButtonItem menuMakeDispatching;
        private DevExpress.XtraBars.BarButtonItem menuConfirmDispatching;
        private DevExpress.XtraBars.BarButtonItem menuFinalizeDispatching;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem barCenter;
        private DevExpress.XtraBars.BarStaticItem barWhereHouse;
        private DevExpress.XtraBars.BarSubItem menuPaintStyle;
        private DevExpress.XtraBars.BarStaticItem barUser;
        private DevExpress.XtraBars.BarStaticItem barDB;
        private DevExpress.XtraBars.BarStaticItem barOrder;
        private DevExpress.XtraBars.BarStaticItem barResult;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem menuDispatchingCar;
        public DevExpress.XtraTabbedMdi.XtraTabbedMdiManager tabbedMdiManager;
        private DevExpress.XtraBars.BarButtonItem buttonTest;

    }
}
