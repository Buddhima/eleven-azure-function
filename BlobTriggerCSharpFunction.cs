using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace SevenEleven.Function
{
    public static class BlobTriggerCSharpFunction
    {
        [FunctionName("BlobTriggerCSharpFunction")]
        public static void Run([BlobTrigger("input-workitems/{name}", Connection = "seven11azurefncstore_STORAGE")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(myBlob);

            ItemLineBuilder itemLineBuilder = new ItemLineBuilder();
            List<ItemLine> itemLineList = itemLineBuilder.build(xmlDocument);

            MSSqlDatasourceHandler datasourceHandler = new MSSqlDatasourceHandler();
            bool result = datasourceHandler.store(itemLineList);

            Console.WriteLine();
        }
    }
}
