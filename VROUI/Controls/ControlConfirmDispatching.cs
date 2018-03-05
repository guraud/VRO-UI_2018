using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using VROUI.popup;
using VROUI.Forms;
using VROUI.objectPool;
using Spring.Context;
using Spring.Context.Support;
using VROUI.Services;
using Devart.Data.Oracle;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data;

using System.Diagnostics;

namespace VROUI.Controls
{
    public partial class ControlConfirmDispatching : DevExpress.XtraEditors.XtraUserControl
    {
        FormMapView mapView = null;

        private class Vehicle
        {
            public string vehicleNo { get; set;}
            public string vehicleTon { get; set;}
            public string vehicleType { get; set;}
            public string ownerShip { get; set;}
            public string maxVolume { get; set;}
            public string maxWeight { get; set;}
            public string maxPallet { get; set;}
            public string maxTurnCount { get; set;}
            public string maxTripCount { get; set;}
            public string startWorkTime { get; set;}
            public string endWorkTime { get; set;}
            public string district01 { get; set;}
            public string maxMultiDistrict01 { get; set;}
            public string reAllocation01 { get; set;}
            public string allocation01 { get; set;}
            public string district02 { get; set;}
            public string maxMultiDistrict02 { get; set;}
            public string reAllocation02 { get; set;}
            public string allocation02 { get; set;}
            public string allocation03 { get; set;}
            public string reAllocation03 { get; set; }

            public void clear()
            {
                vehicleNo = "";
                vehicleTon = "";
                vehicleType = "";
                ownerShip = "";
                maxVolume = "";
                maxWeight = "";
                maxPallet = "";
                maxTurnCount = "";
                maxTripCount = "";
                startWorkTime = "";
                endWorkTime = "";
                district01 = "";
                maxMultiDistrict01 = "";
                reAllocation01 = "";
                allocation01 = "";
                district02 = "";
                maxMultiDistrict02 = "";
                reAllocation02 = "";
                allocation02 = "";
                allocation03 = "";
                reAllocation03 = "";
            }

            public void printVehicle()
            {
                string contentOfVehicle = "\t" + vehicleNo + "\t" + vehicleTon + "\t" + vehicleType
                    + "\t" + maxVolume + "\t" + maxWeight + "\t" + maxPallet
                    + "\t" + startWorkTime + "\t" + endWorkTime
                    + "\t" + district01 + "\t" + maxMultiDistrict01 + "\t" + reAllocation01 + "\t" + allocation01
                    + "\t" + district02 + "\t" + maxMultiDistrict02 + "\t" + reAllocation02 + "\t" + allocation02
                    + "\t" + allocation03 + "\t" + reAllocation03 + "\t";
                Debug.WriteLine(contentOfVehicle);
            }
        }

        public ControlConfirmDispatching()
        {
            InitializeComponent();

            //summary 색상 변경 (색상 변경 시 주석 제거)
            //gridViewDispatchingInfo.RowCellStyle += new RowCellStyleEventHandler(gridViewDispatchingInfo_RowCellStyle)
        }
        
        public void Initialize()
        {
            XtraHelper.InitializeGrid(gridCarList);
            XtraHelper.InitializeGrid(gridDispathingInfo);
            XtraHelper.InitializeGrid(gridDispatchingResult);
            XtraHelper.InitializeGrid(gridDispatchingSummary);
            XtraHelper.InitializeGrid(gridNotAssignedOrder);
            XtraHelper.InitializeGrid(gridCarInfo);

            gridDispathingInfo.AllowDrop = true;
            gridNotAssignedOrder.AllowDrop = true;

            AssignedHitInfo = new List<GridHitInfo>();
            NotAssignedHitInfo = new List<GridHitInfo>();

            AssignedHit = new List<int>();
            NotAssignedHit = new List<int>();
            currentVehicle = new Vehicle();

            _applicationContext = ContextRegistry.GetContext();

            initializeCarUsageList();
            initializeCarList();
            initializeDispatchingList();
            initializeNotAssignedOrderList();

            InitializeDataForCarUsageData();
            InitializeDataForCarData();
            InitializeAllocData();
            InitializeNotAssignedOrderData();

            //InitializeDataForDispatchingResult();
            //InitializeDataForDispatchingSummary();
        }

        private void initializeCarUsageList()
        {
            dtCarUsageList = new DataTable();

            #region Column add
            dtCarUsageList.Columns.Add("gridColumn47", typeof(string));
            dtCarUsageList.Columns.Add("gridColumn48", typeof(string));
            dtCarUsageList.Columns.Add("gridColumn49", typeof(string));
            dtCarUsageList.Columns.Add("gridColumn50", typeof(string));
            #endregion Column add

            #region Column caption
            dtCarUsageList.Columns["gridColumn47"].Caption = "구분";
            dtCarUsageList.Columns["gridColumn48"].Caption = "지입차";
            dtCarUsageList.Columns["gridColumn49"].Caption = "임시차";
            dtCarUsageList.Columns["gridColumn50"].Caption = "성과급";
            #endregion Column caption

            gridCarInfo.DataSource = dtCarUsageList;
            gridCarInfo.RefreshDataSource();
        }

        private void initializeCarList()
        {
            dtCarList = new DataTable();

            #region Column add
            dtCarList.Columns.Add("VEHCID", typeof(string));
            dtCarList.Columns.Add("DIST01", typeof(string));
            dtCarList.Columns.Add("VHCTON", typeof(string));
            dtCarList.Columns.Add("LIMVOL", typeof(string));
            dtCarList.Columns.Add("LIMWGT", typeof(string));
            dtCarList.Columns.Add("LIMPLT", typeof(string));
            dtCarList.Columns.Add("PIECERATE", typeof(string));
            dtCarList.Columns.Add("OPERRATE", typeof(string));
            dtCarList.Columns.Add("STOPID", typeof(string));
            dtCarList.Columns.Add("BOXQTY", typeof(string));
            #endregion Column add

            #region Column caption
            dtCarList.Columns["VEHCID"].Caption = "차량번호";
            dtCarList.Columns["DIST01"].Caption = "권역";
            dtCarList.Columns["VHCTON"].Caption = "차종";
            dtCarList.Columns["LIMVOL"].Caption = "체적";
            dtCarList.Columns["LIMWGT"].Caption = "중량";
            dtCarList.Columns["LIMPLT"].Caption = "PLT";
            dtCarList.Columns["PIECERATE"].Caption = "성과급";
            dtCarList.Columns["OPERRATE"].Caption = "가동율";
            dtCarList.Columns["STOPID"].Caption = "차고지";
            dtCarList.Columns["BOXQTY"].Caption = "박스수";
            #endregion Column caption

            gridCarList.DataSource = dtCarList;
            gridCarList.RefreshDataSource();
        }

        private void initializeDispatchingList()
        {
            dtAllocList = new DataTable();

            #region Column add
            dtAllocList.Columns.Add("VEHCID", typeof(string));
            dtAllocList.Columns.Add("STOPID", typeof(string));
            dtAllocList.Columns.Add("VHCTON", typeof(string));
            dtAllocList.Columns.Add("VHCTYP", typeof(string));
            dtAllocList.Columns.Add("OWNRSH", typeof(string));
            dtAllocList.Columns.Add("LIMVOL", typeof(string));
            dtAllocList.Columns.Add("LIMWGT", typeof(string));
            dtAllocList.Columns.Add("LIMPLT", typeof(string));
            dtAllocList.Columns.Add("LMTRNO", typeof(string));
            dtAllocList.Columns.Add("LMSPNO", typeof(string));
            dtAllocList.Columns.Add("VCTMFR", typeof(string));
            dtAllocList.Columns.Add("VCTMTO", typeof(string));
            dtAllocList.Columns.Add("VDST01", typeof(string));
            dtAllocList.Columns.Add("LMMDS1", typeof(string));
            dtAllocList.Columns.Add("REAL01", typeof(string));
            dtAllocList.Columns.Add("ALLO01", typeof(string));
            dtAllocList.Columns.Add("VDST02", typeof(string));
            dtAllocList.Columns.Add("LMMDS2", typeof(string));
            dtAllocList.Columns.Add("ALLO02", typeof(string));
            dtAllocList.Columns.Add("REAL02", typeof(string));
            dtAllocList.Columns.Add("ALLO03", typeof(string));
            dtAllocList.Columns.Add("REAL03", typeof(string));
            dtAllocList.Columns.Add("TURNNO", typeof(string));
            dtAllocList.Columns.Add("TRIPNO", typeof(string));
            dtAllocList.Columns.Add("STOPNM", typeof(string));
            dtAllocList.Columns.Add("ATTR01", typeof(string));
            dtAllocList.Columns.Add("STOPGR", typeof(string));
            dtAllocList.Columns.Add("ADDRSS", typeof(string));
            dtAllocList.Columns.Add("SDST01", typeof(string));
            dtAllocList.Columns.Add("SDST02", typeof(string));
            dtAllocList.Columns.Add("SVHTON", typeof(string));
            dtAllocList.Columns.Add("SVHTYP", typeof(string));
            dtAllocList.Columns.Add("AVEHID", typeof(string));
            dtAllocList.Columns.Add("SPTMFR", typeof(string));
            dtAllocList.Columns.Add("OFFTFR", typeof(string));
            dtAllocList.Columns.Add("OFFTTO", typeof(string));
            dtAllocList.Columns.Add("DSTLEN", typeof(string));
            dtAllocList.Columns.Add("BOXCNT", typeof(string));
            dtAllocList.Columns.Add("WEIGHT", typeof(string));
            dtAllocList.Columns.Add("VOLUME", typeof(string));
            dtAllocList.Columns.Add("PLTCNT", typeof(string));
            dtAllocList.Columns.Add("ARRVTM", typeof(string));
            dtAllocList.Columns.Add("WORKTM", typeof(string));
            dtAllocList.Columns.Add("LEAVTM", typeof(string));
            dtAllocList.Columns.Add("PREPTM", typeof(string));
            dtAllocList.Columns.Add("UNLDTM", typeof(string));
            dtAllocList.Columns.Add("TOTWTM", typeof(string));
            dtAllocList.Columns.Add("ORDTYP", typeof(string));
            dtAllocList.Columns.Add("MALTYP", typeof(string));
            dtAllocList.Columns.Add("TARF01", typeof(string));
            dtAllocList.Columns.Add("TARF02", typeof(string));
            dtAllocList.Columns.Add("TARF03", typeof(string));
            dtAllocList.Columns.Add("DSPTNO", typeof(string));
            #endregion Column add
        }

