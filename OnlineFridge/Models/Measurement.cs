namespace OnlineFridge.Models {
    public class Measurement {
        public int MeasurementID {get; set;}
        public string? measurementName {get; set;}

        public List<Quantity>? quantities {get; set;}
    }
}