namespace OnlineFridge.Models {
    public class Measurement {
        public int MeasurementID {get; set;}
        public string? measurementName {get; set;}

        public ICollection<Quantity>? quantities {get; set;}
    }
}