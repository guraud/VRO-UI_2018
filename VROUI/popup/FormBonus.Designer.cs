namespace VROUI.popup
{
    partial class FormBonus
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
            this.gridBonusList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.buttonExcel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridBonusList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBonusList
            // 
            this.gridBonusList.Location = new System.Drawing.Point(1, 2);
            this.gridBonusList.MainView = this.gridView1;
            this.gridBonusList.Name = "gridBonusList";
            this.gridBonusList.Size = new System.Drawing.Size(255, 372);
            this.gridBonusList.TabIndex = 0;
            this.gridBonusList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridBonusList;
            this.gridView1.Name = "gridView1";
            // 
            // buttonExcel
            // 
            this.buttonExcel.Location = new System.Drawing.Point(12, 380);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(107, 23);
            this.buttonExcel.TabIndex = 1;
            this.buttonExcel.Text = "엑셀변환";
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(136, 380);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(107, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "닫기";
            // 
            // FormBonus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 410);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.buttonExcel);
            this.Controls.Add(this.gridBonusList);
            this.Name = "FormBonus";
            this.Text = "XtraForm1";
            this.Load += new System.EventHandler(this.FormBonus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBonusList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridBonusList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton buttonExcel;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}