using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Devart.Data.Oracle;
using System.Data;

namespace VROUI.Services
{
    public class DbService
    {
        string _server;
        int _port = 1521;
        string _sid;
        string _userId;
        string _password;
        int _maxPoolSize = 50;
        int _connectionTimeout = 30;

        OracleConnection _connection = null;
        OracleConnectionStringBuilder _connectionInfo = new OracleConnectionStringBuilder();

        # region DbService Properties
        public string Server
        {
            get { return _server; } 
            set { _server = value; }
        }
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public int MaxPoolSize
        {
            get { return _maxPoolSize; }
            set { _maxPoolSize = value; }
        }
        public int ConnectionTimeout
        {
            get { return _connectionTimeout; }
            set { _connectionTimeout = value; }
        }
        #endregion

        public void Initialize()
        {
            _connectionInfo.Direct = true;
            _connectionInfo.Server = _server;
            _connectionInfo.Port = _port;
            _connectionInfo.Sid = _sid;
            _connectionInfo.UserId = _userId;
            _connectionInfo.Password = _password;
            _connectionInfo.MaxPoolSize = _maxPoolSize;
            _connectionInfo.ConnectionTimeout = _connectionTimeout;

            //현재는 초기에 Connect 시도함. 필요시 화면에서 Connect 하는 메뉴 추가해서 사용
            Connect();
        }

        public void Connect()
        {
            _connection = new OracleConnection(_connectionInfo.ConnectionString);
        }

