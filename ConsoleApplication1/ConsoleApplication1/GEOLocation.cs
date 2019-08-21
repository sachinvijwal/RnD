using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.XPath;

public class GEOLocation
{
    private string url = "https://maps.google.com/maps/api/geocode/xml?address='strAddress'&sensor=false&key=AIzaSyC1y-9cXXGWoIPK5M76PVbMtEir-I44_R0";
    private string distanceMatrixURL = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins={0}&destinations={1}";
    private LatLong location;
    private string API_KEY = "AIzaSyC1y-9cXXGWoIPK5M76PVbMtEir-I44_R0";
    public GEOLocation(string address)
    {
        this.Address = address;
    }
    //public GEOLocation(LatLong location)
    //{
    //    this.location = location;
    //}
    public LatLong LatLong
    {
        get
        {
            if (null == location)
                GetLatLong();
            return location;
        }
    }

    private void GetLatLong()
    {
        DataTable result = GetLatLangFromAddress(Address);
        location = new LatLong(double.Parse(result.Rows[0][2].ToString()), double.Parse(result.Rows[0][3].ToString()));
    }

    //public string getCityName()
    //{
    //    string city = null;
    //    try
    //    {
    //        XmlDocument xDoc = new XmlDocument();
    //        xDoc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + this.hotel.LocationInfo + "&key=" + API_KEY);
    //        XmlNodeList xNodelst = xDoc.GetElementsByTagName("result");
    //        XmlNode xNode = xNodelst.Item(0);
    //        city = xNode.SelectSingleNode("address_component[5]/long_name").InnerText;

    //    }
    //    catch (Exception e)
    //    {
    //        city = null;
    //    }

    //    return city;


    //}


    private string address;

    public string Address
    {
        get { return address; }
        private set { address = value; }
    }

    //private string url = "https://maps.google.com/maps/api/geocode/xml?address='strAddress'&sensor=false&key=AIzaSyC1y-9cXXGWoIPK5M76PVbMtEir-I44_R0&language=strlang";
    /*
    public LatLong GetLocation(string address,string destCountryCode=null)
    {
        DataTable result = GetLatLongFromAddress(Address);
        location = new LatLong(double.Parse(result.Rows[0][2].ToString()),double.Parse(result.Rows[0][3].ToString()));
    }*/

    public double GetDrivingDistance(GEOLocation destination, string destCountryCode = null)
    {
        double retVal = 0;
        string url = string.Format(distanceMatrixURL, this.LatLong.ToString(), destination.LatLong.ToString());
        var request = (HttpWebRequest)WebRequest.Create(url);
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                var result = GetLatLangFromAddress(address, destCountryCode);
                foreach (DataRow row in result.Rows)
                {
                    XPathDocument responseXML = new XPathDocument(reader);
                    XPathNavigator nav = responseXML.CreateNavigator();
                    XPathNodeIterator iter = nav.Select("//Status");
                    while (iter.MoveNext())
                    {
                        if (iter.Current.Value != "OK")
                            return -1;
                    }
                    retVal = double.Parse((string)nav.Evaluate("//distance/value"));
                }
            }
        }

        return retVal;
    }

    private DataTable GetLatLangFromAddress(string address, string destCountryCode = null)
    {
        DataTable dtGMap = null;
        List<string> countryCodes = new List<string>();
        if (!string.IsNullOrEmpty(destCountryCode))
            countryCodes = destCountryCode.Split(',').Select(x => x.ToLower()).ToList();
        else
            countryCodes.Add("in");
        for (int i = 0; i < countryCodes.Count; i++)
        {
            url = "https://maps.google.com/maps/api/geocode/xml?address='strAddress'&sensor=false&key=AIzaSyC1y-9cXXGWoIPK5M76PVbMtEir-I44_R0&language=strlang";
            url = url.Replace("strAddress", System.Web.HttpUtility.UrlEncode(address)).Replace("strlang", countryCodes[i]);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ReadWriteTimeout = 10000;
            request.Timeout = 10000;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    if (dsResult.Tables.Contains("result"))
                    {
                        DataTable dtCoordinates = new DataTable();
                        dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)), new DataColumn("Address", typeof(string)), new DataColumn("Latitude", typeof(string)), new DataColumn("Longitude", typeof(string)) });
                        foreach (DataRow row in dsResult.Tables["result"].Rows)
                        {
                            string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                            DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                            dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                        }
                        dtGMap = dtCoordinates;
                        break;
                    }
                }
            }
        }
        return dtGMap;
    }
}

public class CalculateDistance
{
    public double getDistance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        double theta = lon1 - lon2;
        double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        if (unit == 'K')
        {
            dist = dist * 1.609344;
        }
        else if (unit == 'N')
        {
            dist = dist * 0.8684;
        }
        return (dist);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts decimal degrees to radians             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts radians to decimal degrees             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }

}
public class LatLong
{
    public LatLong()
    {
        this.Latitude = 0;
        this.Longitude = 0;
    }
    public LatLong(double Latitude, double Longitude)
    {
        this.Latitude = Latitude;
        this.Longitude = Longitude;
    }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public override string ToString()
    {
        return Latitude.ToString() + "," + Longitude.ToString();
    }
}