        private void initializeNotAssignedOrderList()
        {
            dtNonAllocList = new DataTable();

            #region Column add
            dtNonAllocList.Columns.Add("VEHCID", typeof(string));
            dtNonAllocList.Columns.Add("STOPID", typeof(string));
            dtNonAllocList.Columns.Add("VHCTON", typeof(string));
            dtNonAllocList.Columns.Add("VHCTYP", typeof(string));
            dtNonAllocList.Columns.Add("OWNRSH", typeof(string));
            dtNonAllocList.Columns.Add("LIMVOL", typeof(string));
            dtNonAllocList.Columns.Add("LIMWGT", typeof(string));
            dtNonAllocList.Columns.Add("LIMPLT", typeof(string));
            dtNonAllocList.Columns.Add("LMTRNO", typeof(string));
            dtNonAllocList.Columns.Add("LMSPNO", typeof(string));
            dtNonAllocList.Columns.Add("VCTMFR", typeof(string));
            dtNonAllocList.Columns.Add("VCTMTO", typeof(string));
            dtNonAllocList.Columns.Add("VDST01", typeof(string));
            dtNonAllocList.Columns.Add("LMMDS1", typeof(string));
            dtNonAllocList.Columns.Add("REAL01", typeof(string));
            dtNonAllocList.Columns.Add("ALLO01", typeof(string));
            dtNonAllocList.Columns.Add("VDST02", typeof(string));
            dtNonAllocList.Columns.Add("LMMDS2", typeof(string));
            dtNonAllocList.Columns.Add("ALLO02", typeof(string));
            dtNonAllocList.Columns.Add("REAL02", typeof(string));
            dtNonAllocList.Columns.Add("ALLO03", typeof(string));
            dtNonAllocList.Columns.Add("REAL03", typeof(string));
            dtNonAllocList.Columns.Add("TURNNO", typeof(string));
            dtNonAllocList.Columns.Add("TRIPNO", typeof(string));
            dtNonAllocList.Columns.Add("STOPNM", typeof(string));
            dtNonAllocList.Columns.Add("ATTR01", typeof(string));
            dtNonAllocList.Columns.Add("STOPGR", typeof(string));
            dtNonAllocList.Columns.Add("ADDRSS", typeof(string));
            dtNonAllocList.Columns.Add("SDST01", typeof(string));
            dtNonAllocList.Columns.Add("SDST02", typeof(string));
            dtNonAllocList.Columns.Add("SVHTON", typeof(string));
            dtNonAllocList.Columns.Add("SVHTYP", typeof(string));
            dtNonAllocList.Columns.Add("AVEHID", typeof(string));
            dtNonAllocList.Columns.Add("SPTMFR", typeof(string));
            dtNonAllocList.Columns.Add("OFFTFR", typeof(string));
            dtNonAllocList.Columns.Add("OFFTTO", typeof(string));
            dtNonAllocList.Columns.Add("DSTLEN", typeof(string));
            dtNonAllocList.Columns.Add("BOXCNT", typeof(string));
            dtNonAllocList.Columns.Add("WEIGHT", typeof(string));
            dtNonAllocList.Columns.Add("VOLUME", typeof(string));
            dtNonAllocList.Columns.Add("PLTCNT", typeof(string));
            dtNonAllocList.Columns.Add("ARRVTM", typeof(string));
            dtNonAllocList.Columns.Add("WORKTM", typeof(string));
            dtNonAllocList.Columns.Add("LEAVTM", typeof(string));
            dtNonAllocList.Columns.Add("PREPTM", typeof(string));
            dtNonAllocList.Columns.Add("UNLDTM", typeof(string));
            dtNonAllocList.Columns.Add("TOTWTM", typeof(string));
            dtNonAllocList.Columns.Add("ORDTYP", typeof(string));
            dtNonAllocList.Columns.Add("MALTYP", typeof(string));
            dtNonAllocList.Columns.Add("TARF01", typeof(string));
            dtNonAllocList.Columns.Add("TARF02", typeof(string));
            dtNonAllocList.Columns.Add("TARF03", typeof(string));
            dtNonAllocList.Columns.Add("DSPTNO", typeof(string));
            #endregion Column add

            #region Column caption
            //dtNonAllocList.Columns["TURNNO"].Caption = "회전";
            //dtNonAllocList.Columns["CHECKBOX"].Caption = "선택";
            //dtNonAllocList.Columns["SEQ"].Caption = "SEQ";
            //dtNonAllocList.Columns["TP"].Caption = "TP";
            //dtNonAllocList.Columns["STOPNM"].Caption = "착지명";
            //dtNonAllocList.Columns["SDST01"].Caption = "권역";
            //dtNonAllocList.Columns["OWNRSH"].Caption = "소속";
            //dtNonAllocList.Columns["ADDRSS"].Caption = "주소";
            //dtNonAllocList.Columns["BOXCNT"].Caption = "박스";
            //dtNonAllocList.Columns["PLTCNT"].Caption = "PLT";
            //dtNonAllocList.Columns["WEIGHT"].Caption = "중량";
            //dtNonAllocList.Columns["VOLUME"].Caption = "체적";
            //dtNonAllocList.Columns["LEAVTM"].Caption = "출발시간";
            //dtNonAllocList.Columns["DSTLEN"].Caption = "이동거리";
            //dtNonAllocList.Columns["ARRVTM"].Caption = "도착시간";
            //dtNonAllocList.Columns["WORKTM"].Caption = "하역시간";
            //dtNonAllocList.Columns["SPTMFR"].Caption = "요청시간";
            //dtNonAllocList.Columns["AVEHID"].Caption = "지정차량";
            //dtNonAllocList.Columns["TRIPNO"].Caption = "착지순번";
            //dtNonAllocList.Columns["DATA_GBN"].Caption = "Data Gbn";
            //dtNonAllocList.Columns["GBN_SEQ"].Caption = "Gbn seq";
            //dtNonAllocList.Columns["STATUS"].Caption = "진행상태";
            //dtNonAllocList.Columns["FLAG"].Caption = "FLAG";
            //dtNonAllocList.Columns["R/T"].Caption = "R/T";
            #endregion Column caption

            //gridNotAssignedOrder.DataSource = dtNonAllocList;
            //gridNotAssignedOrder.RefreshDataSource();
        }

        private void InitializeDataForCarUsageData()
        {
            dtCarUsageList.Rows.Clear();

            bizService = getBizService();
            carUsageOradt = bizService.GetCarUsageList();
            #region Print dt
            //PrintDebug(carUsageOradt);
            #endregion

            foreach (DataRow _row in carUsageOradt.Rows)
            {
                DataRow dr = dtCarUsageList.NewRow();

                dr["gridColumn47"] = _row["VHCUSE"] + "";
                dr["gridColumn48"] = _row["OWNCNT"] + "";
                dr["gridColumn49"] = _row["NOWCNT"] + "";
                dr["gridColumn50"] = "0";

                dtCarUsageList.Rows.Add(dr);
            }
            gridCarInfo.DataSource = dtCarUsageList;
            gridCarInfo.RefreshDataSource();

            //gridView4.Columns[0].Width = 100;
            //gridView4.Columns[1].Width = 100;
            //gridView4.Columns[2].Width = 100;
            //setColumnWidth(gridView4);
        }

        private void InitializeDataForCarData()
        {
            dtCarList.Rows.Clear();

            bizService = getBizService();
            OracleDataTable carOradt = bizService.GetCarList();
            #region Print dt
            PrintDebug(carOradt);
            #endregion

            foreach (DataRow _row in carOradt.Rows)
            {
                DataRow dr = dtCarList.NewRow();

                dr["VEHCID"] = _row["VEHCID"];
                dr["DIST01"] = _row["DIST01"];
                dr["VHCTON"] = _row["VHCTON"];
                //dr["LIMVOL"] = _row["LIMVOL"];
                dr["LIMWGT"] = _row["LIMWGT"];
                //dr["LIMPLT"] = _row["LIMPLT"];
                //dr["PIECERATE"] = _row["PIECERATE"];
                //dr["OPERRATE"] = _row["OPERRATE"];
                //dr["STOPID"] = _row["STOPID"];
                //dr["BOXQTY"] = _row["BOXQTY"];

                dtCarList.Rows.Add(dr);
            }
            gridCarList.DataSource = dtCarList;
            gridCarList.RefreshDataSource();

            setColumnWidth(gridView1);
        }

        private void InitializeAllocData()
        {
            bizService = this.getBizService();
            DispathingInfoDt = bizService.sp_vro_get_caralloc_phalist("ALLOCATED", "");
            #region print DispathingInfoDt
            //PrintDebug(DispathingInfoDt);
            #endregion

            foreach (DataRow _row in DispathingInfoDt.Rows)
            {
                DataRow dr = dtAllocList.NewRow();

                dr["STOPID"] = _row["STOPID"];
                dr["SDST01"] = _row["SDST01"];
                dr["BOXCNT"] = _row["BOXCNT"];
                dr["WEIGHT"] = _row["WEIGHT"];
                dr["VOLUME"] = _row["VOLUME"];
                dr["PLTCNT"] = _row["PLTCNT"];
                dr["ARRVTM"] = _row["ARRVTM"];
                dr["DSTLEN"] = _row["DSTLEN"];
                dr["WORKTM"] = _row["WORKTM"];
                dr["TOTWTM"] = _row["TOTWTM"];
                dr["SPTMFR"] = _row["SPTMFR"];
                dr["VEHCID"] = _row["VEHCID"];
                dr["TURNNO"] = _row["TURNNO"];
                dr["TRIPNO"] = _row["TRIPNO"];

                dr["VHCTON"] = _row["VHCTON"];
                dr["VHCTYP"] = _row["VHCTYP"];
                dr["OWNRSH"] = _row["OWNRSH"];
                dr["LIMVOL"] = _row["LIMVOL"];
                dr["LIMWGT"] = _row["LIMWGT"];
                dr["LIMPLT"] = _row["LIMPLT"];
                dr["LMTRNO"] = _row["LMTRNO"];
                dr["LMSPNO"] = _row["LMSPNO"];
                dr["VCTMFR"] = _row["VCTMFR"];
                dr["VCTMTO"] = _row["VCTMTO"];
                dr["VDST01"] = _row["VDST01"];
                dr["LMMDS1"] = _row["LMMDS1"];
                dr["REAL01"] = _row["REAL01"];
                dr["ALLO01"] = _row["ALLO01"];
                dr["VDST02"] = _row["VDST02"];
                dr["LMMDS2"] = _row["LMMDS2"];
                dr["ALLO02"] = _row["ALLO02"];
                dr["REAL02"] = _row["REAL02"];
                dr["ALLO03"] = _row["ALLO03"];
                dr["REAL03"] = _row["REAL03"];
                dr["STOPNM"] = _row["STOPNM"];
                dr["ATTR01"] = _row["ATTR01"];
                dr["STOPGR"] = _row["STOPGR"];
                dr["ADDRSS"] = _row["ADDRSS"];
                dr["SDST02"] = _row["SDST02"];
                dr["SVHTON"] = _row["SVHTON"];
                dr["SVHTYP"] = _row["SVHTYP"];
                dr["AVEHID"] = _row["AVEHID"];
                dr["OFFTFR"] = _row["OFFTFR"];
                dr["OFFTTO"] = _row["OFFTTO"];
                dr["LEAVTM"] = _row["LEAVTM"];
                dr["PREPTM"] = _row["PREPTM"];
                dr["UNLDTM"] = _row["UNLDTM"];
                dr["ORDTYP"] = _row["ORDTYP"];
                dr["MALTYP"] = _row["MALTYP"];
                dr["TARF01"] = _row["TARF01"];
                dr["TARF02"] = _row["TARF02"];
                dr["TARF03"] = _row["TARF03"];
                dr["DSPTNO"] = _row["DSPTNO"];

                dtAllocList.Rows.Add(dr);
            }
        }

