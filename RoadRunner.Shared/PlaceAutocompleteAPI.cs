using System.Collections.Generic;

namespace RoadRunner.Shared
{
    public class PlaceAutocompleteAPI_MatchedSubstring
    {
        public int length { get; set; }
        public int offset { get; set; }
    }

    public class PlaceAutocompleteAPI_Term
    {
        public int offset { get; set; }
        public string value { get; set; }
    }

    public class PlaceAutocompleteAPI_Prediction
    {
        public string description { get; set; }
        public string id { get; set; }
        public List<PlaceAutocompleteAPI_MatchedSubstring> matched_substrings { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public List<PlaceAutocompleteAPI_Term> terms { get; set; }
        public List<string> types { get; set; }
    }

    public class PlaceAutocompleteAPI_RootObject
    {
        public List<PlaceAutocompleteAPI_Prediction> predictions { get; set; }
        public string status { get; set; }
    }
}