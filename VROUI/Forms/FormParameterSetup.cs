using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using VROUI.objectPool;
using VROUI.Services;
using Spring.Context;
using Spring.Context.Support;
using Devart.Data.Oracle;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraSplashScreen;

namespace VROUI.Forms
{
    public partial class FormParameterSetup : DevExpress.XtraEditors.XtraForm
    {
        public FormParameterSetup()
        {
            InitializeComponent();

            XtraHelper.InitializeEditableGrid(gridPolicy);

            XtraHelper.InitializeGrid(gridEngineOption);
            XtraHelper.InitializeGrid(gridZoneOption);
            XtraHelper.InitializeGrid(gridCarOption);
            XtraHelper.InitializeGrid(gridLandingOption);

            XtraHelper.InitializeEditableGrid(gridSelectedEngineOption);
            XtraHelper.InitializeEditableGrid(gridSelectedZoneOption);
            XtraHelper.InitializeEditableGrid(gridSelectedCarOption);
            XtraHelper.InitializeEditableGrid(gridSelectedLandingOption);

            #region 이벤트
            //이벤트 SelectedEngineOption
            repositoryItemComboBoxPOLICY_CODE.EditValueChanged += new EventHandler(repositoryItemComboBox_POLICY_CODE_EditValueChanged);
            //repositoryItemComboBoxZone.EditValueChanged += new EventHandler(repositoryItemComboBoxZone_EditValueChanged);
            //repositoryItemComboBoxDistance_Calc.EditValueChanged += new EventHandler(repositoryItemComboBoxDistance_Calc_EditValueChanged);
            //repositoryItemComboBoxSpeed.EditValueChanged += new EventHandler(repositoryItemComboBoxSpeed_EditValueChanged);
            //repositoryItemComboBoxCumulative_rate.EditValueChanged += new EventHandler(repositoryItemComboBoxCumulative_rate_EditValueChanged);
            //repositoryItemComboBoxDeallocation_ratio.EditValueChanged += new EventHandler(repositoryItemComboBoxDeallocation_ratio_EditValueChanged);
            //repositoryItemComboBoxphase_difference_time.EditValueChanged += new EventHandler(repositoryItemComboBoxphase_difference_time_EditValueChanged);
            //repositoryItemComboBoxoverfloww_order.EditValueChanged += new EventHandler(repositoryItemComboBoxoverfloww_order_EditValueChanged);
            //repositoryItemComboBoxMaximum_weight.EditValueChanged += new EventHandler(repositoryItemComboBoxMaximum_weight_EditValueChanged);
            //repositoryItemComboBoxMaximum_volume.EditValueChanged += new EventHandler(repositoryItemComboBoxMaximum_volume_EditValueChanged);
            //repositoryItemComboBoxMaximum_PLT.EditValueChanged += new EventHandler(repositoryItemComboBoxMaximum__PLT_EditValueChanged);
            //repositoryItemComboBoxBy_weight.EditValueChanged += new EventHandler(repositoryItemComboBoxBy_weight_EditValueChanged);
            //repositoryItemComboBoxBy_volume.EditValueChanged += new EventHandler(repositoryItemComboBoxBy_volume_EditValueChanged);
            //repositoryItemComboBoxBy_PLT.EditValueChanged += new EventHandler(repositoryItemComboBoxBy_PLT_EditValueChanged);
            //repositoryItemComboBoxEnd_time.EditValueChanged += new EventHandler(repositoryItemComboBoxEnd_time_EditValueChanged);
            //repositoryItemComboBoxVehicles_working.EditValueChanged += new EventHandler(repositoryItemComboBoxVehicles_working_EditValueChanged);
            //repositoryItemComboBoxTemporary_Vehicle_maximum_radius.EditValueChanged += new EventHandler(repositoryItemComboBoxTemporary_Vehicle_maximum_radius_EditValueChanged);
            #endregion

            gridView4.BestFitColumns();
        }

        //파라메터화면 (정책/시나리오 설정) 띄우기
        private void FormParameterSetup_Load(object sender, EventArgs e)
        {
            InitializeData();

            //폼로드시 조회, 종료만 활성화. 조회 후 다른 버튼 활성화
            buttonAdd.Enabled = false;
            buttonRemove.Enabled = false;
            buttonConfirm.Enabled = false;
            buttonSave.Enabled = false;
            buttonSaveAs.Enabled = false;
            buttonSaveScenario.Enabled = false;
        }