        private void InitializeNotAssignedOrderData()
        {
            bizService = getBizService();
            NotAssignedOrderDt = bizService.sp_vro_get_caralloc_phalist("NOT ASSIGNED", "");
            #region print NotAssignedOrderDt
            //PrintDebug(NotAssignedOrderDt);
            #endregion


            foreach (DataRow _row in NotAssignedOrderDt.Rows)
            {
                DataRow dr = dtNonAllocList.NewRow();

                dr["STOPID"] = _row["STOPID"];
                dr["SDST01"] = _row["SDST01"];
                dr["BOXCNT"] = _row["BOXCNT"];
                dr["WEIGHT"] = _row["WEIGHT"];
                dr["VOLUME"] = _row["VOLUME"];
                dr["PLTCNT"] = _row["PLTCNT"];
                dr["ARRVTM"] = _row["ARRVTM"];
                dr["DSTLEN"] = _row["DSTLEN"];
                dr["WORKTM"] = _row["WORKTM"];
                dr["TOTWTM"] = _row["TOTWTM"];
                dr["SPTMFR"] = _row["SPTMFR"];
                dr["VEHCID"] = _row["VEHCID"];
                dr["TURNNO"] = _row["TURNNO"];
                dr["TRIPNO"] = _row["TRIPNO"];

                dr["VHCTON"] = _row["VHCTON"];
                dr["VHCTYP"] = _row["VHCTYP"];
                dr["OWNRSH"] = _row["OWNRSH"];
                dr["LIMVOL"] = _row["LIMVOL"];
                dr["LIMWGT"] = _row["LIMWGT"];
                dr["LIMPLT"] = _row["LIMPLT"];
                dr["LMTRNO"] = _row["LMTRNO"];
                dr["LMSPNO"] = _row["LMSPNO"];
                dr["VCTMFR"] = _row["VCTMFR"];
                dr["VCTMTO"] = _row["VCTMTO"];
                dr["VDST01"] = _row["VDST01"];
                dr["LMMDS1"] = _row["LMMDS1"];
                dr["REAL01"] = _row["REAL01"];
                dr["ALLO01"] = _row["ALLO01"];
                dr["VDST02"] = _row["VDST02"];
                dr["LMMDS2"] = _row["LMMDS2"];
                dr["ALLO02"] = _row["ALLO02"];
                dr["REAL02"] = _row["REAL02"];
                dr["ALLO03"] = _row["ALLO03"];
                dr["REAL03"] = _row["REAL03"];
                dr["STOPNM"] = _row["STOPNM"];
                dr["ATTR01"] = _row["ATTR01"];
                dr["STOPGR"] = _row["STOPGR"];
                dr["ADDRSS"] = _row["ADDRSS"];
                dr["SDST02"] = _row["SDST02"];
                dr["SVHTON"] = _row["SVHTON"];
                dr["SVHTYP"] = _row["SVHTYP"];
                dr["AVEHID"] = _row["AVEHID"];
                dr["OFFTFR"] = _row["OFFTFR"];
                dr["OFFTTO"] = _row["OFFTTO"];
                dr["LEAVTM"] = _row["LEAVTM"];
                dr["PREPTM"] = _row["PREPTM"];
                dr["UNLDTM"] = _row["UNLDTM"];
                dr["ORDTYP"] = _row["ORDTYP"];
                dr["MALTYP"] = _row["MALTYP"];
                dr["TARF01"] = _row["TARF01"];
                dr["TARF02"] = _row["TARF02"];
                dr["TARF03"] = _row["TARF03"];
                dr["DSPTNO"] = _row["DSPTNO"];

                dtNonAllocList.Rows.Add(dr);
            }
        }

        private void InitializeDataForDispatchingResult()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("차량번호", typeof(string));
            dt.Columns.Add("차종", typeof(string));
            dt.Columns.Add("톤수", typeof(string));
            dt.Columns.Add("회전", typeof(string));
            dt.Columns.Add("권역", typeof(string));
            dt.Columns.Add("착지명", typeof(string));
            dt.Columns.Add("주소", typeof(string));
            dt.Columns.Add("박스수", typeof(string));
            dt.Columns.Add("PLT", typeof(string));
            dt.Columns.Add("체적", typeof(string));
            dt.Columns.Add("중량", typeof(string));
            dt.Columns.Add("진입차종", typeof(string));
            dt.Columns.Add("도착시간", typeof(string));
            dt.Columns.Add("요청시간", typeof(string));
            dt.Columns.Add("지정차량", typeof(string));
            dt.Columns.Add("불가차량", typeof(string));

            gridDispatchingResult.DataSource = dt;
            gridDispatchingResult.RefreshDataSource();
        }

        private void InitializeDataForDispatchingSummary()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("차량번호", typeof(string));
            dt.Columns.Add("차종", typeof(string));
            dt.Columns.Add("톤수", typeof(string));
            dt.Columns.Add("회전", typeof(string));
            dt.Columns.Add("권역", typeof(string));
            dt.Columns.Add("박스수", typeof(string));
            dt.Columns.Add("체적", typeof(string));
            dt.Columns.Add("중량", typeof(string));
            dt.Columns.Add("PLT", typeof(string));
            dt.Columns.Add("적재율(PLT)", typeof(string));
            dt.Columns.Add("적재율(중량)", typeof(string));
            dt.Columns.Add("적재율(체적)", typeof(string));
            dt.Columns.Add("적재율(MAX)", typeof(string));

