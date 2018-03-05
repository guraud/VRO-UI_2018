using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using Spring.Context;
using Spring.Context.Support;
using VROUI.Services;
using Devart.Data.Oracle;
using VROUI.objectPool;

namespace VROUI.Forms
{
    public partial class FormCarDispatching : DevExpress.XtraEditors.XtraForm
    {
        #region variable

        DataTable dtGridCarDispatching;

        IApplicationContext _applicationContext = ContextRegistry.GetContext();
        BizService bizService;

        #endregion

        public FormCarDispatching()
        {
            InitializeComponent();

            XtraHelper.InitializeEditableGrid(gridCarDispatching);
            repositoryItemComboBoxworking_apply.EditValueChanged += new EventHandler(repositoryItemComboBox_Working_Apply_EditValueChanged);

        }

        private void FormCarDispatching_Load(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void InitializeData()
        {
            dtGridCarDispatching = new DataTable();

            dtGridCarDispatching.Columns.Add("carNum", typeof(string));
            dtGridCarDispatching.Columns.Add("carType", typeof(string));
            dtGridCarDispatching.Columns.Add("working_startTime", typeof(string));
            dtGridCarDispatching.Columns.Add("working_startTime_hour", typeof(int));
            dtGridCarDispatching.Columns.Add("working_startTime_minute", typeof(string));
            dtGridCarDispatching.Columns.Add("working_startTime_second", typeof(string));
            dtGridCarDispatching.Columns.Add("working_endTime", typeof(string));
            dtGridCarDispatching.Columns.Add("working_endTime_hour", typeof(string));
            dtGridCarDispatching.Columns.Add("working_endTime_minute", typeof(string));
            dtGridCarDispatching.Columns.Add("working_endTime_second", typeof(string));
            dtGridCarDispatching.Columns.Add("working_apply", typeof(string));

            gridCarDispatching.DataSource = dtGridCarDispatching;
            gridCarDispatching.RefreshDataSource();
        }

        //조회버튼 클릭
        private void buttonView_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                dtGridCarDispatching.Rows.Clear();

                bizService = (BizService)_applicationContext.GetObject("BizService");
                //OracleDataTable dt = bizService.GetCarList();
                OracleDataTable dt = bizService.GetPreOrderCarList();

                foreach (DataRow _row in dt.Rows)
                {
                    SetRepositoryItemSpinEditBindData(gridCarDispatching, "working_startTime_hour", 23);
                    SetRepositoryItemSpinEditBindData(gridCarDispatching, "working_startTime_minute", 59);
                    SetRepositoryItemSpinEditBindData(gridCarDispatching, "working_startTime_second", 59);

                    SetRepositoryItemSpinEditBindData(gridCarDispatching, "working_endTime_hour", 23);
                    SetRepositoryItemSpinEditBindData(gridCarDispatching, "working_endTime_minute", 59);
                    SetRepositoryItemSpinEditBindData(gridCarDispatching, "working_endTime_second", 59);

                    SetRepositoryItemComboBoxBindData(gridCarDispatching, "FINTWA", "working_apply", dt);

                    DataRow dr = dtGridCarDispatching.NewRow();

                    dr["carNum"] = _row["VEHCID"];
                    dr["carType"] = _row["VHCTON"];

                    string work_startTime = _row["TIMWFR"].ToString();
                    if (!(work_startTime == null || work_startTime.Equals("")))
                    {
                        dr["working_startTime_hour"] = work_startTime.Substring(0, 2); ;
                        dr["working_startTime_minute"] = work_startTime.Substring(3, 2);
                        dr["working_startTime_second"] = work_startTime.Substring(6, 2);
                    }

                    string work_endTime = _row["TIMWTO"].ToString();
                    if (!(work_endTime == null || work_endTime.Equals("")))
                    {
                        dr["working_endTime_hour"] = work_endTime.Substring(0, 2);
                        dr["working_endTime_minute"] = work_endTime.Substring(3, 2);
                        dr["working_endTime_second"] = work_endTime.Substring(6, 2);
                    }

                    dr["working_apply"] = "Y"; ;

                    dtGridCarDispatching.Rows.Add(dr);
                    gridCarDispatching.DataSource = dtGridCarDispatching;

                }

                //gridCarDispatching.DataSource = dt;
                gridCarDispatching.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                screenManager.CloseWaitForm();
            }
        }

