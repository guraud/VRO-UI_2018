using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using VROUI.Controls;
using VROUI.objectPool;
using VROUI.popup;

namespace VROUI.Forms
{
    public partial class FormConfirmDispatching : DevExpress.XtraEditors.XtraForm
    {
        public FormConfirmDispatching()
        {
            InitializeComponent();

            controlConfirmDispatching.Initialize();
        }

        private void buttonChangeVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                FormCarModify formCarModify = new FormCarModify();
                formCarModify.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            FormFlag.formConfirmDispatching_flag = false;
            Close();
        }

        private void FormConfirmDispatching_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Form[] formChildren = this.MdiParent.MdiChildren;
                FormMakeDispatching formMakeDispatching = null;

                foreach (Form formChild in formChildren)
                {
                    if (formChild.Name.Equals("FormMakeDispatching"))
                    {
                        FormFlag.formMakeDispatching_flag = true;
                        formMakeDispatching = new FormMakeDispatching();
                        formMakeDispatching = (FormMakeDispatching)formChild;
                    }
                }
                if (FormFlag.formMakeDispatching_flag)
                {
                    formMakeDispatching.BringToFront();
                    formMakeDispatching.Button_change();
                }
                else
                {
                    formMakeDispatching = new FormMakeDispatching();
                    formMakeDispatching.MdiParent = this.MdiParent;
                    formMakeDispatching.Show();
                    //폼생성 플래그 true
                    FormFlag.formMakeDispatching_flag = true;
                    formMakeDispatching.Button_change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormConfirmDispatching_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormFlag.formConfirmDispatching_flag = false;
            //Close();
        }

    }
}