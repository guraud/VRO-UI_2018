using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Devart.Data.Oracle;
using VROUI.objectPool;

namespace VROUI.Services
{
    public class BizService : IApplicationContextAware
    {
        IApplicationContext _applicationContext = null;
        public DbService _dbService = null;

        #region IApplicationContextAware 멤버

        public IApplicationContext ApplicationContext
        {
            set { _applicationContext = value; }
        }

        #endregion

        public void Initialize()
        {
            _dbService = (DbService)_applicationContext.GetObject("DbService");
        }

        public OracleDataTable GetData_HST()
        {
            OracleDataTable dataTable = _dbService.ExecuteQuery("SELECT * FROM TKXADS18_HST");
            return dataTable;
        }

        public OracleDataTable GetInBound()
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_INBOUND("PLAN2", "KIM");
            return dataTable;
        }

        public OracleDataTable GetScenarioList()
        {
            // Parameter ( 조회타입, 사업부코드, 법인코드, 공장코드,마스터ID, USERID)
            OracleDataTable dataTable = _dbService.SP_VRO_GET_SCENARIO_LIST(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, ApplicationKey.userId);
            //OracleDataTable dataTable = _dbService.ExecuteQuery("SELECT * FROM TKXADS18_PLC WHERE DELMAK='N'");
            return dataTable;
        }

        public OracleDataTable GetPolicyList()
        {
            // Parameter ( 조회타입, 법인코드, 공장코드,마스터ID, USERID)
            OracleDataTable dataTable = _dbService.SP_VRO_GET_POLICY_LIST("N", ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.userId);
            return dataTable;
        }

