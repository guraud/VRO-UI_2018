using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;

namespace VROUI.popup
{
    public partial class FormBonus : DevExpress.XtraEditors.XtraForm
    {
        public FormBonus()
        {
            InitializeComponent();
        }

        private void FormBonus_Load(object sender, EventArgs e)
        {
            InitializeDataForBonusList();
        }

        private void InitializeDataForBonusList()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("순번", typeof(string));
            dt.Columns.Add("차량번호", typeof(string));
            dt.Columns.Add("금액", typeof(string));

            DataRow dr1 = dt.NewRow();
            dr1["순번"] = "1";
            dr1["차량번호"] = "62조6256";
            dr1["금액"] = "10,000";

            dt.Rows.Add(dr1);

            gridBonusList.DataSource = dt;
            gridBonusList.RefreshDataSource();
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(gridView1, "UnSchedule List");
        }

        public void ExportToExcel(BaseView exportView, string sName)
        {
            //string fileName = this.ShowSaveFileDialog(sName);
            string fileName = "c:/" + sName+".xls";

            if (!string.IsNullOrEmpty(fileName))
            {
                DevExpress.XtraPrinting.XlsExportOptions xlsExportOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                //xlsExportOptions.TextExportMode = TextExportMode.Text;
                xlsExportOptions.TextExportMode = TextExportMode.Value;
                System.Windows.Forms.Cursor currentCursor = System.Windows.Forms.Cursor.Current;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                exportView.ExportToXls(fileName, xlsExportOptions);
                System.Windows.Forms.Cursor.Current = currentCursor;
            }
        }

    }
}