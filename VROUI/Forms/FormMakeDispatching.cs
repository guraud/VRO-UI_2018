using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Spring.Context;
using Spring.Context.Support;
using VROUI.Services;
using VROUI.objectPool;
using System.Diagnostics;

using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using Devart.Data.Oracle;

namespace VROUI.Forms
{
    //public delegate void button_change(string a);

    public partial class FormMakeDispatching : DevExpress.XtraEditors.XtraForm
    {
        #region variable

        //버튼 상태 변수
        string plan_status_button;

        IApplicationContext _applicationContext = ContextRegistry.GetContext();
        BizService bizService;

        OracleDataTable oraDt;
        DataTable dtGridSummary;

        #endregion

        FormCarDispatching frm = new FormCarDispatching();

        public FormMakeDispatching()
        {
            InitializeComponent();

            XtraHelper.InitializeGrid(gridDispatchingSummary);

            //버튼 false 로 초기화
            setButtonInit();

            #region event
            //repositoryItemCheckScenario_check.EditValueChanged += new EventHandler(ScenarioCheckBoxItem_EditValueChanged);
            #endregion

        }

        //버튼 초기화 및 각 버튼 클릭 후 link 이동 시 비활성화
        private void setButtonInit()
        {
            button_preorder_info.Enabled = false;
            button_remainder.Enabled = false;
            button_temp.Enabled = false;
            button_tmpcaralloc.Enabled = false;
            button_caralloc.Enabled = false;
            buton_getnumber.Enabled = false;
            button_complete.Enabled = false;
        }

        private void FormMakeDispatching_Load(object sender, EventArgs e)
        {
            //자동배차 실행 관리의 현재 단계 status 가져오기
            bizService = getBizService();
            oraDt = bizService.GetAutoallocate();

            #region Print dt
            PrintDebug(oraDt);
            #endregion

            DataRow statusDr = oraDt.Rows[0];

            //textEdit_plan_date.Text = statusDr["plan_date"].ToString();
            //textEdit_planst.Text = statusDr["planst"].ToString();
            //textEdit_policy_list.Text = statusDr["policy_list"].ToString();
            //textEdit_plan_status.Text = statusDr["plan_status"].ToString();

            //plan_status_button = statusDr["plan_status_button"].ToString();

            textEdit_plan_date.Text = statusDr["PLANDT"].ToString();
            textEdit_planst.Text = statusDr["PLANST"].ToString();
            textEdit_policy_list.Text = statusDr["POLYID"].ToString();
            plan_status_button = statusDr["PLAN_STATUS_BUTTON"].ToString();
            

            SetStatus(plan_status_button);
            InitializeData();

            setGridColumn();
            SetSummary();
        }

        //자동배차 단계별 버튼 설정
        private void SetStatus(string plan_status_button)
        {
            /*
             * 1UU1EE1EE1UU0EE0UU0EE
               1 (버튼활성화) / 0 (버튼비활성화)
        
               UU   :   보정
               UR   :   재보정
               EE   :   실행
               ER   :   재실행
               QQ  :   조회
            */
            //선배차 정보 버튼 설정
            string arr1 = plan_status_button.Substring(0, 3);
            if (arr1.Substring(0, 1).Equals("1")) button_preorder_info.Enabled = false;// true;, 20140325 데모 임시
            button_preorder_info.Text = SetCaption(arr1.Substring(1, 2));

            //자투리 할당 버튼 설정
            string arr2 = plan_status_button.Substring(3, 3);
            if (arr2.Substring(0, 1).Equals("1")) button_remainder.Enabled = true;
            button_remainder.Text = SetCaption(arr2.Substring(1, 2));

            //임시차 할당 설정
            string arr3 = plan_status_button.Substring(6, 3);
            if (arr3.Substring(0, 1).Equals("1")) button_temp.Enabled = true;
            button_temp.Text = SetCaption(arr3.Substring(1, 2));

            //배차지시 가확정 설정
            string arr4 = plan_status_button.Substring(9, 3);
            if (arr4.Substring(0, 1).Equals("1")) button_tmpcaralloc.Enabled = true;
            button_tmpcaralloc.Text = SetCaption(arr4.Substring(1, 2));

            //배차확정 버튼 설정
            string arr5 = plan_status_button.Substring(12, 3);
            if (arr5.Substring(0, 1).Equals("1"))   button_caralloc.Enabled = true;
            button_caralloc.Text = SetCaption(arr5.Substring(1, 2));

            //배차지시 번호부여 설정
            string arr6 = plan_status_button.Substring(15, 3);
            if (arr6.Substring(0, 1).Equals("1"))   buton_getnumber.Enabled = true;
            buton_getnumber.Text = SetCaption(arr6.Substring(1, 2));

            //배차계획 완료 버튼 설정
            string arr7 = plan_status_button.Substring(18, 3);
            if (arr7.Substring(0, 1).Equals("1"))   button_complete.Enabled = true;
            button_complete.Text = SetCaption(arr7.Substring(1, 2));

        }

