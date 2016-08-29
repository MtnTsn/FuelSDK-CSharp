﻿using FuelSDK;
using System;

namespace objsamples
{
    partial class Tester
    {
        static void TestET_OpenEvent()
        {
            var filterDate = new DateTime(2013, 1, 15, 13, 0, 0);

            Console.WriteLine("--- Testing OpenEvent ---");
            var myclient = new ET_Client();

            Console.WriteLine("Retrieve Filtered OpenEvents with GetMoreResults");
            var oe = new ET_OpenEvent
            {
                AuthStub = myclient,
                SearchFilter = new SimpleFilterPart { Property = "EventDate", SimpleOperator = SimpleOperators.greaterThan, DateValue = new[] { filterDate } },
                Props = new[] { "SendID", "SubscriberKey", "EventDate", "Client.ID", "EventType", "BatchID", "TriggeredSendDefinitionObjectID", "PartnerKey" },
            };
            var oeGet = oe.Get();

            Console.WriteLine("Get Status: " + oeGet.Status.ToString());
            Console.WriteLine("Message: " + oeGet.Message);
            Console.WriteLine("Code: " + oeGet.Code.ToString());
            Console.WriteLine("Results Length: " + oeGet.Results.Length);
            Console.WriteLine("MoreResults: " + oeGet.MoreResults.ToString());
            // Since this could potentially return a large number of results, we do not want to print the results
            foreach (ET_OpenEvent openEvent in oeGet.Results)
                Console.WriteLine("SubscriberKey: " + openEvent.SubscriberKey + ", EventDate: " + openEvent.EventDate.ToString());

            while (oeGet.MoreResults)
            {
                Console.WriteLine("Continue Retrieve Filtered OpenEvents with GetMoreResults");
                oeGet = oe.GetMoreResults();
                Console.WriteLine("Get Status: " + oeGet.Status.ToString());
                Console.WriteLine("Message: " + oeGet.Message);
                Console.WriteLine("Code: " + oeGet.Code.ToString());
                Console.WriteLine("Results Length: " + oeGet.Results.Length);
                Console.WriteLine("MoreResults: " + oeGet.MoreResults.ToString());
            }

#if false
            // The following request could potentially bring back large amounts of data if run against a production account	
            Console.WriteLine("Retrieve All OpenEvents with GetMoreResults");
            var oe2 = new ET_OpenEvent
            {
                AuthStub = myclient,
                Props = new[] { "SendID", "SubscriberKey", "EventDate", "Client.ID", "EventType", "BatchID", "TriggeredSendDefinitionObjectID", "PartnerKey" },
            };
            var oeGetAll = oe2.Get();

            Console.WriteLine("Get Status: " + oeGetAll.Status.ToString());
            Console.WriteLine("Message: " + oeGetAll.Message);
            Console.WriteLine("Code: " + oeGetAll.Code.ToString());
            Console.WriteLine("Results Length: " + oeGetAll.Results.Length);
            Console.WriteLine("MoreResults: " + oeGetAll.MoreResults.ToString());
            // Since this could potentially return a large number of results, we do not want to print the results
            foreach (ET_OpenEvent openEvent in oeGet.Results)
                Console.WriteLine("SubscriberKey: " + openEvent.SubscriberKey + ", EventDate: " + openEvent.EventDate.ToString());

            while (oeGetAll.MoreResults)
            {
                oeGetAll = oe2.GetMoreResults();
                Console.WriteLine("Get Status: " + oeGetAll.Status.ToString());
                Console.WriteLine("Message: " + oeGetAll.Message);
                Console.WriteLine("Code: " + oeGetAll.Code.ToString());
                Console.WriteLine("Results Length: " + oeGetAll.Results.Length);
                Console.WriteLine("MoreResults: " + oeGetAll.MoreResults.ToString());
            }
#endif
        }
    }
}
