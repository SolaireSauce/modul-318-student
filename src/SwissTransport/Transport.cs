using System;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace SwissTransport
{
    public class Transport : ITransport
    {
        public Stations GetStations(string query)
        {
            query = System.Uri.EscapeDataString(query);
            var request = CreateWebRequest("http://transport.opendata.ch/v1/locations?query=" + query);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                var message = new StreamReader(responseStream).ReadToEnd();
                var stations = JsonConvert.DeserializeObject<Stations>(message
                    , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return stations;
            }

            return null;
        }

        public Stations StationFinder(string XCord, string YCord)
        {
            XCord = System.Uri.EscapeDataString(XCord);
            YCord = System.Uri.EscapeDataString(YCord);
            var request = CreateWebRequest("http://transport.opendata.ch/v1/locations?x=" + XCord + "&y=" + YCord);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                var message = new StreamReader(responseStream).ReadToEnd();
                var stations = JsonConvert.DeserializeObject<Stations>(message
                    , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return stations;
            }

            return null;
        }

        public StationBoardRoot GetStationBoard(string station)
        {
            station = System.Uri.EscapeDataString(station);
            var request = CreateWebRequest("http://transport.opendata.ch/v1/stationboard?station=" + station);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                var readToEnd = new StreamReader(responseStream).ReadToEnd();
                var stationboard =
                    JsonConvert.DeserializeObject<StationBoardRoot>(readToEnd);
                return stationboard;
            }

            return null;
        }

        public Connections GetConnections(string fromStation, string toStation, DateTime date, DateTime time, Boolean isArrivalTime)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");
            fromStation = System.Uri.EscapeDataString(fromStation);
            toStation = System.Uri.EscapeDataString(toStation);
            string dateString = System.Uri.EscapeDataString(date.Year.ToString() + "-" + date.Month.ToString() + "-" + date.Day.ToString());
            string TimeString = System.Uri.EscapeDataString(time.Hour.ToString() + ":" + time.Minute.ToString());
            string isArrivalTimeString;
            if (isArrivalTime == true)
            {
                isArrivalTimeString = System.Uri.EscapeDataString("1");
            }
            else
            {
                isArrivalTimeString = System.Uri.EscapeDataString("0");
            }
            var request = CreateWebRequest("http://transport.opendata.ch/v1/connections?from=" + fromStation + "&to=" + toStation + "&date=" + dateString + "&time=" + TimeString + "&isArrivalTime=" + isArrivalTimeString + "&limit=12");
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                var readToEnd = new StreamReader(responseStream).ReadToEnd();
                var connections =
                    JsonConvert.DeserializeObject<Connections>(readToEnd);
                return connections;
            }

            return null;
        }

        private static WebRequest CreateWebRequest(string url)
        {
            var request = WebRequest.Create(url);
            var webProxy = WebRequest.DefaultWebProxy;

            webProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Proxy = webProxy;
            
            return request;
        }
    }
}
