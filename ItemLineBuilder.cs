using System;
using System.Collections.Generic;

using System.IO;  
using System.Xml;

using System.Diagnostics;


namespace SevenEleven.Function
{
    public class ItemLineBuilder
    {
        
        public List<ItemLine> build(XmlDocument xmlDocument)
        {

            // Timer to measure time period
            Console.WriteLine("XML parsing start: {0}", DateTime.Now);
            Console.WriteLine();
            Stopwatch timer = new Stopwatch();
            timer.Start();

            List<ItemLine> itemLineList = new List<ItemLine>();

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("b", "http://www.naxml.org/POSBO/Vocabulary/2003-10-16");
            nsmgr.AddNamespace("radiant", "http://www.radiantsystems.com/NAXML-Extension"); 

            XmlNode transmissionHeader = xmlDocument.DocumentElement.SelectSingleNode("//b:JournalReport/b:JournalHeader", nsmgr);
            var beginDate = transmissionHeader.SelectSingleNode("b:BeginDate", nsmgr).InnerText;
            var beginTime = transmissionHeader.SelectSingleNode("b:BeginTime", nsmgr).InnerText;
            var endDate = transmissionHeader.SelectSingleNode("b:EndDate", nsmgr).InnerText;
            var endTime = transmissionHeader.SelectSingleNode("b:EndTime", nsmgr).InnerText;

            
            XmlNodeList saleEventList = xmlDocument.DocumentElement.SelectNodes("//b:JournalReport/b:SaleEvent", nsmgr);


            foreach (XmlNode saleEvent in saleEventList)
            {
                //import attributes of SaleEvent here
                var EventSequenceID = saleEvent.SelectSingleNode("b:EventSequenceID", nsmgr).InnerText;
                var TrainingModeFlag = saleEvent.SelectSingleNode("b:TrainingModeFlag", nsmgr).Attributes["value"].Value;
                var CashierID = saleEvent.SelectSingleNode("b:CashierID", nsmgr).InnerText;
                var RegisterID = saleEvent.SelectSingleNode("b:RegisterID", nsmgr).InnerText;
                var TillID = saleEvent.SelectSingleNode("b:TillID", nsmgr).InnerText;
                var OutsideSalesFlag = saleEvent.SelectSingleNode("b:OutsideSalesFlag", nsmgr).Attributes["value"].Value;
                var TransactionID = saleEvent.SelectSingleNode("b:TransactionID", nsmgr).InnerText;
                var EventStartDate = saleEvent.SelectSingleNode("b:EventStartDate", nsmgr).InnerText;
                var EventStartTime = saleEvent.SelectSingleNode("b:EventStartTime", nsmgr).InnerText;
                var EventEndDate = saleEvent.SelectSingleNode("b:EventEndDate", nsmgr).InnerText;
                var EventEndTime = saleEvent.SelectSingleNode("b:EventEndTime", nsmgr).InnerText;
                var BusinessDate = saleEvent.SelectSingleNode("b:BusinessDate", nsmgr).InnerText;
                var ReceiptDate = saleEvent.SelectSingleNode("b:ReceiptDate", nsmgr).InnerText;
                var ReceiptTime = saleEvent.SelectSingleNode("b:ReceiptTime", nsmgr).InnerText;
            
                // Extract values from special TransactionDetailGroup elements
                // --- Tender properties
                var transactionDetailGroupTenderInfoNode = saleEvent.SelectSingleNode("b:TransactionDetailGroup/b:TransactionLine/b:TenderInfo", nsmgr);
                string TenderCode, TenderSubCode, TenderAmount, ChangeFlag;
                TenderCode = TenderSubCode = TenderAmount = ChangeFlag = String.Empty;

                if (transactionDetailGroupTenderInfoNode != null) {
                    TenderCode = transactionDetailGroupTenderInfoNode.SelectSingleNode("b:Tender/b:TenderCode", nsmgr).InnerText;
                    TenderSubCode = transactionDetailGroupTenderInfoNode.SelectSingleNode("b:Tender/b:TenderSubCode", nsmgr).InnerText;
                    TenderAmount = transactionDetailGroupTenderInfoNode.SelectSingleNode("b:TenderAmount", nsmgr).InnerText;
                    ChangeFlag = transactionDetailGroupTenderInfoNode.SelectSingleNode("b:ChangeFlag", nsmgr).Attributes["value"].Value;
                }

                // --- Tax properties
                var transactionDetailGroupTransactionTaxNode = saleEvent.SelectSingleNode("b:TransactionDetailGroup/b:TransactionLine/b:TransactionTax", nsmgr);
                string TaxLevelID, TaxableSalesAmount, TaxCollectedAmount;
                TaxLevelID = TaxableSalesAmount = TaxCollectedAmount = String.Empty;

                if (transactionDetailGroupTransactionTaxNode != null) {
                    TaxLevelID = transactionDetailGroupTransactionTaxNode.SelectSingleNode("b:TaxLevelID", nsmgr).InnerText;
                    TaxableSalesAmount = transactionDetailGroupTransactionTaxNode.SelectSingleNode("b:TaxableSalesAmount", nsmgr).InnerText;
                    TaxCollectedAmount = transactionDetailGroupTransactionTaxNode.SelectSingleNode("b:TaxCollectedAmount", nsmgr).InnerText;
                }


                // --- FuelLine properties
                var transactionDetailGroupTransactionFuelLineNode = saleEvent.SelectSingleNode("b:TransactionDetailGroup/b:TransactionLine/b:FuelLine", nsmgr);
                string FuelGradeID, FuelPositionID, ServiceLevelCode, FuelLineDescription, FuelLineActualSalesPrice, FuelLineMerchandiseCode, FuelLineRegularSellPrice, FuelLineSalesQuantity, FuelLineSalesAmount, FuelLineSellingUnits;
                FuelGradeID = FuelPositionID = ServiceLevelCode = FuelLineDescription = FuelLineActualSalesPrice = FuelLineMerchandiseCode = FuelLineRegularSellPrice = FuelLineSalesQuantity = FuelLineSalesAmount = FuelLineSellingUnits = String.Empty;

                if (transactionDetailGroupTransactionFuelLineNode != null) {
                    FuelGradeID = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:FuelGradeID", nsmgr).InnerText;
                    FuelPositionID = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:FuelPositionID", nsmgr).InnerText;
                    ServiceLevelCode = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:PriceTierCode", nsmgr).InnerText;
                    FuelLineDescription = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:Description", nsmgr).InnerText;
                    FuelLineActualSalesPrice = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:ActualSalesPrice", nsmgr).InnerText;
                    FuelLineMerchandiseCode = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:MerchandiseCode", nsmgr).InnerText;
                    FuelLineRegularSellPrice = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:RegularSellPrice", nsmgr).InnerText;
                    FuelLineSalesQuantity = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:SalesQuantity", nsmgr).InnerText;
                    FuelLineSalesAmount = transactionDetailGroupTransactionFuelLineNode.SelectSingleNode("b:SalesAmount", nsmgr).InnerText;

                }


                XmlNodeList transactionDetailGroupList = saleEvent.SelectNodes("b:TransactionDetailGroup", nsmgr);

                foreach (XmlNode transactionDetailGroup in transactionDetailGroupList)
                {
                    //import attributes of TransactionDetailGroup here
                    var transactionLineSequenceNumber = transactionDetailGroup.SelectSingleNode("b:TransactionLine/b:TransactionLineSequenceNumber", nsmgr).InnerText;

                    // TransactionLineExtension
                    var DetailLineNumber = transactionDetailGroup.SelectSingleNode("b:TransactionLine/radiant:TransactionLineExtension/radiant:DetailLineNumber", nsmgr).InnerText;
                    var HoseID = transactionDetailGroup.SelectSingleNode("b:TransactionLine/radiant:TransactionLineExtension/radiant:HoseID", nsmgr).InnerText;
                    var RecalledTransactionID = transactionDetailGroup.SelectSingleNode("b:TransactionLine/radiant:TransactionLineExtension/radiant:ItemLine/radiant:RecalledTransactionID", nsmgr).InnerText;
                    var TaxItemizerMask = transactionDetailGroup.SelectSingleNode("b:TransactionLine/radiant:TransactionLineExtension/radiant:ItemLine/radiant:TaxItemizerMask", nsmgr).InnerText;
                    var OriginalTaxItemizerMask = transactionDetailGroup.SelectSingleNode("b:TransactionLine/radiant:TransactionLineExtension/radiant:ItemLine/radiant:OriginalTaxItemizerMask", nsmgr).InnerText;




                    XmlNodeList itemLineNodeList = transactionDetailGroup.SelectNodes("b:TransactionLine/b:ItemLine", nsmgr);

                    foreach (XmlNode itemLineNode in itemLineNodeList)
                    {

                        var description = itemLineNode.SelectSingleNode("b:Description", nsmgr).InnerText;
                        var actualSalesPrice = itemLineNode.SelectSingleNode("b:ActualSalesPrice", nsmgr).InnerText;
                        var merchandiseCode = itemLineNode.SelectSingleNode("b:MerchandiseCode", nsmgr).InnerText;
                        var sellingUnits = itemLineNode.SelectSingleNode("b:SellingUnits", nsmgr).InnerText;
                        var regularSellPrice = itemLineNode.SelectSingleNode("b:RegularSellPrice", nsmgr).InnerText;
                        var salesQuantity = itemLineNode.SelectSingleNode("b:SalesQuantity", nsmgr).InnerText;
                        var salesAmount = itemLineNode.SelectSingleNode("b:SalesAmount", nsmgr).InnerText;
                        var itemTypeCode = itemLineNode.SelectSingleNode("b:ItemTypeCode", nsmgr).InnerText;
                        var salesRestriction = itemLineNode.SelectSingleNode("b:SalesRestriction", nsmgr).InnerText;

                        XmlNode itemCodeNode = itemLineNode.SelectSingleNode("b:ItemCode", nsmgr);

                        if (itemCodeNode != null)
                        {
                            ItemLine itemLine = new ItemLine() {
                                BeginDate = DateTime.Parse(beginDate),
                                BeginTime = TimeSpan.Parse(beginTime),
                                EndDate = DateTime.Parse(endDate),
                                EndTime = TimeSpan.Parse(endTime),

                                EventSequenceID = EventSequenceID,
                                TrainingModeFlag = TrainingModeFlag,
                                CashierID = CashierID,
                                RegisterID = RegisterID,
                                TillID = TillID,
                                OutsideSalesFlag = OutsideSalesFlag,
                                TransactionID = TransactionID,
                                EventStartDate = DateTime.Parse(EventStartDate),
                                EventStartTime = TimeSpan.Parse(EventStartTime),
                                EventEndDate = DateTime.Parse(EventEndDate),
                                EventEndTime = TimeSpan.Parse(EventEndTime),
                                BusinessDate = DateTime.Parse(BusinessDate),
                                ReceiptDate = DateTime.Parse(ReceiptDate),
                                ReceiptTime = TimeSpan.Parse(ReceiptTime),

                                // tender related properties
                                TenderCode = TenderCode,
                                TenderSubCode = TenderSubCode,
                                TenderAmount = TenderAmount,
                                ChangeFlag = ChangeFlag,

                                // tax related properties
                                TaxLevelID = TaxLevelID,
                                TaxableSalesAmount = TaxableSalesAmount,
                                TaxCollectedAmount = TaxCollectedAmount,

                                // fuelLine related properties
                                FuelGradeID = FuelGradeID,
                                FuelPositionID = FuelPositionID,
                                ServiceLevelCode = ServiceLevelCode,
                                FuelLineDescription = FuelLineDescription,
                                FuelLineActualSalesPrice = FuelLineActualSalesPrice,
                                FuelLineMerchandiseCode = FuelLineMerchandiseCode,
                                FuelLineRegularSellPrice = FuelLineRegularSellPrice,
                                FuelLineSalesQuantity = FuelLineSalesQuantity,
                                FuelLineSalesAmount = FuelLineSalesAmount,

                                TransactionLineSequenceNumber = transactionLineSequenceNumber,

                                // TransactionLineExtension
                                DetailLineNumber = DetailLineNumber,
                                HoseID = HoseID,
                                RecalledTransactionID = RecalledTransactionID,
                                TaxItemizerMask = TaxItemizerMask,
                                OriginalTaxItemizerMask = OriginalTaxItemizerMask,

                                description = description,
                                ActualSalesPrice = actualSalesPrice,
                                MerchandiseCode = merchandiseCode,
                                SellingUnits = sellingUnits,
                                RegularSellPrice = regularSellPrice,
                                SalesQuantity = salesQuantity,
                                SalesAmount = salesAmount,
                                itemTypeCode = itemTypeCode,
                                SalesRestriction = salesRestriction

                                

                            };

 
                            XmlNode value = itemCodeNode.SelectSingleNode("b:POSCodeFormat", nsmgr);
                            itemLine.posCodeFormat = value.Attributes["format"].Value;
                            itemLine.posCode = itemCodeNode.SelectSingleNode("b:POSCode", nsmgr).InnerText;
                            itemLine.posCodeModifier = itemCodeNode.SelectSingleNode("b:POSCodeModifier", nsmgr).InnerText;
                            itemLine.inventoryItemID = itemCodeNode.SelectSingleNode("b:InventoryItemID", nsmgr).InnerText;

                            // adding itemLine to the list
                            itemLineList.Add(itemLine);
                        }
                        
                    }
                        
                }
            }

            timer.Stop();

            Console.WriteLine("XML parsing end: {0}", DateTime.Now);
            Console.WriteLine("XML parsing Elapsed time: {0}", timer.Elapsed);

            Console.WriteLine("Number of itemLine elements found {0}", itemLineList.Count);
            Console.WriteLine();

            return itemLineList;
        }
    }
}