        //스핀에디터 데이터 바인딩
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit SetRepositoryItemSpinEditBindData(DevExpress.XtraGrid.GridControl gridControl, string fieldName, int maxValue)
        {
            string repositoryItemName = "repositoryItemSpinEdit";

            RepositoryItemSpinEdit repositoryItemSpinEdit = gridControl.RepositoryItems[repositoryItemName + fieldName] as RepositoryItemSpinEdit;

            //gridControl.BeginUpdate();

            repositoryItemSpinEdit.MaxValue = maxValue;
            repositoryItemSpinEdit.MinValue = 0;
            repositoryItemSpinEdit.Increment = 1;
            repositoryItemSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

            //gridControl.EndUpdate();

            return repositoryItemSpinEdit;
        }

        //콤보박스 데이터 바인딩
        public void SetRepositoryItemComboBoxBindData(GridControl gridControl, string bindDataName, string fieldName, DataTable bindData)
        {
            string repositoryItemName = "repositoryItemComboBox";

            RepositoryItemComboBox repositoryItemComboBox = gridControl.RepositoryItems[repositoryItemName + fieldName] as RepositoryItemComboBox;

            if (repositoryItemComboBox != null)
            {
                gridControl.BeginUpdate();

                repositoryItemComboBox.BorderStyle = BorderStyles.NoBorder;
                repositoryItemComboBox.AutoHeight = false;
                repositoryItemComboBox.Items.Clear();

                repositoryItemComboBox.Items.Add("Y");
                repositoryItemComboBox.Items.Add("N");

                //bandedGridView1.Columns["working_apply"].OptionsColumn.
                gridControl.EndUpdate();
            }
        }

        //저장버튼
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                int lengthArr = 1000;

                lengthArr = bandedGridView1.DataRowCount;
                bizService = (BizService)_applicationContext.GetObject("BizService");

                string[] str_arr = new string[lengthArr];
                string str = "";
                int car_row = 0;

                string start_hour = "";
                string start_minute = "";
                string start_second = "";

                string end_hour = "";
                string end_minute = "";
                string end_second = "";

                foreach (DataRow _row1 in dtGridCarDispatching.Rows)
                {
                    for (int i = 0; i < _row1.ItemArray.Length; i++)
                    {
                        if (i == 3)
                            if (_row1.ItemArray[i].ToString().Length == 1)
                                start_hour = "0" + _row1.ItemArray[i].ToString();
                            else
                                start_hour = _row1.ItemArray[i].ToString();

                        if (i == 4)
                            if (_row1.ItemArray[i].ToString().Length == 1)
                                start_minute = "0" + _row1.ItemArray[i].ToString();
                            else
                                start_minute = _row1.ItemArray[i].ToString();

                        if (i == 5)
                            if (_row1.ItemArray[i].ToString().Length == 1)
                                start_second = "0" + _row1.ItemArray[i].ToString();
                            else
                                start_second = _row1.ItemArray[i].ToString();

                        if (i == 7)
                            if (_row1.ItemArray[i].ToString().Length == 1)
                                end_hour = "0" + _row1.ItemArray[i].ToString();
                            else
                                end_hour = _row1.ItemArray[i].ToString();

                        if (i == 8)
                            if (_row1.ItemArray[i].ToString().Length == 1)
                                end_minute = "0" + _row1.ItemArray[i].ToString();
                            else
                                end_minute = _row1.ItemArray[i].ToString();

                        if (i == 9)
                            if (_row1.ItemArray[i].ToString().Length == 1)
                                end_second = "0" + _row1.ItemArray[i].ToString();
                            else
                                end_second = _row1.ItemArray[i].ToString();

                        //str = _row1.ItemArray[0] + "↑" + start_hour + ":" + start_minute + ":" + start_second + "↑" + end_hour + ":" + end_minute + ":" + end_second;

                        if (!_row1.ItemArray[0].ToString().Equals(""))
                            str = _row1.ItemArray[0] + "↑";
                        else
                            str = "↑";

                        if (!start_hour.Equals("") || !start_minute.Equals("") || !start_second.Equals(""))
                            str += start_hour + ":" + start_minute + ":" + start_second + "↑";
                        else
                            str += "↑";//공백을 안넣으면 업데이트가 안됨.

                        if (!end_hour.Equals("") || !end_minute.Equals("") || !end_second.Equals(""))
                            str += end_hour + ":" + end_minute + ":" + end_second;
                        else
                            str += "";
                    }

                    str_arr[car_row] = str;
                    car_row++;

                    str = "";
                }

