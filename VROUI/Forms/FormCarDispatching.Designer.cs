namespace VROUI.Forms
{
    partial class FormCarDispatching
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridCarDispatching = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.차량번호 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.carNum = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.carType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.working_startTime_hour = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditworking_startTime_hour = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.working_startTime_minute = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditworking_startTime_minute = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.working_startTime_second = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditworking_startTime_second = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.working_endTime_hour = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditworking_endTime_hour = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.working_endTime_minute = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditworking_endTime_minute = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.working_endTime_second = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditworking_endTime_second = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.working_apply = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxworking_apply = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.buttonClose = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSave = new DevExpress.XtraEditors.SimpleButton();
            this.buttonView = new DevExpress.XtraEditors.SimpleButton();
            this.screenManager = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::VROUI.FormWait), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCarDispatching)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_startTime_hour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_startTime_minute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_startTime_second)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_endTime_hour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_endTime_minute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_endTime_second)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxworking_apply)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.gridCarDispatching);
            this.groupControl2.Location = new System.Drawing.Point(10, 78);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1288, 542);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = " [ 선배차 정보관리 ]";
            // 
            // gridCarDispatching
            // 
            this.gridCarDispatching.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCarDispatching.Location = new System.Drawing.Point(16, 40);
            this.gridCarDispatching.MainView = this.bandedGridView1;
            this.gridCarDispatching.Name = "gridCarDispatching";
            this.gridCarDispatching.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditworking_startTime_hour,
            this.repositoryItemSpinEditworking_startTime_minute,
            this.repositoryItemSpinEditworking_startTime_second,
            this.repositoryItemSpinEditworking_endTime_hour,
            this.repositoryItemSpinEditworking_endTime_minute,
            this.repositoryItemSpinEditworking_endTime_second,
            this.repositoryItemComboBoxworking_apply});
            this.gridCarDispatching.Size = new System.Drawing.Size(1256, 487);
            this.gridCarDispatching.TabIndex = 0;
            this.gridCarDispatching.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.차량번호,
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.carNum,
            this.carType,
            this.working_startTime_hour,
            this.working_startTime_minute,
            this.working_startTime_second,
            this.working_endTime_hour,
            this.working_endTime_minute,
            this.working_endTime_second,
            this.working_apply});
            this.bandedGridView1.GridControl = this.gridCarDispatching;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsView.ShowColumnHeaders = false;
            // 
            // 차량번호
            // 
            this.차량번호.AppearanceHeader.Options.UseTextOptions = true;
            this.차량번호.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.차량번호.Caption = "차량번호";
            this.차량번호.Columns.Add(this.carNum);
            this.차량번호.Name = "차량번호";
            this.차량번호.Width = 75;
            // 
            // carNum
            // 
            this.carNum.Caption = "차량정보";
            this.carNum.FieldName = "carNum";
            this.carNum.Name = "carNum";
            this.carNum.Visible = true;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "차종";
            this.gridBand2.Columns.Add(this.carType);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 75;
            // 
            // carType
            // 
            this.carType.Caption = "차종";
            this.carType.FieldName = "carType";
            this.carType.Name = "carType";
            this.carType.Visible = true;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "근무가능 시작시간";
            this.gridBand3.Columns.Add(this.working_startTime_hour);
            this.gridBand3.Columns.Add(this.working_startTime_minute);
            this.gridBand3.Columns.Add(this.working_startTime_second);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.Width = 225;
            // 
            // working_startTime_hour
            // 
            this.working_startTime_hour.Caption = "근무가능 시작시간 시";
            this.working_startTime_hour.ColumnEdit = this.repositoryItemSpinEditworking_startTime_hour;
            this.working_startTime_hour.DisplayFormat.FormatString = "00";
            this.working_startTime_hour.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.working_startTime_hour.FieldName = "working_startTime_hour";
            this.working_startTime_hour.Name = "working_startTime_hour";
            this.working_startTime_hour.Visible = true;
            // 
            // repositoryItemSpinEditworking_startTime_hour
            // 
            this.repositoryItemSpinEditworking_startTime_hour.AutoHeight = false;
            this.repositoryItemSpinEditworking_startTime_hour.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditworking_startTime_hour.EditFormat.FormatString = "00";
            this.repositoryItemSpinEditworking_startTime_hour.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditworking_startTime_hour.Mask.EditMask = "00";
            this.repositoryItemSpinEditworking_startTime_hour.Name = "repositoryItemSpinEditworking_startTime_hour";
            // 
            // working_startTime_minute
            // 
            this.working_startTime_minute.Caption = "근무가능 시작시간 분";
            this.working_startTime_minute.ColumnEdit = this.repositoryItemSpinEditworking_startTime_minute;
            this.working_startTime_minute.DisplayFormat.FormatString = "00";
            this.working_startTime_minute.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.working_startTime_minute.FieldName = "working_startTime_minute";
            this.working_startTime_minute.Name = "working_startTime_minute";
            this.working_startTime_minute.Visible = true;
            // 
            // repositoryItemSpinEditworking_startTime_minute
            // 
            this.repositoryItemSpinEditworking_startTime_minute.AutoHeight = false;
            this.repositoryItemSpinEditworking_startTime_minute.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditworking_startTime_minute.EditFormat.FormatString = "00";
            this.repositoryItemSpinEditworking_startTime_minute.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditworking_startTime_minute.Mask.EditMask = "00";
            this.repositoryItemSpinEditworking_startTime_minute.Name = "repositoryItemSpinEditworking_startTime_minute";
            // 
            // working_startTime_second
            // 
            this.working_startTime_second.Caption = "근무가능 시작시간  초";
            this.working_startTime_second.ColumnEdit = this.repositoryItemSpinEditworking_startTime_second;
            this.working_startTime_second.DisplayFormat.FormatString = "00";
            this.working_startTime_second.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.working_startTime_second.FieldName = "working_startTime_second";
            this.working_startTime_second.Name = "working_startTime_second";
            this.working_startTime_second.Visible = true;
            // 
            // repositoryItemSpinEditworking_startTime_second
            // 
            this.repositoryItemSpinEditworking_startTime_second.AutoHeight = false;
            this.repositoryItemSpinEditworking_startTime_second.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditworking_startTime_second.EditFormat.FormatString = "00";
            this.repositoryItemSpinEditworking_startTime_second.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditworking_startTime_second.Mask.EditMask = "00";
            this.repositoryItemSpinEditworking_startTime_second.Name = "repositoryItemSpinEditworking_startTime_second";
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.Caption = "근무가능 종료시간";
            this.gridBand4.Columns.Add(this.working_endTime_hour);
            this.gridBand4.Columns.Add(this.working_endTime_minute);
            this.gridBand4.Columns.Add(this.working_endTime_second);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.Width = 225;
            // 
            // working_endTime_hour
            // 
            this.working_endTime_hour.Caption = "근무가능 종료시간 시";
            this.working_endTime_hour.ColumnEdit = this.repositoryItemSpinEditworking_endTime_hour;
            this.working_endTime_hour.DisplayFormat.FormatString = "00";
            this.working_endTime_hour.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.working_endTime_hour.FieldName = "working_endTime_hour";
            this.working_endTime_hour.Name = "working_endTime_hour";
            this.working_endTime_hour.Visible = true;
            // 
            // repositoryItemSpinEditworking_endTime_hour
            // 
            this.repositoryItemSpinEditworking_endTime_hour.AutoHeight = false;
            this.repositoryItemSpinEditworking_endTime_hour.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditworking_endTime_hour.EditFormat.FormatString = "00";
            this.repositoryItemSpinEditworking_endTime_hour.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditworking_endTime_hour.Mask.EditMask = "00";
            this.repositoryItemSpinEditworking_endTime_hour.Name = "repositoryItemSpinEditworking_endTime_hour";
            // 
            // working_endTime_minute
            // 
            this.working_endTime_minute.Caption = "근무가능 종료시간 분";
            this.working_endTime_minute.ColumnEdit = this.repositoryItemSpinEditworking_endTime_minute;
            this.working_endTime_minute.DisplayFormat.FormatString = "00";
            this.working_endTime_minute.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.working_endTime_minute.FieldName = "working_endTime_minute";
            this.working_endTime_minute.Name = "working_endTime_minute";
            this.working_endTime_minute.Visible = true;
            // 
            // repositoryItemSpinEditworking_endTime_minute
            // 
            this.repositoryItemSpinEditworking_endTime_minute.AutoHeight = false;
            this.repositoryItemSpinEditworking_endTime_minute.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditworking_endTime_minute.EditFormat.FormatString = "00";
            this.repositoryItemSpinEditworking_endTime_minute.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditworking_endTime_minute.Mask.EditMask = "00";
            this.repositoryItemSpinEditworking_endTime_minute.Name = "repositoryItemSpinEditworking_endTime_minute";
            // 
            // working_endTime_second
            // 
            this.working_endTime_second.Caption = "근무가능 종료시간 초";
            this.working_endTime_second.ColumnEdit = this.repositoryItemSpinEditworking_endTime_second;
            this.working_endTime_second.DisplayFormat.FormatString = "00";
            this.working_endTime_second.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.working_endTime_second.FieldName = "working_endTime_second";
            this.working_endTime_second.Name = "working_endTime_second";
            this.working_endTime_second.Visible = true;
            // 
            // repositoryItemSpinEditworking_endTime_second
            // 
            this.repositoryItemSpinEditworking_endTime_second.AutoHeight = false;
            this.repositoryItemSpinEditworking_endTime_second.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditworking_endTime_second.EditFormat.FormatString = "00";
            this.repositoryItemSpinEditworking_endTime_second.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditworking_endTime_second.Mask.EditMask = "00";
            this.repositoryItemSpinEditworking_endTime_second.Name = "repositoryItemSpinEditworking_endTime_second";
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.Caption = "근무가능시간 적용여부";
            this.gridBand5.Columns.Add(this.working_apply);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.Width = 75;
            // 
            // working_apply
            // 
            this.working_apply.Caption = "근무가능시간 적용여부";
            this.working_apply.ColumnEdit = this.repositoryItemComboBoxworking_apply;
            this.working_apply.FieldName = "working_apply";
            this.working_apply.Name = "working_apply";
            this.working_apply.Visible = true;
            // 
            // repositoryItemComboBoxworking_apply
            // 
            this.repositoryItemComboBoxworking_apply.AutoHeight = false;
            this.repositoryItemComboBoxworking_apply.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxworking_apply.Name = "repositoryItemComboBoxworking_apply";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Image = global::VROUI.Properties.Resources.btn_close;
            this.buttonClose.Location = new System.Drawing.Point(1203, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(95, 48);
            this.buttonClose.TabIndex = 46;
            this.buttonClose.Text = "종료";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Image = global::VROUI.Properties.Resources.btn_save;
            this.buttonSave.Location = new System.Drawing.Point(1036, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(95, 48);
            this.buttonSave.TabIndex = 45;
            this.buttonSave.Text = "저장";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonView
            // 
            this.buttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonView.Image = global::VROUI.Properties.Resources.btn_search;
            this.buttonView.Location = new System.Drawing.Point(932, 12);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(95, 48);
            this.buttonView.TabIndex = 44;
            this.buttonView.Text = "조회";
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            // 
            // FormCarDispatching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 632);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.groupControl2);
            this.Name = "FormCarDispatching";
            this.Text = "선배차 차량관리";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCarDispatching_FormClosed);
            this.Load += new System.EventHandler(this.FormCarDispatching_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCarDispatching)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_startTime_hour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_startTime_minute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_startTime_second)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_endTime_hour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_endTime_minute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditworking_endTime_second)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxworking_apply)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridCarDispatching;
        private DevExpress.XtraEditors.SimpleButton buttonClose;
        private DevExpress.XtraEditors.SimpleButton buttonSave;
        private DevExpress.XtraEditors.SimpleButton buttonView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn carNum;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn carType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn working_startTime_hour;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditworking_startTime_hour;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn working_startTime_minute;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditworking_startTime_minute;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn working_startTime_second;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditworking_startTime_second;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn working_endTime_hour;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditworking_endTime_hour;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn working_endTime_minute;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditworking_endTime_minute;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn working_endTime_second;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditworking_endTime_second;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn working_apply;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxworking_apply;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand 차량번호;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraSplashScreen.SplashScreenManager screenManager;
    }
}