using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VROUI.objectPool;
using DevExpress.XtraEditors.Controls;

namespace VROUI.popup
{
    public partial class FormCarModify : DevExpress.XtraEditors.XtraForm
    {
        public FormCarModify()
        {
            InitializeComponent();
            comboBoxinit();
        }

        private void comboBoxinit()
        {
            ComboBoxItemCollection coll = comboBoxCarList.Properties.Items;
            coll.BeginUpdate();
            try
            {
                //coll.Add(new PersonInfo("Sven"));
                //coll.Add(new PersonInfo("Cheryl"));
                //coll.Add(new PersonInfo("Dirk"));
            }
            finally
            {
                coll.EndUpdate();
            }
            comboBoxCarList.SelectedIndex = -1;

            Controls.Add(comboBoxCarList);
        }


    }
}