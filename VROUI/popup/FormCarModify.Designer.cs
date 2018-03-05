namespace VROUI.popup
{
    partial class FormCarModify
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
            this.buttonClose = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxCarList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.buttonModify = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCarList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(178, 49);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 33);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "닫기";
            // 
            // comboBoxCarList
            // 
            this.comboBoxCarList.Location = new System.Drawing.Point(12, 12);
            this.comboBoxCarList.Name = "comboBoxCarList";
            this.comboBoxCarList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxCarList.Size = new System.Drawing.Size(235, 20);
            this.comboBoxCarList.TabIndex = 2;
            // 
            // buttonModify
            // 
            this.buttonModify.Location = new System.Drawing.Point(97, 49);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(75, 33);
            this.buttonModify.TabIndex = 1;
            this.buttonModify.Text = "변경";
            // 
            // FormCarModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 88);
            this.Controls.Add(this.comboBoxCarList);
            this.Controls.Add(this.buttonModify);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormCarModify";
            this.Text = "FormCarModify";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCarList.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton buttonClose;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxCarList;
        private DevExpress.XtraEditors.SimpleButton buttonModify;

    }
}