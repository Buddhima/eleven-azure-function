using System;

namespace SevenEleven.Function
{
    public class ItemLine
    {
        public DateTime BeginDate 
        { get; set; }

        public TimeSpan BeginTime 
        { get; set; }

        public DateTime EndDate 
        { get; set; }


        public TimeSpan EndTime 
        { get; set; }

        public string ReportSequenceNumber 
        { get; set; }

        public string PrimaryReportPeriod 
        { get; set; }

        public string EventSequenceID 
        { get; set; }

        public string TrainingModeFlag 
        { get; set; }

        public string CashierID 
        { get; set; }

        public string RegisterID 
        { get; set; }

        public string TillID 
        { get; set; }

        public string OutsideSalesFlag 
        { get; set; }

        public string TransactionID 
        { get; set; }

        public DateTime EventStartDate 
        { get; set; }

        public TimeSpan EventStartTime 
        { get; set; }

        public DateTime EventEndDate 
        { get; set; }    

        public TimeSpan EventEndTime 
        { get; set; }  

        public DateTime BusinessDate 
        { get; set; }

        public DateTime ReceiptDate 
        { get; set; }

        public TimeSpan ReceiptTime 
        { get; set; }

        public string OfflineFlag 
        { get; set; }

        public string SuspendFlag 
        { get; set; }

        public string TransactionLineSequenceNumber 
        { get; set; }

        // FuelLine related properties
        public string FuelGradeID 
        { get; set; }

        public string FuelPositionID 
        { get; set; }

        public string PriceTierCode 
        { get; set; }

        public string ServiceLevelCode 
        { get; set; }

        public string FuelLineDescription 
        { get; set; }

        public string FuelLineActualSalesPrice 
        { get; set; }

        public string FuelLineMerchandiseCode 
        { get; set; }

        public string FuelLineRegularSellPrice 
        { get; set; }

        public string FuelLineSalesQuantity 
        { get; set; }

        public string FuelLineSalesAmount 
        { get; set; }

        public string FuelLineSellingUnits 
        { get; set; }


        // <radiant:TransactionLineExtension>
        public string DetailLineNumber 
        { get; set; }

        public string HoseID 
        { get; set; }

        public string RecalledTransactionID 
        { get; set; }

        public string TaxItemizerMask 
        { get; set; }

        public string OriginalTaxItemizerMask 
        { get; set; }

        public string ItemTaxRate 
        { get; set; }
        
        public string EFTCardRestrictions 
        { get; set; }

        // </radiant:TransactionLineExtension>


        // Tender related properties
        public string TenderCode 
        { get; set; }

        public string TenderSubCode 
        { get; set; }

        public string TenderAmount 
        { get; set; }

        public string ChangeFlag 
        { get; set; }

        // Tax related properties
        public string TaxLevelID 
        { get; set; }

        public string TaxableSalesAmount 
        { get; set; }

        public string TaxCollectedAmount 
        { get; set; }

        // <ItemLine>
        public string posCodeFormat 
        { get; set; }

        public string posCode 
        { get; set; }

        public string posCodeModifier 
        { get; set; }
        
        public string inventoryItemID 
        { get; set; }

        public string description 
        { get; set; }

        public string EntryMethod 
        { get; set; }

        public string ActualSalesPrice 
        { get; set; }

        public string MerchandiseCode 
        { get; set; }

        public string SellingUnits 
        { get; set; }

        public string RegularSellPrice 
        { get; set; }

        public string SalesQuantity 
        { get; set; }

        public string SalesAmount 
        { get; set; }

        public string itemTypeCode 
        { get; set; }

        public string SalesRestriction 
        { get; set; }
        // </ItemLine>

    }
}