        /// <summary> 초기화 (시간콤보박스, 그리드용 DataTable) </summary>
        private void InitializeData()
        {
            //시간콤보박스 관련 리스트 init
            list_hours = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    list_hours.Add("0" + i);
                }
                else
                {
                    list_hours.Add(i + "");
                }
            }
            list_minute_second = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    list_minute_second.Add("0" + i);
                }
                else
                {
                    list_minute_second.Add(i + "");
                }
            }

            //그리드 관련 테이블 init
            SetPolicy_DataTable();
            SetGridEngineOption_DataTable();
            SetGridZoneOption_DataTable();
            SetGridCarOption_DataTable();
            SetGridLandingOption_DataTable();
            SetSelectedGridEngineOption_DataTable();
            SetSelectedGridZoneOption_DataTable();
            SetSelectedGridCarOption_DataTable();
            SetSelectedGridLandingOption_DataTable();

            textPolicyID.Text = "";
        }

        #region data table set

        /// <summary> dtPolicy 컬럼/헤더 초기 세팅 & gridPolicy.DataSource와 동기화 </summary>
        private void SetPolicy_DataTable()
        {
            dtPolicy = new DataTable();

            #region column add
            dtPolicy.Columns.Add("checkbox", typeof(string));
            dtPolicy.Columns.Add("POLICY_ID", typeof(string));
            dtPolicy.Columns.Add("POLICY_NAME", typeof(string));
            dtPolicy.Columns.Add("DESCRIPTION", typeof(string));
            dtPolicy.Columns.Add("INSERT_DTTM", typeof(string));
            dtPolicy.Columns.Add("INSERT_ID", typeof(string));
            dtPolicy.Columns.Add("UPDATE_DTTM", typeof(string));
            dtPolicy.Columns.Add("UPDATE_ID", typeof(string));
            #endregion

            #region column title
            dtPolicy.Columns["checkbox"].Caption = "";
            dtPolicy.Columns["POLICY_ID"].Caption = "정책 코드";
            dtPolicy.Columns["POLICY_NAME"].Caption = "정책 명칭";
            dtPolicy.Columns["DESCRIPTION"].Caption = "정책 설명";
            dtPolicy.Columns["INSERT_DTTM"].Caption = "생성일";
            dtPolicy.Columns["INSERT_ID"].Caption = "생성 user ID";
            dtPolicy.Columns["UPDATE_DTTM"].Caption = "변경일";
            dtPolicy.Columns["UPDATE_ID"].Caption = "변경 user ID";
            #endregion

            gridPolicy.DataSource = dtPolicy;
            gridPolicy.RefreshDataSource();

            this.setColumnWidth(gridView1, "gridPolicy");
        }
        /// <summary> dtSelectedGridCarOption 컬럼/헤더 초기 세팅 & gridSelectedCarOption.DataSource와 동기화 </summary>
        private void SetSelectedGridCarOption_DataTable()
        {
            dtSelectedGridCarOption = new DataTable();

            #region column add
            dtSelectedGridCarOption.Columns.Add("caroption_car_number", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_car_type", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_max_landing_num", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_arrival", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_working", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_region", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_mix_region", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_region_reorder", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_area", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_mix_area", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_area_assign", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_area_assign_reorder", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_remainder_assign", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_remainder_assign_reorder", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_1", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_2", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_3", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_4", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_5", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_6", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_7", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_8", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_9", typeof(string));
            dtSelectedGridCarOption.Columns.Add("caroption_10", typeof(string));
            #endregion

            #region column title
            //dtSelectedGridCarOption.Columns["caroption_max_landing_num"].Caption = "일일최대 착지수";
            //dtSelectedGridCarOption.Columns["caroption_arrival"].Caption = "근무시간 이내 차고지 도착";
            //dtSelectedGridCarOption.Columns["caroption_working"].Caption = "근무여부";
            //dtSelectedGridCarOption.Columns["caroption_region"].Caption = "고정권역";
            //dtSelectedGridCarOption.Columns["caroption_mix_region"].Caption = "혼적가능 고정권역 수";
            //dtSelectedGridCarOption.Columns["caroption_region_reorder"].Caption = "고정권역 할당 후 재정렬 방식";
            //dtSelectedGridCarOption.Columns["caroption_mix_area"].Caption = "혼적가능 대권역 수";
            //dtSelectedGridCarOption.Columns["caroption_area"].Caption = "대권역";
            //dtSelectedGridCarOption.Columns["caroption_area_assign"].Caption = "대권역 할당 방식";
            //dtSelectedGridCarOption.Columns["caroption_area_assign_reorder"].Caption = "대권역 할당 후 재정렬 방식";
            //dtSelectedGridCarOption.Columns["caroption_remainder_assign"].Caption = "자투리 할당 방식";
            //dtSelectedGridCarOption.Columns["caroption_remainder_assign_reorder"].Caption = "자투리 할당 후 재정렬 방식";
            //dtSelectedGridCarOption.Columns["caroption_1"].Caption = "1";
            //dtSelectedGridCarOption.Columns["caroption_2"].Caption = "2";
            //dtSelectedGridCarOption.Columns["caroption_3"].Caption = "3";
            //dtSelectedGridCarOption.Columns["caroption_4"].Caption = "4";
            //dtSelectedGridCarOption.Columns["caroption_5"].Caption = "5";
            //dtSelectedGridCarOption.Columns["caroption_6"].Caption = "6";
            //dtSelectedGridCarOption.Columns["caroption_7"].Caption = "7";
            //dtSelectedGridCarOption.Columns["caroption_8"].Caption = "8";
            //dtSelectedGridCarOption.Columns["caroption_9"].Caption = "9";
            //dtSelectedGridCarOption.Columns["caroption_10"].Caption = "10";
            #endregion

            gridSelectedCarOption.DataSource = dtSelectedGridCarOption;
            gridSelectedCarOption.RefreshDataSource();
        }
        /// <summary> dtSelectedGridLandingOption 컬럼/헤더 초기 세팅 & gridSelectedLandingOption.DataSource와 동기화 </summary>
        private void SetSelectedGridLandingOption_DataTable()
        {
            dtSelectedGridLandingOption = new DataTable();

            #region column add
            dtSelectedGridLandingOption.Columns.Add("landingoption_number", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("landingoption_name", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("landingoption_restrict", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("landingoption_box_number", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("landingoption_box_rating", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("landingoption_weight", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("landingoption_volumn", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("landingoption_PLT", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Adhesive_required_startTime", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Adhesive_required_startTime1", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Adhesive_required_startTime2", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Adhesive_required_endTime", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Adhesive_required_endTime1", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Adhesive_required_endTime2", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("OTD_compliance", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("OffTime_startTime", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("OffTime_startTime1", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("OffTime_startTime2", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("OffTime_endTime", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("OffTime_endTime1", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("OffTime_endTime2", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Fixed_handlingTime", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Fixed_handlingTime1", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Fixed_handlingTime2", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("change_handlingTime", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("change_handlingTime1", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("change_handlingTime2", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Recent_delivery_vehicleNumber", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Latest_arrivalTime", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Dispatcher_manualHandling", typeof(string));
            dtSelectedGridLandingOption.Columns.Add("Whether_assigned", typeof(string));
            #endregion

            #region column title
            //dtSelectedGridLandingOption.Columns["Adhesive_required_startTime"].Caption = "점착요구 시작시간 시";
            //dtSelectedGridLandingOption.Columns["Adhesive_required_startTime1"].Caption = "점착요구 시작시간 분";
            //dtSelectedGridLandingOption.Columns["Adhesive_required_startTime2"].Caption = "점착요구 시작시간 초";
            //dtSelectedGridLandingOption.Columns["Adhesive_required_endTime"].Caption = "점착요구 종료시간 시";
            //dtSelectedGridLandingOption.Columns["Adhesive_required_endTime1"].Caption = "점착요구 종료시간 분";
            //dtSelectedGridLandingOption.Columns["Adhesive_required_endTime2"].Caption = "점착요구 종료시간 초";
            //dtSelectedGridLandingOption.Columns["OTD_compliance"].Caption = "OTD 준수여부";
            //dtSelectedGridLandingOption.Columns["OffTime_startTime"].Caption = "Off-time 시작시간 시";
            //dtSelectedGridLandingOption.Columns["OffTime_startTime1"].Caption = "Off-time 시작시간 분";
            //dtSelectedGridLandingOption.Columns["OffTime_startTime2"].Caption = "Off-time 시작시간 초";
            //dtSelectedGridLandingOption.Columns["OffTime_endTime"].Caption = "Off-time 종료시간 시";
            //dtSelectedGridLandingOption.Columns["OffTime_endTime1"].Caption = "Off-time 종료시간 분";
            //dtSelectedGridLandingOption.Columns["OffTime_endTime2"].Caption = "Off-time 종료시간 초";
            //dtSelectedGridLandingOption.Columns["Fixed_handlingTime"].Caption = "고정 하역시간 시";
            //dtSelectedGridLandingOption.Columns["Fixed_handlingTime1"].Caption = "고정 하역시간 분";
            //dtSelectedGridLandingOption.Columns["Fixed_handlingTime2"].Caption = "고정 하역시간 초";
            //dtSelectedGridLandingOption.Columns["change_handlingTime"].Caption = "변동 하역시간 시";
            //dtSelectedGridLandingOption.Columns["change_handlingTime1"].Caption = "변동 하역시간 분";
            //dtSelectedGridLandingOption.Columns["change_handlingTime2"].Caption = "변동 하역시간 초";
            //dtSelectedGridLandingOption.Columns["Recent_delivery_vehicleNumber"].Caption = "최근 배송 차량번호";
            //dtSelectedGridLandingOption.Columns["Latest_arrivalTime"].Caption = "최근 도착시간";
            //dtSelectedGridLandingOption.Columns["Dispatcher_manualHandling"].Caption = "수작업 배차처리";
            //dtSelectedGridLandingOption.Columns["Whether_assigned"].Caption = "할당 여부";
            #endregion

            gridSelectedLandingOption.DataSource = dtSelectedGridLandingOption;
            gridSelectedLandingOption.RefreshDataSource();
        }
        /// <summary> dtSelectedGridEngineOption 컬럼/헤더 초기 세팅 & gridSelectedEngineOption.DataSource와 동기화 </summary>
        private void SetSelectedGridEngineOption_DataTable()
        {
            dtSelectedGridEngineOption = new DataTable();

            #region column add
            dtSelectedGridEngineOption.Columns.Add("Zone", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Distance_Calc", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Distance_Weight", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Speed", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Cumulative_rate", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Deallocation_ratio", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("phase_difference_time", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("hour", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("minute", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("second", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("overfloww_order", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Maximum_weight", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Maximum_volume", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Maximum_PLT", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("By_weight", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("By_volume", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("By_PLT", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("End_time", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Vehicles_working", typeof(string));
            dtSelectedGridEngineOption.Columns.Add("Temporary_Vehicle_maximum_radius", typeof(string));
            #endregion

            #region column title
            //dtSelectedGridEngineOption.Columns["ENGPID"].Caption = " 엔진옵션코드";
            //dtSelectedGridEngineOption.Columns["Zone"].Caption = "권역 정렬";
            //dtSelectedGridEngineOption.Columns["Distance_Calc"].Caption = "거리 계산";
            //dtSelectedGridEngineOption.Columns["Distance_Weight"].Caption = "거리 가중치(%)";
            //dtSelectedGridEngineOption.Columns["Speed"].Caption = "이동 속도";
            //dtSelectedGridEngineOption.Columns["Speed"].Caption = "이동 속도";
            //dtSelectedGridEngineOption.Columns["Cumulative_rate"].Caption = "누적속도 적용";
            //dtSelectedGridEngineOption.Columns["Deallocation_ratio"].Caption = "할당해제 비율(%)";
            //dtSelectedGridEngineOption.Columns["phase_difference_time"].Caption = "상차시간";
            //dtSelectedGridEngineOption.Columns["hour"].Caption = "시";
            //dtSelectedGridEngineOption.Columns["minute"].Caption = "분";
            //dtSelectedGridEngineOption.Columns["second"].Caption = "초";
            //dtSelectedGridEngineOption.Columns["overfloww_order"].Caption = "용량 초과 주문 활당";
            //dtSelectedGridEngineOption.Columns["Maximum_weight"].Caption = "최대 중량 가중치(%)";
            //dtSelectedGridEngineOption.Columns["Maximum_volume"].Caption = "최대 부피 가중치(%)";
            //dtSelectedGridEngineOption.Columns["Maximum_PLT"].Caption = "최대 PLT 가중치(%)";
            //dtSelectedGridEngineOption.Columns["By_weight"].Caption = "중량기준 적재판단";
            //dtSelectedGridEngineOption.Columns["By_volume"].Caption = "부피기준 적재판단";
            //dtSelectedGridEngineOption.Columns["By_PLT"].Caption = "PLT기준 적재판단";
            //dtSelectedGridEngineOption.Columns["End_time"].Caption = "정착요구 종료시간 적용";
            //dtSelectedGridEngineOption.Columns["Vehicles_working"].Caption = "차량 근무시간 적용";
            //dtSelectedGridEngineOption.Columns["Temporary_Vehicle_maximum_radius"].Caption = "임시차량 최대반경(cm)";
            #endregion

            gridSelectedEngineOption.DataSource = dtSelectedGridEngineOption;
            gridSelectedEngineOption.RefreshDataSource();
        }
        /// <summary> dtSelectedGridZoneOption 컬럼/헤더 초기 세팅 & gridSelectedZoneOption.DataSource와 동기화 </summary>
        private void SetSelectedGridZoneOption_DataTable()
        {
            dtSelectedGridZoneOption = new DataTable();

            #region column add
            dtSelectedGridZoneOption.Columns.Add("Zone_Code", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Zone_Name", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Zone_Division", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority01", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority02", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority03", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority04", typeof(string)); 
            dtSelectedGridZoneOption.Columns.Add("Priority05", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority06", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority07", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority08", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority09", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("Priority10", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("zone_Distance_Calc", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("zone_Distance_Weight", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("zone_Speed", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("zone_Cumulative_rate", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("zone_Deallocation_ratio", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("zone_Move_time_limit", typeof(string));
            dtSelectedGridZoneOption.Columns.Add("zone_Temporary_Vehicle_maximum_radius", typeof(string));
            #endregion

            #region column title
            //dtSelectedGridZoneOption.Columns["Zone_Code"].Caption = "코드";
            //dtSelectedGridZoneOption.Columns["Zone_Name"].Caption = "권역명";
            //dtSelectedGridZoneOption.Columns["Zone_Division"].Caption = "구분";
            //dtSelectedGridZoneOption.Columns["Priority01"].Caption = "우선순위01";
            //dtSelectedGridZoneOption.Columns["Priority02"].Caption = "우선순위02";
            //dtSelectedGridZoneOption.Columns["Priority03"].Caption = "우선순위03";
            //dtSelectedGridZoneOption.Columns["Priority04"].Caption = "우선순위04";
            //dtSelectedGridZoneOption.Columns["Priority05"].Caption = "우선순위05";
            //dtSelectedGridZoneOption.Columns["Priority06"].Caption = "우선순위06";
            //dtSelectedGridZoneOption.Columns["Priority07"].Caption = "우선순위07";
            //dtSelectedGridZoneOption.Columns["Priority08"].Caption = "우선순위08";
            //dtSelectedGridZoneOption.Columns["Priority09"].Caption = "우선순위09";
            //dtSelectedGridZoneOption.Columns["Priority10"].Caption = "우선순위10";
            //dtSelectedGridZoneOption.Columns["zone_Distance_Calc"].Caption = "거리계산";
            //dtSelectedGridZoneOption.Columns["zone_Distance_Weight"].Caption = "거리 가중치(%)";
            //dtSelectedGridZoneOption.Columns["zone_Speed"].Caption = "이동 속도";
            //dtSelectedGridZoneOption.Columns["zone_Cumulative_rate"].Caption = "누적 속도";
            //dtSelectedGridZoneOption.Columns["zone_Deallocation_ratio"].Caption = "해제 비율(%)";
            //dtSelectedGridZoneOption.Columns["zone_Move_time_limit"].Caption = "이동시간 제한";
            //dtSelectedGridZoneOption.Columns["zone_Temporary_Vehicle_maximum_radius"].Caption = "최대반경(km)";
            #endregion

            gridSelectedZoneOption.DataSource = dtSelectedGridZoneOption;
            gridSelectedZoneOption.RefreshDataSource();

        }

        private void SetGridZoneOption_DataTable()
        {
            dtGridZoneOption = new DataTable();

            dtGridZoneOption.Columns.Add("zone_policyID", typeof(string));
            dtGridZoneOption.Columns.Add("zone_policy_name", typeof(string));

            dtGridZoneOption.Columns.Add("Zone_Code", typeof(string)); dtGridZoneOption.Columns["Zone_Code"].Caption = "코드";
            dtGridZoneOption.Columns.Add("Zone_Name", typeof(string)); dtGridZoneOption.Columns["Zone_Name"].Caption = "권역명";
            dtGridZoneOption.Columns.Add("Zone_Division", typeof(string)); dtGridZoneOption.Columns["Zone_Division"].Caption = "구분";
            dtGridZoneOption.Columns.Add("Priority01", typeof(string)); dtGridZoneOption.Columns["Priority01"].Caption = "우선순위01";
            dtGridZoneOption.Columns.Add("Priority02", typeof(string)); dtGridZoneOption.Columns["Priority02"].Caption = "우선순위02";
            dtGridZoneOption.Columns.Add("Priority03", typeof(string)); dtGridZoneOption.Columns["Priority03"].Caption = "우선순위03";
            dtGridZoneOption.Columns.Add("Priority04", typeof(string)); dtGridZoneOption.Columns["Priority04"].Caption = "우선순위04";
            dtGridZoneOption.Columns.Add("Priority05", typeof(string)); dtGridZoneOption.Columns["Priority05"].Caption = "우선순위05";
            dtGridZoneOption.Columns.Add("Priority06", typeof(string)); dtGridZoneOption.Columns["Priority06"].Caption = "우선순위06";
            dtGridZoneOption.Columns.Add("Priority07", typeof(string)); dtGridZoneOption.Columns["Priority07"].Caption = "우선순위07";
            dtGridZoneOption.Columns.Add("Priority08", typeof(string)); dtGridZoneOption.Columns["Priority08"].Caption = "우선순위08";
            dtGridZoneOption.Columns.Add("Priority09", typeof(string)); dtGridZoneOption.Columns["Priority09"].Caption = "우선순위09";
            dtGridZoneOption.Columns.Add("Priority10", typeof(string)); dtGridZoneOption.Columns["Priority10"].Caption = "우선순위10";
            dtGridZoneOption.Columns.Add("zone_Distance_Calc", typeof(string)); dtGridZoneOption.Columns["zone_Distance_Calc"].Caption = "거리계산";
            dtGridZoneOption.Columns.Add("zone_Distance_Weight", typeof(string)); dtGridZoneOption.Columns["zone_Distance_Weight"].Caption = "거리 가중치(%)";
            dtGridZoneOption.Columns.Add("zone_Speed", typeof(string)); dtGridZoneOption.Columns["zone_Speed"].Caption = "이동 속도";
            dtGridZoneOption.Columns.Add("zone_Cumulative_rate", typeof(string)); dtGridZoneOption.Columns["zone_Cumulative_rate"].Caption = "누적 속도";
            dtGridZoneOption.Columns.Add("zone_Deallocation_ratio", typeof(string)); dtGridZoneOption.Columns["zone_Deallocation_ratio"].Caption = "해제 비율(%)";
            dtGridZoneOption.Columns.Add("zone_Move_time_limit", typeof(string)); dtGridZoneOption.Columns["zone_Move_time_limit"].Caption = "이동시간 제한";
            dtGridZoneOption.Columns.Add("zone_Temporary_Vehicle_maximum_radius", typeof(string)); dtGridZoneOption.Columns["zone_Temporary_Vehicle_maximum_radius"].Caption = "최대반경(km)";

            gridZoneOption.DataSource = dtGridZoneOption;
            gridZoneOption.RefreshDataSource();
        }
        private void SetGridCarOption_DataTable()
        {
            //banded's hearder disable
            //advBandedGridView1.OptionsView.ShowBands = false;

            dtGridCarOption = new DataTable();

            dtGridCarOption.Columns.Add("car_policyID", typeof(string));
            dtGridCarOption.Columns.Add("car_policy_name", typeof(string));

            dtGridCarOption.Columns.Add("car_carNumber", typeof(string));
            dtGridCarOption.Columns.Add("car_type", typeof(string));
            dtGridCarOption.Columns.Add("car_day_max_landing_num", typeof(string));
            dtGridCarOption.Columns.Add("car_arrival_within_wokingTime", typeof(string));
            dtGridCarOption.Columns.Add("car_Whether_working", typeof(string));
            dtGridCarOption.Columns.Add("car_Fixed_region", typeof(string));
            dtGridCarOption.Columns.Add("car_mix_Fixed_region", typeof(string));
            dtGridCarOption.Columns.Add("car_reorder_Fixed_region", typeof(string));
            dtGridCarOption.Columns.Add("car_areas", typeof(string));
            dtGridCarOption.Columns.Add("car_mix_areas", typeof(string));
            dtGridCarOption.Columns.Add("car_areas_assign_type", typeof(string));
            dtGridCarOption.Columns.Add("car_areas_assign_reorder", typeof(string));
            dtGridCarOption.Columns.Add("car_remainder_assign_type", typeof(string));
            dtGridCarOption.Columns.Add("car_remainder_assign_reorder", typeof(string));
            dtGridCarOption.Columns.Add("car_1", typeof(string));
            dtGridCarOption.Columns.Add("car_2", typeof(string));
            dtGridCarOption.Columns.Add("car_3", typeof(string));
            dtGridCarOption.Columns.Add("car_4", typeof(string));
            dtGridCarOption.Columns.Add("car_5", typeof(string));
            dtGridCarOption.Columns.Add("car_6", typeof(string));
            dtGridCarOption.Columns.Add("car_7", typeof(string));
            dtGridCarOption.Columns.Add("car_8", typeof(string));
            dtGridCarOption.Columns.Add("car_9", typeof(string));
            dtGridCarOption.Columns.Add("car_10", typeof(string));
            
            gridZoneOption.DataSource = dtGridZoneOption;
            gridZoneOption.RefreshDataSource();
            
        }
        private void SetGridLandingOption_DataTable()
        {
            dtGridLandingOption = new DataTable();

            dtGridLandingOption.Columns.Add("landing_policyID", typeof(string));
            dtGridLandingOption.Columns.Add("landing_policy_name", typeof(string));

            dtGridLandingOption.Columns.Add("Landing_num", typeof(string));
            dtGridLandingOption.Columns["Landing_num"].Caption = "착지번호";
            dtGridLandingOption.Columns.Add("Landing_name", typeof(string));
            dtGridLandingOption.Columns["Landing_name"].Caption = "착지명";
            dtGridLandingOption.Columns.Add("Limited_entry_vehicle", typeof(string));
            dtGridLandingOption.Columns["Limited_entry_vehicle"].Caption = "진입차종 제한";
            dtGridLandingOption.Columns.Add("total_box", typeof(string));
            dtGridLandingOption.Columns["total_box"].Caption = "BOX 수";
            dtGridLandingOption.Columns.Add("box_rating", typeof(string));
            dtGridLandingOption.Columns["box_rating"].Caption = "BOX 등급";
            dtGridLandingOption.Columns.Add("weight", typeof(string));
            dtGridLandingOption.Columns["weight"].Caption = "중량";
            dtGridLandingOption.Columns.Add("volume", typeof(string));
            dtGridLandingOption.Columns["volume"].Caption = "부피";
            dtGridLandingOption.Columns.Add("PLT", typeof(string));
            dtGridLandingOption.Columns["PLT"].Caption = "PLT";
            dtGridLandingOption.Columns.Add("landing_required_startTime", typeof(string));
            dtGridLandingOption.Columns["landing_required_startTime"].Caption = "점착요구 시작시간";
            dtGridLandingOption.Columns.Add("landing_required_endTime", typeof(string));
            dtGridLandingOption.Columns["landing_required_endTime"].Caption = "점착요구 종료시간";
            dtGridLandingOption.Columns.Add("landing_OTD_compliance", typeof(string));
            dtGridLandingOption.Columns["landing_OTD_compliance"].Caption = "OTD 준수여부";
            dtGridLandingOption.Columns.Add("landing_offTime_startTime", typeof(string));
            dtGridLandingOption.Columns["landing_offTime_startTime"].Caption = "Off-time 시작시간";
            dtGridLandingOption.Columns.Add("landing_offTime_endTime", typeof(string));
            dtGridLandingOption.Columns["landing_offTime_endTime"].Caption = "Off-time 종료시잔";
            dtGridLandingOption.Columns.Add("landing_Fixed_handlingTime", typeof(string));
            dtGridLandingOption.Columns["landing_Fixed_handlingTime"].Caption = "고정 하역시간";
            dtGridLandingOption.Columns.Add("landing_change_handlingTime", typeof(string));
            dtGridLandingOption.Columns["landing_change_handlingTime"].Caption = "변동 하역시간";
            dtGridLandingOption.Columns.Add("landing_Recent_delivery_vehicleNumber", typeof(string));
            dtGridLandingOption.Columns["landing_Recent_delivery_vehicleNumber"].Caption = "최근 배송 차량번호";
            dtGridLandingOption.Columns.Add("landing_Latest_arrivalTime", typeof(string));
            dtGridLandingOption.Columns["landing_Latest_arrivalTime"].Caption = "최근 도착시간";
            dtGridLandingOption.Columns.Add("landing_Dispatcher_manualHandling", typeof(string));
            dtGridLandingOption.Columns["landing_Dispatcher_manualHandling"].Caption = "수작업 배차처리";
            dtGridLandingOption.Columns.Add("landing_Whether_assigned", typeof(string));
            dtGridLandingOption.Columns["landing_Whether_assigned"].Caption = "할당 여부";

            gridLandingOption.DataSource = dtGridLandingOption;
            gridLandingOption.RefreshDataSource();
        }
        private void SetGridEngineOption_DataTable()
        {
            dtGridEngineOption = new DataTable();

            dtGridEngineOption.Columns.Add("POLICY_ID", typeof(string));
            dtGridEngineOption.Columns["POLICY_ID"].Caption = "정책 코드";
            dtGridEngineOption.Columns.Add("ENGPID", typeof(string));
            dtGridEngineOption.Columns["ENGPID"].Caption = "엔진옵션 코드";
            dtGridEngineOption.Columns.Add("Zone", typeof(string));
            dtGridEngineOption.Columns["Zone"].Caption = "권역 정렬";
            dtGridEngineOption.Columns.Add("Distance_Calc", typeof(string));
            dtGridEngineOption.Columns["Distance_Calc"].Caption = "거리 계산";
            dtGridEngineOption.Columns.Add("Distance_Weight", typeof(string));
            dtGridEngineOption.Columns["Distance_Weight"].Caption = "거리 가중치(%)";
            dtGridEngineOption.Columns.Add("Speed", typeof(string));
            dtGridEngineOption.Columns["Speed"].Caption = "이동 속도";
            dtGridEngineOption.Columns.Add("Cumulative_rate", typeof(string));
            dtGridEngineOption.Columns["Cumulative_rate"].Caption = "누적속도 적용";
            dtGridEngineOption.Columns.Add("Deallocation_ratio", typeof(string));
            dtGridEngineOption.Columns["Deallocation_ratio"].Caption = "할당해제 비율(%)";
            dtGridEngineOption.Columns.Add("phase_difference_time", typeof(string));
            dtGridEngineOption.Columns["phase_difference_time"].Caption = "상차시간";
            dtGridEngineOption.Columns.Add("overfloww_order", typeof(string));
            dtGridEngineOption.Columns["overfloww_order"].Caption = "용량 초과 주문 활당";
            dtGridEngineOption.Columns.Add("Maximum_weight", typeof(string));
            dtGridEngineOption.Columns["Maximum_weight"].Caption = "최대 중량 가중치(%)";
            dtGridEngineOption.Columns.Add("Maximum_volume", typeof(string));
            dtGridEngineOption.Columns["Maximum_volume"].Caption = "최대 부피 가중치(%)";
            dtGridEngineOption.Columns.Add("Maximum_PLT", typeof(string));
            dtGridEngineOption.Columns["Maximum_PLT"].Caption = "최대 PLT 가중치(%)";
            dtGridEngineOption.Columns.Add("By_weight", typeof(string));
            dtGridEngineOption.Columns["By_weight"].Caption = "중량기준 적재판단";
            dtGridEngineOption.Columns.Add("By_volume", typeof(string));
            dtGridEngineOption.Columns["By_volume"].Caption = "부피기준 적재판단";
            dtGridEngineOption.Columns.Add("By_PLT", typeof(string));
            dtGridEngineOption.Columns["By_PLT"].Caption = "PLT기준 적재판단";
            dtGridEngineOption.Columns.Add("End_time", typeof(string));
            dtGridEngineOption.Columns["End_time"].Caption = "정착요구 종료시간 적용";
            dtGridEngineOption.Columns.Add("Vehicles_working", typeof(string));
            dtGridEngineOption.Columns["Vehicles_working"].Caption = "차량 근무시간 적용";
            dtGridEngineOption.Columns.Add("Temporary_Vehicle_maximum_radius", typeof(string));
            dtGridEngineOption.Columns["Temporary_Vehicle_maximum_radius"].Caption = "임시차량 최대반경(cm)";

            gridEngineOption.DataSource = dtGridEngineOption;
            gridEngineOption.RefreshDataSource();
        }
        #endregion
        //조회버튼 클릭
        private void ButtonView_Click(object sender, EventArgs e)
        {
            try
            {
                this.screenManager.ShowWaitForm();

                #region 버튼 활성화 세팅 및 초기화 설정
                buttonAdd.Enabled = true;// 신규 추가 버튼은 미사용
                buttonRemove.Enabled = true;
                buttonConfirm.Enabled = true;
                if (list_policy_code.Count != 0)
                {
                    buttonSave.Enabled = true;
                    buttonSaveAs.Enabled = true;
                    buttonSaveScenario.Enabled = true;
                }
                else
                {
                    buttonSave.Enabled = false;
                    buttonSaveAs.Enabled = false;
                    buttonSaveScenario.Enabled = false;
                }

                //조회버튼 클릭시 신규해제
                new_policy_save = false;
                buttonSave.Text = "기존 이름으로";
                #endregion

                #region 테이터 테이블 초기화
                dtPolicy.Rows.Clear();
                dtGridEngineOption.Rows.Clear();
                dtGridZoneOption.Rows.Clear();
                dtGridCarOption.Rows.Clear();
                dtGridLandingOption.Rows.Clear();

                //gridselectedoption delete
                dtSelectedGridEngineOption.Rows.Clear();
                dtSelectedGridZoneOption.Rows.Clear();
                dtSelectedGridCarOption.Rows.Clear();
                dtSelectedGridLandingOption.Rows.Clear();
                #endregion

                bizService = GetBizService();

                //Policy Table Data 조회 (oraPolicy)
                OracleDataTable oraPolicy;
                if (checkDelete.Checked)  //삭제된 정책 리스트 조회
                {
                    //삭제된 리스트를 보여주는 경우는 조회 시에 policy 테이블에만 삭제 policy display, 옵션 창에는 no display
                    oraPolicy = bizService.GetDeletePolicyList();

                    //삭제된 정책 리스트 조회시 삭제버튼 -> 복원 버튼으로 변경.
                    buttonRemove.Text = "복원";

                    buttonAdd.Enabled = false;
                    buttonConfirm.Enabled = false;
                    buttonSave.Enabled = false;
                    buttonSaveAs.Enabled = false;
                    buttonSaveScenario.Enabled = false;

                    gridView1.OptionsBehavior.Editable = false;
                }
                else  //정책 리스트 조회
                {
                    //정책 리스트 가져오기
                    oraPolicy = bizService.GetPolicyList();

                    PrintDebug(oraPolicy);
                    
                    #region Scenario 콤보 : 시나리오 리스트를 가져와 시나리오 콤보박스에 세팅
                    this.comboScenarioName.Properties.Items.Clear();
                    DataTable dtScenario = bizService.GetScenarioList();
                    foreach (DataRow _row in dtScenario.Rows)
                    {
                        this.comboScenarioName.Properties.Items.Add(_row["DESC01"].ToString());
                        this.comboScenarioCode.Properties.Items.Add(_row["SIMULATION_ID"].ToString());
                    }
                    #endregion

                    //정책 리스트 조회시 삭제버튼 -> 삭제버튼으로 유지
                    buttonRemove.Text = "삭제";

                    buttonAdd.Enabled = true;
                    buttonConfirm.Enabled = true;
                    buttonSave.Enabled = true;
                    buttonSaveAs.Enabled = true;
                    buttonSaveScenario.Enabled = true;

                    gridView1.OptionsBehavior.Editable = true;
                    gridView1.Columns["POLICY_ID"].OptionsColumn.AllowEdit = false;
                    gridView1.Columns["POLICY_NAME"].OptionsColumn.AllowEdit = false;
                    gridView1.Columns["DESCRIPTION"].OptionsColumn.AllowEdit = false;
                    gridView1.Columns["INSERT_DTTM"].OptionsColumn.AllowEdit = false;
                    gridView1.Columns["INSERT_ID"].OptionsColumn.AllowEdit = false;
                    gridView1.Columns["UPDATE_DTTM"].OptionsColumn.AllowEdit = false;
                    gridView1.Columns["UPDATE_ID"].OptionsColumn.AllowEdit = false;

                    setColumnWidth(gridView1, "gridPolicy");
                }

                SetGridPolicy(oraPolicy);  //정책 리스트 조회결과를 gridPolicy에 세팅한다. (oraPolicy > dtPolicy > gridPolicy.DataSource)

                #region 현재 선택된 정책을 설정한 옵션 및 기준정보를 불러온다.
                //상단의 정책리스트 Grid 클릭이벤트에 의해 textPolicyID.Text에 값이 있다면 아래의 기준정보 조회가 동작한다. (화면 로딩후 처음 조회버튼 클릭시에는 엔진옵션 마스터만 보인다.)
                if (textPolicyID.Text == "")
                {
                    SetGridSelectedEngineOption(bizService.GetGridSelectedOptionByPolicy("E", "Master"));   //엔진옵션
                }
                else
                {
                    SetGridSelectedEngineOption(bizService.GetGridSelectedOptionByPolicy("E", textPolicyID.Text));   //엔진옵션
                    SetGridSelectedDistrictOption(bizService.GetGridSelectedOptionByPolicy("Z", textPolicyID.Text)); //권역정보
                    SetGridSelectedVehicleOption(bizService.GetGridSelectedOptionByPolicy("C", textPolicyID.Text));  //차량정보
                    SetGridSelectedStopOption(bizService.GetGridSelectedOptionByPolicy("A", textPolicyID.Text));     //착지정보
                }
                #endregion

                if (list_policy_code.Count == 0)
                {
                    buttonSave.Enabled = false;
                    buttonSaveAs.Enabled = true;
                    buttonSaveScenario.Enabled = true;
                }
                else if (list_policy_code.Count == 1)
                {
                    buttonSave.Enabled = true;
                    buttonSaveAs.Enabled = true;
                    buttonSaveScenario.Enabled = true;
                }
                else if (list_policy_code.Count > 1)
                {
                    buttonSave.Enabled = true;
                    buttonSaveAs.Enabled = false;
                    buttonSaveScenario.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.screenManager.CloseWaitForm();

                comboScenarioName.SelectedIndex = 0;
                comboScenarioCode.SelectedIndex = 0;
            }
        }

        /// <summary> 정책 리스트 조회결과를 gridPolicy에 세팅한다. (oraPolicy > dtPolicy > gridPolicy.DataSource) </summary>
        private void SetGridPolicy(OracleDataTable oraPolicy)
        {
            bool isFirstRow = true;
            foreach (DataRow _row in oraPolicy.Rows)
            {
                DataRow dr = dtPolicy.NewRow();

                gridView1.Columns["checkbox"].ColumnEdit = GetRepositoryItemCheckEdit();

                Debug.Write(string.Format("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", _row["POLICY_ID"], _row["DESCRIPTION"], _row["DESCRIPTION"], _row["INSERT_DTTM"], _row["INSERT_ID"], _row["UPDATE_DTTM"], _row["UPDATE_ID"]));
                Debug.WriteLine(null);

                dr["POLICY_ID"] = _row["POLICY_ID"]; dr["POLICY_NAME"] = _row["DESCRIPTION"];
                dr["DESCRIPTION"] = _row["DESCRIPTION"]; dr["INSERT_DTTM"] = _row["INSERT_DTTM"];
                dr["INSERT_ID"] = _row["INSERT_ID"]; dr["UPDATE_DTTM"] = _row["UPDATE_DTTM"];
                dr["UPDATE_ID"] = _row["UPDATE_ID"];

                if (isFirstRow)
                {
                    policy_code = _row["POLICY_ID"].ToString();
                    textPolicyDescription.Text = _row["DESCRIPTION"].ToString();
                    textPolicyName.Text = _row["DESCRIPTION"].ToString();
                    isFirstRow = false;
                }

                dtPolicy.Rows.Add(dr);
            }

            gridView1.Columns["checkbox"].OptionsColumn.AllowEdit = true;
            gridPolicy.DataSource = dtPolicy;
            gridPolicy.RefreshDataSource();
        }

        //정책리스트 클릭
        private void gridPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                DataRow dr = gridView1.GetFocusedDataRow();

                if (!gridView1.IsSizingState && gridView1.FocusedRowHandle >= 0)//컬럼사이즈 수정은 row 값이 없음.
                {
                    //policy column 선택시 신규 해제
                    new_policy_save = false;
                    buttonSave.Text = "기존 이름으로";

                    //textPolicyName.Text = dr["POLICY_NAME"].ToString();
                    //textPolicyDescription.Text = dr["DESCRIPTION"].ToString();

                    bizService = GetBizService();

                    GridView view = (GridView)gridPolicy.Views[0];

                    int row = view.FocusedRowHandle;

                    textPolicyDescription.Text = view.GetRowCellValue(row, "POLICY_NAME").ToString();
                    textPolicyName.Text = view.GetRowCellValue(row, "DESCRIPTION").ToString();
                    textPolicyID.Text = view.GetRowCellValue(row, "POLICY_ID").ToString();

                    if (view.SelectedRowsCount > 0 && !checkDelete.Checked)//삭제된 policy테이블이 아닐때
                    {
                        #region ( 이전 코드는 policy grid 클릭 시 selectedGridView가 변경되나, 2013.11.26 이후 아무동작하지않게 변경) -> 11.29 policy 컬럼 선택시 옵션 display 변경

                        GridView view_gridSelectedEngineOption = (GridView)gridSelectedEngineOption.Views[0];

                        #region 콤보박스 쿼리
                        ////엔진옵션 탭
                        //string query_type = "";
                        //string key_id = "VRO001_DST071_DSTC01";
                        //Zone_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //key_id = "VRO001_DST071_DSTC02";
                        //Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();

                        ////권역옵션 탭
                        //query_type = "";
                        //key_id = "VRO001_DST072_DSTC04";
                        //Priority_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //zone_Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();

                        ////차량옵션 탭
                        //query_type = "";
                        //key_id = "VRO001_DST073_DSTC16";
                        //caroption_region_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //key_id = "VRO001_DST073_DSTC17";
                        //caroption_area_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //key_id = "VRO001_DST073_DSTC18";
                        //caroption_area_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //key_id = "VRO001_DST073_DSTC19";
                        //caroption_remainder_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //key_id = "VRO001_DST073_DSTC20";
                        //caroption_remainder_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        ////착지옵션 탭
                        //key_id = "VRO001_DST074_DSTC21";
                        //Dispatcher_manualHandling_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        #endregion

                        #region GetGridSelectedOptionByPolicy
                        //OracleDataTable engineOraDt = bizService.GetGridSelectedOptionByPolicy("E", view.GetRowCellValue(row, "POLICY_ID").ToString());
                        //OracleDataTable zoneOraDt = bizService.GetGridSelectedOptionByPolicy("Z", view.GetRowCellValue(row, "POLICY_ID").ToString());
                        //OracleDataTable carOraDt = bizService.GetGridSelectedOptionByPolicy("C", view.GetRowCellValue(row, "POLICY_ID").ToString());
                        //OracleDataTable landingOraDt = bizService.GetGridSelectedOptionByPolicy("A", view.GetRowCellValue(row, "POLICY_ID").ToString());
                        #endregion

                        //foreach (DataRow _row in dtGridEngineOption.Rows)
                        //{
                        //    //if (_row["EngOpt_Code"].Equals(view.GetRowCellValue(row, "POLICY_ID").ToString()))
                        //    //{

                        //    #region 엔진옵션 탭

                        //    //콤보박스 데이터 바인딩 (엔진 옵션)

                        //    ///- IN Parameter   :   
                        //    ///  p_query_type : 차후 필요용도로 사용(현재는 참조하지않음)
                        //    ///  p_key_id :  공통코드 KEY 값

                        //    //string query_type = "";

                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "ENGPID", dtGridEngineOption);

                        //    //string key_id = "VRO001_DST071_DSTC01";
                        //    //Zone_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Zone", Zone_ComboboxOraDt);

                        //    //key_id = "VRO001_DST071_DSTC02";
                        //    //Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Distance_Calc", Distance_Calc_ComboboxOraDt);

                        //    // return value : VELCID , 현재 return value 없음
                        //    //Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "VELCID", "Speed", Speed_ComboboxOraDt);

                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Cumulative_rate", "Cumulative_rate", dtGridEngineOption);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "Deallocation_ratio", dtGridEngineOption);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "hour", list_hours);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "minute", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "second", list_minute_second);
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "overfloww_order", "overfloww_order", dtGridEngineOption);
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_weight", "By_weight", dtGridEngineOption);
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_volume", "By_volume", dtGridEngineOption);
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_PLT", "By_PLT", dtGridEngineOption);
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "End_time", "End_time", dtGridEngineOption);
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Vehicles_working", "Vehicles_working", dtGridEngineOption);

                        //    //중복클릭 데이터 삭제.
                        //    dtSelectedGridEngineOption.Rows.Clear();

                        //    //콤보박스 첫 item이 보여질 내용 디폴트 지정 (엔진옵션, Query로 특정 데이터 보여질 예정)
                        //    //OracleDataTable engineOraDt = bizService.GetGridSelectedOptionByPolicy("E", view.GetRowCellValue(row, "POLICY_ID").ToString());

                        //    //cell에 보여질 값 설정
                        //    foreach (DataRow _row1 in engineOraDt.Rows)
                        //    {
                        //        //DataRow 추가
                        //        DataRow engineDr = dtSelectedGridEngineOption.NewRow();

                        //        engineDr["ENGPID"] = _row1["ENGPID"];

                        //        //값이 아닌 NAME을 display
                        //        foreach (DataRow dr in Zone_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            engineDr["Zone"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row1["DSTORD"].ToString()))
                        //            {
                        //                engineDr["Zone"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        foreach (DataRow dr in Distance_Calc_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            engineDr["Distance_Calc"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                        //            {
                        //                engineDr["Distance_Calc"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        engineDr["Distance_Weight"] = _row1["WGTDST"];

                        //        //이동속도 관련 콤보박스 초기값 설정 작업해야함.
                        //        engineDr["Speed"] = _row1["VELCID"];

                        //        engineDr["Cumulative_rate"] = _row1["FCMVLC"];
                        //        engineDr["Deallocation_ratio"] = _row1["DACRAT"];

                        //        //상차시간 형식 ( 01:00:00 )
                        //        engineDr["hour"] = _row1["LOADTM"];
                        //        //string phase_difference_time = _row1["LOADTM"].ToString();
                        //        //if (!(phase_difference_time == null && phase_difference_time.Equals("") && phase_difference_time.Equals(" ")))
                        //        //{
                        //        //    engineDr["hour"] = phase_difference_time.Substring(0, 2);
                        //        //    engineDr["minute"] = phase_difference_time.Substring(3, 2);
                        //        //    engineDr["second"] = phase_difference_time.Substring(6, 2);
                        //        //}

                        //        engineDr["overfloww_order"] = _row1["FOCAPA"];
                        //        engineDr["Maximum_weight"] = _row1["WGTMXW"];
                        //        engineDr["Maximum_volume"] = _row1["WGTMXV"];
                        //        engineDr["Maximum_PLT"] = _row1["WGTMXP"];
                        //        engineDr["By_weight"] = _row1["FWGTSD"];
                        //        engineDr["By_volume"] = _row1["FVOLSD"];
                        //        engineDr["By_PLT"] = _row1["FPLTSD"];
                        //        engineDr["End_time"] = _row1["FTWEND"];
                        //        engineDr["Vehicles_working"] = _row1["FVHCTW"];
                        //        engineDr["Temporary_Vehicle_maximum_radius"] = _row1["MRTVHC"];

                        //        //Data 추가
                        //        dtSelectedGridEngineOption.Rows.Add(engineDr);
                        //    }

                        //    //DataSource 지정
                        //    gridSelectedEngineOption.DataSource = dtSelectedGridEngineOption;

                        //    //DataRefresh
                        //    gridSelectedEngineOption.RefreshDataSource();

                        //    #endregion

                        //    #region 권역옵션 탭

                        //    //query_type = "";
                        //    //key_id = "VRO001_DST072_DSTC04";
                        //    //Priority_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority01", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority02", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority03", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority04", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority05", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority06", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority07", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority08", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority09", Priority_ComboboxOraDt);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority10", Priority_ComboboxOraDt);

                        //    key_id = "VRO001_DST072_DSTC14";
                        //    zone_Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "zone_Distance_Calc", zone_Distance_Calc_ComboboxOraDt);

                        //    // return value : VELCID , 현재 return value 없음
                        //    //zone_Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
                        //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "VELCID", "zone_Speed", zone_Speed_ComboboxOraDt);

                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedZoneOption, "zone_Cumulative_rate", "zone_Cumulative_rate", dtGridZoneOption);

                        //    //중복클릭 데이터 삭제.
                        //    dtSelectedGridZoneOption.Rows.Clear();

                        //    //콤보박스 첫 item이 보여질 내용 디폴트 지정 (Zone옵션, Query로 특정 데이터 보여질 예정)
                        //    //OracleDataTable zoneOraDt = bizService.GetGridSelectedOptionByPolicy("Z", view.GetRowCellValue(row, "POLICY_ID").ToString());

                        //    foreach (DataRow _row1 in zoneOraDt.Rows)
                        //    {
                        //        //DataRow 추가
                        //        DataRow zoneDr = dtSelectedGridZoneOption.NewRow();

                        //        zoneDr["Zone_Code"] = _row1["DSTTID"];
                        //        zoneDr["Zone_Name"] = _row1["DSTTYP"];
                        //        zoneDr["Zone_Division"] = _row1["DSTTNM"];

                        //        foreach (DataRow dr in Priority_ComboboxOraDt.Rows)
                        //        {
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP01"].ToString()))
                        //            {
                        //                zoneDr["Priority01"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP02"].ToString()))
                        //            {
                        //                zoneDr["Priority02"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP03"].ToString()))
                        //            {
                        //                zoneDr["Priority03"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP04"].ToString()))
                        //            {
                        //                zoneDr["Priority04"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP05"].ToString()))
                        //            {
                        //                zoneDr["Priority05"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP06"].ToString()))
                        //            {
                        //                zoneDr["Priority06"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP07"].ToString()))
                        //            {
                        //                zoneDr["Priority07"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP08"].ToString()))
                        //            {
                        //                zoneDr["Priority08"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP09"].ToString()))
                        //            {
                        //                zoneDr["Priority09"] = dr["NAMES"];
                        //            }
                        //            if (dr["VALES"].ToString().Equals(_row1["ORDP10"].ToString()))
                        //            {
                        //                zoneDr["Priority10"] = dr["NAMES"];
                        //            }
                        //        }

                        //        foreach (DataRow dr in zone_Distance_Calc_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            zoneDr["zone_Distance_Calc"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                        //            {
                        //                zoneDr["zone_Distance_Calc"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        zoneDr["zone_Distance_Weight"] = _row1["DSTWGT"];
                        //        zoneDr["zone_Speed"] = _row1["VELCID"];
                        //        zoneDr["zone_Cumulative_rate"] = _row1["FCMVLC"];
                        //        zoneDr["zone_Deallocation_ratio"] = _row1["DACRAT"];
                        //        zoneDr["zone_Move_time_limit"] = _row1["TMSTOP"];
                        //        zoneDr["zone_Temporary_Vehicle_maximum_radius"] = _row1["DSTVHC"];

                        //        //Data 추가
                        //        dtSelectedGridZoneOption.Rows.Add(zoneDr);
                        //    }

                        //    //DataSource 지정
                        //    //gridSelectedZoneOption.DataSource = dtSelectedGridZoneOption;

                        //    //DataRefresh
                        //    gridSelectedZoneOption.RefreshDataSource();

                        //    #endregion

                        //    #region 차량옵션 탭

                        //    //Parameter ( control, 가져올 caroption 컬럼이름, 현재 보여질 컬럼이름, 데이터 테이블 )
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_arrival_within_wokingTime", "caroption_arrival", dtGridCarOption);
                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_Whether_working", "caroption_working", dtGridCarOption);

                        //    //query_type = "";
                        //    //key_id = "VRO001_DST073_DSTC16";
                        //    //caroption_region_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_region_reorder", caroption_region_reorder_ComboboxOraDt);

                        //    //key_id = "VRO001_DST073_DSTC17";
                        //    //caroption_area_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign", caroption_area_assign_ComboboxOraDt);

                        //    //key_id = "VRO001_DST073_DSTC18";
                        //    //caroption_area_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign_reorder", caroption_area_assign_reorder_ComboboxOraDt);

                        //    //key_id = "VRO001_DST073_DSTC19";
                        //    //caroption_remainder_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign", caroption_remainder_assign_ComboboxOraDt);

                        //    //key_id = "VRO001_DST073_DSTC20";
                        //    //caroption_remainder_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign_reorder", caroption_remainder_assign_reorder_ComboboxOraDt);

                        //    //DataRow 추가
                        //    DataRow carDr = dtSelectedGridCarOption.NewRow();

                        //    //중복클릭 데이터 삭제.
                        //    dtSelectedGridCarOption.Rows.Clear();

                        //    //콤보박스 첫 item이 보여질 내용 디폴트 지정 (Zone옵션, Query로 특정 데이터 보여질 예정)
                        //    //OracleDataTable carOraDt = bizService.GetGridSelectedOptionByPolicy("C", view.GetRowCellValue(row, "POLICY_ID").ToString());

                        //    foreach (DataRow _row2 in carOraDt.Rows)
                        //    {
                        //        //DataRow 추가
                        //        DataRow _carDr = dtSelectedGridCarOption.NewRow();

                        //        _carDr["caroption_car_number"] = _row2["VEHCID"];
                        //        _carDr["caroption_car_type"] = _row2["VHCTON"];
                        //        _carDr["caroption_max_landing_num"] = _row2["LMSPNO"];
                        //        _carDr["caroption_arrival"] = _row2["FINTWA"];
                        //        _carDr["caroption_working"] = _row2["FONDUT"];
                        //        _carDr["caroption_region"] = _row2["DIST01"];
                        //        _carDr["caroption_mix_region"] = _row2["LMMDS1"];

                        //        foreach (DataRow dr in caroption_region_reorder_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            _carDr["caroption_region_reorder"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row2["REAL01"].ToString()))
                        //            {
                        //                _carDr["caroption_region_reorder"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        _carDr["caroption_area"] = _row2["DIST02"];
                        //        _carDr["caroption_mix_area"] = _row2["LMMDS02"];

                        //        foreach (DataRow dr in caroption_area_assign_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            _carDr["caroption_area_assign"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row2["ALLO01"].ToString()))
                        //            {
                        //                _carDr["caroption_area_assign"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        foreach (DataRow dr in caroption_area_assign_reorder_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            _carDr["caroption_area_assign_reorder"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row2["REAL02"].ToString()))
                        //            {
                        //                _carDr["caroption_area_assign_reorder"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        foreach (DataRow dr in caroption_remainder_assign_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            _carDr["caroption_remainder_assign"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row2["ALLO03"].ToString()))
                        //            {
                        //                _carDr["caroption_remainder_assign"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        foreach (DataRow dr in caroption_remainder_assign_reorder_ComboboxOraDt.Rows)
                        //        {
                        //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //            _carDr["caroption_remainder_assign_reorder"] = "default";

                        //            if (dr["VALES"].ToString().Equals(_row2["REAL03"].ToString()))
                        //            {
                        //                _carDr["caroption_remainder_assign_reorder"] = dr["NAMES"];
                        //                break;
                        //            }
                        //        }

                        //        _carDr["caroption_1"] = _row2["ATTR11"];
                        //        _carDr["caroption_2"] = _row2["ATTR12"];
                        //        _carDr["caroption_3"] = _row2["ATTR13"];
                        //        _carDr["caroption_4"] = _row2["ATTR14"];
                        //        _carDr["caroption_5"] = _row2["ATTR15"];
                        //        _carDr["caroption_6"] = _row2["ATTR16"];
                        //        _carDr["caroption_7"] = _row2["ATTR17"];
                        //        _carDr["caroption_8"] = _row2["ATTR18"];
                        //        _carDr["caroption_9"] = _row2["ATTR19"];
                        //        _carDr["caroption_10"] = _row2["ATTR20"];

                        //        //Data 추가
                        //        dtSelectedGridCarOption.Rows.Add(_carDr);
                        //    }

                        //    //DataSource 지정
                        //    gridSelectedCarOption.DataSource = dtSelectedGridCarOption;

                        //    //DataRefresh
                        //    gridSelectedCarOption.RefreshDataSource();

                        //    #endregion

                        //    #region 착지옵션 탭

                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime", list_hours);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime1", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime2", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime", list_hours);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime1", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime2", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "landing_OTD_compliance", "OTD_compliance", dtGridLandingOption);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime", list_hours);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime1", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime2", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime", list_hours);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime1", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime2", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime", list_hours);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime1", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime2", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime", list_hours);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime1", list_minute_second);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime2", list_minute_second);

                        //    //key_id = "VRO001_DST074_DSTC21";
                        //    //Dispatcher_manualHandling_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                        //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "NAMES", "Dispatcher_manualHandling", Dispatcher_manualHandling_ComboboxOraDt);

                        //    SetRepositoryItemComboBoxBindDataYN(gridSelectedLandingOption, "landing_Whether_assigned", "Whether_assigned", dtGridLandingOption);

                        //    //중복클릭 데이터 삭제.
                        //    dtSelectedGridLandingOption.Rows.Clear();

                        //    //콤보박스 첫 item이 보여질 내용 디폴트 지정 (엔진옵션, Query로 특정 데이터 보여질 예정)
                        //    //OracleDataTable landingOraDt = bizService.GetGridSelectedLandingOptionDefault();
                        //    //OracleDataTable landingOraDt = bizService.GetGridSelectedOptionByPolicy("A", view.GetRowCellValue(row, "POLICY_ID").ToString());

                        //    foreach (DataRow _row3 in landingOraDt.Rows)
                        //    {
                        //        //DataRow 추가
                        //        DataRow landingDr = dtSelectedGridLandingOption.NewRow();

                        //        landingDr["landingoption_number"] = _row3["STOPID"];
                        //        landingDr["landingoption_name"] = _row3["STOPNM"];
                        //        landingDr["landingoption_restrict"] = _row3["BVEHID"];
                        //        //TKXADS18_THR 조인정보, 현재 쿼리에서 컬럼 없음. 프로시져 완료 후 가능
                        //        //landingDr["landingoption_box_number"] = _row3["BOXCNT"];
                        //        //landingDr["landingoption_box_rating"] = _row3["BOXLEV"];
                        //        //landingDr["landingoption_weight"] = _row3["WEIGHT"];
                        //        //landingDr["landingoption_volumn"] = _row3["VOLUME"];
                        //        //landingDr["landingoption_PLT"] = _row3["PLTCNT"];

                        //        //DB 데이터 시간 형식 ( 01:00:00 )
                        //        landingDr["Adhesive_required_startTime"] = _row3["TIMEFR"];
                        //        //string time = _row3["TIMEFR"].ToString();
                        //        //if (!(time == null || time.Equals("") || time.Equals(" ")))//업데이트 할때 null 인 데이터는 " " 공백 넣어서 처리, 데이터 받을때도 공백으로 받음...디비 프로시져에서 처리시 공백 요청
                        //        //{
                        //        //    landingDr["Adhesive_required_startTime"] = time.Substring(0, 2);
                        //        //    landingDr["Adhesive_required_startTime1"] = time.Substring(3, 2);
                        //        //    landingDr["Adhesive_required_startTime2"] = time.Substring(6, 2);
                        //        //}

                        //        landingDr["Adhesive_required_endTime"] = _row3["TIMETO"];
                        //        //string time1 = _row3["TIMETO"].ToString();
                        //        //if (!(time1 == null || time1.Equals("") || time1.Equals(" ")))
                        //        //{
                        //        //    landingDr["Adhesive_required_endTime"] = time1.Substring(0, 2);
                        //        //    landingDr["Adhesive_required_endTime1"] = time1.Substring(3, 2);
                        //        //    landingDr["Adhesive_required_endTime2"] = time1.Substring(6, 2);
                        //        //}

                        //        landingDr["OTD_compliance"] = _row3["OTDOPT"];

                        //        landingDr["OffTime_startTime"] = _row3["OFFTFR"];
                        //        //string time2 = _row3["OFFTFR"].ToString();
                        //        //if (!(time2 == null || time2.Equals("") || time2.Equals(" ")))
                        //        //{
                        //        //    landingDr["OffTime_startTime"] = time2.Substring(0, 2);
                        //        //    landingDr["OffTime_startTime1"] = time2.Substring(3, 2);
                        //        //    landingDr["OffTime_startTime2"] = time2.Substring(6, 2);
                        //        //}

                        //        landingDr["OffTime_endTime"] = _row3["OFFTTO"];
                        //        //string time3 = _row3["OFFTTO"].ToString();
                        //        //if (!(time3 == null || time3.Equals("") || time3.Equals(" ")))
                        //        //{
                        //        //    landingDr["OffTime_endTime"] = time3.Substring(0, 2);
                        //        //    landingDr["OffTime_endTime1"] = time3.Substring(3, 2);
                        //        //    landingDr["OffTime_endTime2"] = time3.Substring(6, 2);
                        //        //}

                        //        landingDr["Fixed_handlingTime"] = _row3["PREPTM"];
                        //        //string time4 = _row3["PREPTM"].ToString();
                        //        //if (!(time4 == null || time4.Equals("") || time4.Equals(" ")))
                        //        //{
                        //        //    landingDr["Fixed_handlingTime"] = time4.Substring(0, 2);
                        //        //    landingDr["Fixed_handlingTime1"] = time4.Substring(3, 2);
                        //        //    landingDr["Fixed_handlingTime2"] = time4.Substring(6, 2);
                        //        //}

                        //        landingDr["change_handlingTime"] = _row3["UNLDTM"];
                        //        //string time5 = _row3["UNLDTM"].ToString();
                        //        //if (!(time5 == null || time5.Equals("") || time5.Equals(" ")))
                        //        //{
                        //        //    landingDr["change_handlingTime"] = time5.Substring(0, 2);
                        //        //    landingDr["change_handlingTime1"] = time5.Substring(3, 2);
                        //        //    landingDr["change_handlingTime2"] = time5.Substring(6, 2);
                        //        //}

                        //        //해당 컬럼 없음. 컬럼 다시 확인.
                        //        //landingDr["Recent_delivery_vehicleNumber"] = _row3["HVEHID"];
                        //        //landingDr["Latest_arrivalTime"] = _row3["HARRTM"];

                        //        //foreach (DataRow dr in Dispatcher_manualHandling_ComboboxOraDt.Rows)
                        //        //{
                        //        //    //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        //        //    landingDr["Dispatcher_manualHandling"] = "default";

                        //        //    if (dr["VALES"].ToString().Equals(_row3["MALTYP"].ToString()))
                        //        //    {
                        //        //        landingDr["Dispatcher_manualHandling"] = dr["NAMES"];
                        //        //        break;
                        //        //    }
                        //        //}

                        //        //landingDr["Whether_assigned"] = _row3["FALLOC"];

                        //        //Data 추가
                        //        dtSelectedGridLandingOption.Rows.Add(landingDr);
                        //    }
                        //    //DataSource 지정
                        //    gridSelectedLandingOption.DataSource = dtSelectedGridLandingOption;

                        //    //DataRefresh
                        //    gridSelectedLandingOption.RefreshDataSource();

                        //    #endregion

                        //    //}
                        //}

                        #endregion
                    }
                    else if (view.SelectedRowsCount > 0 && checkDelete.Checked)//삭제된 테이블은 옵션 테이블에 선택된 하나의 row만 보여준다.
                    {
                        //#region 엔진옵션,저장할 엔진옵션 탭

                        ////중복클릭 데이터 삭제.
                        //dtGridEngineOption.Rows.Clear();
                        //dtSelectedGridEngineOption.Rows.Clear();

                        //OracleDataTable engineOraDt = bizService.GetGridSelectedOptionByPolicy("E", view.GetRowCellValue(row, "POLICY_ID").ToString());
                        //foreach (DataRow _row in engineOraDt.Rows)
                        //{
                        //    //엔진옵션
                        //    DataRow dr = dtGridEngineOption.NewRow();

                        //    //dr["EngOpt_Code"] = _row["POLYID"];
                        //    dr["ENGPID"] = _row["ENGPID"];//POLYID로 변경
                        //    dr["Zone"] = _row["DSTORD"];
                        //    dr["Distance_Calc"] = _row["DSTCTY"];
                        //    dr["Distance_Weight"] = _row["WGTDST"];
                        //    dr["Speed"] = _row["VELCID"];
                        //    dr["Cumulative_rate"] = _row["FCMVLC"];
                        //    dr["Deallocation_ratio"] = _row["DACRAT"];
                        //    dr["phase_difference_time"] = _row["LOADTM"];
                        //    dr["overfloww_order"] = _row["FOCAPA"];
                        //    dr["Maximum_weight"] = _row["WGTMXW"];
                        //    dr["Maximum_volume"] = _row["WGTMXV"];
                        //    dr["Maximum_PLT"] = _row["WGTMXP"];
                        //    dr["By_weight"] = _row["FWGTSD"];
                        //    dr["By_volume"] = _row["FVOLSD"];
                        //    dr["By_PLT"] = _row["FPLTSD"];
                        //    dr["End_time"] = _row["FTWEND"];
                        //    dr["Vehicles_working"] = _row["FVHCTW"];
                        //    dr["Temporary_Vehicle_maximum_radius"] = _row["MRTVHC"];

                        //    dtGridEngineOption.Rows.Add(dr);

                        //    //저장할 엔진옵션
                        //    DataRow dr1 = dtSelectedGridEngineOption.NewRow();

                        //    dr1["Zone"] = _row["DSTORD"];
                        //    dr1["Distance_Calc"] = _row["DSTCTY"];
                        //    dr1["Distance_Weight"] = _row["WGTDST"];
                        //    dr1["Speed"] = _row["VELCID"];
                        //    dr1["Cumulative_rate"] = _row["FCMVLC"];
                        //    dr1["Deallocation_ratio"] = _row["DACRAT"];

                        //    //상차시간 형식 ( 01:00:00 )
                        //    string phase_difference_time = _row["LOADTM"].ToString();
                        //    if (!(phase_difference_time == null && phase_difference_time.Equals("") && phase_difference_time.Equals(" ")))
                        //    {
                        //        dr1["hour"] = phase_difference_time.Substring(0, 2);
                        //        dr1["minute"] = phase_difference_time.Substring(3, 2);
                        //        dr1["second"] = phase_difference_time.Substring(6, 2);
                        //    }

                        //    dr1["overfloww_order"] = _row["FOCAPA"];
                        //    dr1["Maximum_weight"] = _row["WGTMXW"];
                        //    dr1["Maximum_volume"] = _row["WGTMXV"];
                        //    dr1["Maximum_PLT"] = _row["WGTMXP"];
                        //    dr1["By_weight"] = _row["FWGTSD"];
                        //    dr1["By_volume"] = _row["FVOLSD"];
                        //    dr1["By_PLT"] = _row["FPLTSD"];
                        //    dr1["End_time"] = _row["FTWEND"];
                        //    dr1["Vehicles_working"] = _row["FVHCTW"];
                        //    dr1["Temporary_Vehicle_maximum_radius"] = _row["MRTVHC"];

                        //    dtSelectedGridEngineOption.Rows.Add(dr1);

                        //}

                        //gridSelectedEngineOption.RefreshDataSource();
                        //gridEngineOption.RefreshDataSource();

                        //#endregion

                        //#region 권역옵션,저장할 권역옵션 탭

                        //dtGridZoneOption.Rows.Clear();
                        //dtSelectedGridZoneOption.Rows.Clear();

                        //OracleDataTable zoneOraDt = bizService.GetGridSelectedOptionByPolicy("Z", view.GetRowCellValue(row, "POLICY_ID").ToString());

                        ////권역 옵션
                        //DataRow ZoneOption_dr = dtGridZoneOption.NewRow();
                        //ZoneOption_dr["zone_policyID"] = gridView1.GetFocusedRowCellValue("POLICY_ID");
                        //ZoneOption_dr["zone_policy_name"] = gridView1.GetFocusedRowCellValue("POLICY_NAME");

                        //dtGridZoneOption.Rows.Add(ZoneOption_dr);

                        //foreach (DataRow _row in zoneOraDt.Rows)
                        //{
                        //    //저장할 권역 옵션
                        //    DataRow dr1 = dtSelectedGridZoneOption.NewRow();
                        //    dr1["Zone_Code"] = _row["DSTTID"];
                        //    dr1["Zone_Name"] = _row["DSTTYP"];
                        //    dr1["Zone_Division"] = _row["DSTTNM"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["Priority01"] = _row["ORDP01"];
                        //    dr1["zone_Distance_Weight"] = _row["DSTWGT"];
                        //    dr1["zone_Speed"] = _row["VELCID"];
                        //    dr1["zone_Cumulative_rate"] = _row["FCMVLC"];
                        //    dr1["zone_Deallocation_ratio"] = _row["DACRAT"];
                        //    dr1["zone_Move_time_limit"] = _row["TMSTOP"];
                        //    dr1["zone_Temporary_Vehicle_maximum_radius"] = _row["DSTVHC"];

                        //    dtSelectedGridZoneOption.Rows.Add(dr1);
                        //}

                        //gridZoneOption.RefreshDataSource();
                        //gridSelectedZoneOption.RefreshDataSource();

                        //#endregion

                        //#region 차량옵션,저장할 차량옵션 탭

                        //dtGridCarOption.Rows.Clear();
                        //dtSelectedGridCarOption.Rows.Clear();

                        //OracleDataTable carOraDt = bizService.GetGridSelectedOptionByPolicy("C", view.GetRowCellValue(row, "POLICY_ID").ToString());

                        ////차량 옵션
                        //DataRow carOption_dr = dtGridCarOption.NewRow();
                        //carOption_dr["car_policyID"] = gridView1.GetFocusedRowCellValue("POLICY_ID");
                        //carOption_dr["car_policy_name"] = gridView1.GetFocusedRowCellValue("POLICY_NAME");

                        //dtGridCarOption.Rows.Add(carOption_dr);

                        //foreach (DataRow _row2 in carOraDt.Rows)
                        //{
                        //    //DataRow 추가
                        //    DataRow _carDr = dtSelectedGridCarOption.NewRow();

                        //    _carDr["caroption_car_number"] = _row2["VEHCID"];
                        //    _carDr["caroption_car_type"] = _row2["VHCTON"];
                        //    _carDr["caroption_max_landing_num"] = _row2["LMSPNO"];
                        //    _carDr["caroption_arrival"] = _row2["FINTWA"];
                        //    _carDr["caroption_working"] = _row2["FONDUT"];
                        //    _carDr["caroption_region"] = _row2["DIST01"];
                        //    _carDr["caroption_mix_region"] = _row2["LMMDS1"];

                        //    _carDr["caroption_region_reorder"] = _row2["REAL01"];

                        //    _carDr["caroption_area"] = _row2["DIST02"];
                        //    _carDr["caroption_mix_area"] = _row2["LMMDS02"];

                        //    _carDr["caroption_area_assign"] = _row2["ALLO01"];
                        //    _carDr["caroption_area_assign_reorder"] = _row2["REAL02"];
                        //    _carDr["caroption_remainder_assign"] = _row2["ALLO03"];
                        //    _carDr["caroption_remainder_assign_reorder"] = _row2["REAL03"];

                        //    _carDr["caroption_1"] = _row2["ATTR11"];
                        //    _carDr["caroption_2"] = _row2["ATTR12"];
                        //    _carDr["caroption_3"] = _row2["ATTR13"];
                        //    _carDr["caroption_4"] = _row2["ATTR14"];
                        //    _carDr["caroption_5"] = _row2["ATTR15"];
                        //    _carDr["caroption_6"] = _row2["ATTR16"];
                        //    _carDr["caroption_7"] = _row2["ATTR17"];
                        //    _carDr["caroption_8"] = _row2["ATTR18"];
                        //    _carDr["caroption_9"] = _row2["ATTR19"];
                        //    _carDr["caroption_10"] = _row2["ATTR20"];

                        //    //Data 추가
                        //    dtSelectedGridCarOption.Rows.Add(_carDr);
                        //}

                        //gridCarOption.DataSource = dtGridCarOption;

                        //gridCarOption.RefreshDataSource();
                        //gridSelectedCarOption.RefreshDataSource();

                        //#endregion

                        //#region 착지옵션,저장할 착지옵션 탭

                        //dtGridLandingOption.Rows.Clear();
                        //dtSelectedGridLandingOption.Rows.Clear();

                        //OracleDataTable landingOraDt = bizService.GetGridSelectedOptionByPolicy("A", view.GetRowCellValue(row, "POLICY_ID").ToString());

                        ////착지 옵션
                        //DataRow laingOption_dr = dtGridLandingOption.NewRow();
                        //laingOption_dr["landing_policyID"] = gridView1.GetFocusedRowCellValue("POLICY_ID");
                        //laingOption_dr["landing_policy_name"] = gridView1.GetFocusedRowCellValue("POLICY_NAME");

                        //dtGridLandingOption.Rows.Add(laingOption_dr);

                        //foreach (DataRow _row3 in landingOraDt.Rows)
                        //{
                        //    //DataRow 추가
                        //    DataRow landingDr = dtSelectedGridLandingOption.NewRow();

                        //    landingDr["landingoption_number"] = _row3["STOPID"];
                        //    landingDr["landingoption_name"] = _row3["STOPNM"];
                        //    landingDr["landingoption_restrict"] = _row3["BVEHID"];
                        //    //TKXADS18_THR 조인정보, 현재 쿼리에서 컬럼 없음. 프로시져 완료 후 가능
                        //    //landingDr["landingoption_box_number"] = _row3["BOXCNT"];
                        //    //landingDr["landingoption_box_rating"] = _row3["BOXLEV"];
                        //    //landingDr["landingoption_weight"] = _row3["WEIGHT"];
                        //    //landingDr["landingoption_volumn"] = _row3["VOLUME"];
                        //    //landingDr["landingoption_PLT"] = _row3["PLTCNT"];

                        //    //DB 데이터 시간 형식 ( 01:00:00 )
                        //    landingDr["Adhesive_required_startTime"] = _row3["TIMEFR"];
                        //    //string time = _row3["TIMEFR"].ToString();
                        //    //if (!(time == null || time.Equals("") || time.Equals(" ")))//업데이트 할때 null 인 데이터는 " " 공백 넣어서 처리, 데이터 받을때도 공백으로 받음...디비 프로시져에서 처리시 공백 요청
                        //    //{
                        //    //    landingDr["Adhesive_required_startTime"] = time.Substring(0, 2);
                        //    //    landingDr["Adhesive_required_startTime1"] = time.Substring(3, 2);
                        //    //    landingDr["Adhesive_required_startTime2"] = time.Substring(6, 2);
                        //    //}

                        //    landingDr["Adhesive_required_endTime"] = _row3["TIMETO"];
                        //    //string time1 = _row3["TIMETO"].ToString();
                        //    //if (!(time1 == null || time1.Equals("") || time1.Equals(" ")))
                        //    //{
                        //    //    landingDr["Adhesive_required_endTime"] = time1.Substring(0, 2);
                        //    //    landingDr["Adhesive_required_endTime1"] = time1.Substring(3, 2);
                        //    //    landingDr["Adhesive_required_endTime2"] = time1.Substring(6, 2);
                        //    //}

                        //    landingDr["OTD_compliance"] = _row3["OTDOPT"];

                        //    landingDr["OffTime_startTime"] = _row3["OFFTFR"];
                        //    //string time2 = _row3["OFFTFR"].ToString();
                        //    //if (!(time2 == null || time2.Equals("") || time2.Equals(" ")))
                        //    //{
                        //    //    landingDr["OffTime_startTime"] = time2.Substring(0, 2);
                        //    //    landingDr["OffTime_startTime1"] = time2.Substring(3, 2);
                        //    //    landingDr["OffTime_startTime2"] = time2.Substring(6, 2);
                        //    //}

                        //    landingDr["OffTime_endTime"] = _row3["OFFTTO"];
                        //    //string time3 = _row3["OFFTTO"].ToString();
                        //    //if (!(time3 == null || time3.Equals("") || time3.Equals(" ")))
                        //    //{
                        //    //    landingDr["OffTime_endTime"] = time3.Substring(0, 2);
                        //    //    landingDr["OffTime_endTime1"] = time3.Substring(3, 2);
                        //    //    landingDr["OffTime_endTime2"] = time3.Substring(6, 2);
                        //    //}

                        //    landingDr["Fixed_handlingTime"] = _row3["PREPTM"];
                        //    //string time4 = _row3["PREPTM"].ToString();
                        //    //if (!(time4 == null || time4.Equals("") || time4.Equals(" ")))
                        //    //{
                        //    //    landingDr["Fixed_handlingTime"] = time4.Substring(0, 2);
                        //    //    landingDr["Fixed_handlingTime1"] = time4.Substring(3, 2);
                        //    //    landingDr["Fixed_handlingTime2"] = time4.Substring(6, 2);
                        //    //}

                        //    landingDr["change_handlingTime"] = _row3["UNLDTM"];
                        //    //string time5 = _row3["UNLDTM"].ToString();
                        //    //if (!(time5 == null || time5.Equals("") || time5.Equals(" ")))
                        //    //{
                        //    //    landingDr["change_handlingTime"] = time5.Substring(0, 2);
                        //    //    landingDr["change_handlingTime1"] = time5.Substring(3, 2);
                        //    //    landingDr["change_handlingTime2"] = time5.Substring(6, 2);
                        //    //}

                        //    //해당 컬럼 없음. 컬럼 다시 확인.
                        //    //landingDr["Recent_delivery_vehicleNumber"] = _row3["HVEHID"];
                        //    //landingDr["Latest_arrivalTime"] = _row3["HARRTM"];
                        //    //landingDr["Dispatcher_manualHandling"] = _row3["MALTYP"];
                        //    //landingDr["Whether_assigned"] = _row3["FALLOC"];

                        //    //Data 추가
                        //    dtSelectedGridLandingOption.Rows.Add(landingDr);
                        //}

                        //gridLandingOption.DataSource = dtGridLandingOption;

                        //gridLandingOption.RefreshDataSource();
                        //gridSelectedLandingOption.RefreshDataSource();

                        //#endregion
                    }
                }

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

        //엔진옵션 column 선택(공통적이거 많음. 나중에 다 공통함수로...)
        private void gridEngineOption_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                GridView view1 = (GridView)gridPolicy.Views[0];
                int b = view1.SelectedRowsCount;

                //if (b > 0)
                if (!gridView2.IsSizingState && gridView2.FocusedRowHandle >= 0)//컬럼사이즈 수정은 row 값이 없음.
                {
                    //int row = gridView2.FocusedRowHandle;

                    ////dtSelectedGridEngineOption.Rows.Clear();

                    //if (gridView2.SelectedRowsCount > 0)
                    //{
                    //    string query_type = "";

                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "ENGPID", dtGridEngineOption);

                    //    string key_id = "VRO001_DST071_DSTC01";
                    //    Zone_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Zone", Zone_ComboboxOraDt);

                    //    key_id = "VRO001_DST071_DSTC02";
                    //    Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Distance_Calc", Distance_Calc_ComboboxOraDt);

                    //    // return value : VELCID , 현재 return value 없음
                    //    Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "VELCID", "Speed", Speed_ComboboxOraDt);

                    //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Cumulative_rate", "Cumulative_rate", dtGridEngineOption);
                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "Deallocation_ratio", dtGridEngineOption);
                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "hour", list_hours);
                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "minute", list_minute_second);
                    //    SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "second", list_minute_second);
                    //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "overfloww_order", "overfloww_order", dtGridEngineOption);
                    //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_weight", "By_weight", dtGridEngineOption);
                    //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_volume", "By_volume", dtGridEngineOption);
                    //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_PLT", "By_PLT", dtGridEngineOption);
                    //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "End_time", "End_time", dtGridEngineOption);
                    //    SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Vehicles_working", "Vehicles_working", dtGridEngineOption);

                    //    //중복클릭 데이터 삭제.
                    //    dtSelectedGridEngineOption.Rows.Clear();

                    //    string a = gridView2.GetRowCellValue(row, "ENGPID").ToString();
                    //    //콤보박스 첫 item이 보여질 내용 디폴트 지정 (엔진옵션, Query로 특정 데이터 보여질 예정)
                    //    OracleDataTable engineOraDt = bizService.GetGridSelectedOptionByPolicy("E", gridView2.GetRowCellValue(row, "POLICY_ID").ToString());

                    //    //cell에 보여질 값 설정
                    //    foreach (DataRow _row1 in engineOraDt.Rows)
                    //    {
                    //        //DataRow 추가
                    //        DataRow engineDr = dtSelectedGridEngineOption.NewRow();

                    //        engineDr["ENGPID"] = _row1["ENGPID"];
                    //        //값이 아닌 NAME을 display
                    //        foreach (DataRow dr in Zone_ComboboxOraDt.Rows)
                    //        {
                    //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                    //            engineDr["Zone"] = "default";

                    //            if (dr["VALES"].ToString().Equals(_row1["DSTORD"].ToString()))
                    //            {
                    //                engineDr["Zone"] = dr["NAMES"];
                    //                break;
                    //            }
                    //        }

                    //        foreach (DataRow dr in Distance_Calc_ComboboxOraDt.Rows)
                    //        {
                    //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                    //            engineDr["Distance_Calc"] = "default";

                    //            if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                    //            {
                    //                engineDr["Distance_Calc"] = dr["NAMES"];
                    //                break;
                    //            }
                    //        }

                    //        engineDr["Distance_Weight"] = _row1["WGTDST"];

                    //        //이동속도 관련 콤보박스 초기값 설정 작업해야함.
                    //        engineDr["Speed"] = _row1["VELCID"];

                    //        engineDr["Cumulative_rate"] = _row1["FCMVLC"];
                    //        engineDr["Deallocation_ratio"] = _row1["DACRAT"];

                    //        //상차시간 형식 ( 01:00:00 )
                    //        string phase_difference_time = _row1["LOADTM"].ToString();
                    //        if (!(phase_difference_time == null && phase_difference_time.Equals("") && phase_difference_time.Equals(" ")))
                    //        {
                    //            engineDr["hour"] = phase_difference_time.Substring(0, 2);
                    //            engineDr["minute"] = phase_difference_time.Substring(3, 2);
                    //            engineDr["second"] = phase_difference_time.Substring(6, 2);
                    //        }

                    //        engineDr["overfloww_order"] = _row1["FOCAPA"];
                    //        engineDr["Maximum_weight"] = _row1["WGTMXW"];
                    //        engineDr["Maximum_volume"] = _row1["WGTMXV"];
                    //        engineDr["Maximum_PLT"] = _row1["WGTMXP"];
                    //        engineDr["By_weight"] = _row1["FWGTSD"];
                    //        engineDr["By_volume"] = _row1["FVOLSD"];
                    //        engineDr["By_PLT"] = _row1["FPLTSD"];
                    //        engineDr["End_time"] = _row1["FTWEND"];
                    //        engineDr["Vehicles_working"] = _row1["FVHCTW"];
                    //        engineDr["Temporary_Vehicle_maximum_radius"] = _row1["MRTVHC"];

                    //        //Data 추가
                    //        dtSelectedGridEngineOption.Rows.Add(engineDr);
                    //    }

                    //    //DataSource 지정
                    //    gridSelectedEngineOption.DataSource = dtSelectedGridEngineOption;

                    //    //DataRefresh
                    //    gridSelectedEngineOption.RefreshDataSource();

                    //    #region 이전 코드 (12.03)
                    //    //bandedGridView2.SetRowCellValue(0, "Zone", gridView2.GetRowCellValue(row, "Zone").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Distance_Calc", gridView2.GetRowCellValue(row, "Distance_Calc").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Distance_Weight", gridView2.GetRowCellValue(row, "Distance_Weight").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Speed", gridView2.GetRowCellValue(row, "Speed").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Cumulative_rate", gridView2.GetRowCellValue(row, "Cumulative_rate").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Deallocation_ratio", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());

                    //    //string phase_difference_time = gridView2.GetRowCellValue(row, "phase_difference_time").ToString();
                    //    //if (!(phase_difference_time == null || phase_difference_time.Equals("")))
                    //    //{
                    //    //    bandedGridView2.SetRowCellValue(0, "hour", phase_difference_time.Substring(0, 2));
                    //    //    bandedGridView2.SetRowCellValue(0, "minute", phase_difference_time.Substring(3, 2));
                    //    //    bandedGridView2.SetRowCellValue(0, "second", phase_difference_time.Substring(6, 2));
                    //    //}

                    //    //bandedGridView2.SetRowCellValue(0, "overfloww_order", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Maximum_weight", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Maximum_volume", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Maximum_PLT", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "By_weight", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "By_volume", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "By_PLT", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "End_time", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Vehicles_working", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());
                    //    //bandedGridView2.SetRowCellValue(0, "Temporary_Vehicle_maximum_radius", gridView2.GetRowCellValue(row, "Deallocation_ratio").ToString());

                    //    //DataRow engineDr = dtSelectedGridEngineOption.NewRow();
                    //    ////default값
                    //    //engineDr["POLICY_ID"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "EngOpt_Code").ToString();
                    //    //engineDr["Zone"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Zone").ToString();
                    //    //engineDr["Distance_Calc"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Distance_Calc").ToString();
                    //    //engineDr["Distance_Weight"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Distance_Weight").ToString();
                    //    //engineDr["Speed"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Speed").ToString();
                    //    //engineDr["Cumulative_rate"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Cumulative_rate").ToString();
                    //    //engineDr["Deallocation_ratio"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Deallocation_ratio").ToString();

                    //    ////상차시간 형식 ( 01:00:00 )
                    //    //phase_difference_time = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "phase_difference_time").ToString();
                    //    //if (!(phase_difference_time == null || phase_difference_time.Equals("") || phase_difference_time.Equals(" ")))
                    //    //{
                    //    //    engineDr["hour"] = phase_difference_time.Substring(0, 2);
                    //    //    engineDr["minute"] = phase_difference_time.Substring(3, 2);
                    //    //    engineDr["second"] = phase_difference_time.Substring(6, 2);
                    //    //}
                    //    //engineDr["overfloww_order"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "overfloww_order").ToString();
                    //    //engineDr["Maximum_weight"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Maximum_weight").ToString();
                    //    //engineDr["Maximum_volume"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Maximum_volume").ToString();
                    //    //engineDr["Maximum_PLT"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Maximum_PLT").ToString();
                    //    //engineDr["By_weight"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "By_weight").ToString();
                    //    //engineDr["By_volume"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "By_volume").ToString();
                    //    //engineDr["By_PLT"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "By_PLT").ToString();
                    //    //engineDr["End_time"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "End_time").ToString();
                    //    //engineDr["Vehicles_working"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Vehicles_working").ToString();
                    //    //engineDr["Temporary_Vehicle_maximum_radius"] = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Temporary_Vehicle_maximum_radius").ToString();

                    //    //dtSelectedGridEngineOption.Rows.Add(engineDr);
                    //    //gridSelectedEngineOption.RefreshDataSource();
                    //    #endregion
                    //}

                }
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

        //권역옵션 column 선택
        private void gridZoneOption_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                bizService = GetBizService();

                if (!gridView4.IsSizingState && gridView4.FocusedRowHandle >= 0)//컬럼사이즈 수정은 row 값이 없음.
                {
                    //int row = gridView4.FocusedRowHandle;

                    //dtSelectedGridEngineOption.Rows.Clear();

                    //string query_type = "";
                    //string key_id = "VRO001_DST072_DSTC04";
                    //Priority_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority01", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority02", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority03", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority04", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority05", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority06", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority07", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority08", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority09", Priority_ComboboxOraDt);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority10", Priority_ComboboxOraDt);

                    //key_id = "VRO001_DST072_DSTC14";
                    //zone_Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "zone_Distance_Calc", zone_Distance_Calc_ComboboxOraDt);

                    //// return value : VELCID , 현재 return value 없음
                    //zone_Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
                    //SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "VELCID", "zone_Speed", zone_Speed_ComboboxOraDt);

                    //SetRepositoryItemComboBoxBindDataYN(gridSelectedZoneOption, "zone_Cumulative_rate", "zone_Cumulative_rate", dtGridZoneOption);

                    ////중복클릭 데이터 삭제.
                    //dtSelectedGridZoneOption.Rows.Clear();

                    ////콤보박스 첫 item이 보여질 내용 디폴트 지정 (Zone옵션, Query로 특정 데이터 보여질 예정)
                    //OracleDataTable zoneOraDt = bizService.GetGridSelectedOptionByPolicy("Z", gridView4.GetRowCellValue(row, "zone_policyID").ToString());

                    //foreach (DataRow _row1 in zoneOraDt.Rows)
                    //{
                    //    //DataRow 추가
                    //    DataRow zoneDr = dtSelectedGridZoneOption.NewRow();

                    //    zoneDr["Zone_Code"] = _row1["DSTTID"];
                    //    zoneDr["Zone_Name"] = _row1["DSTTYP"];
                    //    zoneDr["Zone_Division"] = _row1["DSTTNM"];

                    //    foreach (DataRow dr in Priority_ComboboxOraDt.Rows)
                    //    {
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP01"].ToString()))
                    //        {
                    //            zoneDr["Priority01"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP02"].ToString()))
                    //        {
                    //            zoneDr["Priority02"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP03"].ToString()))
                    //        {
                    //            zoneDr["Priority03"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP04"].ToString()))
                    //        {
                    //            zoneDr["Priority04"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP05"].ToString()))
                    //        {
                    //            zoneDr["Priority05"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP06"].ToString()))
                    //        {
                    //            zoneDr["Priority06"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP07"].ToString()))
                    //        {
                    //            zoneDr["Priority07"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP08"].ToString()))
                    //        {
                    //            zoneDr["Priority08"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP09"].ToString()))
                    //        {
                    //            zoneDr["Priority09"] = dr["NAMES"];
                    //        }
                    //        if (dr["VALES"].ToString().Equals(_row1["ORDP10"].ToString()))
                    //        {
                    //            zoneDr["Priority10"] = dr["NAMES"];
                    //        }
                    //    }

                    //    foreach (DataRow dr in zone_Distance_Calc_ComboboxOraDt.Rows)
                    //    {
                    //        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                    //        zoneDr["zone_Distance_Calc"] = "default";

                    //        if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                    //        {
                    //            zoneDr["zone_Distance_Calc"] = dr["NAMES"];
                    //            break;
                    //        }
                    //    }

                    //    zoneDr["zone_Distance_Weight"] = _row1["DSTWGT"];
                    //    zoneDr["zone_Speed"] = _row1["VELCID"];
                    //    zoneDr["zone_Cumulative_rate"] = _row1["FCMVLC"];
                    //    zoneDr["zone_Deallocation_ratio"] = _row1["DACRAT"];
                    //    zoneDr["zone_Move_time_limit"] = _row1["TMSTOP"];
                    //    zoneDr["zone_Temporary_Vehicle_maximum_radius"] = _row1["DSTVHC"];

                    //    //Data 추가
                    //    dtSelectedGridZoneOption.Rows.Add(zoneDr);
                    //}

                    ////DataSource 지정
                    ////gridSelectedZoneOption.DataSource = dtSelectedGridZoneOption;

                    ////DataRefresh
                    //gridSelectedZoneOption.RefreshDataSource();



                    //#region 이전코드(12.3)
                    ////bizService = getBizService();
                    ////int row = gridView4.FocusedRowHandle;
                    ////dtSelectedGridZoneOption.Rows.Clear();

                    ////OracleDataTable zoneDt = bizService.GetGridSelectedOptionByPolicy("Z", gridView4.GetRowCellValue(row, "zone_policyID").ToString());

                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP01", "Priority01", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP02", "Priority02", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP03", "Priority03", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP04", "Priority04", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP05", "Priority05", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP06", "Priority06", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP07", "Priority07", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP08", "Priority08", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP09", "Priority09", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "ORDP10", "Priority10", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "DSTCTY", "zone_Distance_Calc", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "VELCID", "zone_Speed", zoneDt);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "FCMVLC", "zone_Cumulative_rate", zoneDt);

                    ////foreach (DataRow _row in zoneDt.Rows)
                    ////{
                    ////    DataRow dr = dtSelectedGridZoneOption.NewRow();

                    ////    dr["Zone_Code"] = _row["DSTTID"];
                    ////    dr["Zone_Name"] = _row["DSTTYP"];
                    ////    dr["Zone_Division"] = _row["DSTTNM"];
                    ////    dr["Priority01"] = _row["ORDP01"];
                    ////    dr["Priority02"] = _row["ORDP02"];
                    ////    dr["Priority03"] = _row["ORDP03"];
                    ////    dr["Priority04"] = _row["ORDP04"];
                    ////    dr["Priority05"] = _row["ORDP05"];
                    ////    dr["Priority06"] = _row["ORDP06"];
                    ////    dr["Priority07"] = _row["ORDP07"];
                    ////    dr["Priority08"] = _row["ORDP08"];
                    ////    dr["Priority09"] = _row["ORDP09"];
                    ////    dr["Priority10"] = _row["ORDP10"];
                    ////    dr["zone_Distance_Calc"] = _row["DSTCTY"];
                    ////    dr["zone_Distance_Weight"] = _row["DSTWGT"];
                    ////    dr["zone_Speed"] = _row["VELCID"];
                    ////    dr["zone_Cumulative_rate"] = _row["FCMVLC"];
                    ////    dr["zone_Deallocation_ratio"] = _row["DACRAT"];
                    ////    dr["zone_Move_time_limit"] = _row["TMSTOP"];
                    ////    dr["zone_Temporary_Vehicle_maximum_radius"] = _row["DSTVHC"];

                    ////    dtSelectedGridZoneOption.Rows.Add(dr);
                    ////}

                    ////gridSelectedZoneOption.RefreshDataSource();
                    //#endregion

                    //#region (권역 옵션 클릭시 해당 로우 값 selectedView에 값 세팅 시 (2013.11.26 이전 코드) 리스트 구조가 아니었을때
                    ////GridView view_gridZoneOption = (GridView)gridZoneOption.Views[0];
                    ////GridView view_gridSelectedZoneOption = (GridView)gridSelectedZoneOption.Views[0];

                    ////if (view_gridZoneOption.SelectedRowsCount > 0)
                    ////{
                    ////    int row = view_gridZoneOption.GetSelectedRows()[0];

                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority01", view_gridZoneOption.GetRowCellValue(row, "Priority01").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority02", view_gridZoneOption.GetRowCellValue(row, "Priority02").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority03", view_gridZoneOption.GetRowCellValue(row, "Priority03").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority04", view_gridZoneOption.GetRowCellValue(row, "Priority04").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority05", view_gridZoneOption.GetRowCellValue(row, "Priority05").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority06", view_gridZoneOption.GetRowCellValue(row, "Priority06").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority07", view_gridZoneOption.GetRowCellValue(row, "Priority07").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority08", view_gridZoneOption.GetRowCellValue(row, "Priority08").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority09", view_gridZoneOption.GetRowCellValue(row, "Priority09").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "Priority10", view_gridZoneOption.GetRowCellValue(row, "Priority10").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "zone_Distance_Calc", view_gridZoneOption.GetRowCellValue(row, "zone_Distance_Calc").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "zone_Distance_Weight", view_gridZoneOption.GetRowCellValue(row, "zone_Distance_Weight").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "zone_Speed", view_gridZoneOption.GetRowCellValue(row, "zone_Speed").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "zone_Cumulative_rate", view_gridZoneOption.GetRowCellValue(row, "zone_Cumulative_rate").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "zone_Deallocation_ratio", view_gridZoneOption.GetRowCellValue(row, "zone_Deallocation_ratio").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "zone_Move_time_limit", view_gridZoneOption.GetRowCellValue(row, "zone_Move_time_limit").ToString());
                    ////    view_gridSelectedZoneOption.SetRowCellValue(0, "zone_Temporary_Vehicle_maximum_radius", view_gridZoneOption.GetRowCellValue(row, "zone_Temporary_Vehicle_maximum_radius").ToString());

                    ////    gridSelectedZoneOption.RefreshDataSource();
                    ////}
                    //#endregion

                }
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

        //차량옵션 column 선택
        private void gridCarOption_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!gridView3.IsSizingState&& gridView3.FocusedRowHandle >= 0)//컬럼사이즈 수정은 row 값이 없음.
                //{
                //    setStartProgressPanel();

                //    bizService = getBizService();
                //    //int row = gridView3.GetSelectedRows()[0];
                //    int row = gridView3.FocusedRowHandle;

                //    dtSelectedGridCarOption.Rows.Clear();

                //    //Parameter ( control, 가져올 caroption 컬럼이름, 현재 보여질 컬럼이름, 데이터 테이블 )
                //    SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_arrival_within_wokingTime", "caroption_arrival", dtGridCarOption);
                //    SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_Whether_working", "caroption_working", dtGridCarOption);

                //    string query_type = "";
                //    string key_id = "VRO001_DST073_DSTC16";
                //    caroption_region_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_region_reorder", caroption_region_reorder_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC17";
                //    caroption_area_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign", caroption_area_assign_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC18";
                //    caroption_area_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign_reorder", caroption_area_assign_reorder_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC19";
                //    caroption_remainder_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign", caroption_remainder_assign_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC20";
                //    caroption_remainder_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign_reorder", caroption_remainder_assign_reorder_ComboboxOraDt);

                //    //DataRow 추가
                //    DataRow carDr = dtSelectedGridCarOption.NewRow();

                //    //중복클릭 데이터 삭제.
                //    dtSelectedGridCarOption.Rows.Clear();

                //    //콤보박스 첫 item이 보여질 내용 디폴트 지정 (Zone옵션, Query로 특정 데이터 보여질 예정)
                //    OracleDataTable carOraDt = bizService.GetGridSelectedOptionByPolicy("C", gridView3.GetRowCellValue(row, "car_policyID").ToString());

                //    foreach (DataRow _row2 in carOraDt.Rows)
                //    {
                //        //DataRow 추가
                //        DataRow _carDr = dtSelectedGridCarOption.NewRow();

                //        _carDr["caroption_car_number"] = _row2["VEHCID"];
                //        _carDr["caroption_car_type"] = _row2["VHCTON"];
                //        _carDr["caroption_max_landing_num"] = _row2["LMSPNO"];
                //        _carDr["caroption_arrival"] = _row2["FINTWA"];
                //        _carDr["caroption_working"] = _row2["FONDUT"];
                //        _carDr["caroption_region"] = _row2["DIST01"];
                //        _carDr["caroption_mix_region"] = _row2["LMMDS1"];

                //        foreach (DataRow dr in caroption_region_reorder_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_region_reorder"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["REAL01"].ToString()))
                //            {
                //                _carDr["caroption_region_reorder"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        _carDr["caroption_area"] = _row2["DIST02"];
                //        _carDr["caroption_mix_area"] = _row2["LMMDS02"];

                //        foreach (DataRow dr in caroption_area_assign_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_area_assign"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["ALLO01"].ToString()))
                //            {
                //                _carDr["caroption_area_assign"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        foreach (DataRow dr in caroption_area_assign_reorder_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_area_assign_reorder"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["REAL02"].ToString()))
                //            {
                //                _carDr["caroption_area_assign_reorder"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        foreach (DataRow dr in caroption_remainder_assign_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_remainder_assign"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["ALLO03"].ToString()))
                //            {
                //                _carDr["caroption_remainder_assign"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        foreach (DataRow dr in caroption_remainder_assign_reorder_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_remainder_assign_reorder"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["REAL03"].ToString()))
                //            {
                //                _carDr["caroption_remainder_assign_reorder"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        _carDr["caroption_1"] = _row2["ATTR11"];
                //        _carDr["caroption_2"] = _row2["ATTR12"];
                //        _carDr["caroption_3"] = _row2["ATTR13"];
                //        _carDr["caroption_4"] = _row2["ATTR14"];
                //        _carDr["caroption_5"] = _row2["ATTR15"];
                //        _carDr["caroption_6"] = _row2["ATTR16"];
                //        _carDr["caroption_7"] = _row2["ATTR17"];
                //        _carDr["caroption_8"] = _row2["ATTR18"];
                //        _carDr["caroption_9"] = _row2["ATTR19"];
                //        _carDr["caroption_10"] = _row2["ATTR20"];

                //        //Data 추가
                //        dtSelectedGridCarOption.Rows.Add(_carDr);
                //    }

                //    //DataSource 지정
                //    gridSelectedCarOption.DataSource = dtSelectedGridCarOption;

                //    //DataRefresh
                //    gridSelectedCarOption.RefreshDataSource();


                //    #region 이전코드 (12.3)
                //    //OracleDataTable carDt = bizService.GetGridSelectedOptionByPolicy("C", gridView3.GetRowCellValue(row, "car_policyID").ToString());

                //    ////Parameter ( control, 가져올 caroption 컬럼이름, 현재 보여질 컬럼이름, 데이터 테이블 )
                //    //SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "car_arrival_within_wokingTime", "caroption_arrival", dtGridCarOption);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "car_Whether_working", "caroption_working", dtGridCarOption);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "car_reorder_Fixed_region", "caroption_region_reorder", dtGridCarOption);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "car_areas_assign_type", "caroption_area_assign", dtGridCarOption);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "car_areas_assign_reorder", "caroption_area_assign_reorder", dtGridCarOption);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "car_remainder_assign_type", "caroption_remainder_assign", dtGridCarOption);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "car_remainder_assign_reorder", "caroption_remainder_assign_reorder", dtGridCarOption);

                //    //foreach (DataRow _row2 in carDt.Rows)
                //    //{
                //    //    DataRow carDr = dtSelectedGridCarOption.NewRow();

                //    //    carDr["caroption_car_number"] = _row2["VEHCID"];
                //    //    carDr["caroption_car_type"] = _row2["VHCTYP"];
                //    //    carDr["caroption_max_landing_num"] = _row2["LMSPNO"];
                //    //    carDr["caroption_arrival"] = _row2["FINTWA"];
                //    //    carDr["caroption_working"] = _row2["FONDUT"];
                //    //    carDr["caroption_region"] = _row2["DIST01"];
                //    //    carDr["caroption_mix_region"] = _row2["LMMDS1"];
                //    //    carDr["caroption_region_reorder"] = _row2["REAL01"];
                //    //    carDr["caroption_area"] = _row2["DIST02"];
                //    //    carDr["caroption_mix_area"] = _row2["LMMDS02"];
                //    //    carDr["caroption_area_assign"] = _row2["ALLO01"];
                //    //    carDr["caroption_area_assign_reorder"] = _row2["REAL02"];
                //    //    carDr["caroption_remainder_assign"] = _row2["ALLO03"];
                //    //    carDr["caroption_remainder_assign_reorder"] = _row2["REAL03"];
                //    //    carDr["caroption_1"] = _row2["ATTR01"];
                //    //    carDr["caroption_2"] = _row2["ATTR02"];
                //    //    carDr["caroption_3"] = _row2["ATTR03"];
                //    //    carDr["caroption_4"] = _row2["ATTR04"];
                //    //    carDr["caroption_5"] = _row2["ATTR05"];
                //    //    carDr["caroption_6"] = _row2["ATTR06"];
                //    //    carDr["caroption_7"] = _row2["ATTR07"];
                //    //    carDr["caroption_8"] = _row2["ATTR08"];
                //    //    carDr["caroption_9"] = _row2["ATTR09"];
                //    //    carDr["caroption_10"] = _row2["ATTR10"];

                //    //    dtSelectedGridCarOption.Rows.Add(carDr);
                //    //}
                //    ////DataRefresh
                //    //gridSelectedCarOption.RefreshDataSource();
                //    #endregion

                //    #region (차량 옵션 클릭시 해당 로우 값 selectedView에 값 세팅 시 (2013.11.26 이전 코드) 리스트 구조가 아니었을때
                //    //GridView view_gridCarOption = (GridView)gridCarOption.Views[0];
                //    //GridView view_gridSelectedCarOption = (GridView)gridSelectedCarOption.Views[0];

                //    //if (view_gridCarOption.SelectedRowsCount > 0)
                //    //{
                //    //    int row = view_gridCarOption.GetSelectedRows()[0];

                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_max_landing_num", view_gridCarOption.GetRowCellValue(row, "car_day_max_landing_num").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_arrival", view_gridCarOption.GetRowCellValue(row, "car_arrival_within_wokingTime").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_working", view_gridCarOption.GetRowCellValue(row, "car_Whether_working").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_region", view_gridCarOption.GetRowCellValue(row, "car_Fixed_region").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_mix_region", view_gridCarOption.GetRowCellValue(row, "car_mix_Fixed_region").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_region_reorder", view_gridCarOption.GetRowCellValue(row, "car_reorder_Fixed_region").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_area", view_gridCarOption.GetRowCellValue(row, "car_areas").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_mix_area", view_gridCarOption.GetRowCellValue(row, "car_mix_areas").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_area_assign", view_gridCarOption.GetRowCellValue(row, "car_areas_assign_type").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_area_assign_reorder", view_gridCarOption.GetRowCellValue(row, "car_areas_assign_reorder").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_remainder_assign", view_gridCarOption.GetRowCellValue(row, "car_remainder_assign_type").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_remainder_assign_reorder", view_gridCarOption.GetRowCellValue(row, "car_remainder_assign_reorder").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_1", view_gridCarOption.GetRowCellValue(row, "car_1").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_2", view_gridCarOption.GetRowCellValue(row, "car_2").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_3", view_gridCarOption.GetRowCellValue(row, "car_3").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_4", view_gridCarOption.GetRowCellValue(row, "car_4").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_5", view_gridCarOption.GetRowCellValue(row, "car_5").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_6", view_gridCarOption.GetRowCellValue(row, "car_6").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_7", view_gridCarOption.GetRowCellValue(row, "car_7").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_8", view_gridCarOption.GetRowCellValue(row, "car_8").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_9", view_gridCarOption.GetRowCellValue(row, "car_9").ToString());
                //    //    view_gridSelectedCarOption.SetRowCellValue(0, "caroption_10", view_gridCarOption.GetRowCellValue(row, "car_10").ToString());

                //    //    gridSelectedCarOption.RefreshDataSource();
                //    //}
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //착지옵션 column 선택
        private void gridLandingOption_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                if (!gridView7.IsSizingState && gridView7.FocusedRowHandle >= 0)//컬럼사이즈 수정은 row 값이 없음.
                {
                    //bizService = getBizService();
                    ////int row = gridView7.GetSelectedRows()[0];
                    //int row = gridView7.FocusedRowHandle;

                    //dtSelectedGridLandingOption.Rows.Clear();

                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime", list_hours);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime1", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime2", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime", list_hours);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime1", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime2", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "landing_OTD_compliance", "OTD_compliance", dtGridLandingOption);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime", list_hours);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime1", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime2", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime", list_hours);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime1", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime2", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime", list_hours);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime1", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime2", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime", list_hours);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime1", list_minute_second);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime2", list_minute_second);

                    //string query_type = "";
                    //string key_id = "VRO001_DST074_DSTC21";
                    //Dispatcher_manualHandling_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "NAMES", "Dispatcher_manualHandling", Dispatcher_manualHandling_ComboboxOraDt);

                    //SetRepositoryItemComboBoxBindDataYN(gridSelectedLandingOption, "landing_Whether_assigned", "Whether_assigned", dtGridLandingOption);

                    ////중복클릭 데이터 삭제.
                    //dtSelectedGridLandingOption.Rows.Clear();

                    ////콤보박스 첫 item이 보여질 내용 디폴트 지정 (엔진옵션, Query로 특정 데이터 보여질 예정)
                    //OracleDataTable landingOraDt = bizService.GetGridSelectedOptionByPolicy("A", gridView7.GetRowCellValue(row, "landing_policyID").ToString());

                    //foreach (DataRow _row3 in landingOraDt.Rows)
                    //{
                    //    //DataRow 추가
                    //    DataRow landingDr = dtSelectedGridLandingOption.NewRow();

                    //    landingDr["landingoption_number"] = _row3["STOPID"];
                    //    landingDr["landingoption_name"] = _row3["STOPNM"];
                    //    landingDr["landingoption_restrict"] = _row3["BVEHID"];
                    //    //TKXADS18_THR 조인정보, 현재 쿼리에서 컬럼 없음. 프로시져 완료 후 가능
                    //    //landingDr["landingoption_box_number"] = _row3["BOXCNT"];
                    //    //landingDr["landingoption_box_rating"] = _row3["BOXLEV"];
                    //    //landingDr["landingoption_weight"] = _row3["WEIGHT"];
                    //    //landingDr["landingoption_volumn"] = _row3["VOLUME"];
                    //    //landingDr["landingoption_PLT"] = _row3["PLTCNT"];

                    //    //DB 데이터 시간 형식 ( 01:00:00 )
                    //    string time = _row3["TIMEFR"].ToString();
                    //    if (!(time == null || time.Equals("") || time.Equals(" ")))//업데이트 할때 null 인 데이터는 " " 공백 넣어서 처리, 데이터 받을때도 공백으로 받음...디비 프로시져에서 처리시 공백 요청
                    //    {
                    //        landingDr["Adhesive_required_startTime"] = time.Substring(0, 2);
                    //        landingDr["Adhesive_required_startTime1"] = time.Substring(3, 2);
                    //        landingDr["Adhesive_required_startTime2"] = time.Substring(6, 2);
                    //    }

                    //    string time1 = _row3["TIMETO"].ToString();
                    //    if (!(time1 == null || time1.Equals("") || time1.Equals(" ")))
                    //    {
                    //        landingDr["Adhesive_required_endTime"] = time1.Substring(0, 2);
                    //        landingDr["Adhesive_required_endTime1"] = time1.Substring(3, 2);
                    //        landingDr["Adhesive_required_endTime2"] = time1.Substring(6, 2);
                    //    }

                    //    landingDr["OTD_compliance"] = _row3["OTDOPT"];

                    //    string time2 = _row3["OFFTFR"].ToString();
                    //    if (!(time2 == null || time2.Equals("") || time2.Equals(" ")))
                    //    {
                    //        landingDr["OffTime_startTime"] = time2.Substring(0, 2);
                    //        landingDr["OffTime_startTime1"] = time2.Substring(3, 2);
                    //        landingDr["OffTime_startTime2"] = time2.Substring(6, 2);
                    //    }

                    //    string time3 = _row3["OFFTTO"].ToString();
                    //    if (!(time3 == null || time3.Equals("") || time3.Equals(" ")))
                    //    {
                    //        landingDr["OffTime_endTime"] = time3.Substring(0, 2);
                    //        landingDr["OffTime_endTime1"] = time3.Substring(3, 2);
                    //        landingDr["OffTime_endTime2"] = time3.Substring(6, 2);
                    //    }

                    //    string time4 = _row3["PREPTM"].ToString();
                    //    if (!(time4 == null || time4.Equals("") || time4.Equals(" ")))
                    //    {
                    //        landingDr["Fixed_handlingTime"] = time4.Substring(0, 2);
                    //        landingDr["Fixed_handlingTime1"] = time4.Substring(3, 2);
                    //        landingDr["Fixed_handlingTime2"] = time4.Substring(6, 2);
                    //    }

                    //    string time5 = _row3["UNLDTM"].ToString();
                    //    if (!(time5 == null || time5.Equals("") || time5.Equals(" ")))
                    //    {
                    //        landingDr["change_handlingTime"] = time5.Substring(0, 2);
                    //        landingDr["change_handlingTime1"] = time5.Substring(3, 2);
                    //        landingDr["change_handlingTime2"] = time5.Substring(6, 2);
                    //    }

                    //    //해당 컬럼 없음. 컬럼 다시 확인.
                    //    //landingDr["Recent_delivery_vehicleNumber"] = _row3["HVEHID"];
                    //    //landingDr["Latest_arrivalTime"] = _row3["HARRTM"];

                    //    //foreach (DataRow dr in Dispatcher_manualHandling_ComboboxOraDt.Rows)
                    //    //{
                    //    //    //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                    //    //    landingDr["Dispatcher_manualHandling"] = "default";

                    //    //    if (dr["VALES"].ToString().Equals(_row3["MALTYP"].ToString()))
                    //    //    {
                    //    //        landingDr["Dispatcher_manualHandling"] = dr["NAMES"];
                    //    //        break;
                    //    //    }
                    //    //}

                    //    //landingDr["Whether_assigned"] = _row3["FALLOC"];

                    //    //Data 추가
                    //    dtSelectedGridLandingOption.Rows.Add(landingDr);
                    //}
                    ////DataSource 지정
                    //gridSelectedLandingOption.DataSource = dtSelectedGridLandingOption;

                    ////DataRefresh
                    //gridSelectedLandingOption.RefreshDataSource();

                    //#region 이전코드(12.3)
                    ////OracleDataTable landingDt = bizService.GetGridSelectedOptionByPolicy("A", gridView7.GetRowCellValue(row, "landing_policyID").ToString());

                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime", list_hours);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime1", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime2", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime", list_hours);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime1", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime2", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "landing_OTD_compliance", "OTD_compliance", dtGridLandingOption);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime", list_hours);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime1", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime2", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime", list_hours);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime1", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime2", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime", list_hours);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime1", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime2", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime", list_hours);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime1", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime2", list_minute_second);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "landing_Dispatcher_manualHandling", "Dispatcher_manualHandling", dtGridLandingOption);
                    ////SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "landing_Whether_assigned", "Whether_assigned", dtGridLandingOption);

                    ////foreach (DataRow _row3 in landingDt.Rows)
                    ////{
                    ////    DataRow landingDr = dtSelectedGridLandingOption.NewRow();

                    ////    //landingDr["landing_policyID"] = _row3["POLYID"];
                    ////    //landingDr["landing_policy_name"] = _row3["DESC01"];

                    ////    landingDr["landingoption_number"] = _row3["STOPID"];
                    ////    landingDr["landingoption_name"] = _row3["STOPNM"];
                    ////    landingDr["landingoption_restrict"] = _row3["BVEHID"];
                    ////    //landingDr["landingoption_box_number"] = _row3[""];
                    ////    //landingDr["landingoption_box_rating"] = _row3[""];
                    ////    //landingDr["landingoption_weight"] = _row3[""];
                    ////    //landingDr["landingoption_volumn"] = _row3[""];
                    ////    //landingDr["landingoption_PLT"] = _row3[""];

                    ////    //DB 데이터 시간 형식 ( 01:00:00 )
                    ////    string time = _row3["TIMEFR"].ToString();
                    ////    if (!(time == null || time.Equals("") || time.Equals(" ")))
                    ////    {
                    ////        landingDr["Adhesive_required_startTime"] = time.Substring(0, 2);
                    ////        landingDr["Adhesive_required_startTime1"] = time.Substring(3, 2);
                    ////        landingDr["Adhesive_required_startTime2"] = time.Substring(6, 2);
                    ////    }

                    ////    string time1 = _row3["TIMETO"].ToString();
                    ////    if (!(time1 == null || time1.Equals("") || time1.Equals(" ")))
                    ////    {
                    ////        landingDr["Adhesive_required_endTime"] = time1.Substring(0, 2);
                    ////        landingDr["Adhesive_required_endTime1"] = time1.Substring(3, 2);
                    ////        landingDr["Adhesive_required_endTime2"] = time1.Substring(6, 2);
                    ////    }

                    ////    landingDr["OTD_compliance"] = _row3["OTDOPT"];

                    ////    string time2 = _row3["OFFTFR"].ToString();
                    ////    if (!(time2 == null || time2.Equals("") || time2.Equals(" ")))
                    ////    {
                    ////        landingDr["OffTime_startTime"] = time2.Substring(0, 2);
                    ////        landingDr["OffTime_startTime1"] = time2.Substring(3, 2);
                    ////        landingDr["OffTime_startTime2"] = time2.Substring(6, 2);
                    ////    }

                    ////    string time3 = _row3["OFFTTO"].ToString();
                    ////    if (!(time3 == null || time3.Equals("") || time3.Equals(" ")))
                    ////    {
                    ////        landingDr["OffTime_endTime"] = time3.Substring(0, 2);
                    ////        landingDr["OffTime_endTime1"] = time3.Substring(3, 2);
                    ////        landingDr["OffTime_endTime2"] = time3.Substring(6, 2);
                    ////    }

                    ////    string time4 = _row3["PREPTM"].ToString();
                    ////    if (!(time4 == null || time4.Equals("") || time4.Equals(" ")))
                    ////    {
                    ////        landingDr["Fixed_handlingTime"] = time4.Substring(0, 2);
                    ////        landingDr["Fixed_handlingTime1"] = time4.Substring(3, 2);
                    ////        landingDr["Fixed_handlingTime2"] = time4.Substring(6, 2);
                    ////    }

                    ////    string time5 = _row3["UNLDTM"].ToString();
                    ////    if (!(time5 == null || time5.Equals("") || time5.Equals(" ")))
                    ////    {
                    ////        landingDr["change_handlingTime"] = time5.Substring(0, 2);
                    ////        landingDr["change_handlingTime1"] = time5.Substring(3, 2);
                    ////        landingDr["change_handlingTime2"] = time5.Substring(6, 2);
                    ////    }

                    ////    landingDr["Recent_delivery_vehicleNumber"] = _row3["OTDOPT"];
                    ////    //DB 매핑 정보 모름
                    ////    //landingDr["Latest_arrivalTime"] = _row3[""];
                    ////    //landingDr["Dispatcher_manualHandling"] = _row3[""];
                    ////    //landingDr["Whether_assigned"] = _row3[""];

                    ////    dtSelectedGridLandingOption.Rows.Add(landingDr);
                    ////}

                    //////DataSource 지정
                    ////gridSelectedLandingOption.DataSource = dtSelectedGridLandingOption;

                    //////DataRefresh
                    ////gridSelectedLandingOption.RefreshDataSource();
                    //#endregion

                    //#region (착지 옵션 클릭시 해당 로우 값 selectedView에 값 세팅 시 (2013.11.26 이전 코드) 리스트 구조가 아니었을때
                    ////GridView view_gridLandingOption = (GridView)gridLandingOption.Views[0];
                    ////GridView view_gridSelectedLandingOption = (GridView)gridSelectedLandingOption.Views[0];

                    ////if (view_gridLandingOption.SelectedRowsCount > 0)
                    ////{
                    ////    int row = view_gridLandingOption.GetSelectedRows()[0];

                    ////    string required_startTime = view_gridLandingOption.GetRowCellValue(row, "landing_required_startTime").ToString();
                    ////    if (!(required_startTime == null || required_startTime.Equals("")))
                    ////    {
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Adhesive_required_startTime", required_startTime.Substring(0, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Adhesive_required_startTime1", required_startTime.Substring(3, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Adhesive_required_startTime2", required_startTime.Substring(6, 2));
                    ////    }

                    ////    string required_endTime = view_gridLandingOption.GetRowCellValue(row, "landing_required_endTime").ToString();
                    ////    if (!(required_endTime == null || required_endTime.Equals("")))
                    ////    {
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Adhesive_required_endTime", required_endTime.Substring(0, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Adhesive_required_endTime1", required_endTime.Substring(3, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Adhesive_required_endTime2", required_endTime.Substring(6, 2));
                    ////    }


                    ////    view_gridSelectedLandingOption.SetRowCellValue(0, "OTD_compliance", view_gridLandingOption.GetRowCellValue(row, "landing_OTD_compliance").ToString());

                    ////    string offTime_startTime = view_gridLandingOption.GetRowCellValue(row, "landing_offTime_startTime").ToString();
                    ////    if (!(offTime_startTime == null || offTime_startTime.Equals("")))
                    ////    {
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "OffTime_startTime", offTime_startTime.Substring(0, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "OffTime_startTime1", offTime_startTime.Substring(3, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "OffTime_startTime2", offTime_startTime.Substring(6, 2));
                    ////    }

                    ////    string offTime_endTime = view_gridLandingOption.GetRowCellValue(row, "landing_offTime_endTime").ToString();
                    ////    if (!(offTime_endTime == null || offTime_endTime.Equals("")))
                    ////    {
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "OffTime_endTime", offTime_endTime.Substring(0, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "OffTime_endTime1", offTime_endTime.Substring(3, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "OffTime_endTime2", offTime_endTime.Substring(6, 2));
                    ////    }

                    ////    string Fixed_handlingTime = view_gridLandingOption.GetRowCellValue(row, "landing_Fixed_handlingTime").ToString();
                    ////    if (!(Fixed_handlingTime == null || Fixed_handlingTime.Equals("")))
                    ////    {
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Fixed_handlingTime", Fixed_handlingTime.Substring(0, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Fixed_handlingTime1", Fixed_handlingTime.Substring(3, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "Fixed_handlingTime2", Fixed_handlingTime.Substring(6, 2));
                    ////    }

                    ////    string change_handlingTime = view_gridLandingOption.GetRowCellValue(row, "landing_change_handlingTime").ToString();
                    ////    if (!(change_handlingTime == null || change_handlingTime.Equals("")))
                    ////    {
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "change_handlingTime", change_handlingTime.Substring(0, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "change_handlingTime1", change_handlingTime.Substring(3, 2));
                    ////        view_gridSelectedLandingOption.SetRowCellValue(0, "change_handlingTime2", change_handlingTime.Substring(6, 2));
                    ////    }


                    ////    view_gridSelectedLandingOption.SetRowCellValue(0, "Recent_delivery_vehicleNumber", view_gridLandingOption.GetRowCellValue(row, "landing_Recent_delivery_vehicleNumber").ToString());
                    ////    view_gridSelectedLandingOption.SetRowCellValue(0, "Latest_arrivalTime", view_gridLandingOption.GetRowCellValue(row, "landing_Latest_arrivalTime").ToString());
                    ////    view_gridSelectedLandingOption.SetRowCellValue(0, "Dispatcher_manualHandling", view_gridLandingOption.GetRowCellValue(row, "landing_Dispatcher_manualHandling").ToString());
                    ////    view_gridSelectedLandingOption.SetRowCellValue(0, "Whether_assigned", view_gridLandingOption.GetRowCellValue(row, "landing_Whether_assigned").ToString());

                    ////    gridSelectedLandingOption.RefreshDataSource();
                    ////}

                    //#endregion
                }
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

        ///<summary>엔진옵션 그리드에 데이터 뿌리기 (+콤보박스 세팅)</summary>
        private void SetGridSelectedEngineOption(OracleDataTable engineOraDt)
        {
            try
            {
                dtSelectedGridEngineOption.Rows.Clear();

                GridView view_gridSelectedEngineOption = (GridView)gridSelectedEngineOption.Views[0];
                
                #region
                PrintDebug(engineOraDt);
                #endregion

                EngineCombobox();

                foreach (DataRow _row1 in engineOraDt.Rows)
                {
                    DataRow engineDr = dtSelectedGridEngineOption.NewRow();

                    foreach (DataRow dr in Zone_ComboboxOraDt.Rows)
                    {
                        engineDr["Zone"] = "default";

                        if (dr["VALES"].ToString().Equals(_row1["DISTRICT_SORT"].ToString()))
                        {
                            engineDr["Zone"] = dr["NAMES"];
                            break;
                        }
                    }

                    foreach (DataRow dr in Distance_Calc_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        engineDr["Distance_Calc"] = "default";

                        if (dr["VALES"].ToString().Equals(_row1["DISTANCE_BASE"].ToString()))
                        {
                            engineDr["Distance_Calc"] = dr["NAMES"];
                            break;
                        }
                    }

                    engineDr["Distance_Weight"] = _row1["WEIGHT_DISTANCE"];

                    //이동속도 관련 콤보박스 초기값 설정 작업해야함.
                    //engineDr["Speed"] = _row1["VELCID"];

                    engineDr["Cumulative_rate"] = _row1["ACCUM_SPEED_YN"];
                    engineDr["Deallocation_ratio"] = _row1["DISALLOC_RATE"];

                    //상차시간 형식 ( 01:00:00 )
                    engineDr["hour"] = _row1["LOAD_TIME"];
                    //string phase_difference_time = _row1["LOADTM"].ToString();
                    //if (!(phase_difference_time == null && phase_difference_time.Equals("") && phase_difference_time.Equals(" ")))
                    //{
                    //    engineDr["hour"] = phase_difference_time.Substring(0, 2);
                    //    engineDr["minute"] = phase_difference_time.Substring(3, 2);
                    //    engineDr["second"] = phase_difference_time.Substring(6, 2);
                    //}

                    engineDr["overfloww_order"] = _row1["OVER_CAPA_YN"];//용량초과 주문 할당
                    engineDr["Maximum_weight"] = _row1["BUFFER_WEIGHT"];//최대 중량 가중치
                    engineDr["Maximum_volume"] = _row1["BUFFER_VOLUME"];//최대 부피 가중치
                    engineDr["Maximum_PLT"] = _row1["BUFFER_PALLET"];//최대 팔레트 가중치
                    engineDr["By_weight"] = _row1["APPLY_WEIGHT_YN"]; //중량 적용
                    engineDr["By_volume"] = _row1["APPLY_VOLUME_YN"]; //부피 적용
                    engineDr["By_PLT"] = _row1["APPLY_PALLET_YN"]; //팔레트 적용
                    //착지 TW 종료 시각 적용
                    engineDr["End_time"] = _row1["APPLY_STOP_ENDTIME_YN"];
                    //차량 TW 종료 시각 적용
                    engineDr["Vehicles_working"] = _row1["APPLY_VEHICLE_ENDTIME_YN"];
                    //임시차량 할당 시 첫 거래처 기준 최대 거리
                    engineDr["Temporary_Vehicle_maximum_radius"] = _row1["FIRST_STOP_DISTANCE_LIMIT"];

                    dtSelectedGridEngineOption.Rows.Add(engineDr);
                }

                //DataSource 지정
                gridSelectedEngineOption.DataSource = dtSelectedGridEngineOption;
                gridSelectedEngineOption.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ///<summary>권역정보 그리드에 데이터 뿌리기 (+콤보박스 세팅)</summary>
        private void SetGridSelectedDistrictOption(OracleDataTable zoneOraDt)
        {
            try
            {
                dtSelectedGridZoneOption.Rows.Clear();

                GridView view_gridSelectedZoneOption = (GridView)gridSelectedZoneOption.Views[0];

                #region Print dt
                PrintDebug(zoneOraDt);
                #endregion

                ZoneCombobox();

                //cell에 보여질 값 설정
                foreach (DataRow _row1 in zoneOraDt.Rows)
                {
                    DataRow zoneDr = dtSelectedGridZoneOption.NewRow();

                    SetRepositoryItemComboBoxBindDataYN(gridSelectedZoneOption, "zone_Cumulative_rate", "zone_Cumulative_rate", dtGridZoneOption);

                    zoneDr["Zone_Code"] = _row1["DISTRICT_ID"];
                    zoneDr["Zone_Name"] = _row1["DISTRICT_ID"];
                    zoneDr["Zone_Division"] = _row1["DISTRICT_LEVEL"].ToString() == "0" ? "고정" : "대";

                    foreach (DataRow dr in Priority_ComboboxOraDt.Rows)
                    {
                        if (dr["VALES"].ToString().Equals(_row1["ORDER_PRIORITY_01"].ToString()))
                        {
                            zoneDr["Priority01"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDER_PRIORITY_02"].ToString()))
                        {
                            zoneDr["Priority02"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDER_PRIORITY_03"].ToString()))
                        {
                            zoneDr["Priority03"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDER_PRIORITY_04"].ToString()))
                        {
                            zoneDr["Priority04"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDER_PRIORITY_05"].ToString()))
                        {
                            zoneDr["Priority05"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["VEHICLE_PRIORITY_01"].ToString()))
                        {
                            zoneDr["Priority06"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["VEHICLE_PRIORITY_02"].ToString()))
                        {
                            zoneDr["Priority07"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["VEHICLE_PRIORITY_03"].ToString()))
                        {
                            zoneDr["Priority08"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["VEHICLE_PRIORITY_04"].ToString()))
                        {
                            zoneDr["Priority09"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["VEHICLE_PRIORITY_05"].ToString()))
                        {
                            zoneDr["Priority10"] = dr["NAMES"];
                        }
                    }

                    foreach (DataRow dr in zone_Distance_Calc_ComboboxOraDt.Rows)
                    {
                        zoneDr["zone_Distance_Calc"] = "default";

                        if (dr["VALES"].ToString().Equals(_row1["DISTANCE_CALC_TYPE"].ToString()))
                        {
                            zoneDr["zone_Distance_Calc"] = dr["NAMES"];
                            break;
                        }
                    }

                    zoneDr["zone_Distance_Weight"] = double.Parse(_row1["WEIGHT_DISTANCE"].ToString()) * 100.0;
                    //zoneDr["zone_Speed"] = _row1["VELCID"];
                    zoneDr["zone_Cumulative_rate"] = _row1["ACCUM_SPEED_YN"];
                    zoneDr["zone_Deallocation_ratio"] = _row1["DISALLOC_RATE"];
                    zoneDr["zone_Move_time_limit"] = _row1["TIME_LIMIT"];
                    zoneDr["zone_Temporary_Vehicle_maximum_radius"] = _row1["FIRST_STOP_DISTANCE_LIMIT"];

                    dtSelectedGridZoneOption.Rows.Add(zoneDr);
                }

                //DataSource 지정
                gridSelectedZoneOption.DataSource = dtSelectedGridZoneOption;
                gridSelectedZoneOption.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ///<summary>차량정보 그리드에 데이터 뿌리기 (+콤보박스 세팅)</summary>
        private void SetGridSelectedVehicleOption(OracleDataTable carOraDt)
        {
            try
            {
                dtSelectedGridCarOption.Rows.Clear();

                GridView view_gridSelectedZoneOption = (GridView)gridSelectedZoneOption.Views[0];

                #region Print dt
                PrintDebug(carOraDt);
                #endregion

                CarCombobox();

                //cell에 보여질 값 설정
                foreach (DataRow _row2 in carOraDt.Rows)
                {
                    //DataRow 추가
                    DataRow _carDr = dtSelectedGridCarOption.NewRow();

                    _carDr["caroption_car_number"] = _row2["VEHICLE_ID"];
                    _carDr["caroption_car_type"] = _row2["VEHICLE_TON"];
                    _carDr["caroption_max_landing_num"] = _row2["TOTAL_STOP_LIMIT"];
                    _carDr["caroption_arrival"] = _row2["RETURN_TW_YN"];
                    _carDr["caroption_region"] = _row2["CHARGE_DISTRICT_SML"];
                    _carDr["caroption_mix_region"] = _row2["DISTRICT_MIX_LIMIT_SML"];

                    _carDr["caroption_area"] = _row2["CHARGE_DISTRICT_BIG"];
                    _carDr["caroption_mix_area"] = _row2["DISTRICT_MIX_LIMIT_BIG"];

                    foreach (DataRow dr in caroption_area_assign_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        _carDr["caroption_area_assign"] = "default";

                        if (dr["VALES"].ToString().Equals(_row2["MIX_TYPE_BIG"].ToString()))
                        {
                            _carDr["caroption_area_assign"] = dr["NAMES"];
                            break;
                        }
                    }

                    foreach (DataRow dr in caroption_remainder_assign_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        _carDr["caroption_remainder_assign"] = "default";

                        if (dr["VALES"].ToString().Equals(_row2["DISTRICT_MIX_LIMIT_BIG"].ToString()))
                        {
                            _carDr["caroption_remainder_assign"] = dr["NAMES"];
                            break;
                        }
                    }

                    _carDr["caroption_1"] = _row2["STOP_LIMIT_01"];
                    _carDr["caroption_2"] = _row2["STOP_LIMIT_02"];
                    _carDr["caroption_3"] = _row2["STOP_LIMIT_03"];
                    _carDr["caroption_4"] = _row2["STOP_LIMIT_04"];
                    _carDr["caroption_5"] = _row2["STOP_LIMIT_05"];
                    _carDr["caroption_6"] = _row2["STOP_LIMIT_06"];
                    _carDr["caroption_7"] = _row2["STOP_LIMIT_07"];
                    _carDr["caroption_8"] = _row2["STOP_LIMIT_08"];
                    _carDr["caroption_9"] = _row2["STOP_LIMIT_09"];
                    _carDr["caroption_10"] = _row2["STOP_LIMIT_10"];
                    _carDr["caroption_working"] = _row2["WORK_YN"];

                    //Data 추가
                    dtSelectedGridCarOption.Rows.Add(_carDr);
                }
                //DataSource 지정
                gridSelectedCarOption.DataSource = dtSelectedGridCarOption;
                gridSelectedCarOption.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ///<summary>착지/주문 그리드에 데이터 뿌리기 (+콤보박스 세팅)</summary>
        private void SetGridSelectedStopOption(OracleDataTable landingOraDt)
        {
            try
            {
                GridView view_gridSelectedLandingOption = (GridView)gridSelectedLandingOption.Views[0];

                dtSelectedGridLandingOption.Rows.Clear();

                #region Print dt
                PrintDebug(landingOraDt);
                #endregion

                StopCombobox();

                //cell에 보여질 값 설정
                foreach (DataRow _row3 in landingOraDt.Rows)
                {
                    //DataRow 추가
                    DataRow landingDr = dtSelectedGridLandingOption.NewRow();

                    int nMaxCol = landingOraDt.Columns.Count;
                    string[] temp = new string[nMaxCol];
                    for (int pos = 0; pos < landingOraDt.Columns.Count; pos++)
                        temp[pos] = _row3[pos].ToString();

                    landingDr["landingoption_number"] = _row3["STOP_ID"];
                    //landingDr["landingoption_name"] = _row3["STOPNM"];

                    landingDr["landingoption_restrict"] = _row3["VEHICLE_TON"];
                    landingDr["landingoption_box_number"] = _row3["BOXCNT"];
                    landingDr["landingoption_box_rating"] = _row3["BOX_LEVEL"];
                    landingDr["landingoption_weight"] = _row3["WEIGHT"];
                    landingDr["landingoption_volumn"] = _row3["VOLUME"];
                    landingDr["landingoption_PLT"] = _row3["PALLET"];

                    //DB 데이터 시간 형식 ( 01:00:00 )
                    landingDr["Adhesive_required_startTime"] = _row3["REQ_TIME_FR"];
                    landingDr["Adhesive_required_endTime"] = _row3["REQ_TIME_TO"];
                    landingDr["OTD_compliance"] = _row3["REQ_TIME_YN"];

                    landingDr["OffTime_startTime"] = _row3["OFF_TIME_FR"];
                    landingDr["OffTime_endTime"] = _row3["OFF_TIME_TO"];
                    
                    landingDr["Fixed_handlingTime"] = _row3["UNLOAD_TIME_FIX"];
                    landingDr["change_handlingTime"] = _row3["UNLOAD_TIME_ADD"];
                    //해당 컬럼 없음. 컬럼 다시 확인.
                    landingDr["Recent_delivery_vehicleNumber"] = _row3["VEHICLE_ID"];
                    landingDr["Latest_arrivalTime"] = _row3["ARRIVE_TIME"];
                    landingDr["Dispatcher_manualHandling"] = _row3["MANUAL_ALLOC_LEVEL"]; 
                    landingDr["Whether_assigned"] = _row3["ALLOC_YN"];

                    //Data 추가
                    dtSelectedGridLandingOption.Rows.Add(landingDr);
                }

                //DataSource 지정
                gridSelectedLandingOption.DataSource = dtSelectedGridLandingOption;
                gridSelectedLandingOption.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private BizService GetBizService()
        {
            return (BizService)_applicationContext.GetObject("BizService");
        }

        private void setColumnWidth(GridView gridView, string gridName)
        {
            if (gridName.Equals("gridPolicy"))
            {//gridView1
                #region gridView1 칼럼 너비 설정
                gridView.Columns[0].Width = 10;
                gridView.Columns[1].Width = 120;
                gridView.Columns[2].Width = 120;
                gridView.Columns[3].Width = 300;
                gridView.Columns[4].Width = 200;
                gridView.Columns[5].Width = 110;
                gridView.Columns[6].Width = 200;
                gridView.Columns[7].Width = 110;
                //gridView.Columns[3].Width = (gridView.ViewRect.Width > 0 ? (gridView.ViewRect.Width
                //    - (gridView.Columns[0].Width + gridView.Columns[1].Width
                //    + gridView.Columns[2].Width + gridView.Columns[4].Width
                //    + gridView.Columns[5].Width + gridView.Columns[6].Width
                //    + gridView.Columns[7].Width) ) : 200);
                //    //+ gridView.Columns[7].Width) - 18) : 200);
                #endregion gridView1 칼럼 너비 설정
            }
        }

        //삭제 버튼
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                //DataRow dr = gridView1.GetFocusedDataRow();

                // PLC_DEFAULT 제외하고 삭제, 삭제버튼이면
                if (!(gridView1.GetFocusedRowCellValue("POLICY_ID").ToString().EndsWith("PLC_DEFAULT")) && buttonRemove.Text.Equals("삭제"))
                {
                    textPolicyDescription.Text = "";
                    textPolicyName.Text = "";

                    //Policy table 삭제 여부 플래그 전환 Y -> N 
                    DataRow dr = gridView1.GetFocusedDataRow();

                    gridPolicy.BeginUpdate();

                    bizService = GetBizService();

                    string result = bizService.deletPolicy("D", dr["POLICY_ID"].ToString());

                    //result.substring(0,1) 이 1 이면 OK, 0 이면 error
                    //if (result.Substring(0, 1).ToString().Equals("0"))
                    //{
                    //    string errMsg = result.Substring(1).ToString();
                    //    //Alert 띄우기(공통함수 만듬)
                    //}

                    gridView1.GetFocusedDataRow().Delete();

                    gridPolicy.EndUpdate();

                    gridPolicy.RefreshDataSource();
                }
                else if (buttonRemove.Text.Equals("복원"))
                {
                    delete_recovery();
                }
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

        //기존이름 저장 버튼, 신규이름 저장 버튼
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();
                if (dtSelectedGridEngineOption.Rows.Count != 0 && dtSelectedGridZoneOption.Rows.Count != 0 && dtSelectedGridCarOption.Rows.Count != 0 && dtSelectedGridLandingOption.Rows.Count != 0)
                {
                    if (bandedGridView2.FocusedRowHandle >= 0 && bandedGridView4.FocusedRowHandle >= 0 && bandedGridView1.FocusedRowHandle >= 0 && bandedGridView3.FocusedRowHandle >= 0)
                    {
                        if (checkPolicyHeader() == false)
                            return;

                        if ((MessageBox.Show("현재 시나리오 데이터로 정책을 생성합니다"
                                            , "신규 정책 생성"
                                            , MessageBoxButtons.YesNo
                                            , MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            gridPolicy.BeginUpdate();

                            string result = "";
                            string policyID = getSelectedPolicy(gridView1);
                            string planID = comboScenarioCode.SelectedItem.ToString();

                            //엔진 옵션 Array
                            string[] enginArr = new string[1];
                            enginArr = save_engine_arr();

                            //권역 옵션 Array
                            int zoneOraDt_length = dtSelectedGridZoneOption.Rows.Count;
                            string[] ZoneArr = new string[zoneOraDt_length];
                            ZoneArr = save_zone_arr();

                            //차량 옵션 Array
                            int carOraDt_length = dtSelectedGridCarOption.Rows.Count;
                            string[] carArr = new string[carOraDt_length];
                            carArr = save_car_arr();

                            //착지 옵션 Array
                            int landingOraDt_length = dtSelectedGridLandingOption.Rows.Count;
                            string[] landingArr = new string[landingOraDt_length];
                            landingArr = save_landing_arr();

                            bizService = GetBizService();
                            if (new_policy_save)
                                result = bizService.insertPolicy("C", planID, policyID, textPolicyName.Text, textPolicyDescription.Text, enginArr, ZoneArr, carArr, landingArr);
                            else
                                result = bizService.insertPolicy("U", planID, policyID, textPolicyName.Text, textPolicyDescription.Text, enginArr, ZoneArr, carArr, landingArr);

                            messagebox(result);

                            gridPolicy.EndUpdate();
                            gridPolicy.RefreshDataSource();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("저장할 데이터 없습니다.");
                }
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

        private void buttonSaveScenario_Click(object sender, EventArgs e)
        {
            SplashScreenManager screenManager = new SplashScreenManager();

            if ( dtSelectedGridEngineOption.Rows.Count != 0 && 
                 dtSelectedGridZoneOption.Rows.Count != 0 && 
                 dtSelectedGridCarOption.Rows.Count != 0 && 
                 dtSelectedGridLandingOption.Rows.Count != 0 )
            {
                try
                {
                    screenManager.ShowWaitForm();

                    string planId = comboScenarioCode.SelectedItem.ToString();

                    //bizService = getBizService();

                    string[] enginArr = new string[1];
                    enginArr = save_engine_arr();

                    int zoneOraDt_length = 0;
                    zoneOraDt_length = dtSelectedGridZoneOption.Rows.Count;
                    string[] ZoneArr = new string[zoneOraDt_length];
                    ZoneArr = save_zone_arr();

                    int carOraDt_length = 0;
                    carOraDt_length = dtSelectedGridCarOption.Rows.Count;
                    string[] carArr = new string[carOraDt_length];
                    carArr = save_car_arr();

                    int landingOraDt_length = 0;
                    landingOraDt_length = dtSelectedGridLandingOption.Rows.Count;
                    string[] landingArr = new string[landingOraDt_length];
                    landingArr = save_landing_arr();

                    string result = bizService.updateScenario( planId, enginArr, ZoneArr, carArr, landingArr );

                    //messagebox(result);

                    gridPolicy.RefreshDataSource();
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
            else
            {
                MessageBox.Show("저장할 데이터 없습니다.");

            }
        }

        private bool checkPolicyHeader()
        {
            string strPolicyName = textPolicyName.Text;
            string strPolicyDescription = textPolicyDescription.Text;

            if (strPolicyName.Trim().Equals("") || strPolicyDescription.Trim().Equals(""))
            {
                MessageBox.Show("정책 이름과 설명을 입력해야 합니다", "정책 정보 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //다른이름 저장 버튼 (신규일 때는 disable)
        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            SplashScreenManager screenManager = new SplashScreenManager();

            if (dtSelectedGridEngineOption.Rows.Count != 0 
                && dtSelectedGridZoneOption.Rows.Count != 0 
                && dtSelectedGridCarOption.Rows.Count != 0 
                && dtSelectedGridLandingOption.Rows.Count != 0)
            {
                try
                {
                    screenManager.ShowWaitForm();

                    if (checkPolicyHeader() == false)
                        return;

                    if ((MessageBox.Show("현재 시나리오 데이터로 정책을 생성하시겠습니까?"
                                        , "신규 정책 생성"
                                        , MessageBoxButtons.YesNo
                                        , MessageBoxIcon.Question) == DialogResult.Yes))
                    {

                        if ( getSelectedPolicy(gridView1).ToString()+"" != "" )
                            policy_code = getSelectedPolicy(gridView1);

                        //DataRow dr = gridView1.GetFocusedDataRow();

                        //if (string.IsNullOrEmpty(policy_code))
                        //    policy_code = dr["POLICY_ID"].ToString();

                        string planId = comboScenarioCode.SelectedItem.ToString();

                        bizService = GetBizService();

                        string[] enginArr = new string[1];
                        enginArr = save_engine_arr();

                        int zoneOraDt_length = 0;
                        zoneOraDt_length = dtSelectedGridZoneOption.Rows.Count;
                        string[] ZoneArr = new string[zoneOraDt_length];
                        ZoneArr = save_zone_arr();

                        int carOraDt_length = 0;
                        carOraDt_length = dtSelectedGridCarOption.Rows.Count;
                        string[] carArr = new string[carOraDt_length];
                        carArr = save_car_arr();

                        int landingOraDt_length = 0;
                        landingOraDt_length = dtSelectedGridLandingOption.Rows.Count;
                        string[] landingArr = new string[landingOraDt_length];
                        landingArr = save_landing_arr();

                        //string result = bizService.insertPolicy("R", "", textPolicyName.Text, textPolicyDescription.Text, enginArr, ZoneArr, carArr, landingArr);
                        string result = bizService.insertPolicy("R", planId, policy_code, textPolicyName.Text, textPolicyDescription.Text, enginArr, ZoneArr, carArr, landingArr);

                        //messagebox(result);

                        gridPolicy.RefreshDataSource();
                    }
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
            else
            {
                MessageBox.Show("저장할 데이터 없습니다.");

            }
        }

        //신규 버튼
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();

                //테이블에 보여주지 않고 기존 이름으로 저장시 재조회로 인해 추가된 내용 확인, 다른 이름으로 저장 disable
                #region 테이블 보여지는 부분( disable 요청)

                //DataRow dr = dtPolicy.NewRow();

                //int cnt = (dtPolicy.Rows.Count - 1);
                //DataRow _row = dtPolicy.Rows[cnt];
                //DateTime dtNow = DateTime.Now;

                //string policyCode = (string)_row["POLICY_ID"];
                //policyCode = policyCode.Substring(0, 11) + dtNow.Year + dtNow.Month + dtNow.Day + dtNow.Hour;

                //dr["POLICY_ID"] = policyCode;
                //dr["POLICY_NAME"] = "POLICY_NAME";
                //dr["DESCRIPTION"] = "DESCRIPTION";

                //dtPolicy.Rows.Add(dr);
                //gridPolicy.RefreshDataSource();

                #endregion

                textPolicyDescription.Text = "";
                textPolicyName.Text = "";

                buttonSaveAs.Enabled = false;
                buttonSave.Enabled = false; //true ;//20140325, 데모를 위해 임시 불가처리
                buttonSaveScenario.Enabled = false;

                new_policy_save = true;
                buttonSave.Text = "신규 이름으로";

                dtSelectedGridEngineOption.Rows.Clear();
                dtSelectedGridZoneOption.Rows.Clear();
                dtSelectedGridCarOption.Rows.Clear();
                dtSelectedGridLandingOption.Rows.Clear();

                //저장할 옵션 값들은 디폴트로 세팅 

                #region 저장할 엔진옵션 그리드 (이후 policy code 클릭시 엔진 옵션과 공통함수로...)

                //콤보박스 데이터 바인딩 (엔진 옵션)

                ///- IN Parameter   :   
                ///  p_query_type : 차후 필요용도로 사용(현재는 참조하지않음)
                ///  p_key_id :  공통코드 KEY 값
                string query_type = "";

                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "ENGPID", dtGridEngineOption);

                string key_id = "VRO001_DST071_DSTC01";
                Zone_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Zone", Zone_ComboboxOraDt);

                key_id = "VRO001_DST071_DSTC02";
                Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Distance_Calc", Distance_Calc_ComboboxOraDt);

                // return value : VELCID , 현재 return value 없음
                Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "VELCID", "Speed", Speed_ComboboxOraDt);

                SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Cumulative_rate", "Cumulative_rate", dtGridEngineOption);
                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "Deallocation_ratio", dtGridEngineOption);
                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "hour", list_hours);
                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "minute", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "second", list_minute_second);
                SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "overfloww_order", "overfloww_order", dtGridEngineOption);
                SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_weight", "By_weight", dtGridEngineOption);
                SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_volume", "By_volume", dtGridEngineOption);
                SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_PLT", "By_PLT", dtGridEngineOption);
                SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "End_time", "End_time", dtGridEngineOption);
                SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Vehicles_working", "Vehicles_working", dtGridEngineOption);

                //중복클릭 데이터 삭제.
                dtSelectedGridEngineOption.Rows.Clear();

                bizService = GetBizService();
                OracleDataTable engineOraDt = bizService.GetGridSelectedOptionDefault("E");

                foreach (DataRow _row1 in engineOraDt.Rows)
                {
                    //DataRow 추가
                    DataRow engineDr = dtSelectedGridEngineOption.NewRow();

                    engineDr["ENGPID"] = _row1["ENGPID"];

                    //값이 아닌 NAME을 display
                    foreach (DataRow dr in Zone_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        engineDr["Zone"] = "default";

                        if (dr["VALES"].ToString().Equals(_row1["DSTORD"].ToString()))
                        {
                            engineDr["Zone"] = dr["NAMES"];
                            break;
                        }
                    }

                    foreach (DataRow dr in Distance_Calc_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        engineDr["Distance_Calc"] = "default";

                        if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                        {
                            engineDr["Distance_Calc"] = dr["NAMES"];
                            break;
                        }
                    }

                    engineDr["Distance_Weight"] = _row1["WGTDST"];

                    //이동속도 관련 콤보박스 초기값 설정 작업해야함.
                    engineDr["Speed"] = _row1["VELCID"];

                    engineDr["Cumulative_rate"] = _row1["FCMVLC"];
                    engineDr["Deallocation_ratio"] = _row1["DACRAT"];

                    //상차시간 형식 ( 01:00:00 )
                    engineDr["hour"] = _row1["LOADTM"];
                    //string phase_difference_time = _row1["LOADTM"].ToString();
                    //if (!(phase_difference_time == null && phase_difference_time.Equals("") && phase_difference_time.Equals(" ")))
                    //{
                    //    engineDr["hour"] = phase_difference_time.Substring(0, 2);
                    //    engineDr["minute"] = phase_difference_time.Substring(3, 2);
                    //    engineDr["second"] = phase_difference_time.Substring(6, 2);
                    //}

                    engineDr["overfloww_order"] = _row1["FOCAPA"];
                    engineDr["Maximum_weight"] = _row1["WGTMXW"];
                    engineDr["Maximum_volume"] = _row1["WGTMXV"];
                    engineDr["Maximum_PLT"] = _row1["WGTMXP"];
                    engineDr["By_weight"] = _row1["FWGTSD"];
                    engineDr["By_volume"] = _row1["FVOLSD"];
                    engineDr["By_PLT"] = _row1["FPLTSD"];
                    engineDr["End_time"] = _row1["FTWEND"];
                    engineDr["Vehicles_working"] = _row1["FVHCTW"];
                    engineDr["Temporary_Vehicle_maximum_radius"] = _row1["MRTVHC"];

                    //Data 추가
                    dtSelectedGridEngineOption.Rows.Add(engineDr);
                }

                //DataSource 지정
                gridSelectedEngineOption.DataSource = dtSelectedGridEngineOption;

                //DataRefresh
                gridSelectedEngineOption.RefreshDataSource();

                #endregion

                #region 저장할 권역옵션 그리드

                query_type = "";
                key_id = "VRO001_DST072_DSTC04";
                Priority_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority01", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority02", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority03", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority04", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority05", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority06", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority07", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority08", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority09", Priority_ComboboxOraDt);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority10", Priority_ComboboxOraDt);

                key_id = "VRO001_DST072_DSTC14";
                zone_Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "zone_Distance_Calc", zone_Distance_Calc_ComboboxOraDt);

                // return value : VELCID , 현재 return value 없음
                zone_Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
                SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "VELCID", "zone_Speed", zone_Speed_ComboboxOraDt);

                SetRepositoryItemComboBoxBindDataYN(gridSelectedZoneOption, "zone_Cumulative_rate", "zone_Cumulative_rate", dtGridZoneOption);

                //중복클릭 데이터 삭제.
                dtSelectedGridZoneOption.Rows.Clear();

                //콤보박스 첫 item이 보여질 내용 디폴트 지정 (Zone옵션, Query로 특정 데이터 보여질 예정)
                OracleDataTable zoneOraDt = bizService.GetGridSelectedOptionDefault("Z");

                foreach (DataRow _row1 in zoneOraDt.Rows)
                {
                    //DataRow 추가
                    DataRow zoneDr = dtSelectedGridZoneOption.NewRow();

                    zoneDr["Zone_Code"] = _row1["DSTTID"];
                    zoneDr["Zone_Name"] = _row1["DSTTYP"];
                    zoneDr["Zone_Division"] = _row1["DSTTNM"];

                    foreach (DataRow dr in Priority_ComboboxOraDt.Rows)
                    {
                        if (dr["VALES"].ToString().Equals(_row1["ORDP01"].ToString()))
                        {
                            zoneDr["Priority01"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP02"].ToString()))
                        {
                            zoneDr["Priority02"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP03"].ToString()))
                        {
                            zoneDr["Priority03"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP04"].ToString()))
                        {
                            zoneDr["Priority04"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP05"].ToString()))
                        {
                            zoneDr["Priority05"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP06"].ToString()))
                        {
                            zoneDr["Priority06"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP07"].ToString()))
                        {
                            zoneDr["Priority07"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP08"].ToString()))
                        {
                            zoneDr["Priority08"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP09"].ToString()))
                        {
                            zoneDr["Priority09"] = dr["NAMES"];
                        }
                        if (dr["VALES"].ToString().Equals(_row1["ORDP10"].ToString()))
                        {
                            zoneDr["Priority10"] = dr["NAMES"];
                        }
                    }

                    foreach (DataRow dr in zone_Distance_Calc_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        zoneDr["zone_Distance_Calc"] = "default";

                        if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                        {
                            zoneDr["zone_Distance_Calc"] = dr["NAMES"];
                            break;
                        }
                    }

                    zoneDr["zone_Distance_Weight"] = _row1["DSTWGT"];
                    zoneDr["zone_Speed"] = _row1["VELCID"];
                    zoneDr["zone_Cumulative_rate"] = _row1["FCMVLC"];
                    zoneDr["zone_Deallocation_ratio"] = _row1["DACRAT"];
                    zoneDr["zone_Move_time_limit"] = _row1["TMSTOP"];
                    zoneDr["zone_Temporary_Vehicle_maximum_radius"] = _row1["DSTVHC"];

                    //Data 추가
                    dtSelectedGridZoneOption.Rows.Add(zoneDr);
                }

                //DataSource 지정
                //gridSelectedZoneOption.DataSource = dtSelectedGridZoneOption;

                //DataRefresh
                gridSelectedZoneOption.RefreshDataSource();

                #endregion

                #region 저장할 차량옵션 그리드

                //Parameter ( control, 가져올 caroption 컬럼이름, 현재 보여질 컬럼이름, 데이터 테이블 )
                SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_arrival_within_wokingTime", "caroption_arrival", dtGridCarOption);
                SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_Whether_working", "caroption_working", dtGridCarOption);

                key_id = "VRO001_DST073_DSTC16";
                caroption_region_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_region_reorder", caroption_region_reorder_ComboboxOraDt);

                key_id = "VRO001_DST073_DSTC17";
                caroption_area_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign", caroption_area_assign_ComboboxOraDt);

                key_id = "VRO001_DST073_DSTC18";
                caroption_area_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign_reorder", caroption_area_assign_reorder_ComboboxOraDt);

                key_id = "VRO001_DST073_DSTC19";
                caroption_remainder_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign", caroption_remainder_assign_ComboboxOraDt);

                key_id = "VRO001_DST073_DSTC20";
                caroption_remainder_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign_reorder", caroption_remainder_assign_reorder_ComboboxOraDt);

                //DataRow 추가
                DataRow carDr = dtSelectedGridCarOption.NewRow();

                //중복클릭 데이터 삭제.
                dtSelectedGridCarOption.Rows.Clear();

                //콤보박스 첫 item이 보여질 내용 디폴트 지정 (Zone옵션, Query로 특정 데이터 보여질 예정)
                OracleDataTable carOraDt = bizService.GetGridSelectedOptionDefault("C");

                foreach (DataRow _row2 in carOraDt.Rows)
                {
                    //DataRow 추가
                    DataRow _carDr = dtSelectedGridCarOption.NewRow();

                    _carDr["caroption_car_number"] = _row2["VEHCID"];
                    _carDr["caroption_car_type"] = _row2["VHCTON"];
                    _carDr["caroption_max_landing_num"] = _row2["LMSPNO"];
                    _carDr["caroption_arrival"] = _row2["FINTWA"];
                    _carDr["caroption_working"] = _row2["FONDUT"];
                    _carDr["caroption_region"] = _row2["DIST01"];
                    _carDr["caroption_mix_region"] = _row2["LMMDS1"];

                    foreach (DataRow dr in caroption_region_reorder_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        _carDr["caroption_region_reorder"] = "default";

                        if (dr["VALES"].ToString().Equals(_row2["REAL01"].ToString()))
                        {
                            _carDr["caroption_region_reorder"] = dr["NAMES"];
                            break;
                        }
                    }

                    _carDr["caroption_area"] = _row2["DIST02"];
                    _carDr["caroption_mix_area"] = _row2["LMMDS02"];

                    foreach (DataRow dr in caroption_area_assign_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        _carDr["caroption_area_assign"] = "default";

                        if (dr["VALES"].ToString().Equals(_row2["ALLO01"].ToString()))
                        {
                            _carDr["caroption_area_assign"] = dr["NAMES"];
                            break;
                        }
                    }

                    foreach (DataRow dr in caroption_area_assign_reorder_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        _carDr["caroption_area_assign_reorder"] = "default";

                        if (dr["VALES"].ToString().Equals(_row2["REAL02"].ToString()))
                        {
                            _carDr["caroption_area_assign_reorder"] = dr["NAMES"];
                            break;
                        }
                    }

                    foreach (DataRow dr in caroption_remainder_assign_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        _carDr["caroption_remainder_assign"] = "default";

                        if (dr["VALES"].ToString().Equals(_row2["ALLO03"].ToString()))
                        {
                            _carDr["caroption_remainder_assign"] = dr["NAMES"];
                            break;
                        }
                    }

                    foreach (DataRow dr in caroption_remainder_assign_reorder_ComboboxOraDt.Rows)
                    {
                        //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                        _carDr["caroption_remainder_assign_reorder"] = "default";

                        if (dr["VALES"].ToString().Equals(_row2["REAL03"].ToString()))
                        {
                            _carDr["caroption_remainder_assign_reorder"] = dr["NAMES"];
                            break;
                        }
                    }

                    _carDr["caroption_1"] = _row2["ATTR11"];
                    _carDr["caroption_2"] = _row2["ATTR12"];
                    _carDr["caroption_3"] = _row2["ATTR13"];
                    _carDr["caroption_4"] = _row2["ATTR14"];
                    _carDr["caroption_5"] = _row2["ATTR15"];
                    _carDr["caroption_6"] = _row2["ATTR16"];
                    _carDr["caroption_7"] = _row2["ATTR17"];
                    _carDr["caroption_8"] = _row2["ATTR18"];
                    _carDr["caroption_9"] = _row2["ATTR19"];
                    _carDr["caroption_10"] = _row2["ATTR20"];

                    //Data 추가
                    dtSelectedGridCarOption.Rows.Add(_carDr);
                }

                //DataSource 지정
                gridSelectedCarOption.DataSource = dtSelectedGridCarOption;

                //DataRefresh
                gridSelectedCarOption.RefreshDataSource();

                #endregion

                #region 저장할 착지옵션 그리드

                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime", list_hours);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime1", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime2", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime", list_hours);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime1", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime2", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "landing_OTD_compliance", "OTD_compliance", dtGridLandingOption);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime", list_hours);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime1", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime2", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime", list_hours);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime1", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime2", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime", list_hours);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime1", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime2", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime", list_hours);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime1", list_minute_second);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime2", list_minute_second);

                key_id = "VRO001_DST074_DSTC21";
                Dispatcher_manualHandling_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "NAMES", "Dispatcher_manualHandling", Dispatcher_manualHandling_ComboboxOraDt);

                SetRepositoryItemComboBoxBindDataYN(gridSelectedLandingOption, "landing_Whether_assigned", "Whether_assigned", dtGridLandingOption);

                //중복클릭 데이터 삭제.
                dtSelectedGridLandingOption.Rows.Clear();

                //콤보박스 첫 item이 보여질 내용 디폴트 지정 (엔진옵션, Query로 특정 데이터 보여질 예정)
                //OracleDataTable landingOraDt = bizService.GetGridSelectedLandingOptionDefault();
                OracleDataTable landingOraDt = bizService.GetGridSelectedOptionDefault("A");

                foreach (DataRow _row3 in landingOraDt.Rows)
                {
                    //DataRow 추가
                    DataRow landingDr = dtSelectedGridLandingOption.NewRow();

                    landingDr["landingoption_number"] = _row3["STOPID"];
                    landingDr["landingoption_name"] = _row3["STOPNM"];
                    landingDr["landingoption_restrict"] = _row3["BVEHID"];
                    //TKXADS18_THR 조인정보, 현재 쿼리에서 컬럼 없음. 프로시져 완료 후 가능
                    //landingDr["landingoption_box_number"] = _row3["BOXCNT"];
                    //landingDr["landingoption_box_rating"] = _row3["BOXLEV"];
                    //landingDr["landingoption_weight"] = _row3["WEIGHT"];
                    //landingDr["landingoption_volumn"] = _row3["VOLUME"];

                    //landingDr["landingoption_PLT"] = _row3["PLTCNT"];
                    //DB 데이터 시간 형식 ( 01:00:00 )
                    string time = _row3["TIMEFR"].ToString();
                    if (!(time == null || time.Equals("") || time.Equals(" ")))//업데이트 할때 null 인 데이터는 " " 공백 넣어서 처리, 데이터 받을때도 공백으로 받음...디비 프로시져에서 처리시 공백 요청
                    {
                        landingDr["Adhesive_required_startTime"] = time.Substring(0, 2);
                        landingDr["Adhesive_required_startTime1"] = time.Substring(3, 2);
                        landingDr["Adhesive_required_startTime2"] = time.Substring(6, 2);
                    }

                    string time1 = _row3["TIMETO"].ToString();
                    if (!(time1 == null || time1.Equals("") || time1.Equals(" ")))
                    {
                        landingDr["Adhesive_required_endTime"] = time1.Substring(0, 2);
                        landingDr["Adhesive_required_endTime1"] = time1.Substring(3, 2);
                        landingDr["Adhesive_required_endTime2"] = time1.Substring(6, 2);
                    }

                    landingDr["OTD_compliance"] = _row3["OTDOPT"];

                    string time2 = _row3["OFFTFR"].ToString();
                    if (!(time2 == null || time2.Equals("") || time2.Equals(" ")))
                    {
                        landingDr["OffTime_startTime"] = time2.Substring(0, 2);
                        landingDr["OffTime_startTime1"] = time2.Substring(3, 2);
                        landingDr["OffTime_startTime2"] = time2.Substring(6, 2);
                    }

                    string time3 = _row3["OFFTTO"].ToString();
                    if (!(time3 == null || time3.Equals("") || time3.Equals(" ")))
                    {
                        landingDr["OffTime_endTime"] = time3.Substring(0, 2);
                        landingDr["OffTime_endTime1"] = time3.Substring(3, 2);
                        landingDr["OffTime_endTime2"] = time3.Substring(6, 2);
                    }

                    string time4 = _row3["PREPTM"].ToString();
                    if (!(time4 == null || time4.Equals("") || time4.Equals(" ")))
                    {
                        landingDr["Fixed_handlingTime"] = time4.Substring(0, 2);
                        landingDr["Fixed_handlingTime1"] = time4.Substring(3, 2);
                        landingDr["Fixed_handlingTime2"] = time4.Substring(6, 2);
                    }

                    string time5 = _row3["UNLDTM"].ToString();
                    if (!(time5 == null || time5.Equals("") || time5.Equals(" ")))
                    {
                        landingDr["change_handlingTime"] = time5.Substring(0, 2);
                        landingDr["change_handlingTime1"] = time5.Substring(3, 2);
                        landingDr["change_handlingTime2"] = time5.Substring(6, 2);
                    }

                    //해당 컬럼 없음. 컬럼 다시 확인.
                    //landingDr["Recent_delivery_vehicleNumber"] = _row3["HVEHID"];
                    //landingDr["Latest_arrivalTime"] = _row3["HARRTM"];

                    //foreach (DataRow dr in Dispatcher_manualHandling_ComboboxOraDt.Rows)
                    //{
                    //    //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                    //    landingDr["Dispatcher_manualHandling"] = "default";

                    //    if (dr["VALES"].ToString().Equals(_row3["MALTYP"].ToString()))
                    //    {
                    //        landingDr["Dispatcher_manualHandling"] = dr["NAMES"];
                    //        break;
                    //    }
                    //}

                    //landingDr["Whether_assigned"] = _row3["FALLOC"];

                    //Data 추가
                    dtSelectedGridLandingOption.Rows.Add(landingDr);
                }
                //DataSource 지정
                gridSelectedLandingOption.DataSource = dtSelectedGridLandingOption;

                //DataRefresh
                gridSelectedLandingOption.RefreshDataSource();

                #endregion

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
                
        // 콤보박스 체인징 이벤트           
        private void repositoryItemComboBox_POLICY_CODE_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridSelectedEngineOption.BeginUpdate();

                ComboBoxEdit comboBoxEdit = sender as DevExpress.XtraEditors.ComboBoxEdit;

                bandedGridView2.SetRowCellValue(bandedGridView2.FocusedRowHandle, "POLICY_ID", comboBoxEdit.SelectedItem);
            }
            finally
            {
                gridSelectedEngineOption.EndUpdate();
            }

        }

        ///<summary>콤보박스 데이터 바인딩1 (필드값 그대로 바인딩)</summary>
        public void SetRepositoryItemComboBoxBindData(GridControl gridControl, string fieldName, DataTable bindData)
        {
            string repositoryItemName = "repositoryItemComboBox";

            RepositoryItemComboBox repositoryItemComboBox = gridControl.RepositoryItems[repositoryItemName + fieldName] as RepositoryItemComboBox;

            if (repositoryItemComboBox != null)
            {
                try
                {
                    gridControl.BeginUpdate();

                    repositoryItemComboBox.BorderStyle = BorderStyles.NoBorder;
                    repositoryItemComboBox.AutoHeight = false;
                    repositoryItemComboBox.Items.Clear();

                    if (bindData != null && bindData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in bindData.Rows)
                        {
                            repositoryItemComboBox.Items.Add(dr[fieldName].ToString());
                        }
                    }
                }
                finally
                {
                    gridControl.EndUpdate();
                }

            }
        }

        ///<summary>콤보박스 데이터 바인딩2 (코드테이블 조회하여 코드명,코드값 바인딩)</summary>
        public void SetRepositoryItemComboBoxBindData(GridControl gridControl,string bindDataName, string fieldName, DataTable bindData)
        {
            string repositoryItemName = "repositoryItemComboBox";

            RepositoryItemComboBox repositoryItemComboBox = gridControl.RepositoryItems[repositoryItemName + fieldName] as RepositoryItemComboBox;

            if (repositoryItemComboBox != null)
            {
                try
                {
                    gridControl.BeginUpdate();

                    repositoryItemComboBox.BorderStyle = BorderStyles.NoBorder;
                    repositoryItemComboBox.AutoHeight = false;
                    repositoryItemComboBox.Items.Clear();

                    if (bindData != null && bindData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in bindData.Rows)
                        {
                            repositoryItemComboBox.Items.Add(dr[bindDataName].ToString());
                        }
                    }
                }
                finally
                {
                    gridControl.EndUpdate();
                }
            }
        }

        ///<summary>콤보박스 데이터 바인딩3 (시간 list용)</summary>
        public void SetRepositoryItemComboBoxBindData(GridControl gridControl, string fieldName, List<string> bindData)
        {
            string repositoryItemName = "repositoryItemComboBox";

            RepositoryItemComboBox repositoryItemComboBox = gridControl.RepositoryItems[repositoryItemName + fieldName] as RepositoryItemComboBox;

            if (repositoryItemComboBox != null)
            {
                try
                {
                    gridControl.BeginUpdate();

                    repositoryItemComboBox.BorderStyle = BorderStyles.NoBorder;
                    repositoryItemComboBox.AutoHeight = false;
                    repositoryItemComboBox.Items.Clear();

                    if (bindData != null && bindData.Count > 0)
                    {
                        foreach (string str in bindData)
                        {
                            repositoryItemComboBox.Items.Add(str);
                        }
                    }
                }
                finally
                {
                    gridControl.EndUpdate();
                }
            }
        }

        ///<summary>콤보박스 데이터 바인딩4 (Y,N Flag)</summary>
        public void SetRepositoryItemComboBoxBindDataYN(GridControl gridControl, string bindDataName, string fieldName, DataTable bindData)
        {
            string repositoryItemName = "repositoryItemComboBox";

            RepositoryItemComboBox repositoryItemComboBox = gridControl.RepositoryItems[repositoryItemName + fieldName] as RepositoryItemComboBox;

            if (repositoryItemComboBox != null)
            {
                try
                {
                    gridControl.BeginUpdate();

                    repositoryItemComboBox.BorderStyle = BorderStyles.NoBorder;
                    repositoryItemComboBox.AutoHeight = false;
                    repositoryItemComboBox.Items.Clear();

                    repositoryItemComboBox.Items.Add("Y");
                    repositoryItemComboBox.Items.Add("N");

                }
                finally
                {
                    gridControl.EndUpdate();
                }
            }
        }

        //Policy table 우클릭
        private void gridView1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (checkDelete.Checked)
                {
                    GridView view = sender as GridView;

                    GridHitInfo hitInfo = view.CalcHitInfo(e.Point);

                    if (hitInfo.InRow)
                    {

                        view.FocusedRowHandle = hitInfo.RowHandle;

                        //DataRow row = view.GetDataRow(hitInfo.RowHandle);

                        contextMenuStrip1.Show(view.GridControl, e.Point);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //복원버튼 클릭시
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                delete_recovery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //checkbox 바인딩
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit GetRepositoryItemCheckEdit()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit item = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();

            item.AutoHeight = false;
            item.ValueChecked = "1";
            item.ValueUnchecked = "0";

            item.EditValueChanged += new EventHandler(PolicyCheckBoxItem_EditValueChanged);
            return item;
        }

        //종료버튼
        private void buttonClose_Click(object sender, EventArgs e)
        {
            FormFlag.formParameterSetup_flag = true;
            Close();
        }

        private void initializeDataGrid(DataTable dataGrid)
        {
            dataGrid.Rows.Clear();
        }

        private int getCheckedRowCount(GridView gridView)
        {
            int checkedcnt = 0;
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                if (gridView.GetDataRow(i)[0].ToString() == "1")
                    checkedcnt++;
            }

            return checkedcnt;
        }

        private string getSelectedPolicy(GridView gridView)
        {
            string tempStr = "";
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                if (gridView.GetDataRow(i)[0].ToString() == "1")
                    tempStr = gridView.GetDataRow(i)[1].ToString();
            }

            return tempStr;
        }

        //적용버튼
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                screenManager.ShowWaitForm();
                int selectedRowCount = getCheckedRowCount(gridView1);
                if (selectedRowCount == 1)
                {
                    if (MessageBox.Show("모든 데이터가 초기화됩니다. 적용하시겠습니까?"
                        , "정책 적용"
                        , MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        bizService = GetBizService();

                        string policyID = getSelectedPolicy(gridView1);
                        string scenarioID = comboScenarioCode.SelectedItem.ToString();

                        #region 1
                        initializeDataGrid(dtSelectedGridEngineOption);
                        SetGridSelectedEngineOption(bizService.GetGridSelectedOptionByScenario("E", scenarioID, policyID));
                        #endregion

                        #region 2
                        initializeDataGrid(dtSelectedGridZoneOption);
                        SetGridSelectedDistrictOption(bizService.GetGridSelectedOptionByScenario("Z", scenarioID, policyID));
                        #endregion

                        #region 3
                        initializeDataGrid(dtSelectedGridCarOption);
                        SetGridSelectedVehicleOption(bizService.GetGridSelectedOptionByScenario("C", scenarioID, policyID));
                        #endregion

                        #region 4
                        initializeDataGrid(dtSelectedGridLandingOption);
                        SetGridSelectedStopOption(bizService.GetGridSelectedOptionByScenario("A", scenarioID, policyID));
                        #endregion

                        #region set policy
                        string rtn_value = bizService.sp_vro_update_plan_header("00000", scenarioID);
                        #endregion
                    }
                }
                else if ( selectedRowCount > 1 )
                {
                    MessageBox.Show("1개의 정책만 선택하십시오."
                        , "정책 적용 오류"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                    return;
                }
                else if (selectedRowCount == 0)
                {
                    MessageBox.Show("최소한 1개의 정책을 선택해야 합니다."
                        , "정책 적용 오류"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                    return;
                }
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

        // policy table 체크박스 체인징 이벤트
        public void PolicyCheckBoxItem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = gridView1.GetFocusedDataRow();

                if (((CheckEdit)sender).Checked)
                {
                    list_policy_code.Add(dr["POLICY_ID"].ToString());
                    //POLICY 체크시 삭제 DISABLE
                    if (list_policy_code.Contains("PLC_DEFAULT"))
                    {
                        buttonRemove.Enabled = false;
                    }
                    buttonSave.Enabled = false; //true ;//20140325, 데모를 위해 임시 불가처리
                    buttonSaveAs.Enabled = true;
                    buttonSaveScenario.Enabled = true;

                    GridView view = (GridView)gridPolicy.Views[0];

                    int row = view.FocusedRowHandle;

                    textPolicyDescription.Text = view.GetRowCellValue(row, "POLICY_NAME").ToString();
                    textPolicyName.Text = view.GetRowCellValue(row, "DESCRIPTION").ToString();
                }
                else
                {
                    list_policy_code.Remove(dr["POLICY_ID"].ToString());
                    //POLICY 체크시 삭제 EABLE
                    if (!list_policy_code.Contains("PLC_DEFAULT"))
                    {
                        buttonRemove.Enabled = true;
                    }
                    //policy table 체크 버튼이 하나도 없을 때, 저장버튼 비활성화
                    if (list_policy_code.Count == 0)
                    {
                        buttonSave.Enabled = false;
                        buttonSaveAs.Enabled = false;
                        buttonSaveScenario.Enabled = false;
                    }

                    GridView view = (GridView)gridPolicy.Views[0];

                    int row = -1;
                    Debug.WriteLine("CheckEdit value ---->");
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        Debug.WriteLine(gridView1.GetDataRow(i)[0].ToString());
                        if (gridView1.GetDataRow(i)[0].ToString() == "1")
                            row = i;
                    }

                    if (row == -1)
                    {
                        textPolicyDescription.Text = "";
                        textPolicyName.Text = "";
                    }
                    else
                    {
                        textPolicyDescription.Text = view.GetRowCellValue(row, "POLICY_NAME").ToString();
                        textPolicyName.Text = view.GetRowCellValue(row, "DESCRIPTION").ToString();
                    }
                }
                list_policy_code.Sort();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //기존 이름으로 저장시 selected 엔진옵션
        // '↑' 배열로 입렬시 구분자
        private string[] save_engine_arr()
        {
            DataRow engineDr = (DataRow)bandedGridView2.GetDataRow(0);
            string time = engineDr.ItemArray[8].ToString();
            string engine_str = "";
            string[] enginArr = new string[1];

            int columnLength = engineDr.ItemArray.Length;

            for (int i = 0; i < 21; i++)
            {
                if (i == 1)
                {//권역 정렬 : NAME을 VALUES 로 변경하여 데이터 저장. (예: 각도순 이면 1 )
                    foreach (DataRow dr in Zone_ComboboxOraDt.Rows)
                    {
                        if (engineDr.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))
                        {//Zone_ComboboxOraDt에서 맞는 값이 있으면
                            engine_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                            break;
                        }
                    }
                } 
                else if (i == 2)
                {//거리 계산
                    foreach (DataRow dr in Distance_Calc_ComboboxOraDt.Rows)
                    {
                        if (engineDr.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))
                        {//Zone_ComboboxOraDt에서 맞는 값이 있으면
                            engine_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                            break;
                        }
                    }
                }
                else if (i == 7 || i == 9 || i == 10)
                {
                    continue;
                }
                //else if (i == 4)
                //{//이동 속도 : 작업 예정 
                //    //default
                //    engine_str += engineDr.ItemArray[i].ToString();
                //} 
                //else if (i == 8)
                //{//상차시간
                //    //default
                //    engine_str += engineDr.ItemArray[i].ToString();
                //} 
                //else if (i < 7 && i != 0 && i != 1 && i != 2 && i != 4)
                //{//default
                //    //if (engineDr.ItemArray[i].ToString() == null || engineDr.ItemArray[i].ToString().Equals(""))
                //    //{//해당 컬럼값이 null 이면 스페이스 값으로 대체. 
                //    //    engine_str += engineDr.ItemArray[i] + " ↑";
                //    //}
                //    //else
                //    //{
                //    //    engine_str += engineDr.ItemArray[i] + "↑";
                //    //}

                //    engine_str += engineDr.ItemArray[i].ToString();
                //} 
                //else if (i > 7)
                //{
                //    //if (engineDr.ItemArray[i].ToString() == null || engineDr.ItemArray[i].ToString().Equals(""))//해당 컬럼값이 null 이면 스페이스 값으로 대체. 
                //    //{
                //    //    engine_str += engineDr.ItemArray[i + 2].ToString();
                //    //}
                //    //else
                //    //{
                //    //    engine_str += engineDr.ItemArray[i + 2] + "↑";
                //    //}
                //    engine_str += engineDr.ItemArray[i + 2].ToString();
                //}
                else
                {
                    engine_str += engineDr.ItemArray[i].ToString().TrimEnd() == "" ? " " : engineDr.ItemArray[i].ToString();
                }
                engine_str += "↑";
            }
            enginArr[0] = engine_str.Substring(0,engine_str.Length-1);

            return enginArr;
        }

        //기존 이름으로 저장시 selected 권역옵션
        private string[] save_zone_arr()
        {
            int zoneOraDt_length = 0;
            zoneOraDt_length = dtSelectedGridZoneOption.Rows.Count;

            string[] ZoneArr = new string[zoneOraDt_length];
            string zone_str = "";
            int zone_row = 0;

            foreach (DataRow _row1 in dtSelectedGridZoneOption.Rows)
            {
                for (int i = 0; i < _row1.ItemArray.Length; i++)
                {
                    if (i >= 3 && i <= 12)//우선순위 1~10까지
                    {
                        foreach (DataRow dr in Priority_ComboboxOraDt.Rows)
                        {
                            if (_row1.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                zone_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                    }
                    else if (i == 13)//거리계산
                    {
                        foreach (DataRow dr in zone_Distance_Calc_ComboboxOraDt.Rows)
                        {
                            if (_row1.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                zone_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                    }
                    //else if (i == 15)//이동 속도 : 작업예정
                    //{
                    //    zone_str += _row1.ItemArray[i].ToString();
                    //}
                    else
                    {
                        zone_str += _row1.ItemArray[i].ToString().TrimEnd() == "" ? " " : _row1.ItemArray[i].ToString();
                    }
                    zone_str += "↑";
                }

                ZoneArr[zone_row] = zone_str.Substring(0, zone_str.Length - 1);
                zone_row++;
                zone_str = "";

            }
            return ZoneArr;
        }

        //기존 이름으로 저장시 selected 차량옵션
        private string[] save_car_arr()
        {
            int carOraDt_length = 0;
            carOraDt_length = dtSelectedGridCarOption.Rows.Count;

            string[] carArr = new string[carOraDt_length];
            string car_str = "";
            int car_row = 0;
            
            foreach (DataRow _row2 in dtSelectedGridCarOption.Rows)
            {
                for (int i = 0; i < _row2.ItemArray.Length; i++)
                {
                    if (i == 7)//고정권역 할당 후 재정렬 방식
                    {
                        foreach (DataRow dr in caroption_region_reorder_ComboboxOraDt.Rows)
                        {
                            if (_row2.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                car_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                    }
                    else if (i == 10)//대권역 할당 후 재정렬 방식
                    {
                        foreach (DataRow dr in caroption_area_assign_ComboboxOraDt.Rows)
                        {
                            if (_row2.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                car_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                    }
                    
                    else if (i == 11)//고정권역 할당 후 재정렬 방식
                    {
                        foreach (DataRow dr in caroption_area_assign_reorder_ComboboxOraDt.Rows)
                        {
                            if (_row2.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                car_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                    }
                    else if (i == 12)//고정권역 할당 후 재정렬 방식
                    {
                        foreach (DataRow dr in caroption_remainder_assign_ComboboxOraDt.Rows)
                        {
                            if (_row2.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                car_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                    }
                    else if (i == 13)//고정권역 할당 후 재정렬 방식
                    {
                        foreach (DataRow dr in caroption_remainder_assign_reorder_ComboboxOraDt.Rows)
                        {
                            if (_row2.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                car_str += dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                    }
                    else
                    {
                        car_str += _row2.ItemArray[i].ToString().TrimEnd() == "" ? " " : _row2.ItemArray[i].ToString();
                    }
                    car_str += "↑";
                }

                carArr[car_row] = car_str.Substring(0, car_str.Length - 1);
                car_row++;
                car_str = "";
            }
            return carArr;
        }

        //기존 이름으로 저장시 selected 착지옵션
        private string[] save_landing_arr()
        {
            int landingOraDt_length = 0;
            landingOraDt_length = dtSelectedGridLandingOption.Rows.Count;

            //각 row를 가지는 배열변수
            string[] landingArr = new string[landingOraDt_length];
            //해당 row data를 가지는 변수
            string landing_str = "";
            //zoneArr count
            int landing_row = 0;

            string tempStr = "";

            foreach (DataRow _row3 in dtSelectedGridLandingOption.Rows)
            {
                for (int i = 0; i < _row3.ItemArray.Length; i++)
                {
                    if (i == 9 || i == 10 || i == 12 || i == 13 || i == 16 || i == 17 || i == 19 || i == 20 | i == 22 || i == 23 || i == 25 || i == 26) //시간 format
                    {
                        continue;
                        //if (!_row3.ItemArray[i].ToString().Equals(""))
                        //{
                        //    landing_str += _row3.ItemArray[i] + ":";
                        //}
                    }
                    else if (i == 29)//수작업 배차처리
                    {
                        tempStr = "2";
                        foreach (DataRow dr in Dispatcher_manualHandling_ComboboxOraDt.Rows)
                        {
                            if (_row3.ItemArray[i].ToString().Equals((dr["NAMES"].ToString())))//Priority_ComboboxOraDt에서 맞는 값이 있으면
                            {
                                tempStr = dr["VALES"].ToString().TrimEnd() == "" ? " " : dr["VALES"].ToString();
                                break;
                            }
                        }
                        landing_str += tempStr;
                    }
                    else
                    {
                        string aaaa;
                        aaaa = _row3.ItemArray[i].ToString().TrimEnd();
                        if (aaaa == "3943405")
                            aaaa = _row3.ItemArray[i].ToString();
                        if (aaaa == null)
                            aaaa = " ";
                        landing_str += _row3.ItemArray[i].ToString().TrimEnd() == "" ? " " : _row3.ItemArray[i].ToString();
                    }
                    landing_str += "↑";
                }

                landingArr[landing_row] = landing_str.Substring(0, landing_str.Length - 1);
                landing_row++;
                landing_str = "";
            }

            return landingArr;
        }

        //복원 
        private void delete_recovery()
        {
            DataRow dr = gridView1.GetFocusedDataRow();

            textPolicyDescription.Text = dr["POLICY_ID"].ToString();
            textPolicyName.Text = dr["DESCRIPTION"].ToString();

            //다음 작업 Policy table 삭제 여부 플래그 전환 N -> Y 
            gridPolicy.BeginUpdate();

            bizService = GetBizService();

            string result = bizService.deletPolicy("U", dr["POLICY_ID"].ToString());

            gridView1.GetFocusedDataRow().Delete();

            textPolicyDescription.Text = "";
            textPolicyName.Text = "";

            gridPolicy.EndUpdate();

            gridPolicy.RefreshDataSource();
        }

        ///<summary>엔진옵션 그리드의 콤보박스 바인딩</summary>
        private void EngineCombobox()
        {
            //콤보박스 데이터 바인딩 (엔진 옵션)
            ///  p_key_id :  공통코드 KEY 값
            string query_type = "";

            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "POLICY_ID", dtPolicy);

            string key_id = "VRO001_DST071_DSTC01";
            Zone_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Zone", Zone_ComboboxOraDt);

            key_id = "VRO001_DST071_DSTC02";
            Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "NAMES", "Distance_Calc", Distance_Calc_ComboboxOraDt);

            // return value : VELCID , 현재 return value 없음
            Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "VELCID", "Speed", Speed_ComboboxOraDt);

            SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Cumulative_rate", "Cumulative_rate", dtGridEngineOption);
            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "Deallocation_ratio", dtGridEngineOption);
            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "hour", list_hours);
            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "minute", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedEngineOption, "second", list_minute_second);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "overfloww_order", "overfloww_order", dtGridEngineOption);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_weight", "By_weight", dtGridEngineOption);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_volume", "By_volume", dtGridEngineOption);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "By_PLT", "By_PLT", dtGridEngineOption);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "End_time", "End_time", dtGridEngineOption);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedEngineOption, "Vehicles_working", "Vehicles_working", dtGridEngineOption);
        }

        ///<summary>권역옵션 그리드의 콤보박스 세팅</summary>
        private void ZoneCombobox()
        {
            string query_type = "";
            string key_id = "VRO001_DST072_DSTC04";  //권역의 차량/주문 할당 우선순위의 코드 리스트를 콤보박스에 세팅
            Priority_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority01", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority02", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority03", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority04", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority05", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority06", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority07", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority08", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority09", Priority_ComboboxOraDt);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority10", Priority_ComboboxOraDt);

            key_id = "VRO001_DST072_DSTC14";  //거리계산방식의 코드리스트를 콤보박스에 세팅
            zone_Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "zone_Distance_Calc", zone_Distance_Calc_ComboboxOraDt);

            zone_Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
            SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "VELCID", "zone_Speed", zone_Speed_ComboboxOraDt);
        }

        ///<summary>차량옵션 그리드의 콤보박스 세팅</summary>
        private void CarCombobox()
        {
            //Parameter ( control, 가져올 caroption 컬럼이름, 현재 보여질 컬럼이름, 데이터 테이블 )
            SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_arrival_within_wokingTime", "caroption_arrival", dtGridCarOption);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_Whether_working", "caroption_working", dtGridCarOption);

            string query_type = "";
            string key_id = "VRO001_DST073_DSTC16";  //고정권역 재정렬 기준 옵션리스트 세팅
            caroption_region_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_region_reorder", caroption_region_reorder_ComboboxOraDt);

            key_id = "VRO001_DST073_DSTC17";  //고정권역 혼적방식 옵션리스트 세팅
            caroption_area_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign", caroption_area_assign_ComboboxOraDt);

            key_id = "VRO001_DST073_DSTC18";  //대권역 재정렬 기준 옵션리스트 세팅
            caroption_area_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign_reorder", caroption_area_assign_reorder_ComboboxOraDt);

            key_id = "VRO001_DST073_DSTC19";  //대권역 혼적방식 기준 옵션리스트 세팅
            caroption_remainder_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign", caroption_remainder_assign_ComboboxOraDt);

            key_id = "VRO001_DST073_DSTC20";  //자투리 재정렬 기준 옵션리스트 세팅
            caroption_remainder_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign_reorder", caroption_remainder_assign_reorder_ComboboxOraDt);

        }

        ///<summary>착지/주문옵션 그리드의 콤보박스 세팅</summary>
        private void StopCombobox()
        {
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime", list_hours);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime1", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime2", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime", list_hours);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime1", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime2", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime", list_hours);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime1", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime2", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime", list_hours);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime1", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime2", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime", list_hours);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime1", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime2", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime", list_hours);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime1", list_minute_second);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime2", list_minute_second);

            string query_type = "";
            string key_id = "VRO001_DST074_DSTC21";  //수동배차 후처리 옵션 리스트의 콤보박스 세팅
            Dispatcher_manualHandling_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
            SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "NAMES", "Dispatcher_manualHandling", Dispatcher_manualHandling_ComboboxOraDt);

            SetRepositoryItemComboBoxBindDataYN(gridSelectedLandingOption, "OTD_compliance", "OTD_compliance", dtGridLandingOption);
            SetRepositoryItemComboBoxBindDataYN(gridSelectedLandingOption, "landing_Whether_assigned", "Whether_assigned", dtGridLandingOption);
        }

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

        private void PrintDebug (OracleDataTable dt)
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

        // 시나리오 콤보박스 변경
        private void comboPolicyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.screenManager.ShowWaitForm();

                #region 당 차수 데이터 설정

                comboScenarioCode.SelectedIndex = comboScenarioName.SelectedIndex;
                SetGridSelectedEngineOption(bizService.GetGridSelectedOptionByPolicy("E", comboScenarioCode.SelectedItem.ToString()));
                SetGridSelectedDistrictOption(bizService.GetGridSelectedOptionByPolicy("Z", comboScenarioCode.SelectedItem.ToString()));
                SetGridSelectedVehicleOption(bizService.GetGridSelectedOptionByPolicy("C", comboScenarioCode.SelectedItem.ToString()));
                SetGridSelectedStopOption(bizService.GetGridSelectedOptionByPolicy("A", comboScenarioCode.SelectedItem.ToString()));

                //#region gridSelectedEngineOption
                //try
                //{
                //    comboPolicyCode.SelectedIndex = comboPolicyName.SelectedIndex;

                //    GridView view_gridSelectedEngineOption = (GridView)gridSelectedEngineOption.Views[0];

                //    dtSelectedGridEngineOption.Rows.Clear();

                //    string planid2 = comboPolicyCode.SelectedItem.ToString();
                //    OracleDataTable engineOraDt = bizService.GetGridSelectedOptionByPolicy("E", planid2);
                //    #region
                //    PrintDebug(engineOraDt);
                //    #endregion

                //    //cell에 보여질 값 설정
                //    foreach (DataRow _row1 in engineOraDt.Rows)
                //    {
                //        //DataRow 추가
                //        DataRow engineDr = dtSelectedGridEngineOption.NewRow();

                //        engineDr["ENGPID"] = _row1["ENGPID"];
                //        engineDr["Zone"] = _row1["DSTORD"];
                //        ////값이 아닌 NAME을 display
                //        //foreach (DataRow dr in engineOraDt.Rows)
                //        //{
                //        //    //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //        //    engineDr["Zone"] = "default";

                //        //    if (dr["VALES"].ToString().Equals(_row1["DSTORD"].ToString()))
                //        //    {
                //        //        engineDr["Zone"] = dr["NAMES"];
                //        //        break;
                //        //    }
                //        //}

                //        engineDr["Distance_Calc"] = _row1["DSTCTY"];
                //        //foreach (DataRow dr in Distance_Calc_ComboboxOraDt.Rows)
                //        //{
                //        //    //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //        //    engineDr["Distance_Calc"] = "default";

                //        //    if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                //        //    {
                //        //        engineDr["Distance_Calc"] = dr["NAMES"];
                //        //        break;
                //        //    }
                //        //}

                //        engineDr["Distance_Weight"] = _row1["WGTDST"];

                //        //이동속도 관련 콤보박스 초기값 설정 작업해야함.
                //        engineDr["Speed"] = _row1["VELCID"];

                //        engineDr["Cumulative_rate"] = _row1["FCMVLC"];
                //        engineDr["Deallocation_ratio"] = _row1["DACRAT"];

                //        //상차시간 형식 ( 01:00:00 )
                //        engineDr["hour"] = _row1["LOADTM"];
                //        //string phase_difference_time = _row1["LOADTM"].ToString();
                //        //if (!(phase_difference_time == null && phase_difference_time.Equals("") && phase_difference_time.Equals(" ")))
                //        //{
                //        //    engineDr["hour"] = phase_difference_time.Substring(0, 2);
                //        //    engineDr["minute"] = phase_difference_time.Substring(3, 2);
                //        //    engineDr["second"] = phase_difference_time.Substring(6, 2);
                //        //}

                //        engineDr["overfloww_order"] = _row1["FOCAPA"];
                //        engineDr["Maximum_weight"] = _row1["WGTMXW"];
                //        engineDr["Maximum_volume"] = _row1["WGTMXV"];
                //        engineDr["Maximum_PLT"] = _row1["WGTMXP"];
                //        engineDr["By_weight"] = _row1["FWGTSD"];
                //        engineDr["By_volume"] = _row1["FVOLSD"];
                //        engineDr["By_PLT"] = _row1["FPLTSD"];
                //        engineDr["End_time"] = _row1["FTWEND"];
                //        engineDr["Vehicles_working"] = _row1["FVHCTW"];
                //        engineDr["Temporary_Vehicle_maximum_radius"] = _row1["MRTVHC"];

                //        dtSelectedGridEngineOption.Rows.Add(engineDr);
                //    }

                //    //DataSource 지정
                //    gridSelectedEngineOption.DataSource = dtSelectedGridEngineOption;
                //    gridSelectedEngineOption.RefreshDataSource();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //#endregion gridSelectedEngineOption

                //#region gridSelectedZoneOption
                //try
                //{
                //    //권역옵션 탭
                //    string query_type = "";
                //    string key_id = "VRO001_DST072_DSTC04";
                //    Priority_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);

                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority01", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority02", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority03", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority04", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority05", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority06", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority07", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority08", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority09", Priority_ComboboxOraDt);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "Priority10", Priority_ComboboxOraDt);

                //    key_id = "VRO001_DST072_DSTC14";
                //    zone_Distance_Calc_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "NAMES", "zone_Distance_Calc", zone_Distance_Calc_ComboboxOraDt);

                //    // return value : VELCID , 현재 return value 없음
                //    //zone_Speed_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_movingSpeed_combobox();
                //    SetRepositoryItemComboBoxBindData(gridSelectedZoneOption, "VELCID", "zone_Speed", zone_Speed_ComboboxOraDt);

                //    GridView view_gridSelectedZoneOption = (GridView)gridSelectedZoneOption.Views[0];

                //    dtSelectedGridZoneOption.Rows.Clear();

                //    string planid = comboPolicyCode.SelectedItem.ToString();
                //    OracleDataTable zoneOraDt = bizService.GetGridSelectedOptionByPolicy("Z", planid);
                //    #region Print dt
                //    PrintDebug(zoneOraDt);
                //    #endregion

                //    //cell에 보여질 값 설정
                //    foreach (DataRow _row1 in zoneOraDt.Rows)
                //    {
                //        //DataRow 추가
                //        DataRow zoneDr = dtSelectedGridZoneOption.NewRow();

                //        SetRepositoryItemComboBoxBindDataYN(gridSelectedZoneOption, "zone_Cumulative_rate", "zone_Cumulative_rate", dtGridZoneOption);

                //        zoneDr["Zone_Code"] = _row1["DSTTID"];
                //        zoneDr["Zone_Name"] = _row1["DSTTNM"].ToString().Trim();
                //        zoneDr["Zone_Division"] = _row1["DSTTYP"].ToString() == "0" ? "고정" : "대";

                //        foreach (DataRow dr in Priority_ComboboxOraDt.Rows)
                //        {
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP01"].ToString()))
                //            {
                //                zoneDr["Priority01"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP02"].ToString()))
                //            {
                //                zoneDr["Priority02"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP03"].ToString()))
                //            {
                //                zoneDr["Priority03"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP04"].ToString()))
                //            {
                //                zoneDr["Priority04"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP05"].ToString()))
                //            {
                //                zoneDr["Priority05"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP06"].ToString()))
                //            {
                //                zoneDr["Priority06"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP07"].ToString()))
                //            {
                //                zoneDr["Priority07"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP08"].ToString()))
                //            {
                //                zoneDr["Priority08"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP09"].ToString()))
                //            {
                //                zoneDr["Priority09"] = dr["NAMES"];
                //            }
                //            if (dr["VALES"].ToString().Equals(_row1["ORDP10"].ToString()))
                //            {
                //                zoneDr["Priority10"] = dr["NAMES"];
                //            }
                //        }

                //        foreach (DataRow dr in zone_Distance_Calc_ComboboxOraDt.Rows)
                //        {
                //            zoneDr["zone_Distance_Calc"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row1["DSTCTY"].ToString()))
                //            {
                //                zoneDr["zone_Distance_Calc"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        zoneDr["zone_Distance_Weight"] = double.Parse(_row1["DSTWGT"].ToString()) * 100.0;
                //        zoneDr["zone_Speed"] = _row1["VELCID"];
                //        zoneDr["zone_Cumulative_rate"] = _row1["FCMVLC"];
                //        zoneDr["zone_Deallocation_ratio"] = _row1["DACRAT"];
                //        zoneDr["zone_Move_time_limit"] = _row1["TMSTOP"];
                //        zoneDr["zone_Temporary_Vehicle_maximum_radius"] = _row1["DSTVHC"];

                //        dtSelectedGridZoneOption.Rows.Add(zoneDr);
                //    }

                //    //DataSource 지정
                //    gridSelectedZoneOption.DataSource = dtSelectedGridZoneOption;
                //    gridSelectedZoneOption.RefreshDataSource();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //#endregion gridSelectedZoneOption

                //#region gridCarOption
                //try
                //{
                //    //Parameter ( control, 가져올 caroption 컬럼이름, 현재 보여질 컬럼이름, 데이터 테이블 )
                //    SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_arrival_within_wokingTime", "caroption_arrival", dtGridCarOption);
                //    SetRepositoryItemComboBoxBindDataYN(gridSelectedCarOption, "car_Whether_working", "caroption_working", dtGridCarOption);

                //    string query_type = "";
                //    string key_id = "VRO001_DST073_DSTC16";
                //    caroption_region_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_region_reorder", caroption_region_reorder_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC17";
                //    caroption_area_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign", caroption_area_assign_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC18";
                //    caroption_area_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_area_assign_reorder", caroption_area_assign_reorder_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC19";
                //    caroption_remainder_assign_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign", caroption_remainder_assign_ComboboxOraDt);

                //    key_id = "VRO001_DST073_DSTC20";
                //    caroption_remainder_assign_reorder_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedCarOption, "NAMES", "caroption_remainder_assign_reorder", caroption_remainder_assign_reorder_ComboboxOraDt);

                //    GridView view_gridSelectedZoneOption = (GridView)gridSelectedZoneOption.Views[0];

                //    dtSelectedGridCarOption.Rows.Clear();

                //    string planid = comboPolicyCode.SelectedItem.ToString();
                //    OracleDataTable carOraDt = bizService.GetGridSelectedOptionByPolicy("C", planid);
                //    #region Print dt
                //    PrintDebug(carOraDt);
                //    #endregion

                //    //cell에 보여질 값 설정
                //    foreach (DataRow _row2 in carOraDt.Rows)
                //    {
                //        //DataRow 추가
                //        DataRow _carDr = dtSelectedGridCarOption.NewRow();

                //        _carDr["caroption_car_number"] = _row2["VEHCID"];
                //        _carDr["caroption_car_type"] = _row2["VHCTON"];
                //        _carDr["caroption_max_landing_num"] = _row2["LMSPNO"];
                //        _carDr["caroption_arrival"] = _row2["FINTWA"];
                //        _carDr["caroption_working"] = _row2["FONDUT"];
                //        _carDr["caroption_region"] = _row2["DIST01"];
                //        _carDr["caroption_mix_region"] = _row2["LMMDS1"];

                //        foreach (DataRow dr in caroption_region_reorder_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_region_reorder"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["REAL01"].ToString()))
                //            {
                //                _carDr["caroption_region_reorder"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        _carDr["caroption_area"] = _row2["DIST02"];
                //        _carDr["caroption_mix_area"] = _row2["LMMDS02"];

                //        foreach (DataRow dr in caroption_area_assign_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_area_assign"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["ALLO01"].ToString()))
                //            {
                //                _carDr["caroption_area_assign"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        foreach (DataRow dr in caroption_area_assign_reorder_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_area_assign_reorder"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["REAL02"].ToString()))
                //            {
                //                _carDr["caroption_area_assign_reorder"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        foreach (DataRow dr in caroption_remainder_assign_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_remainder_assign"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["ALLO03"].ToString()))
                //            {
                //                _carDr["caroption_remainder_assign"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        foreach (DataRow dr in caroption_remainder_assign_reorder_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            _carDr["caroption_remainder_assign_reorder"] = "default";

                //            if (dr["VALES"].ToString().Equals(_row2["REAL03"].ToString()))
                //            {
                //                _carDr["caroption_remainder_assign_reorder"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        _carDr["caroption_1"] = _row2["ATTR11"];
                //        _carDr["caroption_2"] = _row2["ATTR12"];
                //        _carDr["caroption_3"] = _row2["ATTR13"];
                //        _carDr["caroption_4"] = _row2["ATTR14"];
                //        _carDr["caroption_5"] = _row2["ATTR15"];
                //        _carDr["caroption_6"] = _row2["ATTR16"];
                //        _carDr["caroption_7"] = _row2["ATTR17"];
                //        _carDr["caroption_8"] = _row2["ATTR18"];
                //        _carDr["caroption_9"] = _row2["ATTR19"];
                //        _carDr["caroption_10"] = _row2["ATTR20"];

                //        //Data 추가
                //        dtSelectedGridCarOption.Rows.Add(_carDr);
                //    }

                //    //DataSource 지정
                //    gridSelectedCarOption.DataSource = dtSelectedGridCarOption;
                //    gridSelectedCarOption.RefreshDataSource();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //#endregion gridCarOption

                //#region gridLandingOption
                //try
                //{
                //    //Parameter ( control, 가져올 caroption 컬럼이름, 현재 보여질 컬럼이름, 데이터 테이블 )
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime", list_hours);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime1", list_minute_second);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_startTime2", list_minute_second);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime", list_hours);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime1", list_minute_second);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Adhesive_required_endTime2", list_minute_second);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "landing_OTD_compliance", "OTD_compliance", dtGridLandingOption);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime", list_hours);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime1", list_minute_second);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_startTime2", list_minute_second);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime", list_hours);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime1", list_minute_second);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "OffTime_endTime2", list_minute_second);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime", list_hours);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime1", list_minute_second);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "Fixed_handlingTime2", list_minute_second);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime", list_hours);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime1", list_minute_second);
                //    //SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "change_handlingTime2", list_minute_second);

                //    string query_type = "";
                //    string key_id = "VRO001_DST074_DSTC21";
                //    Dispatcher_manualHandling_ComboboxOraDt = bizService.GetGridSelectedOptionByPolicy_combobox(query_type, key_id);
                //    SetRepositoryItemComboBoxBindData(gridSelectedLandingOption, "NAMES", "Dispatcher_manualHandling", Dispatcher_manualHandling_ComboboxOraDt);

                //    SetRepositoryItemComboBoxBindDataYN(gridSelectedLandingOption, "landing_Whether_assigned", "Whether_assigned", dtGridLandingOption);

                //    GridView view_gridSelectedLandingOption = (GridView)gridSelectedLandingOption.Views[0];

                //    dtSelectedGridLandingOption.Rows.Clear();

                //    string planid = comboPolicyCode.SelectedItem.ToString();
                //    OracleDataTable landingOraDt = bizService.GetGridSelectedOptionByPolicy("A", planid);
                //    #region Print dt
                //    PrintDebug(landingOraDt);
                //    #endregion

                //    //cell에 보여질 값 설정
                //    foreach (DataRow _row3 in landingOraDt.Rows)
                //    {
                //        //DataRow 추가
                //        DataRow landingDr = dtSelectedGridLandingOption.NewRow();

                //        int nMaxCol = landingOraDt.Columns.Count;
                //        string[] temp = new string[nMaxCol];
                //        for (int pos = 0; pos < landingOraDt.Columns.Count; pos++)
                //            temp[pos] = _row3[pos].ToString();

                //        string temp2 = "";
                //        temp2 = _row3["STOPID"].ToString();
                //        landingDr["landingoption_number"] = _row3["STOPID"];
                //        landingDr["landingoption_name"] = _row3["STOPNM"];
                //        landingDr["landingoption_restrict"] = _row3["VHCTON"].ToString() + "톤 " + _row3["VHCTYP"].ToString() == "0" ? "카고" : "탑";
                //        //TKXADS18_THR 조인정보, 현재 쿼리에서 컬럼 없음. 프로시져 완료 후 가능
                //        landingDr["landingoption_box_number"] = _row3["BOXCNT"];
                //        landingDr["landingoption_box_rating"] = _row3["BOXLEV"];
                //        landingDr["landingoption_weight"] = _row3["WEIGHT"];
                //        landingDr["landingoption_volumn"] = _row3["VOLUME"];
                //        landingDr["landingoption_PLT"] = _row3["PLTCNT"];

                //        //DB 데이터 시간 형식 ( 01:00:00 )
                //        landingDr["Adhesive_required_startTime"] = _row3["TIMEFR"];
                //        //string time = _row3["TIMEFR"].ToString();
                //        //if (!(time == null || time.Equals("") || time.Equals(" ")))//업데이트 할때 null 인 데이터는 " " 공백 넣어서 처리, 데이터 받을때도 공백으로 받음...디비 프로시져에서 처리시 공백 요청
                //        //{
                //        //    landingDr["Adhesive_required_startTime"] = time.Substring(0, 2);
                //        //    landingDr["Adhesive_required_startTime1"] = time.Substring(3, 2);
                //        //    landingDr["Adhesive_required_startTime2"] = time.Substring(6, 2);
                //        //}

                //        landingDr["Adhesive_required_endTime"] = _row3["TIMETO"];
                //        //string time1 = _row3["TIMETO"].ToString();
                //        //if (!(time1 == null || time1.Equals("") || time1.Equals(" ")))
                //        //{
                //        //    landingDr["Adhesive_required_endTime"] = time1.Substring(0, 2);
                //        //    landingDr["Adhesive_required_endTime1"] = time1.Substring(3, 2);
                //        //    landingDr["Adhesive_required_endTime2"] = time1.Substring(6, 2);
                //        //}

                //        landingDr["OTD_compliance"] = _row3["OTDOPT"];

                //        landingDr["OffTime_startTime"] = _row3["OFFTFR"];
                //        //string time2 = _row3["OFFTFR"].ToString();
                //        //if (!(time2 == null || time2.Equals("") || time2.Equals(" ")))
                //        //{
                //        //    landingDr["OffTime_startTime"] = time2.Substring(0, 2);
                //        //    landingDr["OffTime_startTime1"] = time2.Substring(3, 2);
                //        //    landingDr["OffTime_startTime2"] = time2.Substring(6, 2);
                //        //}

                //        landingDr["OffTime_endTime"] = _row3["OFFTTO"];
                //        //string time3 = _row3["OFFTTO"].ToString();
                //        //if (!(time3 == null || time3.Equals("") || time3.Equals(" ")))
                //        //{
                //        //    landingDr["OffTime_endTime"] = time3.Substring(0, 2);
                //        //    landingDr["OffTime_endTime1"] = time3.Substring(3, 2);
                //        //    landingDr["OffTime_endTime2"] = time3.Substring(6, 2);
                //        //}

                //        landingDr["Fixed_handlingTime"] = _row3["PREPTM"];
                //        //string time4 = _row3["PREPTM"].ToString();
                //        //if (!(time4 == null || time4.Equals("") || time4.Equals(" ")))
                //        //{
                //        //    landingDr["Fixed_handlingTime"] = time4.Substring(0, 2);
                //        //    landingDr["Fixed_handlingTime1"] = time4.Substring(3, 2);
                //        //    landingDr["Fixed_handlingTime2"] = time4.Substring(6, 2);
                //        //}

                //        landingDr["change_handlingTime"] = _row3["UNLDTM"];
                //        //string time5 = _row3["UNLDTM"].ToString();
                //        //if (!(time5 == null || time5.Equals("") || time5.Equals(" ")))
                //        //{
                //        //    landingDr["change_handlingTime"] = time5.Substring(0, 2);
                //        //    landingDr["change_handlingTime1"] = time5.Substring(3, 2);
                //        //    landingDr["change_handlingTime2"] = time5.Substring(6, 2);
                //        //}

                //        //해당 컬럼 없음. 컬럼 다시 확인.
                //        landingDr["Recent_delivery_vehicleNumber"] = _row3["HVEHID"];
                //        landingDr["Latest_arrivalTime"] = _row3["HARRTM"];

                //        foreach (DataRow dr in Dispatcher_manualHandling_ComboboxOraDt.Rows)
                //        {
                //            //현재 DB와 데이터가 맞지 않는다면 default로 값 입력, 변경 가능
                //            landingDr["Dispatcher_manualHandling"] = "2";

                //            if (dr["VALES"].ToString().Equals(_row3["MALTYP"].ToString()))
                //            {
                //                landingDr["Dispatcher_manualHandling"] = dr["NAMES"];
                //                break;
                //            }
                //        }

                //        landingDr["Whether_assigned"] = _row3["FALLOC"];

                //        //Data 추가
                //        dtSelectedGridLandingOption.Rows.Add(landingDr);
                //    }

                //    //DataSource 지정
                //    gridSelectedLandingOption.DataSource = dtSelectedGridLandingOption;
                //    gridSelectedLandingOption.RefreshDataSource();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //#endregion gridLandingOption

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.screenManager.CloseWaitForm();
            }
        }

        #region variable
        //policy table Policy 코드
        string policy_code = "";
        //string scenario_id = "";

        //신규여부 flag
        bool new_policy_save = false;

        //checked policy code list
        List<string> list_policy_code = new List<string>();

        DataTable dtPolicy;
        DataTable dtGridEngineOption;
        DataTable dtGridZoneOption;
        DataTable dtGridCarOption;
        DataTable dtGridLandingOption;
        DataTable dtSelectedGridEngineOption;
        DataTable dtSelectedGridZoneOption;
        DataTable dtSelectedGridCarOption;
        DataTable dtSelectedGridLandingOption;

        //시간관련 리스트
        List<string> list_hours;
        List<string> list_minute_second;

        IApplicationContext _applicationContext = ContextRegistry.GetContext();
        BizService bizService;

        //콤보박스 테이블
        //엔진
        OracleDataTable Zone_ComboboxOraDt;
        OracleDataTable Distance_Calc_ComboboxOraDt;
        OracleDataTable Speed_ComboboxOraDt;
        //권역
        OracleDataTable Priority_ComboboxOraDt;
        OracleDataTable zone_Distance_Calc_ComboboxOraDt;
        OracleDataTable zone_Speed_ComboboxOraDt;
        //차량
        OracleDataTable caroption_region_reorder_ComboboxOraDt;
        OracleDataTable caroption_area_assign_ComboboxOraDt;
        OracleDataTable caroption_area_assign_reorder_ComboboxOraDt;
        OracleDataTable caroption_remainder_assign_ComboboxOraDt;
        OracleDataTable caroption_remainder_assign_reorder_ComboboxOraDt;
        //착지
        OracleDataTable Dispatcher_manualHandling_ComboboxOraDt;
        #endregion
    }
}