        public OracleDataTable ExecuteQuery(string query)
        {
            OracleDataTable dataTable = new OracleDataTable();

            _connection.Open();

            try
            {
                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = new OracleCommand(query, _connection);

                adapter.Fill(dataTable);
            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return dataTable;
        }

        //public int ExecuteNonQuery(string query)
        //{
        //    int result = 0;

        //    _connection.Open();

        //    try
        //    {
        //        OracleCommand command = new OracleCommand();
        //        command.CommandText = query;
        //        command.Connection = _connection;

        //        result = command.ExecuteNonQuery();
        //    }
        //    catch (OracleException ex)
        //    {
        //        Console.WriteLine("Exception occurs: {0}", ex.Message);
        //    }
        //    finally
        //    {
        //        _connection.Close();
        //    }

        //    return result;
        //}

        public OracleDataTable SP_VRO_GET_INBOUND(string planId, string userId)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new Devart.Data.Oracle.OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_WORKFLOW_MANAGER.SP_VRO_GET_INBOUND";

                command.Parameters.Add("P_PLAN_ID", OracleDbType.VarChar, planId, ParameterDirection.Input);
                command.Parameters.Add("P_USER_ID", OracleDbType.VarChar, userId, ParameterDirection.Input);


                command.ExecuteNonQuery();


            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return dataTable;
        }

        
        public OracleDataTable SP_VRO_GET_SCENARIO_LIST(string corporation, string plant, string master_id, string planId, string user_id)
        {
            //OracleDataTable dataTable = new OracleDataTable();
            //OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            //_connection.Open();

            //try
            //{
            //    OracleCommand command = new OracleCommand();
            //    command.Connection = _connection;

            //    command.CommandType = System.Data.CommandType.StoredProcedure;

            //    command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_SCENARIO_LIST";
                
            //    command.Parameters.Add("P_CORPORATION", OracleDbType.VarChar, corporation, ParameterDirection.Input);
            //    command.Parameters.Add("P_PLANT", OracleDbType.VarChar, plant, ParameterDirection.Input);
            //    command.Parameters.Add("P_MASTER_ID", OracleDbType.VarChar, master_id, ParameterDirection.Input);
            //    command.Parameters.Add("P_PLAN_ID", OracleDbType.VarChar, planId, ParameterDirection.Input);
            //    command.Parameters.Add("P_USER_ID", OracleDbType.VarChar, user_id, ParameterDirection.Input);
            //    command.Parameters.Add("C_SCENARIO_LIST", OracleDbType.Cursor, ParameterDirection.InputOutput);

            //    //command.Parameters.Add("1", OracleDbType.Integer, ParameterDirection.Output);

            //    command.ExecuteNonQuery();

            //    OracleCursor oraCursor = (OracleCursor)command.Parameters["C_SCENARIO_LIST"].Value;
            //    //int a = (int)command.Parameters["1"].Value ;

            //    oraDataAdapter.Fill(dataTable, oraCursor);

            //}
            //catch (OracleException ex)
            //{
            //    Console.WriteLine("Exception occurs: {0}", ex.Message);
            //}
            //finally
            //{
            //    _connection.Close();
            //}

            //return dataTable;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT '0001' SIMULATION_ID , '시나리오 1' DESC01");
            sql.Append("  FROM dual");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT '0002' SIMULATION_ID , '시나리오 2' DESC01");
            sql.Append("  FROM dual");
            sql.Append(" UNION ALL");
            sql.Append(" SELECT '0003' SIMULATION_ID , '시나리오 3' DESC01");
            sql.Append("  FROM dual");
            return ExecuteQuery(sql.ToString());

        }
        //정책리스트 조회 (TH_VRO_ENG_POLICY)
        public OracleDataTable SP_VRO_GET_POLICY_LIST(string type, string corporation, string plant, string master_id, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();

            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "SP_VRO_GET_POLICY_LIST";
                command.Parameters.Add("p_delete_flag", OracleDbType.VarChar, type, ParameterDirection.Input);
                command.Parameters.Add("p_company", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_center", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_mst_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("c_policy_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_policy_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return dataTable;
            
        }

        public OracleDataTable SP_VRO_GET_OPTION4_ALL_LIST(string optionType, string corporation, string plant, string master_id ,string plan_id, string plan_date, string plan_version, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_OPTION4_ALL_LIST";
                command.Parameters.Add("p_option_type", OracleDbType.VarChar, optionType, ParameterDirection.Input);
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar,plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id,ParameterDirection.Input);
                command.Parameters.Add("c_option_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                //command.Parameters.Add("1", OracleDbType.Integer, ParameterDirection.Output);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_option_list"].Value;
                //int a = (int)command.Parameters["1"].Value ;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public OracleDataTable SP_VRO_GET_OPTION4_DEFAULT(string optionType, string corporation, string plant, string master_id, string plan_id, string plan_data, string plan_version, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_OPTION4_DEFAULT";
                command.Parameters.Add("p_option_type", OracleDbType.VarChar, optionType, ParameterDirection.Input);
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_data, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("c_option_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                //command.Parameters.Add("1", OracleDbType.Integer, ParameterDirection.Output);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_option_list"].Value;
                //int a = (int)command.Parameters["1"].Value ;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }
        //기준정보 조회 (TH_VRO_MST_USER_OPTION/TH_VRO_MST_VEHICLE/TH_VRO_MST_DISTRICT/TH_VRO_MST_STOP)
        public OracleDataTable SP_VRO_GET_OPTION4_BY_POLICY(string optionType, string corporation, string plant, string master_id, string policy_id, string plan_id, string plan_version, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();

            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "SP_VRO_GET_OPTION4_BY_POLICY";
                command.Parameters.Add("p_option_type", OracleDbType.VarChar, optionType, ParameterDirection.Input);
                command.Parameters.Add("p_company", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_center", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_mst_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_policy_id", OracleDbType.VarChar, policy_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_st", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("c_policy_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_policy_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return dataTable;
        }

        // 추가
        public OracleDataTable SP_VRO_GET_OPTION4_BY_PLANPOLY(string optionType, string corporation, string plant, string master_id, string policy_id, string plan_version, string plan_id, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_OPTION4_BY_PLANPOLY";
                command.Parameters.Add("p_option_type", OracleDbType.VarChar, optionType, ParameterDirection.Input);
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_policy_id", OracleDbType.VarChar, policy_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("c_option_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                //command.Parameters.Add("1", OracleDbType.Integer, ParameterDirection.Output);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_option_list"].Value;
                //int a = (int)command.Parameters["1"].Value ;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public string SP_VRO_UPDATE_SCENARIO( string corporation, string plant, string master_id
                                            , string plan_id, string plan_date, string plan_version, string user_id
                                            , string[] array_eng, string[] array_zne, string[] array_car, string[] array_arr)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_UPDATE_SCENARIO";
                command.Parameters.Add("P_CORPORATION", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("P_PLANT", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("P_MASTER_ID", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("P_PLAN_ID", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("P_PLAN_DATE", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("P_PLAN_VERSION", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("P_USER_ID", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("P_ARRAY_ENG", OracleDbType.VarChar, array_eng, ParameterDirection.Input);
                command.Parameters.Add("P_ARRAY_ZNE", OracleDbType.VarChar, array_zne, ParameterDirection.Input);
                command.Parameters.Add("P_ARRAY_CAR", OracleDbType.VarChar, array_car, ParameterDirection.Input);
                command.Parameters.Add("P_ARRRY_ARR", OracleDbType.VarChar, array_arr, ParameterDirection.Input);

                command.Parameters.Add("P_RTN_VALUE", OracleDbType.VarChar, ParameterDirection.Output);

                command.ExecuteNonQuery();

                result = (string)command.Parameters["P_RTN_VALUE"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public string SP_VRO_UPSERT_POLICY(string upsert_type, string corporation, string plant, string master_id, string plan_id
            , string plan_date, string plan_version, string user_id, string policy_code, string policy_name, string policy_desc
            , string[] array_eng, string[] array_zne, string[] array_car, string[] array_arr)
        {
            string result ="";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_UPSERT_POLICY";
                command.Parameters.Add("P_UPSERT_TYPE", OracleDbType.VarChar, upsert_type, ParameterDirection.Input);
                command.Parameters.Add("P_CORPORATION", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("P_PLANT", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("P_MASTER_ID", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("P_PLAN_ID", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("P_PLAN_DATE", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("P_PLAN_VERSION", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("P_USER_ID", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("P_POLICY_ID", OracleDbType.VarChar, policy_code, ParameterDirection.Input);
                command.Parameters.Add("P_POLICY_NAME", OracleDbType.VarChar, policy_name, ParameterDirection.Input);
                command.Parameters.Add("P_POLICY_DESC", OracleDbType.VarChar, policy_desc, ParameterDirection.Input);
                command.Parameters.Add("P_ARRAY_ENG", OracleDbType.VarChar, array_eng, ParameterDirection.Input);
                command.Parameters.Add("P_ARRAY_ZNE", OracleDbType.VarChar, array_zne, ParameterDirection.Input);
                command.Parameters.Add("P_ARRAY_CAR", OracleDbType.VarChar, array_car, ParameterDirection.Input);
                command.Parameters.Add("P_ARRRY_ARR", OracleDbType.VarChar, array_arr, ParameterDirection.Input);

                command.Parameters.Add("P_RTN_VALUE", OracleDbType.VarChar, ParameterDirection.Output);

                command.ExecuteNonQuery();

                result = (string)command.Parameters["P_RTN_VALUE"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public string SP_VRO_DELETE_POLICY(string delete_type, string corporation, string plant, string master_id, string policy_id, string user_id)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_DELETE_POLICY";
                command.Parameters.Add("p_delete_type", OracleDbType.VarChar, delete_type, ParameterDirection.Input);
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_policy_id", OracleDbType.VarChar, policy_id, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);

                command.Parameters.Add("rtn_value", OracleDbType.VarChar, ParameterDirection.Output);

                command.ExecuteNonQuery();

                result = (string)command.Parameters["rtn_value"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public string SP_VRO_APPLY_POLICY(string corporation, string plant, string master_id, string plan_id, string plan_version, string policy_id, string user_id)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_APPLY_POLICY";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_policy_id", OracleDbType.VarChar, policy_id, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);

                command.Parameters.Add("rtn_value", OracleDbType.VarChar, ParameterDirection.Output);

                command.ExecuteNonQuery();

                result = (string)command.Parameters["rtn_value"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public OracleDataTable SP_VRO_GET_PREORDERCAR_INFO(string corporation,string plant, string master_id,string plan_id, string plan_date, string planST, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_PREORDERCAR_INFO";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input); 
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("c_car_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                //command.Parameters.Add("1", OracleDbType.Integer, ParameterDirection.Output);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_car_list"].Value;
                //int a = (int)command.Parameters["1"].Value ;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public string SP_VRO_UPDATE_PREORDERCAR_INFO(string corporation, string plant, string master_id, string plan_id, string plan_date, string plan_version, string user_id, string[] car_arr)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_UPDATE_PREORDERCAR_INFO";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_array_car", OracleDbType.VarChar, car_arr, ParameterDirection.Input);
                //command.Parameters.Add("p_precar_set", OracleDbType.Cursor, dt, ParameterDirection.Input);

                command.Parameters.Add("rtn_value", OracleDbType.VarChar, ParameterDirection.Output);


                command.ExecuteNonQuery();

                result = (string)command.Parameters["rtn_value"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public OracleDataTable SP_VRO_GET_MAKE_DISPATCH_SUM(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id)
        {
            //OracleDataTable dataTable = new OracleDataTable();
            //OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            //_connection.Open();

            //try
            //{
            //    OracleCommand command = new OracleCommand();
            //    command.Connection = _connection;

            //    command.CommandType = System.Data.CommandType.StoredProcedure;

            //    command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_MAKE_DISPATCH_SUM";
            //    command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
            //    command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
            //    command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
            //    command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
            //    command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

            //    command.ExecuteNonQuery();

            //    OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

            //    oraDataAdapter.Fill(dataTable, oraCursor);

            //}
            //catch (OracleException ex)
            //{
            //    Console.WriteLine("Exception occurs: {0}", ex.Message);
            //}
            //finally
            //{
            //    _connection.Close();
            //}
            //return dataTable;

            StringBuilder sql = new StringBuilder();

            sql.Append(" SELECT A.DIVISN, A.CORPOR, A.PLANT , A.MST_ID, A.PLANID, A.PLANST, '' CHOICE, '시나리오 '||SUBSTR(A.PLANID, -1) POLYID,");
            sql.Append("        '자투리 할당 (엔진완료)' PLANPH, TO_CHAR(COUNT( DISTINCT RTRIM(A.VHCCNT) )) ||' 대 ( '|| MAX( A.TOTVHC)|| ' 대 )' VHCCNT,");
            sql.Append("        ROUND( SUM( A.NALCNT ) / MAX( A.STPCNT ) * 100, 2 ) ||' (%)' NALRTO, ROUND( AVG( A.ALDRTO ), 2 ) ||' (%)' ALDRTO,");
            sql.Append("        ROUND( AVG( A.UNTCST ), 2 ) ||' (원/box)' PERCST");
            sql.Append("   FROM (");
            sql.Append("           SELECT A.DIVISN, A.CORPOR, A.PLANT , A.MST_ID, A.PLANID, A.PLANST, MAX( DISTINCT A.VEHCID ) VHCCNT,");
            sql.Append("                  ( SELECT COUNT(1)");
            sql.Append("                      FROM TH_IN_CAR ");
            sql.Append("                     WHERE TRIM(divisn)  = A.DIVISN");
            sql.Append("                       AND CORPOR  = A.CORPOR");
            sql.Append("                       AND PLANT   = A.PLANT");
            sql.Append("                       AND MST_ID  = A.MST_ID");
            sql.Append("                       AND PLANID  = A.PLANID");
            sql.Append("                       AND PLANST  = A.PLANST");
            sql.Append("                       AND ATTR11  > 0");
            sql.Append("                  ) TOTVHC,");
            sql.Append("                  COUNT( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN 1 ELSE NULL END ) NALCNT,");
            sql.Append("                  COUNT( CASE WHEN RTRIM(A.VEHCID) IS NOT NULL AND A.TRIPNO BETWEEN 1 AND 98 THEN 1 ELSE NULL END ) ALDCNT,");
            sql.Append("                  ( SELECT  COUNT( CASE WHEN RTRIM(VEHCID) IS NULL ");
            sql.Append("                                        THEN 1 ");
            sql.Append("                                        ELSE ( CASE WHEN RTRIM(VEHCID) IS NOT NULL AND TRIPNO BETWEEN 1 AND 98 ");
            sql.Append("                                                    THEN 1 ");
            sql.Append("                                                    ELSE NULL ");
            sql.Append("                                                END ) ");
            sql.Append("                                   END ) PLTSUM");
            sql.Append("                      FROM TKXADS18_PHA ");
            sql.Append("                     WHERE TRIM(divisn)  = A.DIVISN");
            sql.Append("                       AND CORPOR  = A.CORPOR");
            sql.Append("                       AND PLANT   = A.PLANT");
            sql.Append("                       AND MST_ID  = A.MST_ID");
            sql.Append("                       AND PLANID  = A.PLANID");
            sql.Append("                       AND PLANST  = A.PLANST");
            sql.Append("                     GROUP BY  DIVISN, CORPOR, PLANT , MST_ID, PLANID, PLANST");
            sql.Append("                   ) STPCNT,");
            sql.Append("                   SUM( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN A.PLTCNT ELSE 0 END ) NLDPLT,");
            sql.Append("                   SUM( CASE WHEN RTRIM(A.VEHCID) IS NOT NULL AND A.TRIPNO BETWEEN 1 AND 98 THEN A.PLTCNT ELSE 0 END ) ALDPLT,");
            sql.Append("                   MAX( A.LIMPLT ) MAXPLT,");
            sql.Append("                   ROUND( SUM( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN A.PLTCNT ELSE 0 END ) / MAX( NVL(A.LIMPLT,1) ) * 100, 3 ) NLDRTO,");
            sql.Append("                   ROUND( SUM( CASE WHEN RTRIM(A.VEHCID) IS NOT NULL AND A.TRIPNO BETWEEN 1 AND 98 THEN A.PLTCNT ELSE 0 END ) / MAX(  DECODE(NVL(A.LIMPLT,1), 0, 1, NVL(A.LIMPLT,1)) ) * 100, 3 ) ALDRTO,");
            sql.Append("                   ( SELECT SUM( PLTCNT ) PLTSUM");
            sql.Append("                       FROM TKXADS18_PHA ");
            sql.Append("                      WHERE TRIM(divisn)  = A.DIVISN");
            sql.Append("                        AND CORPOR  = A.CORPOR");
            sql.Append("                        AND PLANT   = A.PLANT");
            sql.Append("                        AND MST_ID  = A.MST_ID");
            sql.Append("                        AND PLANID  = A.PLANID");
            sql.Append("                        AND PLANST  = A.PLANST");
            sql.Append("                      GROUP BY  DIVISN, CORPOR, PLANT , MST_ID, PLANID, PLANST");
            sql.Append("                   ) PLTTOT,");
            sql.Append("                   ROUND ( ( SUM( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN 0 ELSE (A.DSTLEN/1000) END * A.TARF01 ) +");
            sql.Append("                             SUM( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN 0 ELSE 1 END * A.TARF02 ) +");
            sql.Append("                             MAX( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN 0 ELSE C.COST01 END )");
            sql.Append("                            ) /");
            sql.Append("                            CASE WHEN SUM( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN 0 ELSE A.BOXCNT END ) = 0 THEN 1 ELSE SUM( CASE WHEN RTRIM(A.VEHCID) IS NULL THEN 0 ELSE A.BOXCNT END ) END");
            sql.Append("                            , 2");
            sql.Append("                         ) UNTCST");
            sql.Append("              FROM TKXADS18_PHA A, TH_IN_STOP B, TH_IN_CAR C");
            sql.Append("             WHERE 1 = 1");
            sql.Append("               AND TRIM(A.divisn)  = TRIM(B.divisn(+))");
            sql.Append("               AND A.CORPOR  = B.CORPOR(+)");
            sql.Append("               AND A.PLANT   = B.PLANT(+)");
            sql.Append("               AND A.MST_ID  = B.MST_ID(+)");
            sql.Append("               AND A.PLANID  = B.PLANID(+)");
            sql.Append("               AND A.PLANST  = B.PLANST(+)");
            sql.Append("               AND A.STOPID  = B.STOPID(+)");
            sql.Append("               AND TRIM(A.divisn)  = TRIM(C.divisn(+))");
            sql.Append("               AND A.CORPOR  = C.CORPOR(+)");
            sql.Append("               AND A.PLANT   = C.PLANT(+)");
            sql.Append("               AND A.MST_ID  = C.MST_ID(+)");
            sql.Append("               AND A.PLANID  = C.PLANID(+)");
            sql.Append("               AND A.PLANST  = C.PLANST(+)");
            sql.Append("               AND A.VEHCID  = C.VEHCID(+)");
            sql.Append("               AND A.CORPOR  = '");
            sql.Append(corporation);
            sql.Append("'");
            sql.Append("               AND A.PLANT   = '");
            sql.Append(plant);
            sql.Append("'");
            sql.Append("               AND A.MST_ID  = '");
            sql.Append(master_id);
            sql.Append("'");
            sql.Append("               AND A.PLANID  IN ( SUBSTR( '");
            sql.Append(plan_id);
            sql.Append("' , 1, 8)||'_0001', SUBSTR( '");
            sql.Append(plan_id);
            sql.Append("' , 1, 8)||'_0002', SUBSTR( '");
            sql.Append(plan_id);
            sql.Append("' , 1, 8)||'_0003' )");
            sql.Append("               AND A.PLANST  = ");
            sql.Append(planST);
            sql.Append("             GROUP BY A.DIVISN, A.CORPOR, A.PLANT , A.MST_ID, A.PLANID, A.PLANST, A.VEHCID, A.TURNNO");
            sql.Append("             ORDER BY  A.DIVISN, A.CORPOR, A.PLANT , A.MST_ID, A.PLANID, A.PLANST, A.VEHCID, A.TURNNO");
            sql.Append("        ) A");
            sql.Append("  GROUP BY  A.DIVISN, A.CORPOR, A.PLANT , A.MST_ID, A.PLANID, A.PLANST");
            sql.Append("  ORDER BY  A.DIVISN, A.CORPOR, A.PLANT , A.MST_ID, A.PLANID, A.PLANST");

            return ExecuteQuery(sql.ToString());
            
        }

        public OracleDataTable SP_VRO_GET_AUTOALLOCATE_INFO(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id)
        {
            //OracleDataTable dataTable = new OracleDataTable();
            //OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            //_connection.Open();

            //try
            //{
            //    OracleCommand command = new OracleCommand();
            //    command.Connection = _connection;

            //    command.CommandType = System.Data.CommandType.StoredProcedure;

            //    command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_AUTOALLOCATE_INFO";
            //    command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
            //    command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
            //    command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
            //    command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
            //    command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

            //    command.ExecuteNonQuery();

            //    OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

            //    oraDataAdapter.Fill(dataTable, oraCursor);

            //}
            //catch (OracleException ex)
            //{
            //    Console.WriteLine("Exception occurs: {0}", ex.Message);
            //}
            //finally
            //{
            //    _connection.Close();
            //}
            //return dataTable;

            StringBuilder sql = new StringBuilder();

            sql.Append("select  PLANDT, PLANST , POLYID, FLAG02, '0UU1EE0EE1UU0EEOUUOEE' AS PLAN_STATUS_BUTTON");
            sql.Append("  from TH_IN_PLAN");
            sql.Append(" where 1=1 ");
            sql.Append(" and   CORPOR = '");
            sql.Append(corporation);
            sql.Append("'");
            sql.Append(" and   PLANT = '");
            sql.Append(plant);
            sql.Append("'");
            sql.Append(" and   MST_ID = '");
            sql.Append(master_id);
            sql.Append("'");
            sql.Append(" and   PLANST = ");
            sql.Append(planST);

            return ExecuteQuery(sql.ToString());
        }

        public string SP_VRO_CHG_AUTOALLOCATE_INFO(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id,string policy_id)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_CHG_AUTOALLOCATE_INFO";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_policy_id", OracleDbType.VarChar, policy_id, ParameterDirection.Input);
                command.Parameters.Add("rtn_value", OracleDbType.VarChar, ParameterDirection.Output);

                command.ExecuteNonQuery();

                result = (string)command.Parameters["rtn_value"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public OracleDataTable SP_VRO_GET_TMPCARALLOC_CARLIST(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id, string policy_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_TMPCARALLOC_CARLIST";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_policy_id", OracleDbType.VarChar, policy_id, ParameterDirection.Input);
                command.Parameters.Add("c_car_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_car_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        ///<SUMMARY> 코드테이블에서 코드명+코드값을 가져온다. (보통 콤보박스 세팅용으로 사용) </SUMMARY>
        public OracleDataTable SP_VRO_GET_COMMON_CODE(string qry_type, string key_id, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "SP_VRO_GET_COMMON_CODE";
                command.Parameters.Add("p_key_id", OracleDbType.VarChar, key_id, ParameterDirection.Input);
                command.Parameters.Add("c_common_code_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_common_code_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;

        }
        ///<SUMMARY> 차량속도 설정id 리스트를 가져온다. </SUMMARY>
        public OracleDataTable SP_VRO_GET_MOVING_SPEED(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "SP_VRO_GET_MOVING_SPEED";
                command.Parameters.Add("p_company", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_center", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_mst_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_st", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
            
        }

        public OracleDataTable SP_VRO_GET_ABLETOADJUST_CARS(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_ABLETOADJUST_CARS";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        } 

        public OracleDataTable SP_VRO_GET_CARALLOC_CARLIST(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_CARALLOC_CARLIST";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public OracleDataTable SP_VRO_GET_CARALLOC_PHALIST(string query_type, string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id, string vehicle_id)
        {
            //OracleDataTable dataTable = new OracleDataTable();
            //OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            //_connection.Open();

            //try
            //{
            //    OracleCommand command = new OracleCommand();
            //    command.Connection = _connection;

            //    command.CommandType = System.Data.CommandType.StoredProcedure;

            //    command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_CARALLOC_PHALIST";
            //    command.Parameters.Add("p_query_type", OracleDbType.VarChar, query_type, ParameterDirection.Input);
            //    command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
            //    command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
            //    command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
            //    command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_vehicle_id", OracleDbType.VarChar, vehicle_id, ParameterDirection.Input);
            //    command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

            //    command.ExecuteNonQuery();

            //    OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

            //    oraDataAdapter.Fill(dataTable, oraCursor);

            //}
            //catch (OracleException ex)
            //{
            //    Console.WriteLine("Exception occurs: {0}", ex.Message);
            //}
            //finally
            //{
            //    _connection.Close();
            //}
            //return dataTable;
            StringBuilder sql = new StringBuilder();

           if (query_type.Equals("ALLOCATED"))
            {
                sql.Append(" SELECT A.VEHCID, A.STOPID, A.VHCTON, A.VHCTYP, A.OWNRSH, A.LIMVOL, A.LIMWGT, A.LIMPLT, NULL AS LMTRNO, NULL AS LMSPNO,");
                sql.Append("        A.VCTMFR, A.VCTMTO, A.VDST01, A.LMMDS1, A.REAL01, A.ALLO01, A.VDST02, A.LMMDS2, NULL AS ALLO02, A.REAL02,");
                sql.Append("        A.ALLO03, A.REAL03, A.TURNNO, A.TRIPNO, ");
                sql.Append("        CASE WHEN RTRIM(A.VEHCID) IS NOT NULL AND A.TRIPNO = 0");
                sql.Append("             THEN  '센터 출발'");
                sql.Append("             ELSE  CASE WHEN RTRIM(A.VEHCID) IS NOT NULL AND A.TRIPNO = 99");
                sql.Append("                        THEN  CASE WHEN B.TURNNO = A.TURNNO");
                sql.Append("                                   THEN  '차고지 도착'");
                sql.Append("                                   ELSE  '센터 도착'");
                sql.Append("                              END");
                sql.Append("                   ELSE  C.STOPNM ");
                sql.Append("             END");
                sql.Append("        END STOPNM,");
                sql.Append("        'ATTR01' ATTR01, 'STOPGR' STOPGR, C.ADDRSS, A.SDST01, A.SDST02, A.SVHTON, A.SVHTYP, 'AVEHID' AS AVEHID, ");
                sql.Append("        A.SPTMFR||'-'||DECODE( A.SPTMTO, '23:59:59', '24:00:00', A.SPTMTO ) SPTMFR, 'OFFTFR' OFFTFR,");
                sql.Append("        A.OFFTTO, A.DSTLEN, A.BOXCNT, A.WEIGHT, A.VOLUME, A.PLTCNT, A.ARRVTM, A.WORKTM, A.LEAVTM, A.PREPTM,");
                sql.Append("        A.UNLDTM, A.TOTWTM, A.ORDTYP, NULL AS MALTYP, A.TARF01, A.TARF02, A.TARF03, A.FALLOC, NULL AS DSPTNO");
                sql.Append("   FROM TKXADS18_PHA A");
                sql.Append("   LEFT OUTER JOIN (");
                sql.Append("                     SELECT  A.VEHCID, MAX(A.TURNNO) TURNNO");
                sql.Append("                       FROM    TKXADS18_PHA A");
                sql.Append("                      WHERE   1 = 1");
                sql.Append("                        AND     A.CORPOR  = '");
                sql.Append(corporation);
                sql.Append("'");
                sql.Append("                        AND     A.PLANT   = '");
                sql.Append(plant);
                sql.Append("'");
                sql.Append("                        AND     a.mst_id  = '");
                sql.Append(master_id);
                sql.Append("'");
                sql.Append("                        AND     a.PLANID  = '");
                sql.Append(plan_id);
                sql.Append("'");
                sql.Append("                        AND     a.PLANST  = ");
                sql.Append(planST);

                if (vehicle_id != null && vehicle_id.Length > 0)
                {
                    sql.Append("                        AND     A.VEHCID  = ");
                    sql.Append("   RPAD('");
                    sql.Append(vehicle_id);
                    sql.Append("', 16, CHR(32))");
                }
                sql.Append("                      GROUP BY  A.VEHCID");
                sql.Append("                   ) B");
                sql.Append("     ON  A.VEHCID  = B.VEHCID");
                sql.Append("    LEFT OUTER JOIN TKXADS18_ARR C");
                sql.Append("     ON  A.STOPID = C.STOPID");
                sql.Append("  WHERE   1 = 1");
                sql.Append("    AND     A.CORPOR  = '");
                sql.Append(corporation);
                sql.Append("'");
                sql.Append("    AND     A.PLANT   = '");
                sql.Append(plant);
                sql.Append("'");
                sql.Append("    AND     A.MST_ID  = '");
                sql.Append(master_id);
                sql.Append("'");
                sql.Append("    AND     A.PLANID  = '");
                sql.Append(plan_id);
                sql.Append("'");
                sql.Append("    AND     A.PLANST  = ");
                sql.Append(planST);
                sql.Append("    AND     RTRIM( A.VEHCID ) IS NOT NULL");

                if (vehicle_id != null && vehicle_id.Length > 0)
                {
                    sql.Append("                        AND     A.VEHCID  = ");
                    sql.Append("   RPAD('");
                    sql.Append(vehicle_id);
                    sql.Append("', 16, CHR(32))");
                }
                sql.Append("  ORDER BY  A.VEHCID, A.TURNNO, A.TRIPNO");


            }
            else
            {
                sql.Append(" SELECT A.VEHCID, A.STOPID, A.VHCTON, A.VHCTYP, A.OWNRSH, A.LIMVOL, A.LIMWGT, A.LIMPLT, NULL AS LMTRNO, NULL AS LMSPNO,");
                sql.Append("        A.VCTMFR, A.VCTMTO, A.VDST01, A.LMMDS1, A.REAL01, A.ALLO01, A.VDST02, A.LMMDS2, NULL AS ALLO02, A.REAL02,");
                sql.Append("        A.ALLO03, A.REAL03, A.TURNNO, ROW_NUMBER() OVER (ORDER BY A.SDST01, A.TURNNO, A.TRIPNO) TRIPNO,");
                sql.Append("        NULL AS  STOPNM, A.ATTR01, A.STOPGR, NULL AS ADDRSS, A.SDST01, A.SDST02,");
                sql.Append("        A.SVHTON, A.SVHTYP, NULL AS AVEHID, A.SPTMFR||'-'||DECODE( A.SPTMTO, '23:59:59', '24:00:00', A.SPTMTO ) SPTMFR, A.OFFTFR,");
                sql.Append("        A.OFFTTO, A.DSTLEN, A.BOXCNT, A.WEIGHT, A.VOLUME, A.PLTCNT, A.ARRVTM, A.WORKTM, A.LEAVTM, A.PREPTM,");
                sql.Append("        A.UNLDTM, A.TOTWTM, A.ORDTYP, NULL AS MALTYP, A.TARF01, A.TARF02, A.TARF03, A.FALLOC, NULL AS DSPTNO");
                sql.Append("   FROM TKXADS18_PHA A");
                sql.Append("  WHERE 1 = 1");
                sql.Append("    AND A.CORPOR  = '");
                sql.Append(corporation);
                sql.Append("'");
                sql.Append("    AND A.PLANT   = '");
                sql.Append(plant);
                sql.Append("'");
                sql.Append("    AND a.mst_id  = '");
                sql.Append(master_id);
                sql.Append("'");
                sql.Append("    AND a.PLANID  = '");
                sql.Append(plan_id);
                sql.Append("'");
                sql.Append("    AND a.PLANST  = ");
                sql.Append(planST);
                sql.Append("    AND RTRIM(A.VEHCID) IS NULL");
                sql.Append("  ORDER BY A.SDST01, A.TURNNO, A.TRIPNO");
            }

            return ExecuteQuery(sql.ToString());
        }

        public string SP_VRO_UPDATE_CARALLOC_PHASE(string corporation, string plant, string master_id, string plan_id, string plan_date, string plan_version, string user_id, string step, string polyid)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_UPDATE_CARALLOC_PHASE";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_step_id", OracleDbType.VarChar, step, ParameterDirection.Input);
                command.Parameters.Add("p_poly_id", OracleDbType.VarChar, polyid, ParameterDirection.Input);

                command.Parameters.Add("rtn_value", OracleDbType.VarChar, ParameterDirection.Output);


                command.ExecuteNonQuery();

                result = (string)command.Parameters["rtn_value"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public OracleDataTable SP_VRO_GET_CARUSING_INFO(string query_type, string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id, string vehicle_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_CARUSING_INFO";
                command.Parameters.Add("p_query_type", OracleDbType.VarChar, query_type, ParameterDirection.Input);
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_vehicle_id", OracleDbType.VarChar, vehicle_id, ParameterDirection.Input);
                command.Parameters.Add("c_car_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_car_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public OracleDataTable SP_VRO_GET_CARALLOC_DETAIL(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string plan_type, string plan_step)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_CARALLOC_DETAIL";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_plan_type", OracleDbType.VarChar, plan_type, ParameterDirection.Input);
                command.Parameters.Add("p_plan_step", OracleDbType.VarChar, plan_step, ParameterDirection.Input);
                command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public OracleDataTable SP_VRO_GET_CARALLOC_SUMMARY(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string plan_type, string plan_step)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_CARALLOC_SUMMARY";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_plan_type", OracleDbType.VarChar, plan_type, ParameterDirection.Input);
                command.Parameters.Add("p_plan_step", OracleDbType.VarChar, plan_step, ParameterDirection.Input);
                command.Parameters.Add("c_qry_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_qry_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public OracleDataTable SP_VRO_GET_CAR_USAGE(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id)
        {
            OracleDataTable dataTable = new OracleDataTable();
            OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_VEHICLE_USAGE";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_step", OracleDbType.VarChar, "10001", ParameterDirection.Input);
                command.Parameters.Add("c_usage_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

                command.ExecuteNonQuery();

                OracleCursor oraCursor = (OracleCursor)command.Parameters["c_usage_list"].Value;

                oraDataAdapter.Fill(dataTable, oraCursor);

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public OracleDataTable SP_VRO_GET_CAR_LIST(string corporation, string plant, string master_id, string plan_id, string plan_date, string planST, string user_id)
        {
            //OracleDataTable dataTable = new OracleDataTable();
            //OracleDataAdapter oraDataAdapter = new OracleDataAdapter();

            //_connection.Open();

            //try
            //{
            //    OracleCommand command = new OracleCommand();
            //    command.Connection = _connection;

            //    command.CommandType = System.Data.CommandType.StoredProcedure;

            //    command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_GET_CAR_LIST";
            //    command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
            //    command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
            //    command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
            //    command.Parameters.Add("p_plan_version", OracleDbType.VarChar, planST, ParameterDirection.Input);
            //    command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
            //    command.Parameters.Add("c_car_list", OracleDbType.Cursor, ParameterDirection.InputOutput);

            //    command.ExecuteNonQuery();

            //    OracleCursor oraCursor = (OracleCursor)command.Parameters["c_car_list"].Value;

            //    oraDataAdapter.Fill(dataTable, oraCursor);

            //}
            //catch (OracleException ex)
            //{
            //    Console.WriteLine("Exception occurs: {0}", ex.Message);
            //}
            //finally
            //{
            //    _connection.Close();
            //}
            //return dataTable;
            StringBuilder sql = new StringBuilder();

            sql.Append("select VEHCID, DIST01, VHCTON, LIMWGT");
            sql.Append("  from TH_IN_CAR");

            sql.Append(" where 1=1 ");
            sql.Append(" and   CORPOR = '");
            sql.Append(corporation);
            sql.Append("'");
            sql.Append(" and   PLANT = '");
            sql.Append(plant);
            sql.Append("'");
            sql.Append(" and   MST_ID = '");
            sql.Append(master_id);
            sql.Append("'");
            sql.Append(" and   PLANID = '");
            sql.Append(plan_id);
            sql.Append("'");
            //sql.Append(" and   PLANDT = '20141001'");
            sql.Append(" and   PLANST = ");
            sql.Append(planST);
            sql.Append("   and VEHCID in (");
            sql.Append("                   select distinct VEHCID ");
            sql.Append("                     from TKXADS18_PHA");

            sql.Append("                    where 1=1 ");
            sql.Append("                      and   CORPOR = '");
            sql.Append(corporation);
            sql.Append("'");
            sql.Append("                      and   PLANT = '");
            sql.Append(plant);
            sql.Append("'");
            sql.Append("                      and   MST_ID = '");
            sql.Append(master_id);
            sql.Append("'");
            sql.Append("                      and   PLANID = '");
            sql.Append(plan_id);
            sql.Append("'");
            sql.Append("                      and   PLANST = ");
            sql.Append(planST);
            sql.Append("                      and trim(VEHCID) is not null");
            sql.Append("                 )");
            sql.Append(" order by DIST01, VHCTON, VEHCID");

            return ExecuteQuery(sql.ToString());
        }

        public string SP_VRO_UPDATE_PLAN_HEADER(string corporation, string plant, string master_id, string plan_id, string plan_date, string plan_version, string user_id, string step, string polyid)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_UPDATE_PLAN_HEADER";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);
                command.Parameters.Add("p_step_id", OracleDbType.VarChar, step, ParameterDirection.Input);
                command.Parameters.Add("p_plan_type", OracleDbType.VarChar, "PRE", ParameterDirection.Input);
                command.Parameters.Add("p_poly_id", OracleDbType.VarChar, polyid, ParameterDirection.Input);

                command.Parameters.Add("rtn_value", OracleDbType.VarChar, ParameterDirection.Output);


                command.ExecuteNonQuery();

                result = (string)command.Parameters["rtn_value"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public string SP_VRO_INITIALIZE_PLAN(string corporation, string plant, string master_id, string plan_id, string plan_date, string plan_version, string user_id)
        {
            string result = "";

            _connection.Open();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = _connection;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.CommandText = "PKG_VRO_UI_MANAGER.SP_VRO_INITIALIZE_PLAN";
                command.Parameters.Add("p_corporation", OracleDbType.VarChar, corporation, ParameterDirection.Input);
                command.Parameters.Add("p_plant", OracleDbType.VarChar, plant, ParameterDirection.Input);
                command.Parameters.Add("p_master_id", OracleDbType.VarChar, master_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_id", OracleDbType.VarChar, plan_id, ParameterDirection.Input);
                command.Parameters.Add("p_plan_date", OracleDbType.VarChar, plan_date, ParameterDirection.Input);
                command.Parameters.Add("p_plan_version", OracleDbType.VarChar, plan_version, ParameterDirection.Input);
                command.Parameters.Add("p_user_id", OracleDbType.VarChar, user_id, ParameterDirection.Input);

                command.Parameters.Add("p_rtn_value", OracleDbType.VarChar, ParameterDirection.Output);


                command.ExecuteNonQuery();

                result = (string)command.Parameters["p_rtn_value"].Value;

            }
            catch (OracleException ex)
            {
                Console.WriteLine("Exception occurs: {0}", ex.Message);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }
    }
}
