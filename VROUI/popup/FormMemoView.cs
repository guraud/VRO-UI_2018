using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VROUI.popup
{
    public partial class FormMemoView : DevExpress.XtraEditors.XtraForm
    {
        public FormMemoView()
        {
            InitializeComponent();
        }

        private void FormMemoView_Load(object sender, EventArgs e)
        {
            InitializeDataForMemo();
        }

        private void InitializeDataForMemo()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("순번", typeof(string));
            dt.Columns.Add("주문번호", typeof(string));
            dt.Columns.Add("고객", typeof(string));
            dt.Columns.Add("메모", typeof(string));

            DataRow dr1 = dt.NewRow();
            dr1["순번"] = "1";
            dr1["주문번호"] = "102205461235";
            dr1["고객"] = "customer_001";
            dr1["메모"] = "경기도 용인시 기흥구 하갈동 177";

            dt.Rows.Add(dr1);

            gridMemoList.DataSource = dt;
            gridMemoList.RefreshDataSource();
        }
    }
}