                string result = bizService.SaveCarList(str_arr);

                messagebox(result);

                //// 저장할 선배차 정보관리 테이블포맷
                //DataTable cursorDt = new DataTable();
                //cursorDt.Columns.Add("VEHCID", typeof(string));
                ////cursorDt.Columns.Add("VHCTON", typeof(string));
                //cursorDt.Columns.Add("TIMWFR", typeof(string));
                //cursorDt.Columns.Add("TIMWTO", typeof(string));
                ////cursorDt.Columns.Add("FVHCTW", typeof(string));

                //for( int j =0; j<bandedGridView1.RowCount;j++)
                //{
                //    DataRow dt1 = bandedGridView1.GetDataRow(j);

                //    DataRow cursorDr = cursorDt.NewRow();

                //    cursorDr["VEHCID"] = dt1["carNum"];
                //    //cursorDr["VHCTON"] = dt1["carType"];
                //    cursorDr["TIMWFR"] = dt1["working_startTime_hour"].ToString() + ":" + dt1["working_startTime_minute"].ToString() + ":" + dt1["working_startTime_second"].ToString();
                //    cursorDr["TIMWTO"] = dt1["working_endTime_hour"].ToString() + ":" + dt1["working_endTime_minute"].ToString() + ":" + dt1["working_endTime_second"].ToString();
                //    //cursorDr["FVHCTW"] = dt1["working_apply"];

                //    cursorDt.Rows.Add(cursorDr);
                //}

                //string result = bizService.SaveCarList(cursorDt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                screenManager.CloseWaitForm();
            }
        }

        //근무가능 시간 적용여부 콤보박스 변경 이벤트
        public void repositoryItemComboBox_Working_Apply_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridCarDispatching.BeginUpdate();

                ComboBoxEdit comboBoxEdit = sender as DevExpress.XtraEditors.ComboBoxEdit;

                bandedGridView1.SetRowCellValue(bandedGridView1.FocusedRowHandle, "working_apply", comboBoxEdit.SelectedItem);

                if (comboBoxEdit.SelectedItem.Equals("N"))
                {
                    bandedGridView1.SetRowCellValue(bandedGridView1.FocusedRowHandle, "working_endTime_hour", "23");
                    bandedGridView1.SetRowCellValue(bandedGridView1.FocusedRowHandle, "working_endTime_minute", "59");
                    bandedGridView1.SetRowCellValue(bandedGridView1.FocusedRowHandle, "working_endTime_second", "59");
                }
            }
            finally
            {
                gridCarDispatching.EndUpdate();
            }
        }

        //종료버튼
        private void buttonClose_Click(object sender, EventArgs e)
        {
            FormFlag.formCarDispatching_flag = false;
            Close();
        }

        //폼 closeed 이벤트
        private void FormCarDispatching_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                Form[] form = this.MdiParent.MdiChildren;
                FormMakeDispatching form1 = null;

                foreach (Form _form in form)
                {
                    if (_form.Name.Equals("FormMakeDispatching"))
                    {
                        FormFlag.formMakeDispatching_flag = true;
                        form1 = new FormMakeDispatching();
                        form1 = (FormMakeDispatching)_form;
                    }
                }
                if (FormFlag.formMakeDispatching_flag)
                {
                    form1.BringToFront();
                    form1.Button_change();
                }
                else
                {
                    form1 = new FormMakeDispatching();
                    form1.MdiParent = this.MdiParent;
                    form1.Show();
                    //폼생성 플래그 true
                    FormFlag.formMakeDispatching_flag = true;

                    form1.Button_change();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // messagebox
        public void messagebox(string result)
        {
            //result.substring(0,1) 이 1 이면 OK, 0 이면 error
            if (result.Substring(0, 1).ToString().Equals("0"))
            {
                string errMsg = result.Substring(1).ToString();
                MessageBox.Show(errMsg, "ERROR");
            }
            else
            {
                MessageBox.Show("처리 되었습니다.", "SUCCESS");
            }
        }

    }
}