        internal OracleDataTable GetDeletePolicyList()
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_POLICY_LIST("D", ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.userId);
            return dataTable;
        }

        public OracleDataTable GetOptionList(string type)
        {
            //string planDate = "20131001";
            string planid = ApplicationKey.plan_id2[0] + '↑' + ApplicationKey.plan_id2[1] + '↑' + ApplicationKey.plan_id2[2];
            OracleDataTable dataTable = _dbService.SP_VRO_GET_OPTION4_ALL_LIST(type, ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, planid, ApplicationKey.plan_st, ApplicationKey.plan_st, ApplicationKey.userId);
            //OracleDataTable dataTable = _dbService.ExecuteQuery("SELECT * FROM TKXADS18_ENG");
            return dataTable;
        }

        //선택 policy 관련 옵션 리스트
        public OracleDataTable GetGridSelectedOptionByPolicy(string type, string policy_code)
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_OPTION4_BY_POLICY(type, ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, policy_code, ApplicationKey.plan_id, ApplicationKey.plan_st, ApplicationKey.userId); 
            return dataTable;
        }

        //선택 시나리오 적용
        public OracleDataTable GetGridSelectedOptionByScenario(string type, string policy_code, string plan_id)
        {
            //OracleDataTable dataTable = _dbService.ExecuteQuery("SELECT * FROM TKXADS18_ENG WHERE POLYID ='PLC_DEFAULT'");
            OracleDataTable dataTable = _dbService.SP_VRO_GET_OPTION4_BY_PLANPOLY(type, ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, policy_code, ApplicationKey.plan_st, plan_id, ApplicationKey.userId);
            return dataTable;
        }
            
        //선배차 차량 관리 화면 차량 리스트
        internal OracleDataTable GetCarUsageList()
        {
            //OracleDataTable dataTable = _dbService.ExecuteQuery("SELECT * FROM TKXADS18_CAR");
            OracleDataTable dataTable = _dbService.SP_VRO_GET_CAR_USAGE(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.current_plan_id, "", ApplicationKey.plan_st, ApplicationKey.userId); 
            return dataTable;
        }

        //선배차 차량 관리 화면 차량 리스트
        internal OracleDataTable GetCarList()
        {
            //OracleDataTable dataTable = _dbService.ExecuteQuery("SELECT * FROM TKXADS18_CAR");
            OracleDataTable dataTable = _dbService.SP_VRO_GET_CAR_LIST(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.current_plan_id, "", ApplicationKey.plan_st, ApplicationKey.userId); 
            return dataTable;
        }

        //기존 이름으로 저장
        internal string updatePolicy(string policy_id, string policy_name, string policy_desc, string policy_code, string plan_id, string[] enginArr, string[] ZoneArr, string[] CarArr, string[] landingArr)
        {
            string result = _dbService.SP_VRO_UPSERT_POLICY("U", ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, plan_id
                , " ", ApplicationKey.plan_st, ApplicationKey.userId, policy_id, policy_name, policy_desc, enginArr, ZoneArr, CarArr, landingArr);
            return result;
        }

        // Policy row 삭제
        internal string deletPolicy(string type,string policy_id)
        {
            string result = _dbService.SP_VRO_DELETE_POLICY(type, ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, policy_id, ApplicationKey.userId);
            return result;
        }

        //선배차 차량 관리 화면 차량 리스트
        internal OracleDataTable GetPreOrderCarList()
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_PREORDERCAR_INFO(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, " ", ApplicationKey.plan_st, ApplicationKey.userId);
            return dataTable;
        }

        //배차 실행 관리 summary
        internal OracleDataTable GetMakeDispatchSummary()
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_MAKE_DISPATCH_SUM(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, " ", ApplicationKey.plan_st, ApplicationKey.userId);
            return dataTable;
        }

        //선배차 차량 관리 화면 저장 버튼
        internal string SaveCarList(string[] car_arr)
        {
            DateTime now = DateTime.Now; 
            string planDT = now.Year.ToString()+now.Month.ToString()+now.Day.ToString();

            //test (조건절에 planDT 입력, 현재데이터 20131001)
            planDT = "20131001";

            string result = _dbService.SP_VRO_UPDATE_PREORDERCAR_INFO(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, planDT, ApplicationKey.plan_st, ApplicationKey.userId, car_arr);
            return result;
        }

        internal OracleDataTable GetAutoallocate()
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_AUTOALLOCATE_INFO(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, "", ApplicationKey.plan_st, ApplicationKey.userId);
            return dataTable;
        }

        //가확정 차량리스트
        internal OracleDataTable GetTmpcarallocCarlist(string policyID)
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_TMPCARALLOC_CARLIST(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id2[0], " ", ApplicationKey.plan_st, ApplicationKey.userId, policyID);
            return dataTable;
        }

        internal string updateScenario( string plan_id, string[] enginArr, string[] ZoneArr, string[] carArr, string[] landingArr)
        {
            string result = _dbService.SP_VRO_UPDATE_SCENARIO( ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id
                                                             , plan_id, " "/*plan_date*/, ApplicationKey.plan_st, ApplicationKey.userId
                                                             , enginArr, ZoneArr, carArr, landingArr);
            return result;
        }

        //신규로 생성 후 기존 이름으로 저장(신규는 type C, 다른이름은 type R)
        internal string insertPolicy(string type, string plan_id, string policy_code, string policy_name, string policy_desc
            , string[] enginArr, string[] ZoneArr, string[] carArr, string[] landingArr)
        {
            string result = _dbService.SP_VRO_UPSERT_POLICY(type, ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, plan_id
                , " ", ApplicationKey.plan_st, ApplicationKey.userId, policy_code, policy_name, policy_desc, enginArr, ZoneArr, carArr, landingArr);
            return result;
        }

        //엔진옵션 콤보박스 리스트(권역정렬)
        internal OracleDataTable GetGridSelectedOptionByPolicy_combobox(string query_type, string key_id)
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_COMMON_CODE(query_type, key_id, ApplicationKey.userId);
            return dataTable;
        }

        //신규버튼 클릭시 디폴트 저장옵션 리스트
        internal OracleDataTable GetGridSelectedOptionDefault(string type)
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_OPTION4_DEFAULT(type, ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, "", "", ApplicationKey.plan_st, ApplicationKey.userId);
            return dataTable;
        }

        //이동 속도 콤보박스
        internal OracleDataTable GetGridSelectedOptionByPolicy_movingSpeed_combobox()
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_MOVING_SPEED(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, "", ApplicationKey.plan_st, ApplicationKey.userId);
            return dataTable;
        }

        //가확정 할당,미할당
        internal OracleDataTable sp_vro_get_caralloc_phalist(string type, string carNumber)
        {
            OracleDataTable dataTable = _dbService.SP_VRO_GET_CARALLOC_PHALIST(type, ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.current_plan_id, "", ApplicationKey.plan_st, ApplicationKey.userId, carNumber);
            return dataTable;
        }

        //자동배차 실행 버튼
        internal string sp_vro_update_caralloc_phase(string step)
        {
            string result = _dbService.SP_VRO_UPDATE_CARALLOC_PHASE(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, " ", ApplicationKey.plan_st, ApplicationKey.userId, step, "");
            return result;
        }

        //파라메타 설정 적용
        internal string sp_vro_apply_policy(string policy_id)
        {
            string result = _dbService.SP_VRO_APPLY_POLICY(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, ApplicationKey.plan_st, policy_id, ApplicationKey.userId);
            return result;
        }

        //시나리오 적용 버튼
        internal string sp_vro_update_plan_header(string step, string polyId)
        {
            string result = _dbService.SP_VRO_UPDATE_PLAN_HEADER(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, " ", ApplicationKey.plan_st, ApplicationKey.userId, step, polyId);
            return result;
        }

        //초기화
        internal string sp_vro_initialize_plan()
        {
            string result = _dbService.SP_VRO_INITIALIZE_PLAN(ApplicationKey.company, ApplicationKey.center, ApplicationKey.mst_id, ApplicationKey.plan_id, " ", ApplicationKey.plan_st, ApplicationKey.userId);
            return result;
        }
    }
}
