using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTAURANT.API.DAL.Constants
{
    public struct ConfigurationCons
    {
        public const string WorkOrderStatusOutbound = "WorkOrderStatusOutbound";
        public const string WorkOrderOutbound = "WorkOrderOutbound";

        public const string UserNameCallKMPG = "UserNameCallKMPG";
        public const string PassWordCallKPMG = "PassWordCallKPMG";
        public const string WIPSizeOfDataChunkDownload = "WIPSizeOfDataChunkDownload";
        public const string WIPAbsolutePath = "WIPAbsolutePath";
        public const string WOCheckLotNumberAbsolutePath = "WOCheckLotNumberAbsolutePath";

        public const string SalesOrderAbsolutePath = "SalesOrderAbsolutePath";
        public const string SalesOrderReservationStatus = "SalesOrderReservationStatus";
        public const string SalesOrderSizeOfDataChunkDownload = "SalesOrderSizeOfDataChunkDownload";

        public const string ItemAbsolutePath = "ItemAbsolutePath";
        public const string ItemSizeOfDataChunkDownload = "ItemSizeOfDataChunkDownload";
        public const string WorkOrderStatusAbsolutePath = "WorkOrderStatusAbsolutePath";
    }

    public struct StoreProcedures
    {
        public const string LoadListWips = "usp_LoadListWips";
        public const string LoadListSyncLog = "usp_LoadListSyncLog";
        public const string LoadListOrgByUsername = "usp_LoadListOrgByUsername";
        public const string LoadListSyncLogDetailById = "usp_LoadListSyncLogDetailById";
        public const string LoadReportKienNhapKho = "usp_Report_KienNhapKho";
        public const string LoadReportKienXuatBan = "usp_Report_KienXuatBan";
        public const string LoadListSaleOrderMaster = "usp_LoadListSaleOrderMaster";
        public const string LoadPermissionUser = "usp_LoadPermissionUser";
        public const string LoadListSearchUser = "usp_LoadListSearchUser";
        public const string LoadInformationUser = "usp_LoadInformationUser";
        public const string LoadListKiemKho = "usp_LoadListKiemKho";
        public const string LoadListKienNhapKhoDauKy = "usp_LoadListKienNhapKhoDauKy";
        public const string LoadListChuyenKho = "usp_LoadListChuyenKho";
        public const string LoadListUser = "usp_LoadListUser";
        public const string LoadListGoodsByMaCuon = "usp_LoadListGoodsByMaCuon";
        public const string ImportItem = "usp_ImportItem";
        public const string SearchMaKienCuon = "usp_SearchMaKienCuon";

        public const string LoadReportBaoCaoNhapKho = "usp_Report_NhapKho";
        public const string LoadReportBaoCaoTonKho = "usp_Report_TonKho";
        public const string LoadReportBaoCaoKho = "usp_Report_BaoCaoKho";


        public const string DMS_GetProduct = "DMS_GetProduct";
    }

    public struct ConstantStatus
    {
        public const byte NotStart = 1;
        public const byte InProgress = 2;
        public const byte Synching = 3;
        public const byte Completed = 4;
        public const byte Closed = 5;
        public const byte Canceled = 6;
        public const byte Completed_Error = 7;
    }

    public struct ConstantSaleOrderExportStatus
    {
        public const byte NotSync = 0;
        public const byte SyncError = 1;
        public const byte Completed = 2;
    }

    public enum ReturnVal { OK = 1, Failed = 0, Error = -1 };

    public struct SynBatchStatus
    {
        public const string Completed = "COMPLETED";
        public const string Error = "COMPLETED_ERRORS";
        public const string InProcess = "IN_PROCESS";
    }

    public struct WorkOrderStatus
    {
        public const string Canceled = "ORA_CANCELED";
        public const string Closed = "ORA_CLOSED";
        public const string Competed = "ORA_COMPLETED";
        public const string ON_HOLD = "ORA_ON_HOLD";
        public const string UNRELEASED = "ORA_UNRELEASED";
        public const string RELEASED = "ORA_RELEASED";
    }

    public struct SaleOrderStatus
    {
        public const string Canceled = "CANCELED";
        public const string Closed = "CLOSED";
        public const string Doo_Cancel_Requested = "DOO_CANCEL_REQUESTED";
        public const string Partial_Closed = "PARTIAL_CLOSE";
        public const int SUBMITCANCELLATION = 1;
    }

    public struct OutboundInbound
    {
        public const string WorkOrderGet = "/NITRO Integrators/Reports/XX WO Get Status.xdo";
        public const string WorkOrderGetStatus = "/NITRO Integrators/Reports/XX WO Get Status.xdo";
        public const string ItemGet = "/NITRO Integrators/Reports/XX Item Integration Outbound.xdo";

        public const string SaleOrderGetStatus = "/NITRO Integrators/Reports/XX SO Get Status.xdo";
    }
}