            gridDispatchingSummary.DataSource = dt;
            gridDispatchingSummary.RefreshDataSource();
        }

        private bool copyDataRow(DataRow source, DataRow target)
        {
            bool isSucceeded = false;

            try
            {
                for (int pos = 0; pos < source.ItemArray.Length; pos++)
                {
                    target.ItemArray[pos] = source.ItemArray[pos].ToString();
                }
                isSucceeded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                isSucceeded = false;
            }
            return isSucceeded;
        }

        private void gridDispathingInfo_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(DataRow)))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else if (e.Data.GetDataPresent(typeof(DataRow[])))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridDispathingInfo_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                GridHitInfo hi = gridViewDispatchingInfo.CalcHitInfo(gridDispathingInfo.PointToClient(new Point(e.X, e.Y)));
                int dropRowID = hi.RowHandle;

                if (maxTurnNo(DispathingInfoDt).Equals(0))
                {
                    insertTurnNo();
                    summary_calc(DispathingInfoDt);

                    dropRowID = 1;
                }

                if (hi.InRow || dropRowID > 0)
                {
                    // 놓여지는 위치에서의 데이터
                    GridControl grid = sender as GridControl;
                    GridView view = (GridView)grid.MainView;

                    DataTable table = grid.DataSource as DataTable;

                    DataRow dropRow = table.Rows[dropRowID];
                    object[] arrToStop = dropRow.ItemArray;

                    // 끌고올 때의 데이터
                    DataRow dragRow;
                    DataRow[] arrFrStops = e.Data.GetData(typeof(DataRow[])) as DataRow[];

                    arrFrStops = cleanseData(arrFrStops);
                    //Debug.WriteLine("After cleasing 시작-----> " + arrFrStops.Length);
                    //foreach (DataRow dr in arrFrStops)
                    //{
                    //    Debug.WriteLine(dr.ItemArray[24].ToString());
                    //}
                    //Debug.WriteLine("After cleasing 끝-----> ");

                    for (int idx = 0; idx < arrFrStops.Length; idx++)
                    {
                        gridViewDispatchingInfo.BeginUpdate();
                        //Debug.WriteLine("From Stop: " + arrFrStops[idx].ItemArray[24] + ", To Stop: " + arrToStop[24]);

                        dragRow = arrFrStops[idx];
                        object[] arrFrStop = dragRow.ItemArray;

                        if (!isStop2(arrToStop[24].ToString()))
                        {
                            //Debug.WriteLine(arrFrStop[0]);

                            gridDispathingInfo.RefreshDataSource();
                            gridNotAssignedOrder.RefreshDataSource();

                            gridViewDispatchingInfo.EndUpdate();

                            continue;
                        }

                        DataRow _row = DispathingInfoDt.NewRow();

                        if (dragRow != null && 
                            table != null && 
                            dragRow.Table == table)
                        {//같은 그리드이면
                            bool sameTurn = arrFrStop[22].ToString().Equals(dropRow.ItemArray[22].ToString());
                            bool isLastRoute = isLastOfRoute(arrToStop[24].ToString());

                            //if (sameTurn && isLastRoute)
                            //{
                            //    gridViewDispatchingInfo.EndUpdate();
                            //    continue;
                            //}

                            // 회전 수 변경
                            arrFrStop[22] = dropRow.ItemArray[22];
                            arrFrStop[23] = dropRow.ItemArray[23];
                            _row.ItemArray = arrFrStop;

                            //테이블 변경
                            int currentID = AssignedHit[idx];
                            if (currentID < dropRowID)
                            {
                                table.Rows.RemoveAt(currentID - idx);
                                table.Rows.InsertAt(_row, dropRowID-1);
                            }
                            else
                            {
                                table.Rows.RemoveAt(currentID);
                                table.Rows.InsertAt(_row, dropRowID + idx);
                            }

                            setSeqAssign(table);

                            gridDispathingInfo.RefreshDataSource();

                            gridViewDispatchingInfo.EndUpdate();
                        }
                        else if (dragRow != null && table != null && dragRow.Table != table)
                        {//같은 그리드가 아니면
                            // 회전 수 변경
                            arrFrStop[0] = dropRow.ItemArray[0];
                            arrFrStop[22] = dropRow.ItemArray[22];
                            arrFrStop[23] = dropRow.ItemArray[23];
                            _row.ItemArray = arrFrStop;

                            //테이블 변경
                            int currentID = NotAssignedHit[idx];
                            //if (currentID < dropRowID)
                            //    dragRow.Table.Rows.RemoveAt(currentID - idx);
                            //else
                            //    dragRow.Table.Rows.RemoveAt(currentID);

                            dragRow.Table.Rows.RemoveAt(currentID - idx);

                            //if (currentID < dropRowID)
                            //    table.Rows.InsertAt(_row, dropRowID);
                            //else
                                table.Rows.InsertAt(_row, dropRowID + idx);

                            //테이블 변경
                            //table.Rows.InsertAt(_row, dropRowID + idx);
                            //dragRow.Table.Rows.RemoveAt(downHitInfo2.RowHandle - idx);

                            setSeqAssign(table);
                            setSeqNotassign(NotAssignedOrderDt);

                            labelNotAssign.Text = "미할당 " + NotAssignedOrderDt.Rows.Count + "건";

                            gridDispathingInfo.RefreshDataSource();
                            gridNotAssignedOrder.RefreshDataSource();

                            gridViewDispatchingInfo.EndUpdate();
                        }
                    }
                    removeTurnInAssignedList();
                    removeSummary(DispathingInfoDt);
                    summary_calc(DispathingInfoDt);

                    downHitInfo = null;
                    lastAssignedHitInfo = null;
                    lastNotAssignedHitInfo = null;

                    clearAssignedHit();
                    clearNotAssignedHit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeTurnInAssignedList()
        {
            int stopCount = 0;
            string currentTurnNo;
            List<string> removeTurnList = new List<string>();
            //Debug.WriteLine("removeTurnInAssignedList 시작 -----> ");
            for (int pos = 0; pos < DispathingInfoDt.Rows.Count; pos++)
            {
                DataRow _row = DispathingInfoDt.Rows[pos];
                string turnNo = _row.ItemArray[22].ToString().ToString();
                string tripNo = _row.ItemArray[23].ToString().ToString();

                string stopName = _row.ItemArray[24].ToString();

                //Debug.WriteLine(string.Format("Turn No: {0}, Trip No: {1}, Stop: {2}", turnNo, tripNo, stopName));
                if (isStop(stopName))
                {
                    stopCount++;
                }
                else
                {
                    if (tripNo.Equals("0"))
                    {
                        stopCount = 0;
                        currentTurnNo = turnNo;
                    }
                    else if (tripNo.Equals("99"))
                    {
                        if (stopCount == 0)
                        {
                            removeTurnList.Add(turnNo);
                        }
                    }
                }
            }
            //Debug.WriteLine("removeTurnInAssignedList 끝 -----> ");

            int removedCount = 0;
            foreach ( string turnNo in removeTurnList )
            {
                //bool isSameTurn = false;
                for (int pos = 0; pos - removedCount < DispathingInfoDt.Rows.Count; pos++)
                {
                    DataRow _row = DispathingInfoDt.Rows[pos - removedCount];
                    //if (!turnNo.Equals(_row.ItemArray[22].ToString()))
                    //{
                    //    isSameTurn = false;
                    //}
                    //else if (turnNo.Equals(_row.ItemArray[22].ToString()) && _row.ItemArray[24].ToString().Trim().Length > 0)
                    //{
                    //    isSameTurn = true;
                    //}
                    ////Debug.WriteLine(_row.ItemArray[24].ToString());
                    //if ( isSameTurn )
                    //{
                        if (turnNo.Equals(_row.ItemArray[22].ToString()) || _row.ItemArray[24].ToString().Trim().Length == 0)
                        {
                            DispathingInfoDt.Rows.RemoveAt(pos - removedCount);
                            removedCount++;
                        }
                    //}
                }
            }
            //PrintDebug(DispathingInfoDt);
        }

        private DataTable setSeqAssign(DataTable dt)
        {
            int seq = 0, last = 99;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][23].ToString()))
                {
                    if(dt.Rows[i][24].ToString().Equals("센터 도착") || 
                        dt.Rows[i][24].ToString().Equals("차고지 도착") )
                    {
                        dt.Rows[i][23] = last;
                    }
                    else if (dt.Rows[i][24].ToString().Equals("센터 출발"))
                    {
                        seq = 0;
                        dt.Rows[i][23] = seq.ToString();
                    }
                    else
                    {
                        dt.Rows[i][23] = seq.ToString();
                    }
                    seq++;
                }
                else
                {
                    seq = 1;
                }
            }
            return dt;
        }

        private DataTable setSeqNotassign(DataTable dt)
        {
            int seq = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][23].ToString()))
                {
                    dt.Rows[i][23] = seq.ToString();
                    seq++;
                }
            }
            return dt;
        }

        private DataTable sortDataTableByDistrict(DataTable dt)
        {
            DataTable newDT = new DataTable();

            return newDT;
        }

        private void gridViewDispatchingInfo_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            try
            {
                if (!allowLeave)
                {
                    e.Allow = false;
                    allowLeave = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewDispatchingInfo_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));

                if (!isButtonClicked())
                    return;

                if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                {
                    // 선택할 때 배송지 정보만 선택하도록 하는 부분
                    DataRow selectedRow = DispathingInfoDt.Rows[hitInfo.RowHandle];
                    if (!isStop(selectedRow.ItemArray[24].ToString()))
                    {
                        allowLeave = false;
                        return;
                    }

                    clearAssignedHit();
                    //int[] selectedRows = view.GetSelectedRows();
                    setAssignedHits(view.GetSelectedRows());
                    //Debug.WriteLine("DispatchingInfo Mouse Up ----> view에서 선택된 착지명-----> 시작");
                    //for (int pos = 0; pos < selectedRows.Length; pos++)
                    //{
                    //    string selectedStopName = DispathingInfoDt.Rows[selectedRows[pos]].ItemArray[24].ToString();
                    //    //Debug.WriteLine(selectedStopName);
                    //    if (isStop(selectedStopName))
                    //    {
                    //        setAssignedHit(selectedRows[pos]);
                    //    }
                    //}
                    //Debug.WriteLine("DispatchingInfo Mouse Up ----> view에서 선택된 착지명-----> 끝\n");

                    //Debug.WriteLine("DispatchingInfo Mouse Up AssignedHit 출력 -----> 시작");
                    //for (int pos = 0; pos < AssignedHit.Count; pos++)
                    //{
                    //    string stopName = DispathingInfoDt.Rows[AssignedHit[pos]].ItemArray[24].ToString();
                    //    Debug.WriteLine(stopName);
                    //}
                    //Debug.WriteLine("DispatchingInfo Mouse Up AssignedHit 출력 -----> 끝");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewDispatchingInfo_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));

                if (!isButtonClicked())
                    return;

                if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                {
                    DataRow selectedRow = DispathingInfoDt.Rows[hitInfo.RowHandle];
                    if (!isStop(selectedRow.ItemArray[24].ToString()))
                    {
                        allowLeave = false;
                        return;
                    }

                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        if (lastAssignedHitInfo == null)
                            lastAssignedHitInfo = hitInfo;
                        downHitInfo = hitInfo;

                        clearAssignedHit();
                        setAssignedHit(downHitInfo.RowHandle, lastAssignedHitInfo.RowHandle);
                    }
                    else if (Control.ModifierKeys == Keys.Control)
                    {
                        lastAssignedHitInfo = hitInfo;
                        downHitInfo = hitInfo;

                        setAssignedHit(downHitInfo.RowHandle);
                    }
                    else if (Control.ModifierKeys == Keys.None)
                    {
                        lastAssignedHitInfo = hitInfo;
                        downHitInfo = hitInfo;

                        clearAssignedHit();

                        setAssignedHit(downHitInfo.RowHandle);
                    }

                    AssignedHit.Sort();
                    //Debug.WriteLine("\nDispatchingInfo Mouse Down -----> 선택 시작(" + AssignedHit.Count + ")");
                    //string lastHitStopName = DispathingInfoDt.Rows[lastAssignedHitInfo.RowHandle].ItemArray[24].ToString();
                    //Debug.WriteLine("현재 설정된 착지명: " + lastHitStopName);

                    //for (int pos = 0; pos < AssignedHit.Count; pos++)
                    //{
                    //    string StopName = DispathingInfoDt.Rows[AssignedHit[pos]].ItemArray[24].ToString();
                    //    Debug.WriteLine(StopName);
                    //}
                    //Debug.WriteLine("DispatchingInfo Mouse Down -----> 선택 끝\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewDispatchingInfo_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (DispathingInfoDt.Rows.Count == 0)
                    return;

                if (e.Button == MouseButtons.Left && downHitInfo != null)
                {
                    Size dragSize = SystemInformation.DragSize;

                    Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                        downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                    if (!dragRect.Contains(new Point(e.X, e.Y)))
                    {
                        //view.FocusedRowHandle = downHitInfo.RowHandle;

                        DataRow[] rows = new DataRow[AssignedHit.Count];
                        for (int idx = 0; idx < AssignedHit.Count; idx++)
                        {
                            rows[idx] = DispathingInfoDt.Rows[AssignedHit[idx]];
                        }

                        string currentStopName = DispathingInfoDt.Rows[downHitInfo.RowHandle].ItemArray[24].ToString();
                        if (isStop(currentStopName))
                        {
                            view.GridControl.DoDragDrop(rows, DragDropEffects.Move);
                        }

                        //downHitInfo = null;

                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridNotAssignedOrder_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(typeof(DataRow)))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else if (e.Data.GetDataPresent(typeof(DataRow[])))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridNotAssignedOrder_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                // 놓여지는 위치에서의 데이터
                GridControl grid = sender as GridControl;
                DataTable table = grid.DataSource as DataTable;

                // 끌고올 때의 데이터
                DataRow dragRow;
                DataRow[] arrFrStops = e.Data.GetData(typeof(DataRow[])) as DataRow[];

                arrFrStops = cleanseData(arrFrStops);
                //Debug.WriteLine("\nAfter cleasing -----> 시작 ---> " + arrFrStops.Length);
                //foreach (DataRow dr in arrFrStops)
                //{
                //    Debug.WriteLine(dr.ItemArray[24].ToString());
                //}
                //Debug.WriteLine("After cleasing -----> 끝");

                for (int idx = 0; idx < arrFrStops.Length; idx++)
                {
                    gridViewNotAssignedOrder.BeginUpdate();//

                    dragRow = arrFrStops[idx];
                    object[] arrFrStop = dragRow.ItemArray;

                    if (!isStop(arrFrStop[24].ToString()))
                    {
                        Debug.WriteLine(arrFrStop[0]);

                        gridDispathingInfo.RefreshDataSource();
                        gridNotAssignedOrder.RefreshDataSource();

                        gridViewNotAssignedOrder.EndUpdate();

                        continue;
                    }

                    DataRow _row = NotAssignedOrderDt.NewRow();
                    if (dragRow != null && table != null && dragRow.Table != table)
                    {//다른 grid이면
                        table.ImportRow(dragRow);
                        dragRow.Table.Rows.RemoveAt(AssignedHit[idx] - idx);
                        setSeqAssign(DispathingInfoDt);
                        gridDispathingInfo.RefreshDataSource();
                    }

                    setSeqNotassign(NotAssignedOrderDt);

                    gridNotAssignedOrder.RefreshDataSource();
                    gridViewNotAssignedOrder.EndUpdate();//

                    labelNotAssign.Text = "미할당 " + NotAssignedOrderDt.Rows.Count + "건";
                }
                removeTurnInAssignedList();
                removeSummary(DispathingInfoDt);
                summary_calc(DispathingInfoDt);

                if (DispathingInfoDt.Rows.Count == 0)
                    downHitInfo = null;

                downHitInfo2 = null;
                lastAssignedHitInfo = null;
                lastNotAssignedHitInfo = null;

                clearAssignedHit();
                clearNotAssignedHit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewNotAssignedOrder_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                //downHitInfo = null;

                GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));

                if (!isButtonClicked())
                    return;

                if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                {
                    DataRow selectedRow = NotAssignedOrderDt.Rows[hitInfo.RowHandle];
                    if (!isStop(selectedRow.ItemArray[24].ToString()))
                    {
                        //allowLeave2 = false;
                        return;
                    }

                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        if (lastNotAssignedHitInfo == null)
                            lastNotAssignedHitInfo = hitInfo;
                        downHitInfo2 = hitInfo;

                        clearNotAssignedHit();
                        setNotAssignedHit(downHitInfo2.RowHandle, lastNotAssignedHitInfo.RowHandle);
                    }
                    else if (Control.ModifierKeys == Keys.Control)
                    {
                        lastNotAssignedHitInfo = hitInfo;
                        downHitInfo2 = hitInfo;

                        setNotAssignedHit(downHitInfo2.RowHandle);
                    }
                    else if (Control.ModifierKeys == Keys.None)
                    {
                        lastNotAssignedHitInfo = hitInfo;
                        downHitInfo2 = hitInfo;

                        clearNotAssignedHit();
                        setNotAssignedHit(downHitInfo2.RowHandle);
                    }

                    NotAssignedHit.Sort();
                    Debug.WriteLine("\nNotAssignedOrderDt Mouse Down -----> 선택 시작(" + NotAssignedHit.Count + ")");
                    string lastHitStopName = NotAssignedOrderDt.Rows[lastNotAssignedHitInfo.RowHandle].ItemArray[24].ToString();
                    Debug.WriteLine("현재 설정된 착지명: " + lastHitStopName);

                    for (int pos = 0; pos < NotAssignedHit.Count; pos++)
                    {
                        string StopName = NotAssignedOrderDt.Rows[NotAssignedHit[pos]].ItemArray[24].ToString();
                        Debug.WriteLine(StopName);
                    }
                    Debug.WriteLine("NotAssignedOrderDt Mouse Down -----> 선택 끝\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewNotAssignedOrder_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (NotAssignedOrderDt.Rows.Count == 0)
                    return;

                if (e.Button == MouseButtons.Left && downHitInfo2 != null)
                {
                    Size dragSize = SystemInformation.DragSize;
                    
                    Rectangle dragRect = new Rectangle(new Point(downHitInfo2.HitPoint.X - dragSize.Width / 2,
                        downHitInfo2.HitPoint.Y - dragSize.Height / 2), dragSize);

                    if (!dragRect.Contains(new Point(e.X, e.Y)))
                    {
                        //Debug.WriteLine("\nNotAssignedOrderDt Mouse Move -----> 시작");
                        DataRow[] rows = new DataRow[NotAssignedHit.Count];
                        for (int idx = 0; idx < NotAssignedHit.Count; idx++)
                        {
                            rows[idx] = NotAssignedOrderDt.Rows[NotAssignedHit[idx]];
                            //string stopName = NotAssignedOrderDt.Rows[NotAssignedHit[idx]].ItemArray[24].ToString();
                            //Debug.WriteLine(stopName);
                        }
                        //Debug.WriteLine("NotAssignedOrderDt Mouse Move -----> 끝");

                        string currentStopName = NotAssignedOrderDt.Rows[downHitInfo2.RowHandle].ItemArray[24].ToString();
                        if (isStop(currentStopName))
                        {
                            view.GridControl.DoDragDrop(rows, DragDropEffects.Move);
                        }

                        //downHitInfo = null;

                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewNotAssignedOrder_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));

                if (!isButtonClicked())
                    return;

                if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                {
                    // 선택할 때 배송지 정보만 선택하도록 하는 부분
                    DataRow selectedRow = NotAssignedOrderDt.Rows[hitInfo.RowHandle];
                    if (!isStop(selectedRow.ItemArray[24].ToString()))
                    {
                        //allowLeave2 = false;
                        return;
                    }

                    clearNotAssignedHit();
                    int[] selectedRows = view.GetSelectedRows();
                    setNotAssignedHits(selectedRows);
                    Debug.WriteLine("NotAssignedOrderDt Mouse Up ----> view에서 선택된 착지명-----> 시작");
                    for (int pos = 0; pos < selectedRows.Length; pos++)
                    {
                        string selectedStopName = NotAssignedOrderDt.Rows[selectedRows[pos]].ItemArray[24].ToString();
                        Debug.WriteLine(selectedStopName);
                        //if (isStop(selectedStopName))
                        //{
                        //    setNotAssignedHit(selectedRows[pos]);
                        //}
                    }
                    Debug.WriteLine("NotAssignedOrderDt Mouse Up ----> view에서 선택된 착지명-----> 끝\n");

                    Debug.WriteLine("NotAssignedOrderDt Mouse Up NotAssignedHit 출력 -----> 시작");
                    for (int pos = 0; pos < NotAssignedHit.Count; pos++)
                    {
                        string stopName = NotAssignedOrderDt.Rows[NotAssignedHit[pos]].ItemArray[24].ToString();
                        Debug.WriteLine(stopName);
                    }
                    Debug.WriteLine("NotAssignedOrderDt Mouse Up AssignedHit 출력 -----> 끝");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //드래그시 사용되는 공통 메소드
        public void SetDevExpressGridDragOver(object sender, DragEventArgs e, bool dragDropFlag)
        {
            if (e.AllowedEffect == DragDropEffects.Move)
            {
                GridControl gridControl = sender as GridControl;
                DataTable targetDt = gridControl.DataSource as DataTable;

                e.Effect = DragDropEffects.Move;

                if (!dragDropFlag)
                {
                    object[] objectArray = e.Data.GetData(typeof(object[])) as object[];
                    List<DataRowView> sourceMultiDataRowView = objectArray[1] as List<DataRowView>;

                    if (targetDt.TableName == sourceMultiDataRowView[0].Row.Table.TableName)
                    {
                        e.Effect = DragDropEffects.None;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                }
            }
        }

        //색상 변경
        public void SetRowsColorChangeCustomerRowCell(GridView gridView, RowCellCustomDrawEventArgs e, Color color, string sourceFild, string targetValue)
        {
            if (gridView.Columns[sourceFild] == null) return;

            gridView.BeginUpdate();

            if (gridView.GetRowCellValue(e.RowHandle, gridView.Columns[sourceFild]).Equals(targetValue))
            {
                if (e.Column.FieldName == "LINE")
                {
                    e.Column.AppearanceCell.BackColor = color;
                }
            }

            gridView.EndUpdate();
        }

        //해당 컬럼 매칭 gridView1
        private void gridView1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                GridHitInfo hitInfo = view.CalcHitInfo(e.Point);

                if (hitInfo.InRow)
                {
                    view.FocusedRowHandle = hitInfo.RowHandle;

                    contextMenuStrip1.Show(view.GridControl, e.Point);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //총보너스 팝업
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FormBonus frmBounus = new FormBonus();
            frmBounus.ShowDialog();
        }

        //주문정보 팝업
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            FormOrderInfoList fomOrderInfoList = new FormOrderInfoList();
            fomOrderInfoList.ShowDialog();
        }

        //해당 컬럼 매칭 gridView
        private void gridViewDispatchingInfo_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                GridHitInfo hitInfo = view.CalcHitInfo(e.Point);

                if (hitInfo.InRow)
                {
                    view.FocusedRowHandle = hitInfo.RowHandle;

                    contextMenuStrip2.Show(view.GridControl, e.Point);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //메모 팝업
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            FormMemoView frmMemoView = new FormMemoView();
            frmMemoView.ShowDialog();       
        }

        private void setCurrentVehicle(DataRow vehicle)
        {
            currentVehicle.clear();

            object[] item = vehicle.ItemArray;

            currentVehicle.vehicleNo = item[0].ToString();
            currentVehicle.vehicleTon = item[2].ToString();
            currentVehicle.vehicleType = item[3].ToString();
            currentVehicle.ownerShip = item[4].ToString();
            currentVehicle.maxVolume = item[5].ToString();
            currentVehicle.maxWeight = item[6].ToString();
            currentVehicle.maxPallet = item[7].ToString();
            currentVehicle.maxTurnCount = item[8].ToString();
            currentVehicle.maxTripCount = item[9].ToString();
            currentVehicle.startWorkTime = item[10].ToString();
            currentVehicle.endWorkTime = item[11].ToString();
            currentVehicle.district01 = item[12].ToString();
            currentVehicle.maxMultiDistrict01 = item[13].ToString();
            currentVehicle.reAllocation01 = item[14].ToString();
            currentVehicle.allocation01 = item[15].ToString();
            currentVehicle.district02 = item[16].ToString();
            currentVehicle.maxMultiDistrict02 = item[17].ToString();
            currentVehicle.allocation02 = item[18].ToString();
            currentVehicle.reAllocation02 = item[19].ToString();
            currentVehicle.allocation03 = item[20].ToString();
            currentVehicle.reAllocation03 = item[21].ToString();

            currentVehicle.printVehicle();
        }

        //차량 리스트 클릭 
        private void gridCarList_Click(object sender, EventArgs e)
        {
            try
            {
                setStartProgressPanel();

                if (!gridView1.IsSizingState && gridView1.FocusedRowHandle >= 0)
                {
                    list_pos.Clear();

                    int nSelRows = gridView1.SelectedRowsCount;

                    int row = 0;
                    if ( nSelRows > 0 )
                        row = gridView1.GetSelectedRows()[0];

                    carNumber = (gridView1.GetRowCellValue(row, "VEHCID")).ToString();

                    FormConfirmDispatching formConfirmDispatching = this.Parent as FormConfirmDispatching;
                    formConfirmDispatching.textCar.Text = carNumber;

                    string strCenterName = "[" + ApplicationKey.company + "] THiRA";
                    foreach (var ctrl in formConfirmDispatching.Controls)
                    {
                        string this_type = ctrl.GetType().ToString();
                        //Debug.WriteLine(this_type);

                        if (ctrl.GetType() == typeof(DevExpress.XtraEditors.PanelControl))
                        {
                            DevExpress.XtraEditors.PanelControl pc = ctrl as DevExpress.XtraEditors.PanelControl;
                            pc.Controls["comboBoxEdit1"].Text = strCenterName;
                            pc.Controls["comboBoxEdit2"].Text = strCenterName;
                        }
                    }

                    comboBoxEdit1.Text = strCenterName;

                    //gridCarInfo.DataSource = carUsageOradt;
                    //gridCarInfo.RefreshDataSource();

                    // 배차 정보 조회
                    setAllocList(carNumber);

                    //이미 최초에 데이터를 가져왔기 때문에 여기에서 다시 차량정보 가져오지 않음. 20140326, LSY
                    if (DispathingInfoDt == null)
                    {
                        bizService = getBizService();
                        DispathingInfoDt = bizService.sp_vro_get_caralloc_phalist("ALLOCATED", carNumber);
                    }

                    if (DispathingInfoDt.Rows.Count > 0)
                    {
                        setCurrentVehicle(DispathingInfoDt.Rows[0]);

                        summary_calc(DispathingInfoDt);

                        gridDispathingInfo.DataSource = DispathingInfoDt;
                        gridDispathingInfo.RefreshDataSource();

                        //Debug.WriteLine(gridViewDispatchingInfo.Columns.ColumnByName("gridColumn1").ToString());

                        int nMaxCol = gridViewDispatchingInfo.VisibleColumns.Count;

                        setColumnWidth(gridViewDispatchingInfo);

                        //미할당 주문 목록 조회
                        setNotAllocList();

                        if (NotAssignedOrderDt == null)
                        {
                            bizService = getBizService();
                            NotAssignedOrderDt = bizService.sp_vro_get_caralloc_phalist("NOT ASSIGNED", carNumber);
                        }

                        labelNotAssign.Text = "미할당 " + NotAssignedOrderDt.Rows.Count + "건";

                        gridNotAssignedOrder.DataSource = NotAssignedOrderDt;
                        gridNotAssignedOrder.RefreshDataSource();

                        setColumnWidth(gridViewNotAssignedOrder);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                progressPanel1.Visible = false;
            }
        }

        private void setAllocList(string carNo)
        {
            clearAllocList();

            foreach (DataRow _row in dtAllocList.Rows)
            {
                if (_row.ItemArray[0].ToString().Trim().Equals(carNo))
                {
                    DataRow _dr = DispathingInfoDt.NewRow();

                    _dr.ItemArray = _row.ItemArray;

                    int dicKey = int.Parse(_row["TURNNO"].ToString() + _row["TRIPNO"].ToString());
                    DispathingInfoDt.Rows.InsertAt(_dr, dicKey);
                }
            }
            //PrintDebug(DispathingInfoDt);
        }

        private void setNotAllocList()
        {
            clearNotAllocList();

            foreach (DataRow _row in dtNonAllocList.Rows)
            {
                DataRow _dr = NotAssignedOrderDt.NewRow();

                _dr.ItemArray = _row.ItemArray;

                int dicKey = int.Parse(_row["TURNNO"].ToString() + _row["TRIPNO"].ToString());
                NotAssignedOrderDt.Rows.InsertAt(_dr, dicKey);
            }
            //PrintDebug(NotAssignedOrderDt);
        }

        private void clearAllocList()
        {
            DispathingInfoDt.Clear();
        }

        private void clearNotAllocList()
        {
            NotAssignedOrderDt.Clear();
        }

        //summary 계산
        private void summary_calc(OracleDataTable DispathingInfoDt)
        {
            //각회전의 마지막 row 저장 <각 회전 name, 회전의 첫번째 row >
            Dictionary<string, int> dic_summary_row = new Dictionary<string, int>();
            List<string> turnList = new List<string>();

            int summary_row_loc = 0;
            //중복을 뺀 회전 넘버 리스트에 담기 , 각 회전의 마지막 row 위치 저장
            foreach (DataRow dr in DispathingInfoDt.Rows)
            {
                string turnNo = dr.ItemArray[22].ToString();
                if (!dic_summary_row.ContainsKey(turnNo) && turnNo.Trim().Length > 0)
                {
                    dic_summary_row.Add(turnNo, summary_row_loc);
                    turnList.Add(turnNo);
                }
                summary_row_loc++;
            }

            //int pos = 0;//summary 삽입할 위치 변수

            //컬럼의 summary 값
            float weight = 0;//중량
            float volume = 0;//부피
            float boxcnt = 0;//박스
            float pltcnt = 0;//팔레트

            int insertPos = 0;
            for (int idx = 0; idx < turnList.Count; idx++)
            {
                weight = 0;
                volume = 0;
                boxcnt = 0;
                pltcnt = 0;

                string currentTurnNo = turnList[idx];
                foreach (DataRow _row in DispathingInfoDt.Rows)
                {
                    string turnNo = _row.ItemArray[22].ToString();
                    if (turnNo.Equals(currentTurnNo) && turnNo.Trim().Length > 0)
                    {
                        weight += float.Parse(_row.ItemArray[38].ToString() + "");
                        volume += float.Parse(_row.ItemArray[39].ToString() + "");
                        boxcnt += float.Parse(_row.ItemArray[37].ToString() + "");
                        pltcnt += float.Parse(_row.ItemArray[40].ToString() + "");
                    }
                }
                DataRow _dr = DispathingInfoDt.NewRow();

                if ( (idx + 1) < turnList.Count)
                {
                    insertPos = dic_summary_row[turnList[idx + 1]] + idx;

                    _dr["WEIGHT"] = weight + "";
                    _dr["VOLUME"] = volume + "";
                    _dr["BOXCNT"] = boxcnt + "";
                    _dr["PLTCNT"] = pltcnt + "";

                    DispathingInfoDt.Rows.InsertAt(_dr, insertPos);
                    list_pos.Add(insertPos);
                }
                else
                {
                    insertPos = DispathingInfoDt.Rows.Count;

                    _dr["WEIGHT"] = weight + "";
                    _dr["VOLUME"] = volume + "";
                    _dr["BOXCNT"] = boxcnt + "";
                    _dr["PLTCNT"] = pltcnt + "";

                    DispathingInfoDt.Rows.InsertAt(_dr, insertPos);
                    list_pos.Add(insertPos);
                }
            }
            //DataRow lastDR = DispathingInfoDt.NewRow();

            //lastDR["WEIGHT"] = weight + "";
            //lastDR["VOLUME"] = volume + "";
            //lastDR["BOXCNT"] = boxcnt + "";
            //lastDR["PLTCNT"] = pltcnt + "";

            //DispathingInfoDt.Rows.InsertAt(lastDR, DispathingInfoDt.Rows.Count);
            //list_pos.Add(pos);

            ////각 회전이 끝날때마다 마지막 row에 summary 삽입
            //for (int i = 2; i <= dic_summary_row.Count; i++)//2번째 회전의 앞에 summary row 삽입
            //{
            //    weight = 0;
            //    volume = 0;
            //    boxcnt = 0;
            //    pltcnt = 0;
            //    //회전이 끝날때마다 summary 입력
            //    foreach (DataRow dr1 in DispathingInfoDt.Rows)
            //    {
            //        string turnNo = dr1.ItemArray[22].ToString();
            //        //if (dr1["TURNNO"].ToString() == (i - 1) + "")//회전수 비교 I-1 : 1회전부터 SUMMARY
            //        {
            //            if (!dr1["WEIGHT"].ToString().Equals(""))
            //            {
            //                weight += float.Parse(dr1["WEIGHT"].ToString());
            //            }
            //            if (!dr1["VOLUME"].ToString().Equals(""))
            //            {
            //                volume += float.Parse(dr1["VOLUME"].ToString());
            //            }
            //            if (!dr1["BOXCNT"].ToString().Equals(""))
            //            {
            //                boxcnt += float.Parse(dr1["BOXCNT"].ToString());
            //            }
            //            if (!dr1["PLTCNT"].ToString().Equals(""))
            //            {
            //                pltcnt += float.Parse(dr1["PLTCNT"].ToString());
            //            }
            //        }
            //    }

            //    //row 삽입
            //    DataRow _dr = DispathingInfoDt.NewRow();

            //    string key = string.Format("{0}",i);

            //    pos = int.Parse(dic_summary_row[key].ToString());
            //    if (i != 2)//첫번째 삽입시에는 row insert에 대한 영향을 받지 않음.. 이후 삽입은 i-2 의 pos 값 추가가 필요
            //        pos += i - 2;

            //    //_dr[""] =
            //    _dr["WEIGHT"] = weight + "";
            //    _dr["VOLUME"] = volume + "";
            //    _dr["BOXCNT"] = boxcnt + "";
            //    _dr["PLTCNT"] = pltcnt + "";

            //    DispathingInfoDt.Rows.InsertAt(_dr, pos);
            //    list_pos.Add(pos);
            //}

            ////마지막은 따로 계산
            //weight = 0;
            //volume = 0;
            //boxcnt = 0;
            //pltcnt = 0;
            //int last_turnno = 0;
            ////int nITR = 0;
            ////회전이 끝날때마다 summary 입력
            //foreach (DataRow dr1 in DispathingInfoDt.Rows)
            //{
            //    //마지막 회전수
            //    last_turnno = dic_summary_row.Count;

            //    //Debug.WriteLine("\n" + nITR);
            //    //Console.WriteLine("\n" + nITR++);
            //    //int nMaxRow = DispathingInfoDt.Columns.Count;
            //    //for (int idx = 0; idx < nMaxRow; idx++)
            //    //{
            //    //    Debug.Write(string.Format("\t{0}={1}", idx, dr1[idx]));
            //    //    Console.Write(string.Format("\t{0}={1}", idx, dr1[idx]));
            //    //}
            //    if (dr1["TURNNO"].ToString() == last_turnno + "")
            //    {
            //        if (!dr1["WEIGHT"].ToString().TrimEnd().Equals(""))
            //            weight += float.Parse(dr1["WEIGHT"].ToString());
            //        if (!dr1["VOLUME"].ToString().TrimEnd().Equals(""))
            //            volume += float.Parse(dr1["VOLUME"].ToString());
            //        if (!dr1["BOXCNT"].ToString().TrimEnd().Equals(""))
            //            boxcnt += float.Parse(dr1["BOXCNT"].ToString());
            //        if (!dr1["PLTCNT"].ToString().TrimEnd().Equals(""))
            //            pltcnt += float.Parse(dr1["PLTCNT"].ToString());
            //    }
            //}
            //DataRow lastdr = DispathingInfoDt.NewRow();

            //lastdr["WEIGHT"] = weight + "";
            //lastdr["VOLUME"] = volume + "";
            //lastdr["BOXCNT"] = boxcnt + "";
            //lastdr["PLTCNT"] = pltcnt + "";

            //DispathingInfoDt.Rows.Add(lastdr);
            //list_pos.Add(DispathingInfoDt.Rows.Count-1);

            //배차 작업 내역 화면 
            textBox.Text = boxcnt + "";
            textPLT.Text = pltcnt + "";
            textWeight.Text = weight + "";
            textVolume.Text = volume + "";
        }

        public void gridViewDispatchingInfo_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            foreach (int pos in list_pos)
            {
                if (e.RowHandle == pos)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }

        public void setStartProgressPanel()
        {
            Control parent = progressPanel1.Parent;
            progressPanel1.Location = new Point(parent.Bounds.X + parent.Bounds.Width / 2 - progressPanel1.Width / 2, parent.Bounds.Y + parent.Bounds.Height / 2 - progressPanel1.Height / 2);
            progressPanel1.BackColor = Color.LightGray;
            progressPanel1.BorderStyle = BorderStyles.Office2003;
            progressPanel1.BringToFront();
            progressPanel1.Visible = true;

            Application.DoEvents();
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
                    //Console.Write(string.Format("\t{0}", row2[idx]));
                }
                Debug.WriteLine(Environment.NewLine);
                //Console.WriteLine("");
                nExit++;
                if (nExit == 10) break;
            }
        }

        private void PrintDebug(DataTable dt)
        {
            int rCnt = dt.Rows.Count;
            int cCnt = dt.Columns.Count;
            int nExit = 0;
            foreach (DataRow row2 in dt.Rows)
            {
                for (int idx = 0; idx < dt.Columns.Count; idx++)
                {
                    Debug.Write(string.Format("\t{0}", row2[idx]));
                    //Console.Write(string.Format("\t{0}", row2[idx]));
                }
                Debug.WriteLine(Environment.NewLine);
                //Console.WriteLine(Environment.NewLine);
                nExit++;
                if (nExit == 10) break;
            }
        }

        private void setColumnWidth(GridView gridView)
        {
            //Debug.WriteLine("");
            ControlCollection controlCollection = Controls;
            foreach (Control control in this.Controls)
            {
                if (control.Name.Equals("dockPanel1"))
                {
                    foreach (Control childControl in control.Controls)
                    {
                        if (childControl.Name.Equals("dockPanel1_Container"))
                        {
                            foreach (Control grandChildControl in childControl.Controls)
                            {
                                if (grandChildControl.Name.Equals(gridView.GridControl.Name))
                                {
                                    gridView.Columns[0].Width = 90;
                                    gridView.Columns[1].Width = 40;
                                    gridView.Columns[2].Width = 70;
                                    gridView.Columns[3].Width = 40;
                                    gridView.Columns[4].Width = 40;
                                    gridView.Columns[5].Width = 40;
                                    gridView.Columns[6].Width = 40;
                                    gridView.Columns[7].Width = 40;
                                    gridView.Columns[8].Width = 40;
                                    gridView.Columns[9].Width = 40;

                                    gridView.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[3].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[5].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[7].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[8].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[9].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                                    gridView.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    gridView.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

                                    gridView.Columns[3].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                                    gridView.Columns[4].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                                    gridView.Columns[5].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                                    return;
                                }
                                //Debug.WriteLine(grandChildControl.Name);
                            }
                        }
                        //Debug.WriteLine(childControl.Name);
                    }
                    //Debug.WriteLine(control.Name);
                }
                else if (control.Name.Equals(gridView.GridControl.Name))
                {
                    gridView.VisibleColumns[0].Width = 35;
                    gridView.VisibleColumns[1].Width = 120;
                    gridView.VisibleColumns[2].Width = 40;
                    gridView.VisibleColumns[3].Width = 40;
                    gridView.VisibleColumns[4].Width = 250;
                    gridView.VisibleColumns[5].Width = 70;
                    gridView.VisibleColumns[6].Width = 70;
                    gridView.VisibleColumns[7].Width = 70;
                    gridView.VisibleColumns[8].Width = 70;
                    gridView.VisibleColumns[9].Width = 70;
                    gridView.VisibleColumns[10].Width = 70;
                    gridView.VisibleColumns[11].Width = 70;
                    gridView.VisibleColumns[12].Width = 70;
                    gridView.VisibleColumns[13].Width = 120;
                    gridView.VisibleColumns[14].Width = 70;

                    //Debug.WriteLine(control.Name);

                    return;
                }
                else if (control.Name.Equals("dockPanel4"))
                {
                    foreach (Control childControl in control.Controls)
                    {
                        if (childControl.Name.Equals("dockPanel4_Container"))
                        {
                            foreach (Control grandChildControl in childControl.Controls)
                            {
                                if (grandChildControl.Name.Equals(gridView.GridControl.Name))
                                {
                                    gridView.VisibleColumns[0].Width = 35;
                                    gridView.VisibleColumns[1].Width = 120;
                                    gridView.VisibleColumns[2].Width = 40;
                                    gridView.VisibleColumns[3].Width = 40;
                                    gridView.VisibleColumns[4].Width = 250;
                                    gridView.VisibleColumns[5].Width = 70;
                                    gridView.VisibleColumns[6].Width = 70;
                                    gridView.VisibleColumns[7].Width = 70;
                                    gridView.VisibleColumns[8].Width = 70;
                                    gridView.VisibleColumns[9].Width = 70;
                                    gridView.VisibleColumns[10].Width = 70;
                                    gridView.VisibleColumns[11].Width = 70;
                                    gridView.VisibleColumns[12].Width = 70;
                                    gridView.VisibleColumns[13].Width = 120;
                                    gridView.VisibleColumns[14].Width = 70;

                                    return;
                                }
                                //Debug.WriteLine(grandChildControl.Name);
                            }
                        }
                        //Debug.WriteLine(childControl.Name);
                    }
                }
                //Debug.WriteLine(control.Name);
            }
        }

        private BizService getBizService()
        {
            return (BizService)_applicationContext.GetObject("BizService");
        }

        private bool isStop(string stopName)
        {
            if (stopName.Trim().Length == 0 ||
                stopName.Equals("센터 출발") || 
                stopName.Equals("센터 도착") || 
                stopName.Equals("차고지 도착"))
            {
                return false;
            }
            return true;
        }

        private bool isStop2(string stopName)
        {
            if (stopName.Trim().Length == 0 ||
                stopName.Equals("센터 출발"))
            {
                return false;
            }
            return true;
        }

        private bool isLastOfRoute(string stopName)
        {
            if (stopName.Equals("센터 도착") ||
                stopName.Equals("차고지 도착"))
            {
                return true;
            }
            return false;
        }

        private void setAssignedHit(int first, int last)
        {
            int start = Min(first, last);
            int end = Max(first, last);

            for (int pos = start; pos <= end; pos++)
            {
                DataRow currentRow = DispathingInfoDt.Rows[pos];
                if (isStop(currentRow.ItemArray[24].ToString()))
                {
                    AssignedHit.Add(pos);
                }
            }
        }

        private void setAssignedHit(int element)
        {
            AssignedHit.Add(element);
        }

        private void setAssignedHits(int[] selectedRows)
        {
            for (int pos = 0; pos < selectedRows.Length; pos++)
            {
                string selectedStopName = DispathingInfoDt.Rows[selectedRows[pos]].ItemArray[24].ToString();
                if (isStop(selectedStopName))
                {
                    setAssignedHit(selectedRows[pos]);
                }
            }
        }

        private void removeAtAssignedHit(int element)
        {
            //for (int pos = 0; pos < AssignedHit.Count; pos++)
            //{
                AssignedHit.Remove(element);
            //}
        }

        private void clearAssignedHit()
        {
            AssignedHit.Clear();
        }

        private void setNotAssignedHit(int first, int last)
        {
            int start = Min(first, last);
            int end = Max(first, last);

            for (int pos = start; pos <= end; pos++)
            {
                DataRow currentRow = NotAssignedOrderDt.Rows[pos];
                if (isStop(currentRow.ItemArray[24].ToString()))
                {
                    NotAssignedHit.Add(pos);
                }
            }
        }

        private void setNotAssignedHit(int element)
        {
            NotAssignedHit.Add(element);
        }

        private void setNotAssignedHits(int[] selectedRows)
        {
            for (int pos = 0; pos < selectedRows.Length; pos++)
            {
                string selectedStopName = NotAssignedOrderDt.Rows[selectedRows[pos]].ItemArray[24].ToString();
                if (isStop(selectedStopName))
                {
                    setNotAssignedHit(selectedRows[pos]);
                }
            }
        }

        private void insertTurnNo()
        {
            int turnNo = maxTurnNo(DispathingInfoDt);
            DataRow _row = DispathingInfoDt.NewRow();

            makeTurnStart(DispathingInfoDt, turnNo);
            makeTurnEnd(DispathingInfoDt, turnNo);
        }

        private void makeTurnStart(OracleDataTable oDT, int turnNo)
        {
            DataRow _row = DispathingInfoDt.NewRow();

            object[] obj = _row.ItemArray;

            obj[0] = currentVehicle.vehicleNo;
            obj[2] = currentVehicle.vehicleTon;
            obj[3] = currentVehicle.vehicleType;
            obj[4] = currentVehicle.ownerShip;
            obj[5] = currentVehicle.maxVolume;
            obj[6] = currentVehicle.maxWeight;
            obj[7] = currentVehicle.maxPallet;
            obj[8] = currentVehicle.maxTurnCount;
            obj[9] = currentVehicle.maxTripCount;
            obj[10] = currentVehicle.startWorkTime;
            obj[11] = currentVehicle.endWorkTime;
            obj[12] = currentVehicle.district01;
            obj[13] = currentVehicle.maxMultiDistrict01;
            obj[14] = currentVehicle.reAllocation01;
            obj[15] = currentVehicle.allocation01;
            obj[16] = currentVehicle.district02;
            obj[17] = currentVehicle.maxMultiDistrict02;
            obj[18] = currentVehicle.reAllocation02;
            obj[19] = currentVehicle.allocation02;
            obj[20] = currentVehicle.allocation03;
            obj[21] = currentVehicle.reAllocation03;

            obj[22] = turnNo + 1 + "";
            obj[23] = "0";
            obj[24] = "센터 출발";

            obj[33] = currentVehicle.startWorkTime + "-" + currentVehicle.endWorkTime;
            obj[36] = "0";
            obj[37] = "0";
            obj[38] = "0";
            obj[39] = "0";
            obj[40] = "0";
            obj[41] = currentVehicle.startWorkTime;
            obj[41] = currentVehicle.startWorkTime;
            int startTm = int.Parse(currentVehicle.startWorkTime.Substring(0, 2));
            string workTM = string.Format("{0}", int.Parse(currentVehicle.startWorkTime.Substring(0, 2)) + 1).PadLeft(2, '0');
            obj[43] = string.Format("{0}:00:00", workTM);
            obj[46] = "01:00:00";
            obj[47] = "N";
            obj[48] = "2";
            obj[49] = "0";
            obj[50] = "0";
            obj[51] = "0";

            _row.ItemArray = obj;

            DispathingInfoDt.Rows.InsertAt(_row, DispathingInfoDt.Rows.Count);
        }

        private void makeTurnEnd(OracleDataTable oDT, int turnNo)
        {
            DataRow _row = DispathingInfoDt.NewRow();

            object[] obj = _row.ItemArray;

            obj[0] = currentVehicle.vehicleNo;
            obj[2] = currentVehicle.vehicleTon;
            obj[3] = currentVehicle.vehicleType;
            obj[4] = currentVehicle.ownerShip;
            obj[5] = currentVehicle.maxVolume;
            obj[6] = currentVehicle.maxWeight;
            obj[7] = currentVehicle.maxPallet;
            obj[8] = currentVehicle.maxTurnCount;
            obj[9] = currentVehicle.maxTripCount;
            obj[10] = currentVehicle.startWorkTime;
            obj[11] = currentVehicle.endWorkTime;
            obj[12] = currentVehicle.district01;
            obj[13] = currentVehicle.maxMultiDistrict01;
            obj[14] = currentVehicle.reAllocation01;
            obj[15] = currentVehicle.allocation01;
            obj[16] = currentVehicle.district02;
            obj[17] = currentVehicle.maxMultiDistrict02;
            obj[18] = currentVehicle.reAllocation02;
            obj[19] = currentVehicle.allocation02;
            obj[20] = currentVehicle.allocation03;
            obj[21] = currentVehicle.reAllocation03;

            obj[22] = turnNo + 1 + "";
            obj[23] = "99";
            obj[24] = "차고지 도착";

            obj[33] = currentVehicle.startWorkTime + "-" + currentVehicle.endWorkTime;
            obj[36] = "0";
            obj[37] = "0";
            obj[38] = "0";
            obj[39] = "0";
            obj[40] = "0";
            obj[41] = currentVehicle.startWorkTime;
            obj[41] = currentVehicle.startWorkTime;
            string workTM = string.Format("{0}", int.Parse(currentVehicle.startWorkTime.Substring(0, 2)) + 1).PadLeft(2, '0');
            obj[43] = string.Format("{0}:00:00", workTM);
            obj[46] = "01:00:00";
            obj[47] = "N";
            obj[48] = "2";
            obj[49] = "0";
            obj[50] = "0";
            obj[51] = "0";

            _row.ItemArray = obj;

            DispathingInfoDt.Rows.InsertAt(_row, DispathingInfoDt.Rows.Count);
        }

        private void makeTurnSummary(OracleDataTable oDT, string turnNo)
        {
            DataRow _row = DispathingInfoDt.NewRow();

            object[] obj = _row.ItemArray;

            obj[0] = "";
            obj[2] = "";
            obj[3] = "";
            obj[4] = "";
            obj[5] = "";
            obj[6] = "";
            obj[7] = "";
            obj[8] = "";
            obj[9] = "";
            obj[10] = "";
            obj[11] = "";
            obj[12] = "";
            obj[13] = "";
            obj[14] = "";
            obj[15] = "";
            obj[16] = "";
            obj[17] = "";
            obj[18] = "";
            obj[19] = "";
            obj[20] = "";
            obj[21] = "";

            obj[22] = "";
            obj[23] = "0";
            obj[24] = "";

            obj[33] = "";
            obj[36] = "0";
            obj[37] = "0";
            obj[38] = "0";
            obj[39] = "0";
            obj[40] = "0";
            obj[41] = "";
            obj[41] = "";
            obj[43] = "";
            obj[46] = "";
            obj[47] = "";
            obj[48] = "2";
            obj[49] = "0";
            obj[50] = "0";
            obj[51] = "0";

            _row.ItemArray = obj;

            DispathingInfoDt.Rows.InsertAt(_row, DispathingInfoDt.Rows.Count);
        }

        private void removeSummary(OracleDataTable oDT)
        {
            for (int pos = 0; pos < oDT.Rows.Count; pos++)
            {
                object[] obj = oDT.Rows[pos].ItemArray;
                if (oDT.Rows[pos].ItemArray[22].ToString().Equals("") ||
                    oDT.Rows[pos].ItemArray[23].ToString().Equals(""))
                {
                    oDT.Rows.RemoveAt(pos);
                }
            }
        }

        private int maxTurnNo(OracleDataTable oDT)
        {
            string maxTurnNo = "0";
            for (int pos = 0; pos < oDT.Rows.Count; pos++)
            {
                DataRow _row = oDT.Rows[pos];
                string turnNo = _row.ItemArray[22].ToString().ToString();
                string tripNo = _row.ItemArray[23].ToString().ToString();
                if (tripNo.Equals("0"))
                    maxTurnNo = turnNo;
            }

            return int.Parse(maxTurnNo);
        }

        private DataRow[] cleanseData(DataRow[] dataRows)
        {
            List<DataRow> tempDataRow = new List<DataRow>();
            tempDataRow.Add(dataRows[0]);
            for (int pos = 1; pos < dataRows.Length; pos++)
            {
                if (!isInList(dataRows[pos], tempDataRow))
                {
                    tempDataRow.Add(dataRows[pos]);
                }
            }

            DataRow[] resultDataRows = new DataRow[tempDataRow.Count];
            for(int pos = 0; pos < tempDataRow.Count; pos++)
            {
                resultDataRows[pos] = tempDataRow[pos];
            }
            return resultDataRows;
        }

        private bool isButtonClicked()
        {
            if (Control.ModifierKeys == Keys.None ||
                Control.ModifierKeys == Keys.Control ||
                Control.ModifierKeys == Keys.Shift)
                return true;
            return false;
        }

        private void buttonChangeVehicle_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddTurn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentVehicle.vehicleNo))
            {
                if (currentVehicle.vehicleNo.Trim().Length > 0)
                {
                    insertTurnNo();
                    removeSummary(DispathingInfoDt);
                    summary_calc(DispathingInfoDt);
                }
            }
        }

        private bool isInList(DataRow dataRow, List<DataRow> dataRows)
        {
            foreach (var row in dataRows)
            {
                if (dataRow.ItemArray[24].ToString() == row.ItemArray[24].ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private bool isInList(GridHitInfo hit, List<GridHitInfo> list)
        {
            if (list == null)
                return false;

            foreach (GridHitInfo info in list)
            {
                if (hit.RowHandle == info.RowHandle)
                    return true;
            }
            return false;
        }

        private bool isInList(GridHitInfo hit, List<int> list)
        {
            if (list == null)
                return false;

            for (int pos = 0; pos < list.Count; pos++)
            {
                if (hit.RowHandle == list[pos])
                    return true;
            }
            return false;
        }

        private bool isContinuousList(int[] selectedRows)
        {
            if (selectedRows.Length == 1)
                return true;

            int currentRow = selectedRows[0];
            for (int pos = 1; pos < selectedRows.Length; pos++)
            {
                if (Math.Abs(currentRow - selectedRows[pos]) > 1)
                    return false;
                currentRow = selectedRows[pos];
            }
            return true;
        }

        private int findMaxGapID(int currentID, int[] selectedRows)
        {
            int maxGapID = currentID;

            if (selectedRows.Length == 0)
                return currentID;
            if (selectedRows.Length == 1)
                return selectedRows[0];

            int maxGap = 0;
            for (int pos = 0; pos < selectedRows.Length; pos++)
            {
                if (maxGap < Math.Abs(currentID - selectedRows[pos]))
                {
                    maxGap = Math.Abs(currentID - selectedRows[pos]);
                    maxGapID = selectedRows[pos];
                }
            }

            return maxGapID;
        }

        private int findMinGapID(int currentID, int[] selectedRows)
        {
            int minGapID = currentID;

            if (selectedRows.Length == 0)
                return currentID;
            if (selectedRows.Length == 1)
                return selectedRows[0];

            int minGap = int.MaxValue;
            for (int pos = 0; pos < selectedRows.Length; pos++)
            {
                if (minGap > Math.Abs(currentID - selectedRows[pos]))
                {
                    minGap = Math.Abs(currentID - selectedRows[pos]);
                    minGapID = selectedRows[pos];
                }
            }

            return minGapID;
        }

        private void clearNotAssignedHit()
        {
            NotAssignedHit.Clear();
        }

        private int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        private int Max(int a, int b)
        {
            return a < b ? b : a;
        }

        #region variable

        string carNumber = "";

        Vehicle currentVehicle;
        //bool isAbleToAddTurn = true;

        GridHitInfo downHitInfo = null;
        GridHitInfo downHitInfo2 = null;
        GridHitInfo lastAssignedHitInfo = null;
        GridHitInfo lastNotAssignedHitInfo = null;
        List<GridHitInfo> AssignedHitInfo;
        List<GridHitInfo> NotAssignedHitInfo;
        List<int> AssignedHit;
        List<int> NotAssignedHit;
        bool allowLeave = true;
        //bool allowLeave2 = true;

        IApplicationContext _applicationContext = null;
        BizService bizService;

        List<int> list_pos = new List<int>();

        DataTable dtCarList;
        DataTable dtAllocList;
        DataTable dtNonAllocList;
        DataTable dtCarUsageList;

        //OracleDataTable carListDt;
        OracleDataTable NotAssignedOrderDt;
        OracleDataTable DispathingInfoDt;
        OracleDataTable carUsageOradt;

        //test
        List<DataRowView> sourceMultiDataRowView = new List<DataRowView>();

        #endregion

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            gridDispathingInfo_TMap(sender, e);
        }


        private void gridDispathingInfo_TMap(object sender, EventArgs e)
        {
            //if (currentVehicle == null || currentVehicle.vehicleNo == null)
            //    return;

            // 1. grid에서 값 가져오기
            String strTurnNo = "0";
            int[] selRows = ((GridView)gridDispathingInfo.MainView).GetSelectedRows();

            if (selRows.Length > 0)
            {
                DataRow selectedRow = DispathingInfoDt.Rows[selRows[0]];
                strTurnNo = selectedRow["TURNNO"].ToString();
            }
            else
            {
                //return; // 정리 후 이부분 이렇게 처리
            }

            // 2. database에서 get data
            StringBuilder sb = new StringBuilder();
            //seq, 착지코드, 권역, 박스, 중량, 체적, PLT, 출발시간, 이동거리, 도착시간, 하역시간, 요청시간, 지정차량
            //SEQID, STOPID, SDST01, BOXCNT, WEIGHT, VOLUME, PLTCNT, ARRVTM, DSTLEN, WORKTM, TOTWTM, SPTMFR ||'-'|| SPTMTO, VEHCID
            //a.TURNNO, a.TRIPNO
            String sql =
              sb.Append("select rownum, b.STOPID, a.SDST01, a.BOXCNT, a.WEIGHT, a.VOLUME, a.PLTCNT, a.ARRVTM, a.DSTLEN, a.WORKTM, a.TOTWTM, a.SPTMFR ||'-'|| a.SPTMTO as SPTMFR, a.VEHCID, ")
                .Append(" round(b.LONG01 + (b.LONG02/60) + (b.LONG03/3600), 4) LONG01, round(b.lati01 + (b.lati02/60) + (b.lati03/3600), 4) lati01")
                .Append(" from tkxads18_pha a,")
                .Append(" tkxads18_arr b")
                .Append(" where a.divisn = b.divisn")
                .Append(" and a.corpor = b.corpor")
                .Append(" and a.mst_id = b.mst_id")
                .Append(" and a.planid = b.planid")
                .Append(" and a.planst = b.planst")
                .Append(" and b.stopid = a.stopid")
                .Append(" and a.CORPOR = '").Append(ApplicationKey.company).Append("'")
                .Append(" and a.PLANT = '").Append(ApplicationKey.center).Append("'")
                .Append(" and a.MST_ID = '").Append(ApplicationKey.mst_id).Append("'")
                .Append(" and a.PLANID = '").Append(ApplicationKey.current_plan_id).Append("'")
                .Append(" and a.PLANDT = '").Append(ApplicationKey.current_plan_id.Substring(0, 8)).Append("'")
                .Append(" and a.PLANST = ").Append(ApplicationKey.plan_st).Append("")
                //.Append(" and a.vehcid = '").Append("강원80바1078").Append("'")
                .Append(" and a.vehcid = '").Append(currentVehicle.vehicleNo).Append("'")
                .Append(" and a.turnno = ").Append(strTurnNo).Append("")
                .Append(" order by TURNNO, TRIPNO").ToString();

            OracleDataTable dt = bizService._dbService.ExecuteQuery(sql);

            // 3. 경로 연결 정보 구성
            double[,] vectors = new double[dt.Rows.Count, 2];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow aRow = dt.Rows[i];
                double lg = Convert.ToDouble(aRow["LONG01"]);
                double lat = Convert.ToDouble(aRow["LATI01"]);

                vectors[i, 0] = lg;
                vectors[i, 1] = lat;
            }

            // 4. MapView 보이기
            if (mapView != null)
                mapView.Dispose();

            mapView = new FormMapView(dt, vectors);
            mapView.Show();
            mapView.BringToFront();
        }

    }
}
