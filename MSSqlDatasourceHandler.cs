using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Text;

using System.Diagnostics;

namespace SevenEleven.Function
{
    public class MSSqlDatasourceHandler
    {
        
        public bool store(List<ItemLine> itemLineList)
        {

            // Timer to measure time period
            Console.WriteLine("Storing data start: {0}", DateTime.Now);
            Console.WriteLine();
            Stopwatch timer = new Stopwatch();
            timer.Start();


            Console.WriteLine("Storing data into MSSQL table....");
// need to use 'dotnet add package System.Data.SqlClient' at first time
// need to allow IP at firewall in Azure

            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "appseven11dbserver.database.windows.net"; 
                builder.UserID = "novigiadmin";            
                builder.Password = "Novigi@dmin";     
                builder.InitialCatalog = "7eleven_db";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    // StringBuilder sb = new StringBuilder();
                    // sb.Append("INSERT INTO ITEMLINE (ReportSequenceNumber, EventSequenceID) VALUES ('aaa', 'bbbb')");
                    // String sql = sb.ToString();

                    // using (SqlCommand command = new SqlCommand(sql, connection))
                    // {
                    //     command.Connection.Open();
                    //     int result = command.ExecuteNonQuery();

                    //     Console.WriteLine("Affected row count: " + result);
                    // }

                    DataTable dataTable = convertToDataTable(itemLineList);

                    using (SqlBulkCopy bulk = new SqlBulkCopy(connection))
                    {
                        connection.Open();
                        bulk.DestinationTableName = "ITEMLINE";
                        bulk.WriteToServer(dataTable);
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            timer.Stop();

            Console.WriteLine("Storing data end: {0}", DateTime.Now);
            Console.WriteLine("Storing data Elapsed time: {0}", timer.Elapsed);
            
            return true;

        }


        private DataTable convertToDataTable(List<ItemLine> itemLineList) {
            DataTable dataTable = initializeDataTable();

            foreach(ItemLine itemLine in itemLineList)
            {
                DataRow row = dataTable.NewRow();

                //chunk 1
                row[0] = itemLine.BeginDate;
                row[1] = itemLine.BeginTime;
                row[2] = itemLine.EndDate;
                row[3] = itemLine.EndTime;
                row[4] = itemLine.ReportSequenceNumber;
                row[5] = itemLine.EventSequenceID;

                // chunk 2
                row[6] = itemLine.TrainingModeFlag;
                row[7] = itemLine.CashierID;
                row[8] = itemLine.RegisterID;
                row[9] = itemLine.TillID;
                row[10] = itemLine.OutsideSalesFlag;
                row[11] = itemLine.TransactionID;

                // chunk 3
                row[12] = itemLine.EventStartDate;
                row[13] = itemLine.EventStartTime;
                row[14] = itemLine.EventEndDate;
                row[15] = itemLine.BusinessDate;
                row[16] = itemLine.ReceiptDate;
                row[17] = itemLine.ReceiptTime;

                // chunk 4
                row[18] = itemLine.OfflineFlag;
                row[19] = itemLine.SuspendFlag;
                row[20] = itemLine.TransactionLineSequenceNumber;
                row[21] = itemLine.FuelGradeID;
                row[22] = itemLine.FuelPositionID;
                row[23] = itemLine.PriceTierCode;

                // chunk 5
                row[24] = itemLine.ServiceLevelCode;
                row[25] = itemLine.FuelLineDescription;
                row[26] = itemLine.EntryMethod;
                row[27] = itemLine.FuelLineActualSalesPrice;
                row[28] = itemLine.FuelLineMerchandiseCode;
                row[29] = itemLine.FuelLineRegularSellPrice;

                // chunk 6
                row[30] = itemLine.FuelLineSalesQuantity;
                row[31] = itemLine.FuelLineSalesAmount;
                row[32] = itemLine.FuelLineSellingUnits;
                row[33] = itemLine.DetailLineNumber;
                row[34] = itemLine.HoseID;
                row[35] = itemLine.RecalledTransactionID;

                // chunk 7
                row[36] = itemLine.TaxItemizerMask;
                row[37] = itemLine.OriginalTaxItemizerMask;
                row[38] = itemLine.posCode;
                row[39] = itemLine.description;
                row[40] = itemLine.ActualSalesPrice;
                row[41] = itemLine.MerchandiseCode;

                // chunk 8
                row[42] = itemLine.SellingUnits;
                row[43] = itemLine.RegularSellPrice;
                row[44] = itemLine.SalesQuantity;
                row[45] = itemLine.SalesAmount;
                row[46] = itemLine.itemTypeCode;
                row[47] = itemLine.SalesRestriction;

                // chunk 9
                row[48] = itemLine.TenderCode;
                row[49] = itemLine.TenderSubCode;
                row[50] = itemLine.ChangeFlag;
                row[51] = itemLine.TaxLevelID;
                row[52] = itemLine.TaxableSalesAmount;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private DataTable initializeDataTable()
        {
            DataTable dataTable = new DataTable();

            // chunk 1
            dataTable.Columns.Add("BeginDate", typeof(object));
            dataTable.Columns.Add("BeginTime", typeof(object));
            dataTable.Columns.Add("EndDate", typeof(object));
            dataTable.Columns.Add("EndTime", typeof(object));
            dataTable.Columns.Add("ReportSequenceNumber", typeof(string));
            dataTable.Columns.Add("EventSequenceID", typeof(string));

            // chunk 2
            dataTable.Columns.Add("TrainingModeFlag", typeof(string));
            dataTable.Columns.Add("CashierID", typeof(string));
            dataTable.Columns.Add("RegisterID", typeof(string));
            dataTable.Columns.Add("TillID", typeof(string));
            dataTable.Columns.Add("OutsideSalesFlag", typeof(string));
            dataTable.Columns.Add("TransactionID", typeof(string));

            // chunk 3
            dataTable.Columns.Add("EventStartDate", typeof(object));
            dataTable.Columns.Add("EventStartTime", typeof(object));
            dataTable.Columns.Add("EventEndDate", typeof(object));
            dataTable.Columns.Add("BusinessDate", typeof(object));
            dataTable.Columns.Add("ReceiptDate", typeof(object));
            dataTable.Columns.Add("ReceiptTime", typeof(object));

            // chunk 4
            dataTable.Columns.Add("OfflineFlag", typeof(string));
            dataTable.Columns.Add("SuspendFlag", typeof(string));
            dataTable.Columns.Add("TransactionLineSequenceNumber", typeof(string));
            dataTable.Columns.Add("FuelGradeID", typeof(string));
            dataTable.Columns.Add("FuelPositionID", typeof(string));
            dataTable.Columns.Add("PriceTierCode", typeof(string));

            // chunk 5
            dataTable.Columns.Add("ServiceLevelCode", typeof(string));
            dataTable.Columns.Add("FuelLineDescription", typeof(string));
            dataTable.Columns.Add("EntryMethod", typeof(string));
            dataTable.Columns.Add("FuelLineActualSalesPrice", typeof(string));
            dataTable.Columns.Add("FuelLineMerchandiseCode", typeof(string));
            dataTable.Columns.Add("FuelLineRegularSellPrice", typeof(string));

            // chunk 6
            dataTable.Columns.Add("FuelLineSalesQuantity", typeof(string));
            dataTable.Columns.Add("FuelLineSalesAmount", typeof(string));
            dataTable.Columns.Add("FuelLineSellingUnits", typeof(string));
            dataTable.Columns.Add("DetailLineNumber", typeof(string));
            dataTable.Columns.Add("HoseID", typeof(string));
            dataTable.Columns.Add("RecalledTransactionID", typeof(string));

            // chunk 7
            dataTable.Columns.Add("TaxItemizerMask", typeof(string));
            dataTable.Columns.Add("OriginalTaxItemizerMask", typeof(string));
            dataTable.Columns.Add("POSCode", typeof(string));
            dataTable.Columns.Add("ItemLineDescription", typeof(string));
            dataTable.Columns.Add("ItemLineActualSalesPrice", typeof(string));
            dataTable.Columns.Add("ItemLineMerchandiseCode", typeof(string));

            // chunk 8
            dataTable.Columns.Add("ItemLineSellingUnits", typeof(string));
            dataTable.Columns.Add("ItemLineRegularSellPrice", typeof(string));
            dataTable.Columns.Add("ItemLineSalesQuantity", typeof(string));
            dataTable.Columns.Add("ItemLineSalesAmount", typeof(string));
            dataTable.Columns.Add("ItemTypeCode", typeof(string));
            dataTable.Columns.Add("SalesRestriction", typeof(string));

            // chunk 9
            dataTable.Columns.Add("TenderCode", typeof(string));
            dataTable.Columns.Add("TenderSubCode", typeof(string));
            dataTable.Columns.Add("ChangeFlag", typeof(string));
            dataTable.Columns.Add("TaxLevelID", typeof(string));
            dataTable.Columns.Add("TaxableSalesAmount", typeof(string));

            return dataTable;
        }

    }
}