        //버튼 캡쳐 세팅
        private string SetCaption(string status)
        {
            switch (status)
            {
                case "UU":
                    return "보정";
                case "UR":
                    return "재보정";
                case "EE":
                    return "실행";
                case "ER":
                    return "재실행";
                case "QQ":
                    return "조회";
                default :
                    return "";
            }
        }

        private void SetSummary()
        {
            try
            {
                GridView view_gridSummary = (GridView)gridDispatchingSummary.Views[0];

                dtGridSummary.Rows.Clear();

                bizService = getBizService();
                OracleDataTable summaryOradt = bizService.GetMakeDispatchSummary();
                #region Print dt
                //PrintDebug(summaryOradt);
                #endregion

                foreach (DataRow _row in summaryOradt.Rows)
                {
                    DataRow dr = dtGridSummary.NewRow();

                    gridView1.Columns["gridColumn1"].ColumnEdit = GetRepositoryItemCheckEdit();
                    dr["gridColumn2"] = _row["POLYID"];
                    dr["gridColumn3"] = _row["PLANPH"];
                    dr["gridColumn4"] = _row["VHCCNT"];
                    dr["gridColumn5"] = _row["NALRTO"];
                    dr["gridColumn6"] = _row["ALDRTO"];
                    dr["gridColumn7"] = _row["PERCST"];
                
                    dtGridSummary.Rows.Add(dr);
                }

                gridDispatchingSummary.DataSource = dtGridSummary;
                gridDispatchingSummary.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitializeData()
        {
            labelControl8.Text = "배차지시" + Environment.NewLine + " 가확정";
            labelControl11.Text = "배차지시" + Environment.NewLine + "번호부여";
            labelControl12.Text = "배차계획" + Environment.NewLine + "  완료";

            dtGridSummary = new DataTable();

            dtGridSummary.Columns.Add("gridColumn1", typeof(string));
            dtGridSummary.Columns.Add("gridColumn2", typeof(string));
            dtGridSummary.Columns.Add("gridColumn3", typeof(string));
            dtGridSummary.Columns.Add("gridColumn4", typeof(string));
            dtGridSummary.Columns.Add("gridColumn5", typeof(string));
            dtGridSummary.Columns.Add("gridColumn6", typeof(string));
            dtGridSummary.Columns.Add("gridColumn7", typeof(string));

            dtGridSummary.Columns["gridColumn1"].Caption = "";
            dtGridSummary.Columns["gridColumn2"].Caption = "Policy";
            dtGridSummary.Columns["gridColumn3"].Caption = "계획 수행 단계";
            dtGridSummary.Columns["gridColumn4"].Caption = "투입 차량 대수";
            dtGridSummary.Columns["gridColumn5"].Caption = "미할당율(%)";
            dtGridSummary.Columns["gridColumn6"].Caption = "평균 적재율(%)";
            dtGridSummary.Columns["gridColumn6"].Caption = "평균 적재율(%)";
            dtGridSummary.Columns["gridColumn7"].Caption = "계획 배송원가(원/BOX)";

            gridDispatchingSummary.DataSource = dtGridSummary;
            gridDispatchingSummary.RefreshDataSource();
        }

        private void setGridColumn()
        {
            gridView1.Columns[0].Width = 40;
            gridView1.Columns[1].Width = 600;
            gridView1.Columns[2].Width = 400;
            gridView1.Columns[3].Width = 300;
            gridView1.Columns[4].Width = 300;
            gridView1.Columns[5].Width = 300;
            gridView1.Columns[6].Width = 300;

            gridView1.Columns[0].AppearanceCell.Options.UseTextOptions = true;
            gridView1.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[1].AppearanceCell.Options.UseTextOptions = true;
            gridView1.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridView1.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[2].AppearanceCell.Options.UseTextOptions = true;
            gridView1.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            gridView1.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[3].AppearanceCell.Options.UseTextOptions = true;
            gridView1.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gridView1.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[4].AppearanceCell.Options.UseTextOptions = true;
            gridView1.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gridView1.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[5].AppearanceCell.Options.UseTextOptions = true;
            gridView1.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gridView1.Columns[5].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns[6].AppearanceCell.Options.UseTextOptions = true;
            gridView1.Columns[6].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            gridView1.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridView1.OptionsBehavior.Editable = true;
        }

        //선배차 정보 클릭
        private void button_preorder_info_Click(object sender, EventArgs e)
        {
            try
            {
                Form[] form = this.MdiParent.MdiChildren;
                FormCarDispatching formCarDispatching = null;

                foreach (Form _form in form)
                {
                    if (_form.Name.Equals("FormCarDispatching"))
                    {
                        FormFlag.formCarDispatching_flag = true;
                        formCarDispatching = new FormCarDispatching();
                        formCarDispatching = (FormCarDispatching)_form;
                    }
                }
                if (FormFlag.formCarDispatching_flag)
                {
                    formCarDispatching.BringToFront();
                }
                else
                {
                    formCarDispatching = new FormCarDispatching();
                    formCarDispatching.MdiParent = this.MdiParent;
                    formCarDispatching.Show();
                    //폼생성 플래그 true
                    FormFlag.formCarDispatching_flag = true;
                }
                setButtonInit();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //배차지시 가확정 버튼 클릭시
        private void button_tmpcaralloc_Click(object sender, EventArgs e)
        {
            try
            {
                Form[] form = this.MdiParent.MdiChildren;
                FormConfirmDispatching formConfirmDispatching = null;

                //선택 값 출력
                int checkcnt = 0;
                string tempStr = "";
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    if (gridView1.GetDataRow(i)[0].ToString() == "1")
                    {
                        checkcnt++;
                        tempStr = ApplicationKey.plan_id2[i];
                    }
                }

                if (checkcnt > 1) return;

                ApplicationKey.current_plan_id = tempStr;

                if (tempStr.Trim().Length == 0)
                {
                    MessageBox.Show("배차 결과를 선택한 다음 가확정을 진행할 수 있습니다.", "배차 결과 미선택", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (Form _form in form)
                {
                    if (_form.Name.Equals("FormConfirmDispatching"))
                    {
                        FormFlag.formConfirmDispatching_flag = true;
                        formConfirmDispatching = new FormConfirmDispatching();
                        formConfirmDispatching = (FormConfirmDispatching)_form;
                    }
                }
                //폼이 살아있으면 focus
                if (FormFlag.formConfirmDispatching_flag)
                {
                    formConfirmDispatching.BringToFront();
                }
                else//없으면 생성
                {
                    formConfirmDispatching = new FormConfirmDispatching();
                    formConfirmDispatching.MdiParent = this.MdiParent;
                    formConfirmDispatching.Show();
                    //폼생성 플래그 true
                    FormFlag.formConfirmDispatching_flag = true;
                }
                setButtonInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //종료버튼
        private void buttonClose_Click(object sender, EventArgs e)
        {
            FormFlag.formMakeDispatching_flag = false;
            Close();
        }

        //조회버튼
        private void buttonView_Click(object sender, EventArgs e)
        {
            try
            {
                this.screenManager.ShowWaitForm();

                //dtGridSummary.Rows.Clear();

                //bizService = getBizService();
                //OracleDataTable summaryOradt = bizService.GetMakeDispatchSummary();

                //foreach (DataRow _row in summaryOradt.Rows)
                //{
                //    DataRow dr = dtGridSummary.NewRow();

                //    gridView1.OptionsBehavior.Editable = false;
                //    gridView1.Columns["gridColumn1"].ColumnEdit = GetRepositoryItemCheckEdit();
                //    dr["gridColumn2"] = _row["POLYID"];
                //    dr["gridColumn3"] = _row["PLANPH"];
                //    dr["gridColumn4"] = _row["VHCCNT"];
                //    dr["gridColumn5"] = _row["NALRTO"];
                //    dr["gridColumn6"] = _row["ALDRTO"];
                //    dr["gridColumn7"] = _row["PERCST"];
                //    dtGridSummary.Rows.Add(dr);
                //}

                //gridDispatchingSummary.DataSource = dtGridSummary;
                //gridDispatchingSummary.RefreshDataSource();

                SetSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                setButton();
                this.screenManager.CloseWaitForm();
            }
        }

        /// <summary>
        /// 선배차 종료시 Button_change 발. 쿼리로 현재 단계 재조회
        /// </summary>
        public void Button_change()
        {
            bizService = getBizService();
            oraDt = bizService.GetAutoallocate();

            DataRow statusDr = oraDt.Rows[0];

            //textEdit_plan_date.Text = statusDr["plan_date"].ToString();
            //textEdit_planst.Text = statusDr["planst"].ToString();
            //textEdit_policy_list.Text = statusDr["policy_list"].ToString();
            //textEdit_plan_status.Text = statusDr["plan_status"].ToString();

            //plan_status_button = statusDr["plan_status_button"].ToString();

            textEdit_plan_date.Text = statusDr["PLANDT"].ToString();
            textEdit_planst.Text = statusDr["PLANST"].ToString();
            textEdit_policy_list.Text = statusDr["POLYID"].ToString();
            plan_status_button = statusDr["PLAN_STATUS_BUTTON"].ToString();
            

            SetStatus(plan_status_button);
        }

        //자투리 할당 실행 버튼 클릭
        private void button_remainder_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    screenManager.ShowWaitForm();

            //    bizService = getBizService();
            //    string result = bizService.sp_vro_update_caralloc_phase("STEP04");

            //    setButtonInit();
            //    Button_change();

            //    SetSummary();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    screenManager.CloseWaitForm();
            //}

            try
            {
                screenManager.ShowWaitForm();

                Process UserProcess = new Process();
                UserProcess.StartInfo.UseShellExecute = true;
                UserProcess.StartInfo.FileName = "D:/Source/VRO/VRO Demo 1.0/VROUI/bin/x64/Debug/Engine/WrapperThread.exe";
                UserProcess.StartInfo.CreateNoWindow = false;
                UserProcess.StartInfo.WorkingDirectory = Application.StartupPath.ToString();
                //UserProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //UserProcess.StartInfo.Arguments = "PARAM1 PARAM2 PARAM3"; //argument가 필요없으면 삭제하세요.
                UserProcess.Start();


                //Process.Start(Application.StartupPath.ToString() + @"\run.bat PARAM1 PARAM2 PARAM3");

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

        //임시차 할당 실행 버튼 클릭
        private void button_temp_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                bizService = getBizService();
                string result = bizService.sp_vro_update_caralloc_phase("STEP09");

                setButtonInit();
                Button_change();
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

        private void PrintDebug(OracleDataTable dt)
        {
            int rCnt = dt.Rows.Count;
            int cCnt = dt.Columns.Count;
            int nExit = 0;
            foreach (DataRow row2 in dt.Rows)
            {
                for (int idx = 0; idx < dt.Columns.Count; idx++)
                {
                    Debug.Write(string.Format("\t{0}", row2[idx]));
                }
                Debug.WriteLine("");
                nExit++;
                if (nExit == 50) break;
            }
        }

        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit GetRepositoryItemCheckEdit()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit item = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();

            item.AutoHeight = false;
            item.ValueChecked = "1";
            item.ValueUnchecked = "0";

            item.EditValueChanged += new EventHandler(ScenarioCheckBoxItem_EditValueChanged);
            return item;
        }

        // policy table 체크박스 체인징 이벤트
        public void ScenarioCheckBoxItem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridView1.GetFocusedDataRow();

                if (((CheckEdit)sender).Checked)
                {
                    //list_policy_code.Add(dr["POLICY_CODE"].ToString());
                    ////POLICY 체크시 삭제 DISABLE
                    //if (list_policy_code.Contains("PLC_DEFAULT"))
                    //{
                    //    buttonRemove.Enabled = false;
                    //}
                    //buttonSave.Enabled = true;
                    //buttonSaveAs.Enabled = true;
                }
                else
                {
                    //list_policy_code.Remove(dr["POLICY_CODE"].ToString());
                    ////POLICY 체크시 삭제 EABLE
                    //if (!list_policy_code.Contains("PLC_DEFAULT"))
                    //{
                    //    buttonRemove.Enabled = true;
                    //}
                    ////policy table 체크 버튼이 하나도 없을 때, 저장버튼 비활성화
                    //if (list_policy_code.Count == 0)
                    //{
                    //    buttonSave.Enabled = false;
                    //    buttonSaveAs.Enabled = false;
                    //}
                }
                //list_policy_code.Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormMakeDispatching_Activated(object sender, EventArgs e)
        {
            try
            {
                bizService = getBizService();
                oraDt = bizService.GetAutoallocate();

                #region Print dt
                //PrintDebug(oraDt);
                #endregion

                DataRow statusDr = oraDt.Rows[0];

                //textEdit_plan_date.Text = statusDr["plan_date"].ToString();
                //textEdit_planst.Text = statusDr["planst"].ToString();
                //textEdit_policy_list.Text = statusDr["policy_list"].ToString();
                //textEdit_plan_status.Text = statusDr["plan_status"].ToString();

                //plan_status_button = statusDr["plan_status_button"].ToString();

                textEdit_plan_date.Text = statusDr["PLANDT"].ToString();
                textEdit_planst.Text = statusDr["PLANST"].ToString();
                textEdit_policy_list.Text = statusDr["POLYID"].ToString();
                plan_status_button = statusDr["PLAN_STATUS_BUTTON"].ToString();
                

                SetStatus(plan_status_button);

                InitializeData();
                SetSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                setButton();
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.screenManager.ShowWaitForm();

                string tempStr = "";
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    if (gridView1.GetDataRow(i)[0].ToString() == "1")
                    {
                        tempStr += ApplicationKey.plan_id2[i] + ";";
                    }
                }

                if (tempStr.Equals(""))
                {
                    MessageBox.Show("선택된 시나리오가 없습니다", "시나리오 적용 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                textEdit_policy_list.Text = tempStr;
                ApplicationKey.current_plan_id = tempStr;

                bizService = getBizService();
                string rtn_value = bizService.sp_vro_update_plan_header("10000", tempStr);
                //ApplicationKey.current_plan_id = tempStr;

                SetSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                setButton();
                this.screenManager.CloseWaitForm();
            }
        }

        private void setButton()
        {
            //자동배차 실행 관리의 현재 단계 status 가져오기
            bizService = getBizService();
            oraDt = bizService.GetAutoallocate();

            #region Print dt
            //PrintDebug(oraDt);
            #endregion

            DataRow statusDr = oraDt.Rows[0];

            //textEdit_plan_date.Text = statusDr["plan_date"].ToString();
            //textEdit_planst.Text = statusDr["planst"].ToString();
            //textEdit_policy_list.Text = statusDr["policy_list"].ToString();
            //textEdit_plan_status.Text = statusDr["plan_status"].ToString();

            //plan_status_button = statusDr["plan_status_button"].ToString();

            textEdit_plan_date.Text = statusDr["PLANDT"].ToString();
            textEdit_planst.Text = statusDr["PLANST"].ToString();
            textEdit_policy_list.Text = statusDr["POLYID"].ToString();
            plan_status_button = statusDr["PLAN_STATUS_BUTTON"].ToString();
            

            SetStatus(plan_status_button);
        }

        private void button_caralloc_Click(object sender, EventArgs e)
        {

        }

        private BizService getBizService()
        {
            return (BizService)_applicationContext.GetObject("BizService");
        }

        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("초기화하겠습니까?", "초기화", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bizService = getBizService();
                string result = bizService.sp_vro_initialize_plan();

                setButton();
            }
        }

        private void gridDispatchingSummary_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("클릭했다");
        }
    }
}